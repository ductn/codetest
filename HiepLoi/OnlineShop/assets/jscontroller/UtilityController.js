﻿var URL_APPLICATION = $('#URL_APPLICATION').val();
$.fn.hasAttr = function (name) {
    return this.attr(name) !== undefined;
};
var getUrlParameter = function getUrlParameter(sParam) {
    var sPageURL = window.location.search.substring(1),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : decodeURIComponent(sParameterName[1]);
        }
    }
};
var removeUrlParameter = function removeUrlParameter(sParam) {
    var url = window.location.href.split('?')[0] + '?';
    var sPageURL = decodeURIComponent(window.location.search.substring(1)),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;
    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');
        if (sParameterName[0] != sParam) {
            url = url + sParameterName[0] + '=' + sParameterName[1] + '&'
        }
    }
    return url.substring(0, url.length - 1);
};


var UtilityConfig = {
    pageSize: $.cookie('pageSize') ? $.cookie('pageSize') : 5,
    pageIndex: 1,
}
var UtilityController = {
    init: function () {
        UtilityController.loadData();
        UtilityController.registerEvent();
    },
    registerEvent: function () {
        $('#frmSaveData').validate({
            rules: {
                txtName: "required"
            },
            messages: {
                txtName: "Vui lòng nhập tên",
            }
        });
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            UtilityController.ChangeStatus(id, btn);
        });
        $('#filter_count_perpage').off('change').on('change', function () {
            var slt = $(this).val();
            $.cookie('pageSize', slt, { path: '/' });
            location.reload();
        });
        $('#btnAddNew').off('click').on('click', function () {
            $('#modalAddUpdate').modal('show');
            UtilityController.resetForm();
        });
        $('#btnSave').off('click').on('click', function () {
            if ($('#frmSaveData').valid()) {
                UtilityController.saveData();
            }
        });
        $('.btn-edit').off('click').on('click', function (e) {
            e.preventDefault();
            $('#modalAddUpdate').modal('show');
            var id = $(this).data('id');
            UtilityController.loadDetail(id);
        });
        $('.btn-delete').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            bootbox.confirm("Bạn có chắc chắn muốn xóa bản ghi này không?", function (result) {
                if (result == true) {
                    UtilityController.deleteData(id);
                }
            });
        });
        $('#btnSearch').off('click').on('click', function () {
            UtilityController.loadData(true);
        });
        $('#txtSearchS').off('keypress').on('keypress', function (e) {
            if (e.which == 13) {
                UtilityController.loadData(true);
            }
        });
        $('#btnReset').off('click').on('click', function () {
            $('#txtSearchS').val('');
            $('#ddlStatusS').val('true');
            UtilityController.loadData(true);
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
                        UtilityController.deleteAllData(values);
                    }
                });
            } else {
                bootbox.alert("Chọn bản ghi cần xóa");
            }
        });
    },
    deleteAllData: function (ids) {
        $.ajax({
            url: URL_APPLICATION + '/Admin/Utility/DeleteMulti',
            data: {
                ids: ids
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    //bootbox.alert("Xóa thành công", function () {
                    UtilityController.loadData(true);
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
            url: URL_APPLICATION + '/Admin/Utility/Delete',
            data: {
                id: id
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    //bootbox.alert("Xóa thành công", function () {
                    UtilityController.loadData(true);
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
            url: URL_APPLICATION + '/Admin/Utility/GetDetail',
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
                    $('#txtDesription').val(data.Desription);
                    $('#txtSort').val(data.Sort);
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
        var Name = $('#txtName').val();
        var Desription = $('#txtDesription').val();
        var Sort = $('#txtSort').val();
        var status = $('#chkStatus').prop('checked');
        var Utility = {
            Id: id,
            Name: Name,
            Desription: Desription,
            Sort: Sort,
            Status: status
        };
        $.ajax({
            url: URL_APPLICATION + '/Admin/Utility/SaveData',
            data: {
                strUtility: JSON.stringify(Utility)
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    //bootbox.alert("Lưu thành công", function () {
                    $('#modalAddUpdate').modal('hide');
                    UtilityController.loadData(true);
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
        var intDisplayOrder = $('#total_count_row').html();
        try {
            if (intDisplayOrder != 0) {
                intDisplayOrder = parseInt(intDisplayOrder) + 1;
            } else {
                intDisplayOrder = 1;
            }
            
        } catch (e) {

        }
        //try {
        //    $('#txtCode').val(intDisplayOrder);
        //} catch (e) {

        //}
        $('#txtName').val('');
        $('#txtDesription').val('');
        $('#txtSort').val(intDisplayOrder);
        $('#chkStatus').prop('checked', true);
        $('#titleAddUpdate').html('THÊM MỚI');
    },
    ChangeStatus: function (id, btn) {
        $.ajax({
            url: "/Admin/Utility/ChangeStatus",
            data: { id: id },
            dataType: "json",
            type: "POST",
            success: function (response) {
                if (response.status == true) {
                    btn.html('<span title="Kích hoạt" class="btn btn-success btn-xs"><i class="fa fa-check"></i ></span>');
                }
                else {
                    btn.html('<span title="Không kích hoạt" class="btn btn-default btn-xs"><i class="fa fa-check"></i ></span>');
                }
            }
        });
    },
    loadData: function (changePageSize) {
        var Name = $('#txtSearchS').val();
        var status = $('#ddlStatusS').val();
        var Utility = {
            Name: Name,
            Status: status
        };
        $.ajax({
            url: URL_APPLICATION + '/Admin/Utility/LoadData',
            type: 'GET',
            data: {
                strUtility: JSON.stringify(Utility),
                page: UtilityConfig.pageIndex,
                pageSize: UtilityConfig.pageSize
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
                            Name: item.Name,
                            Desription: item.Desription,
                            Sort: item.Sort,
                            Status: strStatus
                        });
                    });
                    $('#tblDataIndex').html(html);
                    if (response.total == 0) {
                        $('#tblDataTable').addClass('hidden');
                        $('#tblDatapagination').addClass('hidden');
                        $('#tblNoDataTable').removeClass('hidden');
                    } else {
                        $('#tblDataTable').removeClass('hidden');
                        $('#tblDatapagination').removeClass('hidden');
                        $('#tblNoDataTable').addClass('hidden');
                    }
                    $('#total_count_row').html(response.total);
                    $('#from_record').html((((UtilityConfig.pageIndex - 1) * UtilityConfig.pageSize) + 1));
                    $('#to_record').html((((UtilityConfig.pageIndex - 1) * UtilityConfig.pageSize) + grd_row));
                    $('#filter_count_perpage').val(UtilityConfig.pageSize);

                    UtilityController.paging(response.total, function () {
                        UtilityController.loadData();
                    }, changePageSize);
                    UtilityController.registerEvent();
                }
            }
        });


    },
    paging: function (totalRow, callback, changePageSize) {
        var totalPage = Math.ceil(totalRow / UtilityConfig.pageSize);

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
            last: '<span aria-hidden="true">&raquo;</span>',
            prev: 'Trước',
            onPageClick: function (event, page) {
                UtilityConfig.pageIndex = page;
                setTimeout(callback, 200);
            }
        });
    }
}
UtilityController.init();