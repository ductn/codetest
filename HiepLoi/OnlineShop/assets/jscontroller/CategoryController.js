var URL_APPLICATION = $('#URL_APPLICATION').val();
var _URL = window.URL || window.webkitURL;
function ChangeFile(obj) {
    var file, img;
    if ((file = $('#' + obj)[0].files[0])) {
        img = new Image();
        img.onload = function () {
            var formData = new FormData();
            formData.append('file', file);
            $.ajax({
                type: 'post',
                url: URL_APPLICATION + '/Handler/SliderUploader.ashx?istype=Category',
                data: formData,
                success: function (status) {
                    if (status != 'error') {
                        var my_path = status;
                        $("#Img" + obj).attr("src", URL_APPLICATION + my_path);
                        var hid = obj.split('_');
                        $("#" + hid[1]).val(my_path);
                    }
                },
                processData: false,
                contentType: false,
                error: function () {
                    alert("Lỗi!");
                }
            });
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
jQuery.validator.addMethod("specialChars", function (value, element) {
    var regex = /[!@#$%^&*\=\[\]{}]/;
    var id = element.id;
    if (regex.test(value)) {
        $("#" + id).focus();
        return false;  // FAIL validation when REGEX matches
    } else {
        return true;   // PASS validation otherwise
    };
}, "please use only alphanumeric or alphabetic characters");
jQuery.validator.addMethod("valNot0", function (value, element) {
    var id = element.id;
    if (value == 0 || value == "0") {
        $("#" + id).focus();
        return false;  // FAIL validation when REGEX matches
    } else {
        return true;   // PASS validation otherwise
    };
}, "please use valNot0");
jQuery.validator.addMethod("focusRequire", function (value, element) {
    var id = element.id;
    if (value == null || value == "") {
        $("#" + id).focus();
        return false;
    } else {
        return true;   // PASS validation otherwise
    };
}, "please use focusRequire");
function sendFile(file) {
    var formData = new FormData();
    formData.append('file', $('#fileUpload')[0].files[0]);
    $.ajax({
        type: 'post',
        url: URL_APPLICATION + '/Handler/fileUploader.ashx',
        data: formData,
        success: function (status) {
            if (status != 'error') {
                var my_path = status;
                $("#myUploadedImg").attr("src", URL_APPLICATION + my_path);
                $("#Avatar").val(my_path);
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
$("#fileUpload").on('change', function () {
    var file, img;
    if ((file = this.files[0])) {
        img = new Image();
        img.onload = function () {
            sendFile(file);
        };
        img.onerror = function () {
            alert("Not a valid file:" + file.type);
        };
        img.src = _URL.createObjectURL(file);
    }
});
var autoSizeIframe = function autoSizeIframe() {
    try {
        var mainWrapperHeight = $('body').height();
        var ViewViecCuaToi = $("#popup-view-Category", parent.document.body);
        ViewViecCuaToi.find('iframe').height(mainWrapperHeight);
    } catch (e) {
        // TODO: handle exception
    }
};
var CategoryConfig = {
    pageSize: $.cookie('pageSize') ? $.cookie('pageSize') : 5,
    pageIndex: 1,
}
var CategoryController = {
    init: function () {
        CategoryController.loadData();
        CategoryController.registerEvents();
    },
    registerEvents: function () {
        $('#formCategory').validate({
            rules: {
                Code: { required: true, specialChars: true },
                Name: { required: true, specialChars: true }
            },
            messages: {
                Code: { required: "Vui lòng nhập mã", specialChars: "Định dạng không hợp lệ" },
                Name: { required: "Vui lòng nhập tên", specialChars: "Định dạng không hợp lệ" }
            }
        }); 
        $('.btnSave').off('click').on('click', function () {
            if ($('#formCategory').valid()) {
                $("#formCategory").submit();
            }
        });
        $('#filter_count_perpage').off('change').on('change',function () {
            var slt = $(this).val();
            $.cookie('pageSize', slt, { path: '/' });
            var href = CategoryController.removeParams('page').replace('=undefined', '');
            window.location.href = href;
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
        $('.btn-CategoryUnit').off('click').on('click', function (e) {
            e.preventDefault();
            $('#CategoryUnitModal').modal('show');
            var id = $(this).data('id');
            CategoryController.loadDetailCategoryUnit(id);
        });
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: URL_APPLICATION + "/Admin/Category/ChangeStatus",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    if (response.status == true) {
                        btn.html('<span title="Kích hoạt" class="btn btn-success btn-xs"><i class="fa fa-check"></i ></span>');
                    }
                    else {
                        btn.html('<span title="Khóa" class="btn btn-default btn-xs"><i class="fa fa-check"></i ></span>');
                    }
                }
            });
        });
        $('.btn-GoiYMuaSam').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: URL_APPLICATION + "/Admin/Category/ChangeGoiYMuaSam",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    if (response.status == true) {
                        btn.html('<span title="Kích hoạt" class="btn btn-success btn-xs"><i class="fa fa-check"></i ></span>');
                    }
                    else {
                        btn.html('<span title="Khóa" class="btn btn-default btn-xs"><i class="fa fa-check"></i ></span>');
                    }
                }
            });
        });
        $('.btn-view').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            var controller = $(this).data('controller');
            var pupop = "";
            pupop += "<div class=\"modal fade style-16\" id=\"popup-view-Category\">";
            pupop += "<div class=\"modal-dialog modal-xl modal-dialog-centered\">";
            pupop += "<div class=\"modal-content\" >";
            pupop += "<div class=\"modal-header\">";
            pupop += "<h4 class=\"modal-title\" style=\"font-weight:bold;\">THÔNG TIN NGÀNH HÀNG</h4>";
            pupop += "<button type=\"button\" class=\"close\" data-dismiss=\"modal\">&times;</button>";
            pupop += "</div>";
            pupop += "<div class=\"modal-body\" id=\"popup-content-view-Category\" style='padding: 2px;'>";
            pupop += "<iframe src='" + controller + "' width='100%' height='Auto' style='border:none;'></iframe>";
            pupop += "</div>";
            pupop += "</div>";
            pupop += "</div >";
            pupop += "</div >";
            $('#popup-view-Category').remove();
            $('.page-footer').append(pupop);
            $('#popup-view-Category').modal();
        });
        $('.btn-delete').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            bootbox.confirm("Bạn có chắc chắn muốn xóa bản ghi này không?", function (result) {
                if (result == true) {
                    CategoryController.deleteData(id);
                }
            });
        });
        $('#btnDeleteAll').off('click').on('click', function () {
            var values = $(".checkSingle:checked").map(function () { return $(this).data('id'); }).get();
            if (values != "" && values != null) {
                $('#CheckAll').prop('checked', false);
                bootbox.confirm("Bạn có chắc chắn muốn xóa danh sách bản ghi này không?", function (result) {
                    if (result == true) {
                        CategoryController.deleteAllData(values);
                    }
                });
            } else {
                bootbox.alert("Chọn bản ghi cần xóa");
            }
        });
        $('body').on('click', '.btn-delete-categoryunit', function () {
            var CategoryId = $('#hidIDCategory').val();
            var CategoryUnitId = $(this).data('id');
            CategoryController.deleteCategoryUnit(CategoryId,CategoryUnitId);
        });
        $('#ddlUnit').off('change').on('change', function () {
            CategoryController.saveDataCategoryUnit();
        });
    },
    loadDetailCategoryUnit: function (id) {
        CategoryController.loadSelectCategoryUnit(id);
        $.ajax({
            url: URL_APPLICATION + '/Admin/Category/GetDetailCategoryUnit',
            data: {
                id: id
            },
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    $('#titleCategoryUnits').html('CẬP NHẬT CATEGORY - UNIT');
                    var strDanhSachCategoryUnit = "";
                    var data = response.data;
                    var TitleCategory = response.TitleCategory;
                    $.each(data, function (i, item) {
                        strDanhSachCategoryUnit += '<div class="alert alert-warning input-group" style="padding: 5px;font-size: 10px;margin-bottom: 5px;"><div style="position:relative;flex:1 1 auto;width:1%;min-width:0;margin-bottom:0">' + item.UnitTitle + '</div><div class="input-group-append"><a title="Xóa" class="btn btn-danger btn-xs btn-delete-categoryunit" data-id="' + item.Id + '"><i class="fa fa-trash-o"></i></a></div></div>';
                    });
                    $('#hidIDCategory').val(id);
                    $('#TitleCategory').text(TitleCategory);
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
    loadSelectCategoryUnit: function (ProductCategoryId) {
        $.ajax({
            url: URL_APPLICATION + '/Admin/Category/loadSelectUnit',
            data: {
                Id: ProductCategoryId
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    var html = '<option value="0">--- Chọn ---</option>';
                    $.each(response.data, function (i, item) {
                        html += '<option value="' + item.Id + '">' + item.Title + '</option>';
                    });
                    $('#ddlUnit').html(html);
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
        var CategoryId = parseFloat($('#hidIDCategory').val());
        var UnitId = $('#ddlUnit').val();
        var json = {
            CategoryId: CategoryId,
            UnitId: UnitId
        };
        $.ajax({
            url: URL_APPLICATION + '/Admin/Category/SaveCategoryUnit',
            data: {
                json: JSON.stringify(json)
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    CategoryController.loadDetailCategoryUnit(CategoryId);
                } else {
                    bootbox.alert(response.message);
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    },
    deleteCategoryUnit: function (CategoryId, CategoryUnitId) {
        $.ajax({
            url: URL_APPLICATION + '/Admin/Category/deleteCategoryUnit',
            data: {
                CategoryUnitId: CategoryUnitId
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    //bootbox.alert("Xóa thành công", function () {
                    CategoryController.loadDetailCategoryUnit(CategoryId);
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
            url: URL_APPLICATION + '/Admin/Category/DeleteMulti',
            data: {
                ids: ids
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    //bootbox.alert("Xóa thành công", function () {
                    window.location.href = window.location.href;
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
        var ids = [];
        ids[0] = id;
        $.ajax({
            url: URL_APPLICATION + '/Admin/Category/DeleteMulti',
            data: {
                ids: ids
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    window.location.href = window.location.href;
                } else {
                    bootbox.alert(response.message);
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    },
    loadData: function () {
        $('#filter_count_perpage').val(CategoryConfig.pageSize);
    },
    removeParams: function (sParam) {
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
    }
}
CategoryController.init();