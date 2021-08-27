var URL_APPLICATION = $('#URL_APPLICATION').val();
var businessFieldConfig = {
    pageSize: $.cookie('pageSize') ? $.cookie('pageSize'):10,
    pageIndex: 1,
}
var businessFieldController = {
    init: function () {

        businessFieldController.loadData();
        businessFieldController.registerEvent();
    },
    registerEvent: function () {
        $('#frmSaveData').validate({
            rules: {
                txtName:"required",
            },
            messages: {
                txtName:"Vui lòng nhập tên",
            }
        });
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            businessFieldController.ChangeStatus(id,btn);
        });
        $('#filter_count_perpage').off('change').on('change', function () {
            var slt = $(this).val();
            $.cookie('pageSize', slt);
            location.reload();
        });
        $('#btnAddNew').off('click').on('click', function () {
            $('#modalAddUpdate').modal('show');
            businessFieldController.resetForm();
        });
        $('#btnSave').off('click').on('click', function () {
            if ($('#frmSaveData').valid()) {
                businessFieldController.saveData();
            }
        });
        $('.btn-edit').off('click').on('click', function (e) {
            e.preventDefault();
            $('#modalAddUpdate').modal('show');
            var id = $(this).data('id');
            businessFieldController.loadDetail(id);
        });
        $('.btn-delete').off('click').on('click', function (e) {
            alert(3333);
            e.preventDefault();
            var id = $(this).data('id');
            bootbox.confirm("Bạn có chắc chắn muốn xóa bản ghi này không?", function (result) {
                if (result == true) {
                    businessFieldController.deleteData(id);
                }
            });
        });
        $('#btnSearch').off('click').on('click', function () {
            businessFieldController.loadData(true);
        });
        $('#txtSearchS').off('keypress').on('keypress', function (e) {
            if (e.which == 13) {
                businessFieldController.loadData(true);
            }
        });
        $('#btnReset').off('click').on('click', function () {
            $('#txtSearchS').val('');
            $('#ddlStatusS').val('true');
            businessFieldController.loadData(true);
        });
        $('#CheckAll').off('change').on('change', function () {
            if (this.checked) {
                $(".checkSingle").each(function () {
                    this.checked = true;
                });
            } else {
                $(".checkSingle").each(function () {
                    this.checked = false;
                });
            }
        });
        $('#btnDeleteAll').off('click').on('click', function () {
            var values = $(".checkSingle:checked").map(function () { return $(this).data('id'); }).get();
            if (values != "" && values != null) {
                $('#CheckAll').prop('checked', false);
                bootbox.confirm("Bạn có chắc chắn muốn xóa danh sách bản ghi này không?", function (result) {
                    if (result == true) {
                        businessFieldController.deleteAllData(values);
                    }
                });
            } else {
                bootbox.alert("Chọn bản ghi cần xóa");
            }
        });
    },
    deleteAllData: function (ids) {
        $.ajax({
            url: URL_APPLICATION + '/Admin/SysFunction/DeleteMulti',
            data: {
                ids: ids
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    //bootbox.alert("Xóa thành công", function () {
                        businessFieldController.loadData(true);
                    //});
                } else {
                    bootbox.alert(response.message);
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    },
    deleteData: function (id) {
        $.ajax({
            url: URL_APPLICATION + '/Admin/BusinessField/Delete',
            data: {
                id: id
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    //bootbox.alert("Xóa thành công", function () {
                        businessFieldController.loadData(true);
                    //});
                } else {
                    bootbox.alert(response.message);
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    },
    loadDetail: function (id) {
        $.ajax({
            url: URL_APPLICATION + '/Admin/BusinessField/GetDetail',
            data: {
                id: id
            },
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    var data = response.data;
                    $('#titleAddUpdate').html('CẬP NHẬT');
                    $('#hidID').val(data.Id);
                    $('#txtName').val(data.Name);
                    $('#txtCode').val(data.Code);
                    $('#chkStatus').prop('checked', data.Status);
                } else {
                    bootbox.alert(response.message);
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    },
    saveData: function () {
        var id = parseFloat($('#hidID').val());
        var name = $('#txtName').val();
        var code = $('#txtCode').val();
        var status = $('#chkStatus').prop('checked');
        var businessfield = {
            Id: id,
            Name: name,
            Code: code,
            Status: status
        };
        $.ajax({
            url: URL_APPLICATION + '/Admin/BusinessField/SaveData',
            data: {
                strBusinessField: JSON.stringify(businessfield)
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    //bootbox.alert("Lưu thành công", function () {
                        $('#modalAddUpdate').modal('hide');
                        businessFieldController.loadData(true);
                    //});
                } else {
                    bootbox.alert(response.message);
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    },
    resetForm: function () {
        $('#hidID').val('0');
        $('#txtIcons').val('');
        $('#txtName').val('');
        $('#txtCode').val('');
        $('#chkStatus').prop('checked', true);
        $('#titleAddUpdate').html('THÊM MỚI');
    },
    ChangeStatus: function (id,btn) {
        $.ajax({
            url: "/Admin/BusinessField/ChangeStatus",
            data: { id: id },
            dataType: "json",
            type: "POST",
            success: function (response) {
                if (response.status == "1") {
                    btn.html('<span title="Kích hoạt" class="btn btn-success btn-xs"><i class="fa fa-check"></i ></span>');
                    //businessFieldController.loadData();
                }
                else {
                    btn.html('<span title="Không kích hoạt" class="btn btn-default btn-xs"><i class="fa fa-check"></i ></span>');
                    //businessFieldController.loadData();
                }
            }
        });
    },
    loadData: function (changePageSize) {
        var name = $('#txtSearchS').val();
        var status = $('#ddlStatusS').val();
        var businessField = {
                Name: name,
                Status: status
            };
        $.ajax({
            url: URL_APPLICATION + '/Admin/BusinessField/LoadData',
            type: 'GET',
            data: {
                strBusinessField: JSON.stringify(businessField),
                page: businessFieldConfig.pageIndex,
                pageSize:businessFieldConfig.pageSize
            },
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    var grd_row = '';
                    var data = response.data;
                    var html = '';
                    var template = $('#data-template-index').html();
                    $.each(data, function (i, item) {
                        grd_row = (i + 1);
                        var strStatus = '<span title="Kích hoạt" class="btn btn-success btn-xs"><i class="fa fa-check"></i ></span>';
                        if (item.Status == false) {
                            strStatus = '<span title="Không kích hoạt" class="btn btn-default btn-xs"><i class="fa fa-check"></i ></span>';
                        }
                        html += Mustache.render(template, {
                            STT: (i + 1),
                            Id: item.Id,
                            Code: item.Code,
                            Name: item.Name,
                            Desription: item.Desription,
                            Status: strStatus
                        });
                    });
                    $('#tblDataIndex').html(html);

                    $('#total_count_row').html(response.total);
                    $('#from_record').html((((businessFieldConfig.pageIndex - 1) * businessFieldConfig.pageSize) + 1));
                    $('#to_record').html((((businessFieldConfig.pageIndex - 1) * businessFieldConfig.pageSize) + grd_row));
                    $('#filter_count_perpage').val(businessFieldConfig.pageSize);

                    businessFieldController.paging(response.total, function () {
                        businessFieldController.loadData();
                    }, changePageSize);
                    businessFieldController.registerEvent();
                }
            }
        });
    },
    paging: function (totalRow, callback, changePageSize) {
        var totalPage = Math.ceil(totalRow / businessFieldConfig.pageSize);

        //Unbind pagination if it existed or click change pagesize
        if ($('#pagination-index a').length === 0 || changePageSize) {
            $('#pagination-index').empty();
            $('#pagination-index').removeData("twbs-pagination");
            $('#pagination-index').unbind("page");
        }

        $('#pagination-index').twbsPagination({
            totalPages: totalPage,
            visiblePages: 5,
            first: '<span aria-hidden="true">&laquo;</span>',
            next: 'Tiếp',
            last:'<span aria-hidden="true">&raquo;</span>',
            prev: 'Trước',
            onPageClick: function (event, page) {
                businessFieldConfig.pageIndex = page;
                setTimeout(callback, 200);
            }
        });
    }
}
businessFieldController.init();