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
        url: URL_APPLICATION + '/Handler/fileUploader.ashx',
        data: formData,
        success: function (Report) {
            if (Report != 'error') {
                var my_path = Report;
                $("#myUploadedImg").attr("src", URL_APPLICATION + my_path);
                $("#Avatar").val(my_path);
            }
        },
        processData: false,
        ReportType: false,
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
        var ViewViecCuaToi = $("#popup-view-Report", parent.document.body);
        ViewViecCuaToi.find('iframe').height(mainWrapperHeight);
    } catch (e) {
        // TODO: handle exception
    }
};
var ReportConfig = {
    pageSize: $.cookie('pageSize') ? $.cookie('pageSize') : 5,
    pageIndex: 1,
}
var Report = {
    init: function () {
        Report.loadData();
        Report.registerEvents();
    },
    registerEvents: function () {
        $('#filter_count_perpage').off('change').on('change', function () {
            var slt = $(this).val();
            $.cookie('pageSize', slt, { path: '/' });
            var href = Report.removeParams('page').replace('=undefined', '');
            window.location.href = href;
        });
        $('.btn-searchFromBCDoanhNghiep').off('click').on('click', function () {
            $("#isXuatFile").val(0);
            $("#PhuongId").val($("#XaPhuong").val());// gán giá trị cho biến ẩn
            $("#XaPhuongId").val($("#XaPhuong").val());// gán giá trị cho biến ẩn
            $("#searchFromBCDoanhNghiep").submit();
        });
        $('.btn-ExcelBCDoanhNghiep').off('click').on('click', function () {
            $("#isXuatFile").val(1);// gán giá trị để biết xuất excel hay không
            $("#searchFromBCDoanhNghiep").submit();
        });
        $('.btn-searchFromBCHopTacXa').off('click').on('click', function () {
            $("#isXuatFile").val(0);
            $("#XaPhuongId").val($("#XaPhuong").val());// gán giá trị cho biến ẩn
            $("#searchFromBCHopTacXa").submit();
        });
        $('.btn-ExcelBCHopTacXa').off('click').on('click', function () {
            $("#isXuatFile").val(1);// gán giá trị để biết xuất excel hay không
            $("#searchFromBCHopTacXa").submit();
        });
        $('.btn-searchFromBCHoKinhDoanh').off('click').on('click', function () {
            $("#isXuatFile").val(0);
            $("#XaPhuongId").val($("#XaPhuong").val());// gán giá trị cho biến ẩn
            $("#searchFromBCHoKinhDoanh").submit();
        });
        $('.btn-ExcelBCHoKinhDoanh').off('click').on('click', function () {
            $("#isXuatFile").val(1);// gán giá trị để biết xuất excel hay không
            $("#searchFromBCHoKinhDoanh").submit();
        });
        $('#HuyenId').on('change', function () {
            var HuyenId = $('#HuyenId').val();
            Report.getProvinceByParent(HuyenId);
        });
        $('#ProvinceId').on('change', function () {
            var HuyenId = $('#ProvinceId').val();
            Report.getProvinceByParent(HuyenId);
        });
        $('.btn-delete').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            bootbox.confirm("Bạn có chắc chắn muốn xóa bản ghi này không?", function (result) {
                if (result == true) {
                    Report.deleteData(id);
                }
            });
        });
        $('#btnDeleteAll').off('click').on('click', function () {
            var values = $(".checkSingle:checked").map(function () { return $(this).data('id'); }).get();
            if (values != "" && values != null) {
                $('#CheckAll').prop('checked', false);
                bootbox.confirm("Bạn có chắc chắn muốn xóa danh sách bản ghi này không?", function (result) {
                    if (result == true) {
                        Report.deleteAllData(values);
                    }
                });
            } else {
                bootbox.alert("Chọn bản ghi cần xóa");
            }
        });
    },
    deleteAllData: function (ids) {
        $.ajax({
            url: URL_APPLICATION + '/Admin/Report/DeleteMulti',
            data: {
                ids: ids
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.Report == true) {
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
            url: URL_APPLICATION + '/Admin/Report/DeleteMulti',
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
    getProvinceByParent: function (HuyenId) {
        var url = URL_APPLICATION + '/Admin/Province/getProvinceByParent?parentid=' + HuyenId
        $.ajax({
            url: url,
            data: "",
            type: 'GET',
            success: function (response) {
                $("#XaPhuong").html(response);
            },
            error: function (err) {
                console.log(err);
            }
        });
    },
    loadData: function () {
        $('#filter_count_perpage').val(ReportConfig.pageSize);
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
Report.init();