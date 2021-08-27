var URL_APPLICATION = $('#URL_APPLICATION').val();
$(".custom-select2-chucnang").select2({
    templateResult: formatchucnang,
    templateSelection: formatchucnang
});
function formatchucnang(opt) {
    if (!opt.id) {
        return opt.text;
    }
    var optimage = $(opt.element).attr('data-icon');
    var cap = "";
    try {
        cap = $(opt.element).attr('data-cap');
    } catch (e) {

    }
    var module = "";
    var isModule = "";
    try {
        isModule = $(opt.element).attr('data-isModule');
    } catch (e) {

    }
    if (isModule == 1) {
        module += ' <span style="font-size: 10px;padding: 1px 2px;" class="btn btn-round btn-xs btn-warning" title="BIÊN TẬP">';
        module += '<i class="fa fa-gears"></i> <span class="hidden-phone" style="color: white"> BIÊN TẬP </span>';
        module += '</span>';
    } else if (isModule == 2) {
        module += ' <span style="font-size: 10px;padding: 1px 2px;" class="btn btn-round btn-xs btn-success" title="PHÊ DUYỆT">';
        module += '<i class="fa fa-gears"></i> <span class="hidden-phone" style="color: white"> PHÊ DUYỆT </span>';
        module += '</span>';
    } else if (isModule == 3) {
        module += ' <span style="font-size: 10px;padding: 1px 2px;" class="btn btn-round btn-xs btn-info" title="QUẢN TRỊ">';
        module += '<i class="fa fa-gears"></i> <span class="hidden-phone" style="color: white"> QUẢN TRỊ </span>';
        module += '</span>';
    } else if (isModule == 4) {
        module += ' <span style="font-size: 10px;padding: 1px 2px;" class="btn btn-round btn-xs btn-primary" title="DỰ ÁN">';
        module += '<i class="fa fa-vcard-o"></i> <span class="hidden-phone" style="color: white"> DỰ ÁN </span>';
        module += '</span>';
    } else if (isModule == 5) {
        module += ' <span style="font-size: 10px;padding: 1px 2px;" class="btn btn-round btn-xs btn-danger" title="HỆ THỐNG">';
        module += '<i class="fa fa-gears"></i> <span class="hidden-phone" style="color: white"> HỆ THỐNG </span>';
        module += '</span>';
    }

    if (!optimage) {
        return opt.text;
    } else {
        if (cap != "") {
            var $opt = $(
                '<span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;' + cap + '<i class="material-icons f-left">' + optimage + '</i>' + opt.text + module + '</span>'
            );
            return $opt;
        } else {
            var $opt = $(
                '<span><i class="material-icons f-left">' + optimage + '</i>' + opt.text + module + '</span>'
            );
            return $opt;
        }
    }
}

$(".custom-select2-single").select2({
    templateResult: formatState,
    templateSelection: formatState
});
function formatState(opt) {
    if (!opt.id) {
        return opt.text;
    }
    var optimage = $(opt.element).attr('data-icon');
    if (!optimage) {
        return opt.text;
    } else {
        var $opt = $(
            '<span><i class="material-icons f-left">' + optimage + '</i>' + opt.text + '</span>'
        );
        return $opt;
    }
}

$(".custom-color-select2-single").select2({
    templateResult: formatStateColor,
    templateSelection: formatStateColor
});
function formatStateColor(opt) {
    if (!opt.id) {
        return opt.text;
    }
    var optimage = $(opt.element).attr('data-color');
    if (!optimage) {
        return opt.text;
    } else {
        var $opt = $(
            '<span class="' + optimage + '"><i class="fa fa-yelp"></i> ' + opt.text + '</span>'
        );
        return $opt;
    }
}

$(".custom-module-select2-single").select2({
    templateResult: formatStatemodule,
    templateSelection: formatStatemodule
});
function formatStatemodule(opt) {
    if (!opt.id) {
        return opt.text;
    }
    var optimage = $(opt.element).attr('data-color');
    if (!optimage) {
        return opt.text;
    } else {
        var $opt = $(
            '<span class="btn btn-round btn-xs ' + optimage + ' btn-block" title="' + opt.text + '"><i class="fa fa-gears"></i> <span class="hidden-phone" style="color: white"> ' + opt.text + ' </span></span>'
        );
        return $opt;
    }
}

