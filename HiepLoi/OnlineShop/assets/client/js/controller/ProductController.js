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
function sendFile(file) {
    var formData = new FormData();
    formData.append('file', $('#fileUpload')[0].files[0]);
    $.ajax({
        type: 'post',
        url: URL_APPLICATION + '/fileUploader.ashx',
        data: formData,
        success: function (Product) {
            if (Product != 'error') {
                var my_path = Product;
                $("#myUploadedImg").attr("src", my_path);
                $("#Avatar").val(my_path);
            }
        },
        processData: false,
        ProductType: false,
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
        var ViewViecCuaToi = $("#popup-view-Product", parent.document.body);
        ViewViecCuaToi.find('iframe').height(mainWrapperHeight);
    } catch (e) {
        // TODO: handle exception
    }
};
//var ProductConfig = {
//    pageSize: $.cookie('pageSize') ? $.cookie('pageSize') : 5,
//    pageIndex: 1,
//}
var Product = {
    init: function () {
        //Product.loadData();
        Product.registerEvents();
    },
    registerEvents: function () {
        $('#formProduct').validate({
            rules: {
                Name: { required: true, specialChars: true },
                Price: { required: true, specialChars: true },
            },
            messages: {
                Name: { required: "Vui lòng nhập tên sản phẩm!", specialChars: "Không được chứa ký tự đặc biệt!" },
                Price: { required: "Vui lòng nhập giá sản phẩm!", specialChars: "Không được chứa ký tự đặc biệt!" },
            }
        }); 
        $('.btnSave').off('click').on('click', function () {
            if ($("#Name").val() == "") {
                $("#Name").focus();
            }
            else if ($("#Price").val() == "") {
                $("#Price").focus();
            }
            if ($('#formProduct').valid()) {
                // replace dot
                $("#Price").val($("#Price").val().replace(/\./g, ""));
                $("#PromotionPrice").val($("#PromotionPrice").val().replace(/\./g, ""));
                $("#Quantity").val($("#Quantity").val().replace(/\./g, ""));
                // end replace dot
                getAllImages();
                $("#formProduct").submit();
            }
        });
        $('.btn-chuyen').off('click').on('click', function (e) {
            e.preventDefault();
            var idProduct = $(this).data('id');
            var nextstatus = $(this).data('nextstatus');
            bootbox.confirm("Bạn có chắc chắn muốn chuyển không?", function (result) {
                if (result == true) {
                    Product.changeStatusQT(idProduct, nextstatus);
                }
            });
        });
        //$('.btnChuyen').off('click').on('click', function () {
        //    $("#formProduct").submit();
        //    //parent.document.location.reload();
        //});
        $('#filter_count_perpage').off('change').on('change',function () {
            var slt = $(this).val();
            $.cookie('pageSize', slt, { path: '/' });
            var href = Product.removeParams('page').replace('=undefined', '');
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
                url: "/Admin/Product/ChangeProduct",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    console.log(response);
                    if (response.Product == true) {
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
            pupop += "<div class=\"modal fade style-16\" id=\"popup-view-Product\">";
            pupop += "<div class=\"modal-dialog modal-xl modal-dialog-centered\">";
            pupop += "<div class=\"modal-Product\" >";
            pupop += "<div class=\"modal-header\">";
            pupop += "<h4 class=\"modal-title\" style=\"font-weight:bold;\">THÔNG TIN CHỨC VỤ</h4>";
            pupop += "<button type=\"button\" class=\"close\" data-dismiss=\"modal\">&times;</button>";
            pupop += "</div>";
            pupop += "<div class=\"modal-body\" id=\"popup-Product-view-Product\" style='padding: 2px;'>";
            pupop += "<iframe src='" + controller + "' width='100%' height='500px' style='border:none;'></iframe>";
            pupop += "</div>";
            pupop += "</div>";
            pupop += "</div >";
            pupop += "</div >";
            $('#popup-view-Product').remove();
            $('.page-footer').append(pupop);
            $('#popup-view-Product').modal();
        });
        $('.btn-delete').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            bootbox.confirm("Bạn có chắc chắn muốn xóa bản ghi này không?", function (result) {
                if (result == true) {
                    Product.deleteData(id);
                }
            });
        });
        $('#btnDeleteAll').off('click').on('click', function () {
            var values = $(".checkSingle:checked").map(function () { return $(this).data('id'); }).get();
            if (values != "" && values != null) {
                $('#CheckAll').prop('checked', false);
                bootbox.confirm("Bạn có chắc chắn muốn xóa danh sách bản ghi này không?", function (result) {
                    Product.deleteAllData(values);
                });
            } else {
                bootbox.alert("Chọn bản ghi cần xóa");
            }
        });
        $("#Price").keyup(function (e) {
            // cho phep gõ số
            var _value = $(this).val().replace(/\./g, "");
            var tmp = Formatcurrency(_value).replace(/,/g, ".");
            $(this).val(tmp);
        });
        $("#PromotionPrice").keyup(function (e) {
            // cho phep gõ số
            var _value = $(this).val().replace(/\./g, "");
            var tmp = Formatcurrency(_value).replace(/,/g, ".");
            $(this).val(tmp);
        });
        $("#Quantity").keyup(function (e) {
            // cho phep gõ số
            var _value = $(this).val().replace(/\./g, "");
            var tmp = Formatcurrency(_value).replace(/,/g, ".");
            $(this).val(tmp);
        });
    },
    deleteAllData: function (ids) {
        $.ajax({
            url: URL_APPLICATION + '/Admin/Product/DeleteMultiProduct',
            data: {
                ids: ids
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.Product == true) {
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
            url: URL_APPLICATION + '/QuanLyGianHang/DeleteMultiProduct',
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
    changeStatusQT: function (idProduct, nextStatus) {
        $.ajax({
            url: URL_APPLICATION + '/QuanLyGianHang/ChangeStatusQTProduct',
            data: {
                idProduct: idProduct,
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
        $('#filter_count_perpage').val(ProductConfig.pageSize);
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
Product.init();