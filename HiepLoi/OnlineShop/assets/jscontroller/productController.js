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
$(document).ready(function () {
    $('body').on('change', '#CategoryID', function () {
        var categoryID = $('#CategoryID').val();
        ddlNhaSanXuat(categoryID, 'UnitId');
    });
    $('body').on('change', '#UnitId', function () {
        var categoryID = $('#CategoryID').val();
        var unitId = $('#UnitId').val();
        ddlNhomSanPham(categoryID,unitId,'ProductCategoryParentId');
    });
    $('body').on('change', '#ProductCategoryParentId', function () {
        var categoryID = $('#CategoryID').val();
        var unitId = $('#UnitId').val();
        var productCategoryParentId = $('#ProductCategoryParentId').val();
        ddlNhanhSanPham(categoryID, unitId, productCategoryParentId, 'ProductCategoryId');
    });
});
function ddlNhaSanXuat(CategoryID, idsub) {
    var CATEGORY_ID = CategoryID;
    var UNIT_ID = 0;
    var PRODUCTCATEGORY_PARENTID = 0;
    var PRODUCTCATEGORY_ID = 0;
    var json = {
        DomainName: URL_APPLICATION,
        CategoryId: CATEGORY_ID,
        UnitId: UNIT_ID,
        ProductCategoryParentId: PRODUCTCATEGORY_PARENTID,
        ProductCategoryId: PRODUCTCATEGORY_ID
    };
    var url = URL_APPLICATION + '/Product/APICategoryUnit';
    $.ajax({
        url: url,
        type: 'POST',
        data: {
            NganhHang: JSON.stringify(json)
        },
        dataType: "json",
        success: function (response) {
            if (response.status == true) {
                var CategoryUnitViewModels = response.CategoryUnitViewModels;
                var option = "";
                option += '<option value="0">--Chọn--</option>';
                if (CategoryUnitViewModels != null && CategoryUnitViewModels != "") {
                    $.each(CategoryUnitViewModels, function (i, item) {
                        option += '<option value="' + item.UnitId + '">' + item.UnitTitle + '</option>';
                    });
                }
                $('#' + idsub).html(option);
            }
        },
        error: function (err) {
            console.log(err);
        }
    });
}
function ddlNhomSanPham(CategoryID,UnitId,idsub) {
    var CATEGORY_ID = CategoryID;
    var UNIT_ID = UnitId;
    var PRODUCTCATEGORY_PARENTID = 0;
    var PRODUCTCATEGORY_ID = 0;
    var json = {
        DomainName: URL_APPLICATION,
        CategoryId: CATEGORY_ID,
        UnitId: UNIT_ID,
        ProductCategoryParentId: PRODUCTCATEGORY_PARENTID,
        ProductCategoryId: PRODUCTCATEGORY_ID
    };
    var url = URL_APPLICATION + '/Product/APIProductCategoryUnit';
    $.ajax({
        url: url,
        type: 'POST',
        data: {
            NganhHang: JSON.stringify(json)
        },
        dataType: "json",
        success: function (response) {
            if (response.status == true) {
                var ProductCategoryUnitViewModels = response.ProductCategoryUnitViewModels;
                var option = "";
                option += '<option value="0">--Chọn--</option>';
                if (ProductCategoryUnitViewModels != null && ProductCategoryUnitViewModels != "") {
                    $.each(ProductCategoryUnitViewModels, function (i, item) {
                        option += '<option value="' + item.ProductCategoryId + '">' + item.ProductCategoryName + '</option>';
                    });
                }
                $('#' + idsub).html(option);
            }
        },
        error: function (err) {
            console.log(err);
        }
    });
}
function ddlNhanhSanPham(CategoryID, UnitId, ProductCategoryParentId, idsub) {
    var CATEGORY_ID = CategoryID;
    var UNIT_ID = UnitId;
    var PRODUCTCATEGORY_PARENTID = ProductCategoryParentId;
    var PRODUCTCATEGORY_ID = 0;
    var json = {
        DomainName: URL_APPLICATION,
        CategoryId: CATEGORY_ID,
        UnitId: UNIT_ID,
        ProductCategoryParentId: PRODUCTCATEGORY_PARENTID,
        ProductCategoryId: PRODUCTCATEGORY_ID
    };
    var url = URL_APPLICATION + '/Product/APIProductCategoryParent';
    $.ajax({
        url: url,
        type: 'POST',
        data: {
            NganhHang: JSON.stringify(json)
        },
        dataType: "json",
        success: function (response) {
            if (response.status == true) {
                var ProductCategorys = response.ProductCategorys;
                var option = "";
                option += '<option value="0">--Chọn--</option>';
                if (ProductCategorys != null && ProductCategorys != "") {
                    $.each(ProductCategorys, function (i, item) {
                        option += '<option value="' + item.Id + '">' + item.Name + '</option>';
                    });
                }
                $('#' + idsub).html(option);
            }
        },
        error: function (err) {
            console.log(err);
        }
    });
}
function sendFile(file) {
    var formData = new FormData();
    formData.append('file', $('#fileUpload')[0].files[0]);
    $.ajax({
        type: 'post',
        url: URL_APPLICATION + '/Handler/fileUploader.ashx',
        data: formData,
        success: function (Product) {
            if (Product != 'error') {
                var my_path = Product;
                $("#myUploadedImg").attr("src", URL_APPLICATION + my_path);
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
var ProductConfig = {
    pageSize: $.cookie('pageSize') ? $.cookie('pageSize') : 5,
    pageIndex: 1,
}
var Product = {
    init: function () {
        Product.loadData();
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
        $('.btnChuyen').off('click').on('click', function () {
            $("#formProduct").submit();
            //parent.document.location.reload();
        });
        $('#filter_count_perpage').off('change').on('change', function () {
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
        $('.btn-chuyen').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            var controller = $(this).data('controller');
            var iframe = "";
            iframe += "<iframe src='" + $('#URL_APPLICATION').val() + "/Admin/" + controller + "/Action/" + id + "' width='100%' height='450px' style='border:none;'></iframe>";
            $('#content-sanpham').html(iframe);
            $('#modalAction').modal();
        });
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
                    if (result == true) {
                        Product.deleteAllData(values);
                    }
                });
            } else {
                bootbox.alert("Chọn bản ghi cần xóa");
            }
        });
        $('.btn-images').off('click').on('click', function (e) {
            e.preventDefault();
            $('#imagesManange').modal('show');
            $('#hidProductID').val($(this).data('id'));
            Product.loadImages();
        });

        $('#btnChooImages').off('click').on('click', function (e) {
            e.preventDefault();
            var finder = new CKFinder();
            finder.selectActionFunction = function (url) {
                $('#imageList').append('<div style="float:left"><img src="' + url + '" width="100" /><a href="#" class="btn-delImage"><i class="fa fa-times"></i></a></div>');

                $('.btn-delImage').off('click').on('click', function (e) {
                    e.preventDefault();
                    $(this).parent().remove();
                });

            };
            finder.popup();
        });
        $('#btnSaveImages').off('click').on('click', function () {
            var images = [];
            var id = $('#hidProductID').val();
            $.each($('#imageList img'), function (i, item) {
                images.push($(item).prop('src'));
            })
            console.log(id);
            $.ajax({
                url: URL_APPLICATION + '/Admin/Product/SaveImages',
                type: 'POST',
                data: {
                    id: id,
                    images: JSON.stringify(images)
                },
                dataType: 'json',
                success: function (response) {
                    if (response.status)
                    {
                        $('#imagesManange').modal('hide');
                        $('#imageList').html('');
                        alert('Save thành công');
                    }
          
                    //thong bao thanh cong
                }
            });
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
        $('.btn-Status').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: URL_APPLICATION + "/Admin/Product/ChangeStatus",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    if (response.status == true) {
                        btn.html('<span title="Kích hoạt" class="btn btn-success btn-xs" style="font-size: 10px;padding: 0px 2px;"><i class="fa fa-check"></i ></span>');
                    }
                    else {
                        btn.html('<span title="Khóa" class="btn btn-default btn-xs" style="font-size: 10px;padding: 0px 2px;"><i class="fa fa-check"></i ></span>');
                    }
                }
            });
        });
        $('.btn-IsHot').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: URL_APPLICATION + "/Admin/Product/ChangeIsHot",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    if (response.status == true) {
                        btn.html('<span title="Kích hoạt" class="btn btn-success btn-xs" style="font-size: 10px;padding: 0px 2px;"><i class="fa fa-check"></i ></span>');
                    }
                    else {
                        btn.html('<span title="Khóa" class="btn btn-default btn-xs" style="font-size: 10px;padding: 0px 2px;"><i class="fa fa-check"></i ></span>');
                    }
                }
            });
        });
        $('.btn-IsPromotion').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: URL_APPLICATION + "/Admin/Product/ChangeIsPromotion",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    if (response.status == true) {
                        btn.html('<span title="Kích hoạt" class="btn btn-success btn-xs" style="font-size: 10px;padding: 0px 2px;"><i class="fa fa-check"></i ></span>');
                    }
                    else {
                        btn.html('<span title="Khóa" class="btn btn-default btn-xs" style="font-size: 10px;padding: 0px 2px;"><i class="fa fa-check"></i ></span>');
                    }
                }
            });
        });
        $('.btn-IsMain').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: URL_APPLICATION + "/Admin/Product/ChangeIsMain",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    if (response.status == true) {
                        btn.html('<span title="Kích hoạt" class="btn btn-success btn-xs" style="font-size: 10px;padding: 0px 2px;"><i class="fa fa-check"></i ></span>');
                    }
                    else {
                        btn.html('<span title="Khóa" class="btn btn-default btn-xs" style="font-size: 10px;padding: 0px 2px;"><i class="fa fa-check"></i ></span>');
                    }
                }
            });
        });
        $('.btn-GoiYMuaSam').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: URL_APPLICATION + "/Admin/Product/ChangeGoiYMuaSam",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    if (response.status == true) {
                        btn.html('<span title="Kích hoạt" class="btn btn-success btn-xs" style="font-size: 10px;padding: 0px 2px;"><i class="fa fa-check"></i ></span>');
                    }
                    else {
                        btn.html('<span title="Khóa" class="btn btn-default btn-xs" style="font-size: 10px;padding: 0px 2px;"><i class="fa fa-check"></i ></span>');
                    }
                }
            });
        });
        $('.btn-category-GoiYMuaSam').off('click').on('click', function (e) {
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
                        $('.btn-category-GoiYMuaSam[data-id$="' + id + '"]').html('<span title="Kích hoạt" class="btn btn-success btn-xs" style="font-size: 10px;padding: 0px 2px;"><i class="fa fa-check"></i ></span>');
                    }
                    else {
                        $('.btn-category-GoiYMuaSam[data-id$="' + id + '"]').html('<span title="Khóa" class="btn btn-default btn-xs" style="font-size: 10px;padding: 0px 2px;"><i class="fa fa-check"></i ></span>');
                    }
                }
            });
        });
        $('.btn-IsDiscount').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: URL_APPLICATION + "/Admin/Product/ChangeIsDiscount",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    if (response.status == true) {
                        btn.html('<span title="Kích hoạt" class="btn btn-success btn-xs" style="font-size: 10px;padding: 0px 2px;"><i class="fa fa-check"></i ></span>');
                    }
                    else {
                        btn.html('<span title="Khóa" class="btn btn-default btn-xs" style="font-size: 10px;padding: 0px 2px;"><i class="fa fa-check"></i ></span>');
                    }
                }
            });
        });
        $('.btn-IsTrending').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: URL_APPLICATION + "/Admin/Product/ChangeIsTrending",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    if (response.status == true) {
                        btn.html('<span title="Kích hoạt" class="btn btn-success btn-xs" style="font-size: 10px;padding: 0px 2px;"><i class="fa fa-check"></i ></span>');
                    }
                    else {
                        btn.html('<span title="Khóa" class="btn btn-default btn-xs" style="font-size: 10px;padding: 0px 2px;"><i class="fa fa-check"></i ></span>');
                    }
                }
            });
        });
    },
    deleteAllData: function (ids) {
        $.ajax({
            url: URL_APPLICATION + '/Admin/Product/DeleteMulti',
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
            url: URL_APPLICATION + '/Admin/Product/DeleteMulti',
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
    },
    loadImages: function () {
        $.ajax({
            url: URL_APPLICATION + '/Admin/Product/LoadImages',
            type: 'GET',
            data: {
                id: $('#hidProductID').val()
            },
            dataType: 'json',
            success: function (response) {
                    var data = response.data;
                    var html = '';
                    $.each(data, function (i, item) {
                        html += '<div style="float:left"><img src="' + item + '" width="100" /><a href="#" class="btn-delImage"><i class="fa fa-times"></i></a></div>'
                    });
                    $('#imageList').html(html);

                    $('.btn-delImage').off('click').on('click', function (e) {
                        e.preventDefault();
                        $(this).parent().remove();
                    });

                //thong bao thanh cong
            }
        });
    }
}
Product.init();