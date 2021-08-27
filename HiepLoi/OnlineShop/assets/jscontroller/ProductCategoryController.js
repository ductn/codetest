var URL_APPLICATION = $('#URL_APPLICATION').val();
var _URL = window.URL || window.webkitURL;
function sendFileTopNav(file) {
    var formData = new FormData();
    formData.append('file', file);
    $.ajax({
        type: 'post',
        url: URL_APPLICATION + '/Handler/SliderUploader.ashx?istype=ProductCategory',
        data: formData,
        success: function (status) {
            if (status != 'error') {
                var my_path = status;
                $("#UploadedProfileIcons").attr("src", URL_APPLICATION + my_path);
                $("#ViewProfile_txtIcons").val(my_path);
            }
        },
        processData: false,
        contentType: false,
        error: function () {
            alert("Lỗi!");
        }
    });
}
var _URL = window.URL || window.webkitURL;
function ChangeFile(obj) {
    var file, img;
    if ((file = $('#' + obj)[0].files[0])) {
        img = new Image();
        img.onload = function () {
            sendFileTopNav(file);
        };
        img.onerror = function () {
            alert("Not a valid file:" + file.type);
        };
        img.src = _URL.createObjectURL(file);
    }
}
function clickBrowse(obj) {
    $('#' + obj).click();
    return false;
}
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


