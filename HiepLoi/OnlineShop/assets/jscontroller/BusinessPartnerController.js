var URL_APPLICATION = $('#URL_APPLICATION').val();
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
jQuery.validator.addMethod("checkNumber09", function (value, element) {
    var regex = /^[0-9-+]+$/;
    var id = element.id;
    if (value != null && value != '') {
        if (regex.test(value)) {
            return true;  // FAIL validation when REGEX matches
        } else {
            $("#" + id).focus();
            return false;   // PASS validation otherwise
        };
    }
    return true;
}, "please use checkNumber09"); 
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
    var _url = URL_APPLICATION + '/Handler/SliderUploader.ashx';
    $.ajax({
        type: 'post',
        url: _url,
        data: formData,
        success: function (status) {
            if (status != 'error') {
                var my_path = status;
                $("#myUploadedImg").attr("src", URL_APPLICATION + my_path);
                $("#Image").val(my_path);
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
        var ViewViecCuaToi = $("#popup-view-BusinessPartner", parent.document.body);
        ViewViecCuaToi.find('iframe').height(mainWrapperHeight);
    } catch (e) {
        // TODO: handle exception
    }
};
function cancel() {
    var url = URL_APPLICATION + "/Admin/BusinessPartner/Index";
    window.location.href = url;
}
var BusinessPartnerConfig = {
    pageSize: $.cookie('pageSize') ? $.cookie('pageSize') : 5,
    pageIndex: 1,
}
var BusinessPartner = {
    init: function () {
        BusinessPartner.loadData();
        BusinessPartner.registerEvents();
    },
    registerEvents: function () {
        $('#formBusinessPartner').validate({
            rules: {
                TieuDe: { required: true, specialChars: true },
                Email: { email: true },
                SDT: { checkNumber09: true },
            },
            messages: {
                TieuDe: { required: "Vui lòng nhập tên ngân hàng", specialChars: "Định dạng không hợp lệ" },
                Email: { email: "Định dạng không hợp lệ" },
                SDT: { checkNumber09: "Định dạng không hợp lệ" },
            }
        });
        $('body').on('click', '#btnSelectLogo', function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (url) {
                $('#Image').attr("value", url);
                $("#myUploadedLogo").attr("src", url);
            };
            finder.popup();
        });
        $('.btnSave').off('click').on('click', function () {
            if ($('#formBusinessPartner').valid()) {
                $("#formBusinessPartner").submit();
            }
        });
        $('#filter_count_perpage').off('change').on('change', function () {
            var slt = $(this).val();
            $.cookie('pageSize', slt, { path: '/' });
            var href = BusinessPartner.removeParams('page').replace('=undefined', '');
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
        $('.btn-view').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            var controller = $(this).data('controller');
            var pupop = "";
            pupop += "<div class=\"modal fade style-16\" id=\"popup-view-BusinessPartner\">";
            pupop += "<div class=\"modal-dialog modal-xl modal-dialog-centered\">";
            pupop += "<div class=\"modal-content\" >";
            pupop += "<div class=\"modal-header\">";
            pupop += "<h4 class=\"modal-title\" style=\"font-weight:bold;\">THÔNG TIN</h4>";
            pupop += "<button type=\"button\" class=\"close\" data-dismiss=\"modal\">&times;</button>";
            pupop += "</div>";
            pupop += "<div class=\"modal-body\" id=\"popup-content-view-BusinessPartner\" style='padding: 2px;'>";
            pupop += "<iframe src='" +URL_APPLICATION + "/" + controller + "' width='100%' height='500px' style='border:none;'></iframe>";
            pupop += "</div>";
            pupop += "</div>";
            pupop += "</div >";
            pupop += "</div >";
            $('#popup-view-BusinessPartner').remove();
            $('.page-footer').append(pupop);
            $('#popup-view-BusinessPartner').modal();
        });
        $('.btn-delete').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            bootbox.confirm("Bạn có chắc chắn muốn xóa bản ghi này không?", function (result) {
                if (result == true) {
                    BusinessPartner.deleteData(id);
                }
            });
        });
        $('#btnDeleteAll').off('click').on('click', function () {
            var values = $(".checkSingle:checked").map(function () { return $(this).data('id'); }).get();
            if (values != "" && values != null) {
                $('#CheckAll').prop('checked', false);
                bootbox.confirm("Bạn có chắc chắn muốn xóa danh sách bản ghi này không?", function (result) {
                    if (result==true) {
                        BusinessPartner.deleteAllData(values);
                    }
                });
            } else {
                bootbox.alert("Chọn bản ghi cần xóa");
            }
        });
    },
    deleteAllData: function (ids) {
        $.ajax({
            url: URL_APPLICATION + '/Admin/BusinessPartner/DeleteMulti',
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
            url: URL_APPLICATION + '/Admin/BusinessPartner/DeleteMulti',
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
        $('#filter_count_perpage').val(BusinessPartnerConfig.pageSize);
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
BusinessPartner.init();