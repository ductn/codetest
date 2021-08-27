var URL_APPLICATION = $('#URL_APPLICATION').val();
var _URL = window.URL || window.webkitURL;
function ChangeFile(obj) {
    var file, img;
    if ((file = $('#' + obj)[0].files[0])) {
        img = new Image();
        img.onload = function () {
            var formData = new FormData();
            formData.append('file', file);
            $.ajax({
                type: 'post',
                url: URL_APPLICATION + '/Handler/SliderUploader.ashx?istype=User',
                data: formData,
                success: function (status) {
                    if (status != 'error') {
                        var my_path = status;
                        $("#Img" + obj).attr("src", URL_APPLICATION + my_path);
                        var hid = obj.split('_');
                        $("#" + hid[1]).val(my_path);
                    }
                },
                processData: false,
                contentType: false,
                error: function () {
                    alert("Lỗi!");
                }
            });
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
var autoSizeIframe = function autoSizeIframe() {
    try {
        var mainWrapperHeight = $('body').height();
        var ViewViecCuaToi = $("#popup-view-user", parent.document.body);
        ViewViecCuaToi.find('iframe').height(mainWrapperHeight);
    } catch (e) {
        // TODO: handle exception
    }
};
var userConfig = {
    pageSize: $.cookie('pageSize') ? $.cookie('pageSize') : 5,
    pageIndex: 1,
}
var user = {
    init: function () {
        user.loadData();
        user.registerEvents();
    },
    registerEvents: function () {
        $('#filter_count_perpage').off('change').on('change', function () {
            var slt = $(this).val();
            $.cookie('pageSize', slt, { path: '/' });
            var href = user.removeParams('page').replace('=undefined', '');
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
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: URL_APPLICATION + "/Admin/User/ChangeStatus",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    console.log(response);
                    if (response.status == true) {
                        btn.html('<span title="Kích hoạt" class="btn btn-success btn-xs"><i class="fa fa-check"></i ></span>');
                    }
                    else {
                        btn.html('<span title="Khóa" class="btn btn-default btn-xs"><i class="fa fa-check"></i ></span>');
                    }
                }
            });
        });
        $('.btn-view').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            var controller = $(this).data('controller');
            var pupop = "";
            pupop += "<div class=\"modal fade style-16\" id=\"popup-view-user\">";
            pupop += "<div class=\"modal-dialog modal-xl modal-dialog-centered\">";
            pupop += "<div class=\"modal-content\" >";
            pupop += "<div class=\"modal-header\">";
            pupop += "<h4 class=\"modal-title\" style=\"font-weight:bold;\">THÔNG TIN TÀI KHOẢN</h4>";
            pupop += "<button type=\"button\" class=\"close\" data-dismiss=\"modal\">&times;</button>";
            pupop += "</div>";
            pupop += "<div class=\"modal-body\" id=\"popup-content-view-user\" style='padding: 2px;'>";
            pupop += "<iframe src='" + controller + "' width='100%' height='500px' style='border:none;'></iframe>";
            pupop += "</div>";
            pupop += "</div>";
            pupop += "</div >";
            pupop += "</div >";
            $('#popup-view-user').remove();
            $('.page-footer').append(pupop);
            $('#popup-view-user').modal();
        });
        $('.btn-delete').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            bootbox.confirm("Bạn có chắc chắn muốn xóa bản ghi này không?", function (result) {
                if (result == true) {
                    user.deleteData(id);
                }
            });
        });
        $('#btnDeleteAll').off('click').on('click', function () {
            var values = $(".checkSingle:checked").map(function () { return $(this).data('id'); }).get();
            if (values != "" && values != null) {
                $('#CheckAll').prop('checked', false);
                bootbox.confirm("Bạn có chắc chắn muốn xóa danh sách bản ghi này không?", function (result) {
                    if (result == true) {
                        user.deleteAllData(values);
                    }
                });
            } else {
                bootbox.alert("Chọn bản ghi cần xóa");
            }
        });
    },
    deleteAllData: function (ids) {
        $.ajax({
            url: URL_APPLICATION + '/Admin/User/DeleteMulti',
            data: {
                ids: ids
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
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
            url: URL_APPLICATION + '/Admin/User/DeleteMulti',
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
    confirmLogin: function (ids) {
        var json = {
            ID: ids
        };
        $.ajax({
            url: '/Login/confirmLogin',
            data: {
                json: JSON.stringify(json)
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    window.location.href = response.data;
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
        $('#filter_count_perpage').val(userConfig.pageSize);
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
user.init();