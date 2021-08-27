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
jQuery.validator.addMethod("valNot0", function (value, element) {
    var id = element.id;
    if (value == 0 || value == "0") {
        $("#" + id).focus();
        return false;  // FAIL validation when REGEX matches
    } else {
        return true;   // PASS validation otherwise
    };
}, "please use valNot0");
jQuery.validator.addMethod("setMatKhau", function (value, element) {
    var Pw = value;
    $('#Password').val(Pw);
    return true;
}, "please use setMatKhau");
jQuery.validator.addMethod("ckMatKhau", function (value, element) {
    var Pw = $('#Password').val();
    var rePw = value;
    if (Pw != rePw) {
        return false;  // FAIL validation when REGEX matches
    } else {
        return true;   // PASS validation otherwise
    };
}, "please use ckMatKhau");
jQuery.validator.addMethod("ckDongY", function (value, element) {
    var id = element.id;
    var elem = document.getElementById(''+id);

    if (elem.checked) {
        return true;
    }
    else {
        return false;
    }
}, "please use ckMatKhau");
//Captcha
var code;
function createCaptchaI() {
    //clear the contents of captcha div first 
    document.getElementById('captcha').innerHTML = "";
    var charsArray =
        "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ@!#$%^&*";
    var lengthOtp = 6;
    var captcha = [];
    for (var i = 0; i < lengthOtp; i++) {
        //below code will not allow Repetition of Characters
        var index = Math.floor(Math.random() * charsArray.length + 1); //get the next character from the array
        if (captcha.indexOf(charsArray[index]) === -1) { captcha.push(charsArray[index]); }
        else { i--; }
    }
    var canv = document.createElement("canvas");
    canv.id = "captcha";
    canv.width = 110;
    canv.height = 40;
    var ctx = canv.getContext("2d");
    ctx.font = "23px Georgia";
    ctx.strokeText(captcha.join(""), 0, 30);
    //storing captcha so that can validate you can save it somewhere else according to your specific requirements
    code = captcha.join("");
    document.getElementById("captcha").appendChild(canv); // adds the canvas to the body element
}
function validateCaptchaI() {
    event.preventDefault();
    var cpatchaTextBox = document.getElementById("cpatchaTextBox").value;
    console.log(cpatchaTextBox);
    console.log(code);
    if (cpatchaTextBox !== code) {
        bootbox.alert({
            message: "Vui lòng nhập đúng mã xác thực!",
            callback: function () {

            }
        })
        createCaptchaI();
        $('#cpatchaTextBox').val('');
        $('#cpatchaTextBox').focus();
        return false;
    }
    else { }
    return true;
}
var DangKyAppPortal = {
    init: function () {
        DangKyAppPortal.loadData();
        DangKyAppPortal.registerEvents();
    },
    registerEvents: function () {
        $('#formRegister').validate({
            rules: {
                UserName: { required: true, specialChars: true},
                Name: { required: true, specialChars: true },
                Email: { required: true, email: true },
                Password: { required: true, setMatKhau:true },
                RetypePassword: { required: true, ckMatKhau: true },
                chkAgree: { ckDongY: true },
            },
            messages: {
                UserName: { required: "Vui lòng nhập Tên đăng nhập", specialChars: "Tên đăng nhập không hợp lệ"},
                Name: { required: "Vui lòng nhập Tên đơn vị kinh doanh", specialChars: "Tên đơn vị kinh doanh không hợp lệ" },
                Email: { required: "Vui lòng nhập Email", email: "Email không hợp lệ " },
                Password: { required: "Vui lòng nhập Mật khẩu", setMatKhau: ""},
                RetypePassword: { required: "Vui lòng nhập Xác nhận mật khẩu", ckMatKhau: "Mật khẩu không trùng nhau" },
                chkAgree: { ckDongY: "Vui lòng chọn đồng ý" },
            }
        }); 
        $('body').on('click', '.btnRegister', function () {
            if (validateCaptchaI() == true) {
                if ($('#formRegister').valid()) {
                    $('#Name').removeAttr('disabled');
                    $('#Phone').removeAttr('disabled');
                    $('#Address').removeAttr('disabled');
                    $('#ProvinceID').removeAttr('disabled');
                    $('#HuyenID').removeAttr('disabled');
                    $('#XaID').removeAttr('disabled');
                    $('#formRegister').submit();
                }
            } 
            
        });
        $('body').on('change', '#ProvinceID', function () {
            var tinhID = $('#ProvinceID').val();
            DangKyAppPortal.getProvinceTinhByParent(tinhID, null);

        });
        $('body').on('change', '#HuyenID', function () {
            var HuyenID = $('#HuyenID').val();
            DangKyAppPortal.getProvinceByParent(HuyenID, null);

        });
    },
    loadData: function (changePageSize) {

    },
    getProvinceTinhByParent: function (ProvinceID, HuyenID) {
        var url = URL_APPLICATION + '/Home/getProvinceByParent?parentid=' + ProvinceID;
        $.ajax({
            url: url,
            data: "",
            type: 'GET',
            success: function (response) {
                $("#HuyenID").html(response);
            },
            error: function (err) {
                console.log(err);
            }
        });
    },
    getProvinceByParent: function (HuyenId, XaID) {
        var url = URL_APPLICATION + '/Home/getProvinceByParent?parentid=' + HuyenId;
        $.ajax({
            url: url,
            data: "",
            type: 'GET',
            success: function (response) {
                $("#XaID").html(response);
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
                        var Pw = $('#Password').val();
                        if (response.loai == "3") { // loai=3 -> Hộ kinh doanh
                            var data = response.dataHKD;
                            $('#Name').val(data.Biz_VietName);
                            $('#Email').val(data.Biz_Email);
                            $('#Phone').val(data.Biz_Tel);
                            $('#HuyenID').val(data.ProvinceId);
                            $('#Address').val(response.sonha);
                            if (data.ProvinceId != null) {
                                DangKyAppPortal.getProvinceByParent(data.ProvinceId, data.XaPhuongId);
                            }
                        } else if (response.loai == "2") {// loai =2 -> hợp tác xã
                            var data = response.dataHTX;
                            $('#Name').val(data.Biz_VietName);
                            $('#Email').val(data.Biz_Email);
                            $('#Phone').val(data.Biz_Tel);
                            $('#HuyenID').val(data.ProvinceId);
                            $('#Address').val(response.sonha);
                            if (DangKyAppPortal.ProvinceId != null) {
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
                                DangKyAppPortal.getProvinceByParent(data.HuyenId, data.PhuongId);
                            }
                        }
                    } else {
                        $("#UserName").focus();
                        alert("Tên người dùng đã tồn tại trong hệ thống!");
                    }
                    
                }
            }
        })
    },
}
DangKyAppPortal.init();