function sendFileTopNav(file) {
    var formData = new FormData();
    formData.append('file', file);
    $.ajax({
        type: 'post',
        url: URL_APPLICATION + '/Handler/fileUploader.ashx',
        data: formData,
        success: function (status) {
            if (status != 'error') {
                var my_path = status;
                $("#UploadedProfileAvatar").attr("src", URL_APPLICATION + my_path);
                $("#ViewProfile_txtAvatar").val(my_path);
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
function ChangeFile(obj) {
    var file, img;
    if ((file = $('#' + obj)[0].files[0])) {
        img = new Image();
        img.onload = function () {
            sendFileTopNav(file);
        };
        img.onerror = function () {
            alert("Not a valid file:" + file.type);
        };
        img.src = _URL.createObjectURL(file);
    }
}
function clickBrowse(obj) {
    $('#' + obj).click();
    return false;
}
var top_nav = {
    init: function () {
        top_nav.loadData();
        top_nav.registerEvents();
    },
    registerEvents: function () {
        $('#frmViewProfile').validate({
            rules: {
                ViewProfile_txtname: "required",
                ViewProfile_txtemail: {
                    required: true,
                    email: true
                }
            },
            messages: {
                ViewProfile_txtname: "Vui lòng tên",
                ViewProfile_txtemail: {
                    required: "Vui lòng nhập email",
                    email: "Không đúng định dạng email"
                }
            }
        });
        $('#frmChangePassword').validate({
            rules: {
                ChangePassword_txtpassword: "required",
                ChangePassword_txtre_password: {
                    required: true,
                    equalTo: "#ChangePassword_txtpassword"
                }
            },
            messages: {
                ChangePassword_txtpassword: "Vui lòng mật khẩu",
                ChangePassword_txtre_password: {
                    required: "Vui lòng lại mật khẩu",
                    equalTo: "Không khớp mật khẩu"
                }
            }
        });
        $('.modal-view-profile').off('click').on('click', function (e) {
            e.preventDefault();
            $('#ModalProfile').modal('show');
            var id = $(this).data('id');
            $('#ViewProfile_hidID').val(id);
            top_nav.loadDetail(id);
        });
        $('#btnViewProfile').off('click').on('click', function () {
            if ($('#frmViewProfile').valid()) {
                top_nav.SaveProfile();
            }
        });
        $('.modal-change-password').off('click').on('click', function (e) {
            e.preventDefault();
            $('#ModalChangePassword').modal('show');
            var id = $(this).data('id');
            $('#ChangePassword_hidID').val(id);
        });
        $('#btnChangePassword').off('click').on('click', function () {
            if ($('#frmChangePassword').valid()) {
                top_nav.changePassword();
            }
        });
        $('.Menu_Module').off('click').on('click', function () {
            var module = $(this).data('module');
            var url = $(this).data('url');
            $.cookie('Module', module, { path: '/', expires: 365 });
            window.location.href = url;
        });
    },
    changePassword: function () {
        var ID = parseFloat($('#ChangePassword_hidID').val());
        var Password = $('#ChangePassword_txtpassword').val();
        var json = {
            ID: ID,
            Password: Password
        };
        var url = URL_APPLICATION + '/Admin/User/ChangePassword';
        $.ajax({
            url: url,
            data: {
                json: JSON.stringify(json)
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    bootbox.alert("Đổi mật khẩu thành công", function () {
                        $('#ModalChangePassword').modal('hide');
                    });
                } else {
                    bootbox.alert(Status);
                }
            },
            error: function (data, Status) {
                console.log(data.responseText);
            }
        });
    },
    SaveProfile: function () {
        var ID = parseFloat($('#ViewProfile_hidID').val());
        var Avatar = $('#ViewProfile_txtAvatar').val();
        var Name = $('#ViewProfile_txtname').val();
        var Email = $('#ViewProfile_txtemail').val();
        var Phone = $('#ViewProfile_txtphone').val();
        var json = {
            ID: ID,
            Avatar: Avatar,
            Name: Name,
            Email: Email,
            Phone: Phone
        };
        var url = URL_APPLICATION + '/Admin/User/SaveProfile';
        $.ajax({
            url: url,
            data: {
                json: JSON.stringify(json)
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    bootbox.alert("Đổi thông tin thành công", function () {
                        var data = response.data;
                        $('.user-Info-name').html(data.Name);
                        $('.modal-u-img').attr('src', data.Avatar);
                        $('#ModalProfile').modal('hide');
                    });
                } else {
                    bootbox.alert(Status);
                }
            },
            error: function (data, Status) {
                console.log(data.responseText);
            }
        });
    },
    loadDetail: function (id) {
        var url = URL_APPLICATION + '/Admin/User/GetDetail';
        $.ajax({
            url: url,
            type: 'GET',
            data: {
                id: id,
            },
            dataType: 'json',
            async: true,
            success: function (response) {
                if (response.status == true) {
                    var data = response.data;
                    $('#titleViewProfile').html('CẬP NHẬT THÔNG TIN');
                    $('#ViewProfile_txtuserName').html(data.UserName);
                    $("#ViewProfile_txtAvatar").val(data.Avatar);
                    $("#UploadedProfileAvatar").attr("src", data.Avatar);
                    $('#ViewProfile_txtname').val(data.Name);
                    $('#ViewProfile_txtemail').val(data.Email);
                    $('#ViewProfile_txtphone').val(data.Phone);
                } else {
                    bootbox.alert(response.message);
                }
            },
            error: function (data, Status) {
                console.log(data.responseText);
            }
        });
    },
    loadData: function () {
        var module = '';
        try {
            module = $.cookie('Module');
        } catch (err) {
        }

        $('ul#sidebarnav li.nav-item').each(function (i) {
            $(this).addClass("hidden");
        });
        var host = window.location.host;
        var href = window.location.href.replace(host, "").replace("http://", "").replace("https://", "");
        var homeHref = $('#URL_APPLICATION').val() + "home";
        if (href != homeHref) {
            $(".block-" + module + "").removeClass("hidden");
        }
    }
}
top_nav.init();