var ProductCategoryConfig = {
    pageSize: $.cookie('pageSize') ? $.cookie('pageSize') : 5,
    pageIndex: 1,
}
var ProductCategoryController = {
    init: function () {
        ProductCategoryController.loadData();
        ProductCategoryController.registerEvent();
    },
    registerEvent: function () {
        $('#frmSaveData').validate({
            rules: {
                txtName: "required",
            },
            messages: {
                txtName: "Vui lòng nhập tên",
            }
        });
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            ProductCategoryController.ChangeStatus(id, btn);
        });
        $('.btn-ShowOnHome').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            ProductCategoryController.ChangeShowOnHome(id, btn);
        });
        $('#filter_count_perpage').off('change').on('change', function () {
            var slt = $(this).val();
            $.cookie('pageSize', slt, { path: '/' });
            location.reload();
        });
        $('#btnAddNew').off('click').on('click', function () {
            $('#modalAddUpdate').modal('show');
            ProductCategoryController.resetForm();
        });
        $('#btnSave').off('click').on('click', function () {
            if ($('#frmSaveData').valid()) {
                ProductCategoryController.saveData();
            }
        });
        $('.btn-CategoryUnit').off('click').on('click', function (e) {
            e.preventDefault();
            $('#CategoryUnitModal').modal('show');
            var id = $(this).data('id');
            ProductCategoryController.loadDetailCategoryUnit(id);
        });
        $('.btn-edit').off('click').on('click', function (e) {
            e.preventDefault();
            $('#modalAddUpdate').modal('show');
            var id = $(this).data('id');
            ProductCategoryController.loadDetail(id);
        });
        $('.btn-delete').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            bootbox.confirm("Bạn có chắc chắn muốn xóa bản ghi này không?", function (result) {
                if (result == true) {
                    ProductCategoryController.deleteData(id);
                }
            });
        });
        $('#btnSearch').off('click').on('click', function () {
            ProductCategoryController.loadData(true);
        });
        $('#txtSearchS').off('keypress').on('keypress', function (e) {
            if (e.which == 13) {
                ProductCategoryController.loadData(true);
            }
        });
        $('#btnReset').off('click').on('click', function () {
            $('#txtSearchS').val('');
            $('#ddlStatusS').val('true');
            ProductCategoryController.loadData(true);
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
                        ProductCategoryController.deleteAllData(values);
                    }
                });
            } else {
                bootbox.alert("Chọn bản ghi cần xóa");
            }
        });
        $('body').on('click','.btn-delete-productcategoryunit', function () {
            var ProductCategoryId = $('#hidIDProductCategory').val();
            var ProductCategoryUnitId = $(this).data('id');
            ProductCategoryController.deleteProductCategoryUnit(ProductCategoryId, ProductCategoryUnitId);
        });
        $('#ddlCategoryUnit').off('change').on('change', function () {
            ProductCategoryController.saveDataCategoryUnit();
        });
    },
    loadDetailCategoryUnit: function (id) {
        ProductCategoryController.loadSelectCategoryUnit(id);
        $.ajax({
            url: URL_APPLICATION + '/Admin/ProductCategory/GetDetailCategoryUnit',
            data: {
                id: id
            },
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    $('#titleCategoryUnits').html('CẬP NHẬT CATEGORY - UNIT - PRODUCTCATEGORY');
                    var strDanhSachCategoryUnit = "";
                    var data = response.data;
                    var TitleProductCategory = response.TitleProductCategory;
                    $.each(data, function (i, item) {
                        strDanhSachCategoryUnit += '<div class="alert alert-warning input-group" style="padding: 5px;font-size: 10px;margin-bottom: 5px;"><div style="position:relative;flex:1 1 auto;width:1%;min-width:0;margin-bottom:0">' + item.CategoryName + " - " + item.UnitTitle + '</div><div class="input-group-append"><a title="Xóa" class="btn btn-danger btn-xs btn-delete-productcategoryunit" data-id="' + item.Id +'"><i class="fa fa-trash-o"></i></a></div></div>';
                    });
                    $('#hidIDProductCategory').val(id);
                    $('#TitleProductCategory').text(TitleProductCategory);
                    $('#danh-sach-CategoryUnit').html(strDanhSachCategoryUnit);
                } else {
                    bootbox.alert(response.message);
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    },
    saveDataCategoryUnit: function () {
        var ProductCategoryId = parseFloat($('#hidIDProductCategory').val());
        var _CategoryUnit = $('#ddlCategoryUnit').val();
        var CategoryId = "";
        var UnitId = "";
        if (_CategoryUnit != null) {
            CategoryId = _CategoryUnit.split('-')[0];
            UnitId = _CategoryUnit.split('-')[1];
        }
        var json = {
            ProductCategoryId: ProductCategoryId,
            CategoryId: CategoryId,
            UnitId: UnitId
        };
        $.ajax({
            url: URL_APPLICATION + '/Admin/ProductCategory/SaveProductCategoryUnit',
            data: {
                json: JSON.stringify(json)
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    ProductCategoryController.loadDetailCategoryUnit(ProductCategoryId);
                } else {
                    bootbox.alert(response.message);
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    },
    deleteProductCategoryUnit: function (ProductCategoryId, ProductCategoryUnitId) {
        $.ajax({
            url: URL_APPLICATION + '/Admin/ProductCategory/deleteProductCategoryUnit',
            data: {
                ProductCategoryUnitId: ProductCategoryUnitId
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    //bootbox.alert("Xóa thành công", function () {
                    ProductCategoryController.loadDetailCategoryUnit(ProductCategoryId);
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
    deleteAllData: function (ids) {
        $.ajax({
            url: URL_APPLICATION + '/Admin/ProductCategory/DeleteMulti',
            data: {
                ids: ids
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    //bootbox.alert("Xóa thành công", function () {
                    ProductCategoryController.loadData(true);
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
            url: URL_APPLICATION + '/Admin/ProductCategory/Delete',
            data: {
                id: id
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    //bootbox.alert("Xóa thành công", function () {
                    ProductCategoryController.loadData(true);
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
        ProductCategoryController.loadSelectParentId(0);
        $.ajax({
            url: URL_APPLICATION + '/Admin/ProductCategory/GetDetail',
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
                    $("#ViewProfile_txtIcons").val(data.Icons);
                    var img = URL_APPLICATION + data.Icons;
                    if (data.Icons == null || data.Icons == "") {
                        img = URL_APPLICATION + "\\MediaUploader\\no_image.jpg";
                    }
                    $("#UploadedProfileIcons").attr("src",img);
                    $('#chkShowOnHome').prop('checked', data.ShowOnHome);
                    $('#txtName').val(data.Name);
                    $('#ddlParentId').val(data.ParentID).trigger('change');
                    $('#txtDisplayOrder').val(data.DisplayOrder);
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
        var Id = parseFloat($('#hidID').val());
        var Icons = $('#ViewProfile_txtIcons').val();
        var Name = $('#txtName').val();
        var parentId = $('#ddlParentId').val();
        var DisplayOrder = $('#txtDisplayOrder').val();
        var ShowOnHome = $('#chkShowOnHome').prop('checked');
        var status = $('#chkStatus').prop('checked');
        var json = {
            Id: Id,
            Icons: Icons,
            Name: Name,
            ParentId: parentId,
            DisplayOrder: DisplayOrder,
            ShowOnHome: ShowOnHome,
            Status: status
        };
        $.ajax({
            url: URL_APPLICATION + '/Admin/ProductCategory/SaveData',
            data: {
                json: JSON.stringify(json)
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    //bootbox.alert("Lưu thành công", function () {
                    $('#modalAddUpdate').modal('hide');
                    ProductCategoryController.loadData(true);
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
        var parentid = getUrlParameter('parentid');
        if (parentid == null || parentid == '' || parentid == 'undefined') {
            parentid = 0;
        }
        ProductCategoryController.loadSelectParentId(parentid);
        $('#hidID').val('0');
        $('#txtIcons').val('');
        var img = URL_APPLICATION + "\\MediaUploader\\no_image.jpg";
        $("#UploadedProfileIcons").attr("src", img);
        $("#ViewProfile_txtIcons").val("");
        $('#txtName').val('');
        var intDisplayOrder = $('#total_count_row').html();
        try {
            intDisplayOrder = parseInt(intDisplayOrder) + 1;
        } catch (e) {

        }
        $('#txtDisplayOrder').val(intDisplayOrder);
        $('#chkShowOnHome').prop('checked', false);
        $('#chkStatus').prop('checked', true);
        $('#titleAddUpdate').html('THÊM MỚI');
    },
    ChangeShowOnHome: function (id, btn) {
        $.ajax({
            url: "/Admin/ProductCategory/ChangeShowOnHome",
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
    ChangeStatus: function (id, btn) {
        $.ajax({
            url: "/Admin/ProductCategory/ChangeStatus",
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
    loadSelectParentId: function (ParentId) {
        $.ajax({
            url: URL_APPLICATION + '/Admin/ProductCategory/loadSelectParentId',
            data: {
                ParentId: ParentId
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    var html = '<option value="0">--- Chọn ---</option>';
                    $.each(response.data, function (i, item) {
                        html += '<option value="' + item.Id + '">' + item.Name + '</option>';
                    });
                    $('#ddlParentId').html(html);
                    $('#ddlParentId').val(ParentId).trigger('change');
                } else {
                    bootbox.alert(response.message);
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    },
    loadSelectCategoryUnit: function (ProductCategoryId) {
        $.ajax({
            url: URL_APPLICATION + '/Admin/ProductCategory/loadSelectCategoryUnit',
            data: {
                Id: ProductCategoryId
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    var html = '<option value="0">--- Chọn ---</option>';
                    $.each(response.data, function (i, item) {
                        html += '<option value="' + item.CategoryID + "-" + item.UnitId + '">' + item.CategoryName + ' - ' + item.UnitTitle + '</option>';
                    });
                    $('#ddlCategoryUnit').html(html);
                    //$('#ddlCategoryUnit').val(ParentId).trigger('change');
                } else {
                    bootbox.alert(response.message);
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    },
    loadData: function (changePageSize) {
        var Name = $('#txtSearchS').val();
        var status = $('#ddlStatusS').val();
        var parentid = getUrlParameter('parentid');
        if (parentid == null || parentid == '' || parentid == 'undefined') {
            parentid = 0;
        }
        var json = {
            Name: Name,
            ParentId: parentid,
            Status: status
        };
        var url = URL_APPLICATION + '/Admin/ProductCategory/LoadData';
        $.ajax({
            url: url,
            type: 'GET',
            data: {
                json: JSON.stringify(json),
                page: ProductCategoryConfig.pageIndex,
                pageSize: ProductCategoryConfig.pageSize
            },
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    var grd_row = '';
                    var data = response.data;
                    var backParentId = response.parentid;
                    var html = '';
                    var template = $('#data-template-index').html();
                    $.each(data, function (i, item) {
                        grd_row = (i + 1);
                        var strStatus = '<span title="Kích hoạt" class="btn btn-success btn-xs"><i class="fa fa-check"></i ></span>';
                        if (item.Status == false) {
                            strStatus = '<span title="Không kích hoạt" class="btn btn-default btn-xs"><i class="fa fa-check"></i ></span>';
                        }
                        var strIcons = "<img src='" + URL_APPLICATION + item.Icons + "' alt='' style='max-width: 50px; max-height: 50px'>";
                        var strShowOnHome = '<span title="Kích hoạt" class="btn btn-success btn-xs"><i class="fa fa-check"></i ></span>';
                        if (item.ShowOnHome == false) {
                            strShowOnHome = '<span title="Không kích hoạt" class="btn btn-default btn-xs"><i class="fa fa-check"></i ></span>';
                        }
                        var Name = "<a href='" + URL_APPLICATION + "/Admin/ProductCategory/Index/?parentid=" + item.Id + "'>" + item.Name + "</a>";
                        html += Mustache.render(template, {
                            STT: (i + 1),
                            Id: item.Id,
                            Icons: strIcons,
                            ShowOnHome: strShowOnHome,
                            Name: Name,
                            DisplayOrder: item.DisplayOrder,
                            Status: strStatus
                        });
                    });
                    $('#tblDataIndex').html(html);

                    $('#total_count_row').html(response.total);
                    $('#from_record').html((((ProductCategoryConfig.pageIndex - 1) * ProductCategoryConfig.pageSize) + 1));
                    $('#to_record').html((((ProductCategoryConfig.pageIndex - 1) * ProductCategoryConfig.pageSize) + grd_row));
                    $('#filter_count_perpage').val(ProductCategoryConfig.pageSize);

                    try {
                        if (parentid != 0) {
                            var linkParentId = "<a href='" + URL_APPLICATION +"/Admin/ProductCategory/Index/?parentid=" + backParentId + "'><i class=\"fa fa-arrow-circle-o-up\"></i> Lên một cấp </a>";
                            $('#link-parent-id').html(linkParentId);
                        }
                    } catch (e) {

                    }
                    ProductCategoryController.paging(response.total, function () {
                        ProductCategoryController.loadData();
                    }, changePageSize);
                    ProductCategoryController.registerEvent();
                }
            }
        });


    },
    paging: function (totalRow, callback, changePageSize) {
        var totalPage = Math.ceil(totalRow / ProductCategoryConfig.pageSize);

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
                ProductCategoryConfig.pageIndex = page;
                setTimeout(callback, 200);
            }
        });
    }
}
ProductCategoryController.init();