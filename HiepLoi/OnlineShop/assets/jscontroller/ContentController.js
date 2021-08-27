var URL_APPLICATION = $('#URL_APPLICATION').val();
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
$(function () {
    $("#formTinTuc").validate({
        rules: {
            bEffectiveDate: {
                required: true,
                specialChars: false
            },
        },
        messages: {
            bEffectiveDate: {
                required: "Vui lòng nhập ngày bắt đầu",
                specialChars: "Vui lòng nhập đúng định dạng"
            },
        }
    })
});
function sendFile(file) {
    var formData = new FormData();
    formData.append('file', $('#fileUpload')[0].files[0]);
    $.ajax({
        type: 'post',
        url: URL_APPLICATION + '/Handler/fileUploader.ashx',
        data: formData,
        success: function (Content) {
            if (Content != 'error') {
                var my_path = Content;
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
        var ViewViecCuaToi = $("#popup-view-Content", parent.document.body);
        ViewViecCuaToi.find('iframe').height(mainWrapperHeight);
    } catch (e) {
        // TODO: handle exception
    }
};
var ContentConfig = {
    pageSize: $.cookie('pageSize') ? $.cookie('pageSize') : 5,
    pageIndex: 1,
}
var Content = {
    init: function () {
        Content.loadData();
        Content.registerEvents();
    },
    registerEvents: function () {
        $('#formTinTuc').validate({
            rules: {
                Name: { required: true, specialChars: true },
                Description: { required: true, specialChars: true },
                Author: { required: true, specialChars: true },
            },
            messages: {
                Name: { required: "Vui lòng nhập tiều đề", specialChars: "Không được chứa ký tự đặc biệt!" },
                Description: { required: "Vui lòng nhập mô tả", specialChars: "Không được chứa ký tự đặc biệt!" },
                Author: { required: "Vui lòng nhập Nguồn/tác giả", specialChars: "Không được chứa ký tự đặc biệt!" },
            }
        }); 
        $('.btnSave').off('click').on('click', function () {
            if ($('#formTinTuc').valid()) {
                //var checkForm = 1;

                //var tieuDe = $("#Name").val();
                //var moTa = $("#Description").val();
                //var tacGia = $("#Author").val();

                //if (tieuDe=="") {
                //    bootbox.alert("Vui lòng nhập tiêu đề");
                //    $("#Name").focus();
                //    checkForm = 0;
                //} else {
                //    checkForm = 1;
                //}

                //if (moTa == "") {
                //    bootbox.alert("Vui lòng nhập mô tả");
                //    $("#Description").focus();
                //    checkForm = 0;
                //} else {
                //    checkForm = 1;
                //}

                //if (tacGia == "") {
                //    bootbox.alert("Vui lòng nhập Nguồn/tác giả");
                //    $("#Author").focus();
                //    checkForm = 0;
                //} else {
                //    checkForm = 1;
                //}

                //if (checkForm == 1) {
                //    $("#formTinTuc").submit();
                //}
                $("#formTinTuc").submit();
            }
        });
        $('.btnChuyen').off('click').on('click', function () {
            var ActionId = $('#SysActionId').val();
            if ($('#formTinTuc').valid() && ActionId == "1003")//Duyệt công khai
            {
                $("#formTinTuc").submit();
            }
            else {
                $("#formTinTuc").submit();
            }
            //parent.document.location.reload();
        });
        $('#SysActionId').on('change', function () {
            var ActionId = $(this).val();
            if (ActionId == "1003")//Duyệt công khai
            {
                $("#DivCongKhai").show();
            } else {
                $("#bEffectiveDate").val('');
                $("#eEffectiveDate").val('');
                $("#DivCongKhai").hide();
            }
        });
        $('#filter_count_perpage').off('change').on('change',function () {
            var slt = $(this).val();
            $.cookie('pageSize', slt, { path: '/' });
            var href = Content.removeParams('page').replace('=undefined', '');
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
                url: "/Admin/Content/ChangeContent",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    console.log(response);
                    if (response.Content == true) {
                        btn.html('<span title="Kích hoạt" class="btn btn-success btn-xs"><i class="fa fa-check"></i ></span>');
                    }
                    else {
                        btn.html('<span title="Khóa" class="btn btn-default btn-xs"><i class="fa fa-check"></i ></span>');
                    }
                }
            });
        });*/
        $('.btn-ModelAction').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            var controller = $(this).data('controller');
            var iframe = "";
            iframe += "<iframe src='" + $('#URL_APPLICATION').val() + "/Admin/" + controller + "/Action/" + id + "' width='100%' height='450px' style='border:none;'></iframe>";
            $('#content-tintuc').html(iframe); 
            $('#modalAction').modal();
        });
        $('.btn-view').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            var controller = $(this).data('controller');
            var pupop = "";
            pupop += "<div class=\"modal fade style-16\" id=\"popup-view-Content\">";
            pupop += "<div class=\"modal-dialog modal-xl modal-dialog-centered\">";
            pupop += "<div class=\"modal-content\" >";
            pupop += "<div class=\"modal-header\">";
            pupop += "<h4 class=\"modal-title\" style=\"font-weight:bold;\">THÔNG TIN CHỨC VỤ</h4>";
            pupop += "<button type=\"button\" class=\"close\" data-dismiss=\"modal\">&times;</button>";
            pupop += "</div>";
            pupop += "<div class=\"modal-body\" id=\"popup-content-view-Content\" style='padding: 2px;'>";
            pupop += "<iframe src='" + controller + "' width='100%' height='500px' style='border:none;'></iframe>";
            pupop += "</div>";
            pupop += "</div>";
            pupop += "</div >";
            pupop += "</div >";
            $('#popup-view-Content').remove();
            $('.page-footer').append(pupop);
            $('#popup-view-Content').modal();
        });
        $('.btn-delete').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            bootbox.confirm("Bạn có chắc chắn muốn xóa bản ghi này không?", function (result) {
                if (result == true) {
                    Content.deleteData(id);
                }
            });
        });
        $('#btnDeleteAll').off('click').on('click', function () {
            var values = $(".checkSingle:checked").map(function () { return $(this).data('id'); }).get();
            if (values != "" && values != null) {
                $('#CheckAll').prop('checked', false);
                bootbox.confirm("Bạn có chắc chắn muốn xóa danh sách bản ghi này không?", function (result) {
                    if (result == true) {
                        Content.deleteAllData(values);
                    }
                });
            } else {
                bootbox.alert("Chọn bản ghi cần xóa");
            }
        });
    },
    deleteAllData: function (ids) {
        $.ajax({
            url: URL_APPLICATION + '/Admin/Content/DeleteMulti',
            data: {
                ids: ids
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.Content == true) {
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
            url: URL_APPLICATION + '/Admin/Content/DeleteMulti',
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
        $('#filter_count_perpage').val(ContentConfig.pageSize);
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
Content.init();