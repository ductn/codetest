var URL_APPLICATION = $('#URL_APPLICATION').val();
//validate form
//jQuery.validator.addMethod("specialChars", function (value, element) {
//    var regex = /[!@#$%^&*\=\[\]{}]/;
//    var id = element.id;
//    if (regex.test(value)) {
//        $("#" + id).focus();
//        return false;  // FAIL validation when REGEX matches
//    } else {
//        return true;   // PASS validation otherwise
//    };
//}, "please use only alphanumeric or alphabetic characters");
//jQuery.validator.addMethod("checkNumber09", function (value, element) {
//    var regex = /^[0-9-+]+$/;
//    var id = element.id;
//    if (value != null && value != '') {
//        if (regex.test(value)) {
//            return true;  // FAIL validation when REGEX matches
//        } else {
//            $("#" + id).focus();
//            return false;   // PASS validation otherwise
//        };
//    }
//    return true;
//}, "please use checkNumber09"); 
//$(function () {
//    $("#formContact").validate({
//        rules: {
//            Name: {
//                required: true,
//                specialChars: true
//            },
//            Phone: {
//                required: true,
//                checkNumber09: true
//            },
//            Email: {
//                required: true,
//                email:true
//            },
//            Title: {
//                required: true,
//                specialChars: true
//            },
//            Content: {
//                required: true,
//                specialChars: true
//            },
           
//        },
//        messages: {
//            Name: {
//                required: "Vui lòng nhập họ tên",
//                specialChars: "Vui lòng nhập đúng định dạng"
//            },
//            Phone: {
//                required: "Vui lòng nhập số điện thoại",
//                checkNumber09: "Vui lòng nhập đúng định dạng"
//            },
//            Email: {
//                required: "Vui lòng nhập email",
//                email: "Vui lòng nhập đúng định dạng"
//            },
//            Title: {
//                required: "Vui lòng nhập chủ đề",
//                specialChars: "Vui lòng nhập đúng định dạng"
//            },
//            Content: {
//                required: "Vui lòng nhập nội dung",
//                specialChars: "Vui lòng nhập đúng định dạng"
//            },
//        }
//    })
//})
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
        return 0;
    }
    else { }
    return 1;
}
$('#refresh').click(function () {
    $('#cpatchaTextBox').val(' ');
    $('#cpatchaTextBox').focus();
    createCaptchaI();
});
var contact = {
    init: function () {
        contact.registerEvents();
    },
    registerEvents: function () {
        $('#btnSend').off('click').on('click', function () {
            //if ($('#formContact').valid()) {
                //if (validateCaptchaI() == 1) {
                    var name = $('#Name').val();
                    var mobile = $('#Phone').val();
                    var title = $('#Title').val();
                    var email = $('#Email').val();
                    var content = $('#Content').val();
                    $.ajax({
                        url: URL_APPLICATION + '/Contact/Send',
                        type: 'POST',
                        dataType: 'json',
                        data: {
                            name: name,
                            mobile: mobile,
                            title: title,
                            email: email,
                            content: content
                        },
                        success: function (res) {
                            if (res.status == true) {
                                alert("Gửi thành công!");
                                contact.resetForm();
                                //bootbox.alert({
                                //    message: "Gửi thành công!",
                                //    callback: function () {
                                //        contact.resetForm();
                                //    }
                                //})
                            }
                        }
                    });
                //}
            //}
        });
    },
    resetForm: function () {
        $('#Name').val('');
        $('#Phone').val('');
        $('#Title').val('');
        $('#Email').val('');
        $('#Content').val('');
    }
}
contact.init();