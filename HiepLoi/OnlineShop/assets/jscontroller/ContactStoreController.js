﻿var URL_APPLICATION = $('#URL_APPLICATION').val();
window.closeModal = function () {
    $('#modalAction').modal('hide');
    location.reload();
};
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
        success: function (ContactStore) {
            if (ContactStore != 'error') {
                var my_path = ContactStore;
                $("#myUploadedImg").attr("src", URL_APPLICATION + my_path);
                $("#Avatar").val(my_path);
            }
        },
        processData: false,
        ContactStoreType: false,
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
        var ViewViecCuaToi = $("#popup-view-ContactStore", parent.document.body);
        ViewViecCuaToi.find('iframe').height(mainWrapperHeight);
    } catch (e) {
        // TODO: handle exception
    }
};
var ContactStoreConfig = {
    pageSize: $.cookie('pageSize') ? $.cookie('pageSize') : 5,
    pageIndex: 1,
}
var ContactStore = {
    init: function () {
        ContactStore.loadData();
        ContactStore.registerEvents();
    },
    registerEvents: function () {
        $('#formContactStore').validate({
            rules: {
                Name: { required: true, specialChars: true },
                Email: { required: true, specialChars: false },
                Title: { required: true, specialChars: true },
                Content: { required: true, specialChars: true },
                AnswerContent: { required: true, specialChars: true },
            },
            messages: {
                Name: { required: "Vui lòng nhập tên người gửi!", specialChars: "Định dạng không hợp lệ" },
                Email: { required: "Vui lòng nhập địa chỉ email gửi!", specialChars: "Định dạng không hợp lệ" },
                Title: { required: "Vui lòng nhập tiêu đề!", specialChars: "Định dạng không hợp lệ" },
                Content: { required: "Vui lòng nhập nội dung liên hệ!", specialChars: "Định dạng không hợp lệ" },
                AnswerContent: { required: "Vui lòng nhập nội dung trả lời liên hệ!", specialChars: "Định dạng không hợp lệ" },
            }
        }); 
        $('.btnSave').off('click').on('click', function () {
            if ($('#formContactStore').valid()) {
                //getAllImages();
               $("#formContactStore").submit();
            }
        });
        $('.btnChuyen').off('click').on('click', function () {
            $("#formContactStore").submit();
            //parent.document.location.reload();
        });
        $('#filter_count_perpage').off('change').on('change',function () {
            var slt = $(this).val();
            $.cookie('pageSize', slt, { path: '/' });
            var href = ContactStore.removeParams('page').replace('=undefined', '');
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
       /* $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/Admin/ContactStore/ChangeContactStore",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    console.log(response);
                    if (response.ContactStore == true) {
                        btn.html('<span title="Kích hoạt" class="btn btn-success btn-xs"><i class="fa fa-check"></i ></span>');
                    }
                    else {
                        btn.html('<span title="Khóa" class="btn btn-default btn-xs"><i class="fa fa-check"></i ></span>');
                    }
                }
            });
        });*/
        $('.btn-chuyen').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            var controller = $(this).data('controller');
            var iframe = "";
            iframe += "<iframe src='" + $('#URL_APPLICATION').val() + "/Admin/" + controller + "/Action/" + id + "' width='100%' height='450px' style='border:none;'></iframe>";
            $('#content-lienhe').html(iframe); 
            $('#modalAction').modal();
        });
        $('.btn-view').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            var controller = $(this).data('controller');
            var pupop = "";
            pupop += "<div class=\"modal fade style-16\" id=\"popup-view-ContactStore\">";
            pupop += "<div class=\"modal-dialog modal-xl modal-dialog-centered\">";
            pupop += "<div class=\"modal-ContactStore\" >";
            pupop += "<div class=\"modal-header\">";
            pupop += "<h4 class=\"modal-title\" style=\"font-weight:bold;\">THÔNG TIN CHỨC VỤ</h4>";
            pupop += "<button type=\"button\" class=\"close\" data-dismiss=\"modal\">&times;</button>";
            pupop += "</div>";
            pupop += "<div class=\"modal-body\" id=\"popup-ContactStore-view-ContactStore\" style='padding: 2px;'>";
            pupop += "<iframe src='" + controller + "' width='100%' height='500px' style='border:none;'></iframe>";
            pupop += "</div>";
            pupop += "</div>";
            pupop += "</div >";
            pupop += "</div >";
            $('#popup-view-ContactStore').remove();
            $('.page-footer').append(pupop);
            $('#popup-view-ContactStore').modal();
        });
        $('.btn-delete').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            bootbox.confirm("Bạn có chắc chắn muốn xóa bản ghi này không?", function (result) {
                if (result == true) {
                    ContactStore.deleteData(id);
                }
            });
        });
        $('#btnDeleteAll').off('click').on('click', function () {
            var values = $(".checkSingle:checked").map(function () { return $(this).data('id'); }).get();
            if (values != "" && values != null) {
                $('#CheckAll').prop('checked', false);
                bootbox.confirm("Bạn có chắc chắn muốn xóa danh sách bản ghi này không?", function (result) {
                    if (result == true) {
                        ContactStore.deleteAllData(values);
                    }
                });
            } else {
                bootbox.alert("Chọn bản ghi cần xóa");
            }
        });
    },
    deleteAllData: function (ids) {
        $.ajax({
            url: URL_APPLICATION + '/Admin/ContactStore/DeleteMulti',
            data: {
                ids: ids
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.ContactStore == true) {
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
            url: URL_APPLICATION + '/Admin/ContactStore/DeleteMulti',
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
        $('#filter_count_perpage').val(ContactStoreConfig.pageSize);
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
ContactStore.init();