var URL_APPLICATION = $('#URL_APPLICATION').val();
processMessage();

//thong bao sau khi add-edit-delete
function processMessage() {
    var msgSuccess = $('#msgSuccess').val();
    var msgError = $('#msgError').val();
    if (msgSuccess) {
        new PNotify({
            title: ' Thành công',
            text: msgSuccess,
            type: 'success',
            styling: 'bootstrap3'
        });
    }
    if (msgError) {
        new PNotify({
            title: ' Không thành công',
            text: msgError,
            type: 'error',
            styling: 'bootstrap3'
        });
    }
    var url = $('#URL_APPLICATION').val() + "/Home/XoaPNotifySession";
    $.get(url, function (data, status) {
        //alert("Thành công");
    });
}
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
function readMoney(_value, id) {
    var abc = Formatcurrency(_value).replace(/,/g, ".");
    $("#" + id).val(abc);
}
function sendFile(file) {
    var formData = new FormData();
    formData.append('file', $('#fileUpload')[0].files[0]);
    $.ajax({
        type: 'post',
        url: URL_APPLICATION + '/fileUploader.ashx',
        data: formData,
        success: function (Store) {
            if (Store != 'error') {
                var my_path = Store;
                $("#myUploadedImg").attr("src", my_path);
                $("#Avatar").val(my_path);
            }
        },
        processData: false,
        StoreType: false,
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
        var ViewViecCuaToi = $("#popup-view-Store", parent.document.body);
        ViewViecCuaToi.find('iframe').height(mainWrapperHeight);
    } catch (e) {
        // TODO: handle exception
    }
};
//var StoreConfig = {
//    pageSize: $.cookie('pageSize') ? $.cookie('pageSize') : 5,
//    pageIndex: 1,
//}
var Store = {
    init: function () {
        //Store.loadData();
        Store.registerEvents();
    },
    registerEvents: function () {
        $('#formStore').validate({
            rules: {
                Title: { required: true },
                Email: { required: true, email: true },
                HoTen: { required: true },
                Phone: { required: true},
                DiaChi: { required: true },
                NganhNgheName: { required: true },
                QuyMoVon: { required: true },
                NhanLuc: { required: true },
                DoanhThu: { required: true },
                LoiNhuan: { required: true },
            },
            messages: {
                Title: { required: "Vui lòng nhập tên gian hàng!"},
                Email: { required: "Vui lòng nhập E-mail!", email: "Vui lòng nhập đúng định dạng email" },
                HoTen: { required: "Vui lòng nhập tên đơn vị kinh doanh!"},
                Phone: { required: "Vui lòng nhập điện thoại!"},
                DiaChi: { required: "Vui lòng nhập địa chỉ!" },
                NganhNgheName: { required: "Vui lòng nhập ngành nghề!"},
                QuyMoVon: { required: "Vui lòng nhập quy mô vốn!" },
                NhanLuc: { required: "Vui lòng nhập nhân lực!" },
                DoanhThu: { required: "Vui lòng nhập doanh thu!" },
                LoiNhuan: { required: "Vui lòng nhập lợi nhuận!" },
            }
        }); 
        $("#Phone").keyup(function (e) {
            // cho phep gõ số
            var _value = $(this).val().replace(/\./g, "");
            readMoney(_value, 'Phone');
        });
        $("#PhoneOther").keyup(function (e) {
            // cho phep gõ số
            var _value = $(this).val().replace(/\./g, "");
            readMoney(_value, 'PhoneOther');
        });
        $("#NhanLuc").keyup(function (e) {
            // cho phep gõ số
            var _value = $(this).val().replace(/\./g, "");
            readMoney(_value, 'NhanLuc');
        });
        $("#QuyMoVon").keyup(function (e) {
            // cho phep gõ số
            var _value = $(this).val().replace(/\./g, "");
            readMoney(_value, 'QuyMoVon');
        });
        $("#NhanLuc").keyup(function (e) {
            // cho phep gõ số
            var _value = $(this).val().replace(/\./g, "");
            readMoney(_value, 'NhanLuc');
        });
        $("#DoanhThu").keyup(function (e) {
            // cho phep gõ số
            var _value = $(this).val().replace(/\./g, "");
            readMoney(_value, 'DoanhThu');
        });
        $("#LoiNhuan").keyup(function (e) {
            // cho phep gõ số
            var _value = $(this).val().replace(/\./g, "");
            readMoney(_value, 'LoiNhuan');
        });
        $('.btnSave').off('click').on('click', function () {
            if ($('#formStore').valid()) {
                // replace dot
                $("#QuyMoVon").val($("#QuyMoVon").val().replace(/\./g, ""));
                $("#NhanLuc").val($("#NhanLuc").val().replace(/\./g, ""));
                $("#DoanhThu").val($("#DoanhThu").val().replace(/\./g, ""));
                $("#LoiNhuan").val($("#LoiNhuan").val().replace(/\./g, ""));
                // end replace dot
                getAllImages();
                $("#formStore").submit();
            }
        });
        $('.btn-chuyen').off('click').on('click', function (e) {
            e.preventDefault();
            var idStore = $(this).data('id');
            var nextstatus = $(this).data('nextstatus');
            bootbox.confirm("Bạn có chắc chắn muốn chuyển không?", function (result) {
                if (result == true) {
                    Store.changeStatusQT(idStore, nextstatus);
                }
            });
        });
        //$('.btnChuyen').off('click').on('click', function () {
        //    $("#formStore").submit();
        //    //parent.document.location.reload();
        //});
        $('#filter_count_perpage').off('change').on('change',function () {
            var slt = $(this).val();
            $.cookie('pageSize', slt, { path: '/' });
            var href = Store.removeParams('page').replace('=undefined', '');
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
                url: "/Admin/Store/ChangeStore",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    console.log(response);
                    if (response.Store == true) {
                        btn.html('<span title="Kích hoạt" class="btn btn-success btn-xs"><i class="fa fa-check"></i ></span>');
                    }
                    else {
                        btn.html('<span title="Khóa" class="btn btn-default btn-xs"><i class="fa fa-check"></i ></span>');
                    }
                }
            });
        });*/
        $('.btn-view').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            var controller = $(this).data('controller');
            var pupop = "";
            pupop += "<div class=\"modal fade style-16\" id=\"popup-view-Store\">";
            pupop += "<div class=\"modal-dialog modal-xl modal-dialog-centered\">";
            pupop += "<div class=\"modal-Store\" >";
            pupop += "<div class=\"modal-header\">";
            pupop += "<h4 class=\"modal-title\" style=\"font-weight:bold;\">THÔNG TIN CHỨC VỤ</h4>";
            pupop += "<button type=\"button\" class=\"close\" data-dismiss=\"modal\">&times;</button>";
            pupop += "</div>";
            pupop += "<div class=\"modal-body\" id=\"popup-Store-view-Store\" style='padding: 2px;'>";
            pupop += "<iframe src='" + controller + "' width='100%' height='500px' style='border:none;'></iframe>";
            pupop += "</div>";
            pupop += "</div>";
            pupop += "</div >";
            pupop += "</div >";
            $('#popup-view-Store').remove();
            $('.page-footer').append(pupop);
            $('#popup-view-Store').modal();
        });
        $('.btn-delete').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            bootbox.confirm("Bạn có chắc chắn muốn xóa bản ghi này không?", function (result) {
                if (result == true) {
                    Store.deleteData(id);
                }
            });
        });
        $('#btnDeleteAll').off('click').on('click', function () {
            var values = $(".checkSingle:checked").map(function () { return $(this).data('id'); }).get();
            if (values != "" && values != null) {
                $('#CheckAll').prop('checked', false);
                bootbox.confirm("Bạn có chắc chắn muốn xóa danh sách bản ghi này không?", function (result) {
                    Store.deleteAllData(values);
                });
            } else {
                bootbox.alert("Chọn bản ghi cần xóa");
            }
        });
        
    },
    deleteAllData: function (ids) {
        $.ajax({
            url: URL_APPLICATION + '/Admin/Store/DeleteMulti',
            data: {
                ids: ids
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.Store == true) {
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
            url: URL_APPLICATION + '/QuanLyGianHang/DeleteMulti',
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
    changeStatusQT: function (idStore, nextStatus) {
        $.ajax({
            url: URL_APPLICATION + '/QuanLyGianHang/ChangeStatusQT',
            data: {
                idStore: idStore,
                nextStatus: nextStatus
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
        $('#filter_count_perpage').val(StoreConfig.pageSize);
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
Store.init();