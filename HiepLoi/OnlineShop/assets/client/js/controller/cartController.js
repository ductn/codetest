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
$(function () {
    $("#frmGioHang").validate({
        rules: {
            full_name: {
                required: true,
                specialChars: true
            },
            address: {
                required: true
            },
            phone: {
                required: true,
                checkNumber09: true
            }
        },
        messages: {
            full_name: {
                required: "Vui lòng nhập họ tên",
                specialChars: "Vui lòng nhập đúng định dạng"
            },
            address: {
                required: "Vui lòng nhập địa chỉ",
            },
            phone: {
                required: "Vui lòng nhập số điện thoại",
                checkNumber09: "Vui lòng nhập đúng định dạng"
            }
        }
    })
})
var cart = {
    init: function () {
        cart.regEvents();
    },
    regEvents: function () {
        $('.addCart').off('click').on('click', function (e) {
            e.preventDefault();
            var idProduct = $(this).data('id');
            var idStore = $(this).data('idstore');
            bootbox.confirm("Thêm vào giỏ hàng?", function (result) {
                if (result == true) {
                    var quantity = 1;
                    cart.AddCart(idProduct, idStore, quantity);
                }
            });
        });
        $('.addCartQuality').off('click').on('click', function (e) {
            e.preventDefault();
            var idProduct = $(this).data('id');
            var idStore = $(this).data('idstore');
            var quantity = $('#quality').val();
            bootbox.confirm("Thêm vào giỏ hàng?", function (result) {
                if (result == true) {
                    cart.AddCart(idProduct, idStore, quantity);
                }
            });
        });
        $('.quality input').change(function () {
            var soluong = $(this).val();
            var idItemGioHang = $(this).data('id');
            cart.updateQuantity(idItemGioHang, soluong);
        });

        $('.btn-order').off('click').on('click', function () {
            var check = 0;
            var chkNam = $('#chkNam').is(':checked');
            var fullname = $('#fullname').val();
            var address = $('#address').val();
            var phone = $('#phone').val();
            var note = $('#note').val();
            var email = $('#email').val();
            var Khach = $('#UserKhachMua').val();
            var GioiTinh = 1 //Nữ
            if (chkNam == true) {
                GioiTinh = 0
            }
            if ($('#frmGioHang').valid()) {
                cart.creatOrder(fullname, address, phone, note, GioiTinh, Khach, email);
            }
            //if (fullname == "") {
            //    alert("Vui lòng nhập tên");
            //    check += 1;
            //}
            //if (phone == "") {
            //    alert("Vui lòng nhập Số điện thoại");
            //    check += 1;
            //}
            //if (address == "" || address == null || address == '') {
            //    alert("Vui lòng nhập địa chỉ giao hàng");
            //    check += 1;
            //}
            //if (check==0) {
            //    cart.creatOrder(fullname, address, phone, note, GioiTinh, Khach, email);
            //}
        });
        $('.btnContinue').off('click').on('click', function () {
            window.history.back();
        });
        $('.btn-xacnhan').off('click').on('click', function (e) {
            e.preventDefault();
            var idOrder = $(this).data('id');
            var nextstatus = $(this).data('nextstatus');
            bootbox.confirm("Xác nhận đơn hàng?", function (result) {
                if (result == true) {
                    cart.changeStatusQT(idOrder, nextstatus);
                }
            });
        });
        $('.btn-huy').off('click').on('click', function (e) {
            e.preventDefault();
            var idOrder = $(this).data('id');
            var nextstatus = $(this).data('nextstatus');
            bootbox.confirm("Hủy đơn hàng?", function (result) {
                if (result == true) {
                    cart.changeStatusQT(idOrder, nextstatus);
                }
            });
        });
        $('#btnPayment').off('click').on('click', function () {
            window.location.href = "/thanh-toan";
        });
        $('#btnUpdate').off('click').on('click', function () {
            var listProduct = $('.txtQuantity');
            var cartList = [];
            $.each(listProduct, function (i, item) {
                cartList.push({
                    Quantity: $(item).val(),
                    Product: {
                        ID: $(item).data('id')
                    }
                });
            });

            $.ajax({
                url: URL_APPLICATION + '/Cart/Update',
                data: { cartModel: JSON.stringify(cartList) },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/gio-hang";
                    }
                }
            })
        });

        $('#btnDeleteAll').off('click').on('click', function () {
            $.ajax({
                url: URL_APPLICATION + '/Cart/DeleteAll',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/gio-hang";
                    }
                }
            })
        });

        $('.btn-delete').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            cart.DeleteCart(id);
            //bootbox.confirm("Xóa khỏi giỏ hàng?", function (result) {
            //    if (result == true) {
            //        var id = $(this).data('id');
            //        cart.DeleteCart(id);
            //    }
            //});
        });
    },
    AddCart: function (idProduct, idStore, quantity) {
        $.ajax({
            url: URL_APPLICATION + '/Cart/AddItem',
            data: {
                idProduct: idProduct,
                idStore: idStore,
                quantity: quantity
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    bootbox.confirm(response.message, function (result) {
                        window.location.href = window.location.href;
                    });
                } else {
                    bootbox.alert(response.message);
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    },
    DeleteCart: function (id) {
        $.ajax({
            url: URL_APPLICATION + '/Cart/Delete',
            data: { id: id },
            dataType: 'json',
            type: 'POST',
            success: function (res) {
                if (res.status == true) {
                    window.location.href = window.location.href;
                }
            }
        })
    },
    updateQuantity: function (idItemGioHang, soluong) {
        $.ajax({
            url: URL_APPLICATION + '/Cart/UpdateQuantity',
            data: {
                idItemGioHang: idItemGioHang,
                soluong: soluong
            },
            dataType: 'json',
            type: 'POST',
            success: function (res) {
                if (res.status == true) {
                    window.location.href = window.location.href;
                }
            }
        })
    },
    creatOrder: function (fullname, address, phone, note, GioiTinh, Khach, email) {
        $.ajax({
            url: URL_APPLICATION + '/Cart/CreatOrder',
            data: {
                fullname: fullname,
                address: address,
                phone: phone,
                note: note,
                gioiTinh: GioiTinh,
                userKhach: Khach,
                email: email,
            },
            dataType: 'json',
            type: 'POST',
            success: function (res) {
                if (res.status == true) {
                    bootbox.alert({
                        message: "Đặt hàng thành công!",
                        callback: function () {
                            var url = URL_APPLICATION + "/quan-ly-don-hang-khach.html";
                            window.location.href = url;
                        }
                    })
                }
            }
        })
    },
    changeStatusQT: function (idOrder, nextStatus) {
        $.ajax({
            url: URL_APPLICATION + '/QuanLyGianHang/ChangeStatusQTDonHang',
            data: {
                idOrder: idOrder,
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
}
cart.init();