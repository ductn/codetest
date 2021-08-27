var URL_APPLICATION = $('#URL_APPLICATION').val();
//validate form
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
$(function () {
    $("#formChangePass").validate({
        rules: {
            PasswordOld: {
                required: true,
                specialChars: false
            },
            PasswordNew1: {
                required: true,
                specialChars: false
            },
            PasswordNew2: {
                required: true,
                specialChars: false
            },
        },
        messages: {
            PasswordOld: {
                required: "Vui lòng nhập!",
                specialChars: "Vui lòng nhập đúng định dạng"
            },
            PasswordNew1: {
                required: "Vui lòng nhập!",
                checkNumber09: "Vui lòng nhập đúng định dạng"
            },
            PasswordNew2: {
                required: "Vui lòng nhập!",
                email: "Vui lòng nhập đúng định dạng"
            },
        }
    })
})
var user = {
    init: function () {

        user.loadProvince();
        user.registerEvent();
    },
    registerEvent: function () {
        $('.btnRegister').off('click').on('click', function () {
            var Pass = $('#Password').val();
            var Retypepass = $('#RetypePassword').val();
            validateCaptcha();
            //if (Pass == Retypepass) {
                if (user.checkAgreement() == true)
                {
                    $("#Name").prop('disabled', false);
                    //$("#Email").prop('disabled', false);
                    $("#Phone").prop('disabled', false);
                    $("#Address").prop('disabled', false);
                    $("#ProvinceID").prop('disabled', false);
                    $("#HuyenID").prop('disabled', false);
                    $("#XaID").prop('disabled', false);
                   $("#formRegister").submit();
                }
            //} else {
            //    alert("Nhập mật khẩu chưa khớp!");
            //}
        });
        $('.btn-ChangePass').off('click').on('click', function () {
            if ($('#formChangePass').valid())
            {
                var PasswordNew2 = $("#PasswordNew2").val();
                var PasswordNew1 = $("#PasswordNew1").val();
                if (PasswordNew2 != PasswordNew1) {
                    alert('Chưa trùng khớp');
                    $(this).focusin();
                } else {
                    var PasswordOld = $("#PasswordOld").val();
                    var UserName = $("#UserName").val();
                    $.ajax({
                        url: URL_APPLICATION + '/User/CheckPassOld',
                        type: "POST",
                        data: {
                            UserName: UserName,
                            PasswordOld: PasswordOld
                        },
                        dataType: "json",
                        success: function (response) {
                            if (response.status == true) {
                                $("#formChangePass").submit();
                            }
                            else {
                                alert("Mật khẩu cũ chưa đúng!");
                            }
                        }
                    })
                }
            }
        });
        $('#ResetChangePass').off('click').on('click', function () {
            user.resetFormChangePass();
        });
        $('#ddlProvince').off('change').on('change', function () {
            var id = $(this).val();
            if (id != '') {
                user.loadDistrict(parseInt(id));
            }
            else {
                $('#ddlDistrict').html('');
            }
        });
        $('#HuyenID').on('change', function () {
            var HuyenID = $('#HuyenID').val();
            var XaID = 0;
            user.getProvinceByParent(HuyenID, XaID);
        });
        $("#UserName").focusout(function () {
            var UserName = $(this).val();
            user.getUserInfor(UserName);
        });
        $("#PasswordOld").focusout(function () {
            var PasswordOld = $(this).val();
            var UserName = $("#UserName").val();
            user.checkPassOld(UserName , PasswordOld);
        });
        $("#PasswordOld").focusout(function () {
            var PasswordOld = $(this).val();
            var UserName = $("#UserName").val();
            user.checkPassOld(UserName, PasswordOld);
        });
        $("#PasswordNew2").focusout(function () {
            var PasswordNew2 = $(this).val();
            var PasswordNew1 = $("#PasswordNew1").val();
            if (PasswordNew2 != PasswordNew1) {
                $("#PasswordNew2").focusin();
                alert('Chưa trùng khớp');
            } else {

            }
        });
    },
    checkAgreement: function () {
        var elem = document.getElementById('chkAgree');

        if (elem.checked) {
            return true;
        }
        else {
            alert('Vui lòng nhấn chọn Đồng ý.')
            return false;
        }
    },
    loadProvince: function () {
        $.ajax({
            url: URL_APPLICATION + '/User/LoadProvince',
            type: "POST",
            dataType: "json",
            success: function (response) {
                if (response.status == true) {
                    var html = '<option value="">--Chọn tỉnh thành--</option>';
                    var data = response.data;
                    $.each(data, function (i, item) {
                        html += '<option value="' + item.ID + '">' + item.Name + '</option>'
                    });
                    $('#ddlProvince').html(html);
                }
            }
        })
    },
    loadDistrict: function (id) {
        $.ajax({
            url: URL_APPLICATION + '/User/LoadDistrict',
            type: "POST",
            data: { provinceID: id },
            dataType: "json",
            success: function (response) {
                if (response.status == true) {
                    var html = '<option value="">--Chọn quận huyện--</option>';
                    var data = response.data;
                    $.each(data, function (i, item) {
                        html += '<option value="' + item.ID + '">' + item.Name + '</option>'
                    });
                    $('#ddlDistrict').html(html);
                }
            }
        })
    },
    getProvinceByParent: function (HuyenId, XaID) {
        var url = URL_APPLICATION + '/Admin/Province/getProvinceByParent?parentid=' + HuyenId
        $.ajax({
            url: url,
            data: "",
            type: 'GET',
            success: function (response) {
                $("#XaID").html(response);
                if (XaID != 0) {
                    $("#XaID").val(XaID);
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    },
    getUserInfor: function (UserName) {
        $.ajax({
            url: URL_APPLICATION + '/User/GetUserInfor',
            type: "POST",
            data: {
                UserName: UserName
            },
            dataType: "json",
            success: function (response) {
                if (response.status == true) {
                    if (response.checkUser == false) {
                        if (response.loai == "3") { // loai=3 -> Hộ kinh doanh
                            var data = response.dataHKD;
                            $('#Name').val(data.Biz_VietName);
                            $('#Email').val(data.Biz_Email);
                            $('#Phone').val(data.Biz_Tel);
                            $('#HuyenID').val(data.ProvinceId);
                            $('#Address').val(response.sonha);
                            if (data.ProvinceId != null) {
                                user.getProvinceByParent(data.ProvinceId, data.XaPhuongId);
                            }
                        } else if (response.loai == "2") {// loai =2 -> hợp tác xã
                            var data = response.dataHTX;
                            $('#Name').val(data.Biz_VietName);
                            $('#Email').val(data.Biz_Email);
                            $('#Phone').val(data.Biz_Tel);
                            $('#HuyenID').val(data.ProvinceId);
                            $('#Address').val(response.sonha);
                            if (data.ProvinceId != null) {
                                user.getProvinceByParent(data.ProvinceId, data.XaPhuongId);
                            }
                        } else {//loai =1 -> doanh nghiệp
                            var data = response.dataDN;
                            $('#Name').val(data.TenDoanhNghiep);
                            $('#Email').val(data.Email);
                            $('#Phone').val(data.DienThoai);
                            $('#HuyenID').val(data.HuyenId);
                            $('#Address').val(response.sonha);
                            if (data.HuyenId != null) {
                                user.getProvinceByParent(data.HuyenId, data.PhuongId);
                            }
                        }
                    } else {
                        alert("Tên người dùng đã tồn tại trong hệ thống!");
                    }
                    
                }
            }
        })
    },
    checkPassOld: function (UserName , PasswordOld) {
        $.ajax({
            url: URL_APPLICATION + '/User/CheckPassOld',
            type: "POST",
            data: {
                UserName: UserName,
                PasswordOld: PasswordOld
            },
            dataType: "json",
            success: function (response) {
                if (response.status == true) {
                }
                else {
                    alert("Mật khẩu cũ chưa đúng!");
                }
            }
        })
    },
    resetFormChangePass: function () {
        $('#PasswordOld').val('');
        $('#PasswordNew1').val('');
        $('#PasswordNew2').val('');
    },
}
user.init();
