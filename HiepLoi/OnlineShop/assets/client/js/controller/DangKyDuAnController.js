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
function getMucTieuDuAn() {
    $('#tbMucTieuDuAn tbody').each(function () {
        var localMucTieuDuAn = [];
        $(this).find('tr').each(function () {
            var col = 1;
            var mucTieu = "";
            var tenNganh = "";
            var maNganhVSIC = "";
            var maNganhCPC = "";
            $(this).find('td').each(function () {
                var $itm = $(this);
                if (col == 2) {
                    mucTieu = $itm.children('textarea').val();
                }
                else if (col == 3) {
                    tenNganh = $itm.children('textarea').val();
                }
                else if (col == 4) {
                    maNganhVSIC = $itm.children('input').val();
                }
                else if (col == 5) {
                    maNganhCPC = $itm.children('input').val();
                }
                col = col + 1;
            });
            localMucTieuDuAn.push({
                'dataMucTieu': mucTieu,
                'dataTenNganh': tenNganh,
                'dataMaNganhVSIC': maNganhVSIC,
                'dataMaNganhCPC': maNganhCPC,
            });
        });
        var ThongTinMucTieuDuAn = JSON.stringify(localMucTieuDuAn);
        $("#MucTieuDauTu").val(ThongTinMucTieuDuAn);
    });
    return true;
}
function getTenNhaDauTu() {
    $('#tbTenNhaDauTu tbody').each(function () {
        var localTenNhaDauTu = [];
        $(this).find('tr').each(function () {
            var col = 1;
            var tenNhaDauTu = "";
            $(this).find('td').each(function () {
                var $itm = $(this);
                if (col == 2) {
                    tenNhaDauTu = $itm.children('input').val();
                }
                col = col + 1;
            });
            localTenNhaDauTu.push({
                'datatenNhaDauTu': tenNhaDauTu,
            });
        });
        var ThongTinTenNhaDauTu = JSON.stringify(localTenNhaDauTu);
        $("#DSTenNhaDauTu").val(ThongTinTenNhaDauTu);
    });
    return true;
}
function getNguonVonDauTu() {
    $('#tbNguonVonDauTu tbody').each(function () {
        var localNguonVonDauTu = [];
        $(this).find('tr').each(function () {
            var col = 1;
            var tenNhaDauTu = "";
            var vonGopVND = "";
            var vonGopUSD = "";
            var tyLe = "";
            var phuongThucGop = "";
            var tienDoGop = "";
            $(this).find('td').each(function () {
                var $itm = $(this);
                if (col == 2) {
                    tenNhaDauTu = $itm.children('input').val();
                }
                else if (col == 3) {
                    vonGopVND = $itm.children('input').val();
                }
                else if (col == 4) {
                    vonGopUSD = $itm.children('input').val();
                }
                else if (col == 5) {
                    tyLe = $itm.children('input').val();
                }
                else if (col == 6) {
                    phuongThucGop = $itm.children('input').val();
                }
                else if (col == 7) {
                    tienDoGop = $itm.children('input').val();
                }
                col = col + 1;
            });
            localNguonVonDauTu.push({
                'dataTenNhaDauTu': tenNhaDauTu,
                'dataVonGopVND': vonGopVND,
                'dataVonGopUSD': vonGopUSD,
                'dataTyLe': tyLe,
                'dataPhuongThucGop': phuongThucGop,
                'dataTienDoGop': tienDoGop,
            });
        });
        var ThongTinNguonVonDauTu = JSON.stringify(localNguonVonDauTu);
        $("#DSNguonVonDauTu").val(ThongTinNguonVonDauTu);
    });
    return true;
}
function getVonDieuLeNDT() {
    $('#tbVonDieuLeNDT tbody').each(function () {
        var localVonDieuLeNDT = [];
        $(this).find('tr').each(function () {
            var col = 1;
            var tenNhaDauTu = "";
            var vonGopVND = "";
            var vonGopUSD = "";
            var tyLe = "";
            $(this).find('td').each(function () {
                var $itm = $(this);
                if (col == 2) {
                    tenNhaDauTu = $itm.children('input').val();
                }
                else if (col == 3) {
                    vonGopVND = $itm.children('input').val();
                }
                else if (col == 4) {
                    vonGopUSD = $itm.children('input').val();
                }
                else if (col == 5) {
                    tyLe = $itm.children('input').val();
                }
                col = col + 1;
            });
            localVonDieuLeNDT.push({
                'dataTenNhaDauTu': tenNhaDauTu,
                'dataVonGopVND': vonGopVND,
                'dataVonGopUSD': vonGopUSD,
                'dataTyLe': tyLe,
            });
        });
        var ThongTinVonDieuLeNDT = JSON.stringify(localVonDieuLeNDT);
        $("#DSVonDieuLeNDT").val(ThongTinVonDieuLeNDT);
    });
    return true;
}
function RemoveRowMucTieuDuAn(i) {
    $("#tbMucTieuDuAn tbody #tbMucTieuDuAn" + i).remove();
}
function RemoveRowTenNhaDauTu(i) {
    $("#tbTenNhaDauTu tbody #tbTenNhaDauTu" + i).remove();
}
function RemoveRowNguonVonDauTu(i) {
    $("#tbNguonVonDauTu tbody #tbNguonVonDauTu" + i).remove();
}
function RemoveRowVonDieuLeNDT(i) {
    $("#tbVonDieuLeNDT tbody #tbVonDieuLeNDT" + i).remove();
}
function RemoveRowNhaDauTu(i) {
    $("#tbNhaDauTu tbody #tbNhaDauTu" + i).remove();
}
function EditRowNhaDauTu(index) {
    if ($('#hidDSNhaDauTu').val() != "") {
        $('#checkEdit').val(1);
        $('#checkIndexEdit').val(index);
        var data = jQuery.parseJSON($('#hidDSNhaDauTu').val());
        $.each(data, function (i, item) {
            if (item.dataindex == index) {
                $('#LoaiHinhNDT').val(item.dataLoaiHinhNDT);
                if (item.dataLoaiHinhNDT == 1) {
                    $('#chkCaNhan').click();
                    $('#HoTenNDTCaNhan').val(item.dataHoTenNDTCaNhan);
                    $('#GioiTinhNDTCaNhan').val(item.dataGioiTinhNDTCaNhan);
                    $('#NgaySinhNDTCaNhan').val(item.dataNgaySinhNDTCaNhan);
                    $('#QuocTichNDTCaNhan').val(item.dataQuocTichNDTCaNhan);
                    $('#CMNDNDTCaNhan').val(item.dataCMNDNDTCaNhan);
                    $('#NgayCapCMNDNDTCaNhan').val(item.dataNgayCapCMNDNDTCaNhan);
                    $('#NoiCapCMNDNDTCaNhan').val(item.dataNoiCapCMNDNDTCaNhan);
                    $('#CTCNNDTCaNhan').val(item.dataCTCNNDTCaNhan);
                    $('#SoCTCNNDTCaNhan').val(item.dataSoCTCNNDTCaNhan);
                    $('#NgayCapCTCNNDTCaNhan').val(item.dataNgayCapCTCNNDTCaNhan);
                    $('#NgayHetHanCTCNNDTCaNhan').val(item.dataNgayHetHanCTCNNDTCaNhan);
                    $('#NoiCapCTCNNDTCaNhan').val(item.dataNoiCapCTCNNDTCaNhan);
                    $('#DiaChiTTNDTCaNhan').val(item.dataDiaChiTTNDTCaNhan);
                    $('#ChoOHTNDTCaNhan').val(item.dataChoOHTNDTCaNhan);
                    $('#DienThoaiNDTCaNhan').val(item.dataDienThoaiNDTCaNhan);
                    $('#FaxNDTCaNhan').val(item.dataFaxNDTCaNhan);
                    $('#EmailNDTCaNhan').val(item.dataEmailNDTCaNhan);
                } else {
                    $('#chkToChuc').click();
                    $('#TenNDTToChuc').val(item.dataTenNDTToChuc);
                    $('#GCNNDTToChuc').val(item.dataGCNNDTToChuc);
                    $('#NgayCapGCNNDTToChuc').val(item.dataNgayCapGCNNDTToChuc);
                    $('#NoiCapGCNNDTToChuc').val(item.dataNoiCapGCNNDTToChuc);
                    $('#TruSoNDTToChuc').val(item.dataTruSoNDTToChuc);
                    $('#DienThoaiNDTToChuc').val(item.dataDienThoaiNDTToChuc);
                    $('#FaxNDTToChuc').val(item.dataFaxNDTToChuc);
                    $('#EmailNDTToChuc').val(item.dataEmailNDTToChuc);
                    $('#WebsiteNDTToChuc').val(item.dataWebsiteNDTToChuc);
                    $('#TyLeTVHopDanhNDTToChuc').val(item.dataTyLeTVHopDanhNDTToChuc);
                    $('#HoTenDaiDienNDTToChuc').val(item.dataHoTenDaiDienNDTToChuc);
                    $('#GioiTinhDaiDienNDTToChuc').val(item.dataGioiTinhDaiDienNDTToChuc);
                    $('#NgaySinhDaiDienNDTToChuc').val(item.dataNgaySinhDaiDienNDTToChuc);
                    $('#QuocTichDaiDienNDTToChuc').val(item.dataQuocTichDaiDienNDTToChuc);
                    $('#CMNDDaiDienNDTToChuc').val(item.dataCMNDDaiDienNDTToChuc);
                    $('#NgayCapCMNDDaiDienNDTToChuc').val(item.dataNgayCapCMNDDaiDienNDTToChuc);
                    $('#NoiCapCMNDDaiDienNDTToChuc').val(item.dataNoiCapCMNDDaiDienNDTToChuc);
                    $('#DiaChiTTDaiDienNDTToChuc').val(item.dataDiaChiTTDaiDienNDTToChuc);
                    $('#ChoOHTDaiDienNDTToChuc').val(item.dataChoOHTDaiDienNDTToChuc);
                    $('#DienThoaiDaiDienNDTToChuc').val(item.dataDienThoaiDaiDienNDTToChuc);
                    $('#FaxDaiDienNDTToChuc').val(item.dataFaxDaiDienNDTToChuc);
                    $('#EmailDaiDienNDTToChuc').val(item.dataEmailDaiDienNDTToChuc);
                }
            }
        });
    }
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
        var ViewViecCuaToi = $("#popup-view-DangKyDauTuDuAn", parent.document.body);
        ViewViecCuaToi.find('iframe').height(mainWrapperHeight);
    } catch (e) {
        // TODO: handle exception
    }
};
//var DangKyDauTuDuAnConfig = {
//    pageSize: $.cookie('pageSize') ? $.cookie('pageSize') : 5,
//    pageIndex: 1,
//}
var DangKyDauTuDuAn = {
    init: function () {
        //DangKyDauTuDuAn.loadData();
        DangKyDauTuDuAn.registerEvents();
    },
    registerEvents: function () {
        $('#formDangKyDauTuDuAn').validate({
            rules: {
                TenDuAn: { required: true, specialChars: true },
            },
            messages: {
                TenDuAn: { required: "Vui lòng nhập tên sản phẩm!", specialChars: "Không được chứa ký tự đặc biệt!" },
            }
        }); 
        $('.btnSave').off('click').on('click', function () {
            if ($("#TenDuAn").val() == "") {
                $("#TenDuAn").focus();
            }
            if ($('#formDangKyDauTuDuAn').valid()) {
                getMucTieuDuAn();
                getTenNhaDauTu();
                getNguonVonDauTu();
                getVonDieuLeNDT();
                $('#DSNhaDauTu').val($('#hidDSNhaDauTu').val());
                $("#formDangKyDauTuDuAn").submit();
            }
        });
        $('.btn-chuyen').off('click').on('click', function (e) {
            e.preventDefault();
            var idDangKyDauTuDuAn = $(this).data('id');
            var nextstatus = $(this).data('nextstatus');
            bootbox.confirm("Bạn có chắc chắn muốn chuyển không?", function (result) {
                if (result == true) {
                    DangKyDauTuDuAn.changeStatusQT(idDangKyDauTuDuAn, nextstatus);
                }
            });
        });
        $('#filter_count_perpage').off('change').on('change',function () {
            var slt = $(this).val();
            $.cookie('pageSize', slt, { path: '/' });
            var href = DangKyDauTuDuAn.removeParams('page').replace('=undefined', '');
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
        $('.btn-view').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            var controller = $(this).data('controller');
            var pupop = "";
            pupop += "<div class=\"modal fade style-16\" id=\"popup-view-DangKyDauTuDuAn\">";
            pupop += "<div class=\"modal-dialog modal-xl modal-dialog-centered\">";
            pupop += "<div class=\"modal-DangKyDauTuDuAn\" >";
            pupop += "<div class=\"modal-header\">";
            pupop += "<h4 class=\"modal-title\" style=\"font-weight:bold;\">THÔNG TIN CHỨC VỤ</h4>";
            pupop += "<button type=\"button\" class=\"close\" data-dismiss=\"modal\">&times;</button>";
            pupop += "</div>";
            pupop += "<div class=\"modal-body\" id=\"popup-DangKyDauTuDuAn-view-DangKyDauTuDuAn\" style='padding: 2px;'>";
            pupop += "<iframe src='" + controller + "' width='100%' height='500px' style='border:none;'></iframe>";
            pupop += "</div>";
            pupop += "</div>";
            pupop += "</div >";
            pupop += "</div >";
            $('#popup-view-DangKyDauTuDuAn').remove();
            $('.page-footer').append(pupop);
            $('#popup-view-DangKyDauTuDuAn').modal();
        });
        $('.btn-delete').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            bootbox.confirm("Bạn có chắc chắn muốn xóa bản ghi này không?", function (result) {
                if (result == true) {
                    DangKyDauTuDuAn.deleteData(id);
                }
            });
        });
        $('#btnDeleteAll').off('click').on('click', function () {
            var values = $(".checkSingle:checked").map(function () { return $(this).data('id'); }).get();
            if (values != "" && values != null) {
                $('#CheckAll').prop('checked', false);
                bootbox.confirm("Bạn có chắc chắn muốn xóa danh sách bản ghi này không?", function (result) {
                    DangKyDauTuDuAn.deleteAllData(values);
                });
            } else {
                bootbox.alert("Chọn bản ghi cần xóa");
            }
        });
        $('#HuyenDiaPhuong').on('change', function () {
            var HuyenID = $('#HuyenDiaPhuong').val();
            var XaID = 0;
            DangKyDauTuDuAn.getProvinceByParent(HuyenID, XaID);
        });
        $('#btnAddMucTieuDuAn').on('click', function (e) {
            e.preventDefault();
            var index = $('#hidIndexMucTieuDuAn').val();
            $("#tbMucTieuDuAn tbody").append("<tr id='tbMucTieuDuAn" + index + "'>" +
                "<td>" + index +"</td>" +
                "<td><textarea  cols='30' class='form-control'> </textarea></td>" +
                "<td><textarea  cols='30' class='form-control'> </textarea></td>" +
                "<td><input type='text' class='form-control' /></td>" +
                "<td><input type='text' class='form-control' /></td>" +
                "<td><a class='btn btn-danger btn-sm' onclick='RemoveRowMucTieuDuAn(" + index + ");'><i class='fa fa-trash-o'></i></a></td>" +
                "</tr>");
            $('#hidIndexMucTieuDuAn').val(parseInt(index) + 1);
        });

        $('#btnAddTenNhaDauTu').on('click', function (e) {
            e.preventDefault();
            var index = $('#hidIndexTenNhaDauTu').val();
            $("#tbTenNhaDauTu tbody").append("<tr id='TenNhaDauTu" + index + "'>" +
                "<td>" + index + "</td>" +
                "<td><input type='text' class='form-control'/></td>" +
                "<td><a class='btn btn-danger btn-sm' onclick='RemoveRowTenNhaDauTu(" + index + ");'><i class='fa fa-trash-o'></i></a></td>" +
                "</tr>");
            $('#hidIndexTenNhaDauTu').val(parseInt(index) + 1);
        });

        $('#btnAddNguonVonDauTu').on('click', function (e) {
            e.preventDefault();
            var index = $('#hidIndexNguonVonDauTu').val();
            $("#tbNguonVonDauTu tbody").append("<tr id='tbNguonVonDauTu" + index + "'>" +
                "<td>" + index + "</td>" +
                    "<td><input type='text' class='form-control' /></td>" +
                    "<td><input type='text' class='form-control' /></td>" +
                    "<td><input type='text' class='form-control' /></td>" +
                    "<td><input type='text' class='form-control' /></td>" +
                    "<td><input type='text' class='form-control' /></td>" +
                    "<td><input type='text' class='form-control' /></td>" +
                    "<td><a class='btn btn-danger btn-sm' onclick='RemoveRowNguonVonDauTu(" + index + ");'><i class='fa fa-trash-o'></i></a></td>" +
                "</tr>");
            $('#hidIndexNguonVonDauTu').val(parseInt(index) + 1);
        });

        $('#btnAddVonDieuLeNDT').on('click', function (e) {
            e.preventDefault();
            var index = $('#hidIndexVonDieuLeNDT').val();
            $("#tbVonDieuLeNDT tbody").append("<tr id='tbVonDieuLeNDT" + index + "'>" +
                "<td>" + index + "</td>" +
                "<td><input type='text' class='form-control' /></td>" +
                "<td><input type='text' class='form-control' /></td>" +
                "<td><input type='text' class='form-control' /></td>" +
                "<td><input type='text' class='form-control' /></td>" +
                "<td><a class='btn btn-danger btn-sm' onclick='RemoveRowVonDieuLeNDT(" + index + ");'><i class='fa fa-trash-o'></i></a></td>" +
                "</tr>");
            $('#hidIndexVonDieuLeNDT').val(parseInt(index) + 1);
        });
        $('.btnSaveNDT').off('click').on('click', function () {
            var index = $('#hidIndexNhaDauTu').val();
            var localNhaDauTu = [];
            if ($('#hidDSNhaDauTu').val()!="") {
                var data = jQuery.parseJSON($('#hidDSNhaDauTu').val());
                $.each(data, function (i, item) {
                    localNhaDauTu.push(item);
                });
            }

            var LoaiHinhNDT = $('#LoaiHinhNDT').val();

            //Thông tin cá nhân
            var HoTenNDTCaNhan = $('#HoTenNDTCaNhan').val();
            var GioiTinhNDTCaNhan = $('#GioiTinhNDTCaNhan').val();
            var NgaySinhNDTCaNhan = $('#NgaySinhNDTCaNhan').val();
            var QuocTichNDTCaNhan = $('#QuocTichNDTCaNhan').val();
            var CMNDNDTCaNhan = $('#CMNDNDTCaNhan').val();
            var NgayCapCMNDNDTCaNhan = $('#NgayCapCMNDNDTCaNhan').val();
            var NoiCapCMNDNDTCaNhan = $('#NoiCapCMNDNDTCaNhan').val();
            var CTCNNDTCaNhan = $('#CTCNNDTCaNhan').val();
            var SoCTCNNDTCaNhan = $('#SoCTCNNDTCaNhan').val();
            var NgayCapCTCNNDTCaNhan = $('#NgayCapCTCNNDTCaNhan').val();
            var NgayHetHanCTCNNDTCaNhan = $('#NgayHetHanCTCNNDTCaNhan').val();
            var NoiCapCTCNNDTCaNhan = $('#NoiCapCTCNNDTCaNhan').val();
            var DiaChiTTNDTCaNhan = $('#DiaChiTTNDTCaNhan').val();
            var ChoOHTNDTCaNhan = $('#ChoOHTNDTCaNhan').val();
            var DienThoaiNDTCaNhan = $('#DienThoaiNDTCaNhan').val();
            var FaxNDTCaNhan = $('#FaxNDTCaNhan').val();
            var EmailNDTCaNhan = $('#EmailNDTCaNhan').val();
            //End thông tin cá nhân

            //Thông tin tổ chức
            var TenNDTToChuc = $('#TenNDTToChuc').val();
            var GCNNDTToChuc = $('#GCNNDTToChuc').val();
            var NgayCapGCNNDTToChuc = $('#NgayCapGCNNDTToChuc').val();
            var NoiCapGCNNDTToChuc = $('#NoiCapGCNNDTToChuc').val();
            var TruSoNDTToChuc = $('#TruSoNDTToChuc').val();
            var DienThoaiNDTToChuc = $('#DienThoaiNDTToChuc').val();
            var FaxNDTToChuc = $('#FaxNDTToChuc').val();
            var EmailNDTToChuc = $('#EmailNDTToChuc').val();
            var WebsiteNDTToChuc = $('#WebsiteNDTToChuc').val();
            var TyLeTVHopDanhNDTToChuc = $('#TyLeTVHopDanhNDTToChuc').val();
            var HoTenDaiDienNDTToChuc = $('#HoTenDaiDienNDTToChuc').val();
            var GioiTinhDaiDienNDTToChuc = $('#GioiTinhDaiDienNDTToChuc').val();
            var NgaySinhDaiDienNDTToChuc = $('#NgaySinhDaiDienNDTToChuc').val();
            var QuocTichDaiDienNDTToChuc = $('#QuocTichDaiDienNDTToChuc').val();
            var CMNDDaiDienNDTToChuc = $('#CMNDDaiDienNDTToChuc').val();
            var NgayCapCMNDDaiDienNDTToChuc = $('#NgayCapCMNDDaiDienNDTToChuc').val();
            var NoiCapCMNDDaiDienNDTToChuc = $('#NoiCapCMNDDaiDienNDTToChuc').val();
            var DiaChiTTDaiDienNDTToChuc = $('#DiaChiTTDaiDienNDTToChuc').val();
            var ChoOHTDaiDienNDTToChuc = $('#ChoOHTDaiDienNDTToChuc').val();
            var DienThoaiDaiDienNDTToChuc = $('#DienThoaiDaiDienNDTToChuc').val();
            var FaxDaiDienNDTToChuc = $('#FaxDaiDienNDTToChuc').val();
            var EmailDaiDienNDTToChuc = $('#EmailDaiDienNDTToChuc').val();
            //End thông tin tổ chức

            var ten = "";
            var so = "";
            if ($('#checkEdit').val() == 0) { // Thêm mới nhà đầu tư
                if (LoaiHinhNDT == 1) { // cá nhân
                    ten = HoTenNDTCaNhan;
                    so = CMNDNDTCaNhan;
                    localNhaDauTu.push({
                        'dataindex': index,
                        'dataLoaiHinhNDT': LoaiHinhNDT,
                        'dataHoTenNDTCaNhan': HoTenNDTCaNhan,
                        'dataGioiTinhNDTCaNhan': GioiTinhNDTCaNhan,
                        'dataNgaySinhNDTCaNhan': NgaySinhNDTCaNhan,
                        'dataQuocTichNDTCaNhan': QuocTichNDTCaNhan,
                        'dataCMNDNDTCaNhan': CMNDNDTCaNhan,
                        'dataNgayCapCMNDNDTCaNhan': NgayCapCMNDNDTCaNhan,
                        'dataNoiCapCMNDNDTCaNhan': NoiCapCMNDNDTCaNhan,
                        'dataCTCNNDTCaNhan': CTCNNDTCaNhan,
                        'dataSoCTCNNDTCaNhan': SoCTCNNDTCaNhan,
                        'dataNgayCapCTCNNDTCaNhan': NgayCapCTCNNDTCaNhan,
                        'dataNgayHetHanCTCNNDTCaNhan': NgayHetHanCTCNNDTCaNhan,
                        'dataNoiCapCTCNNDTCaNhan': NoiCapCTCNNDTCaNhan,
                        'dataDiaChiTTNDTCaNhan': DiaChiTTNDTCaNhan,
                        'dataChoOHTNDTCaNhan': ChoOHTNDTCaNhan,
                        'dataDienThoaiNDTCaNhan': DienThoaiNDTCaNhan,
                        'dataFaxNDTCaNhan': FaxNDTCaNhan,
                        'dataEmailNDTCaNhan': EmailNDTCaNhan,
                    });
                }
                else // tổ chức
                {
                    ten = TenNDTToChuc;
                    so = GCNNDTToChuc;
                    localNhaDauTu.push({
                        'dataindex': index,
                        'dataLoaiHinhNDT': LoaiHinhNDT,
                        'dataTenNDTToChuc': TenNDTToChuc,
                        'dataGCNNDTToChuc': GCNNDTToChuc,
                        'dataNgayCapGCNNDTToChuc': NgayCapGCNNDTToChuc,
                        'dataNoiCapGCNNDTToChuc': NoiCapGCNNDTToChuc,
                        'dataTruSoNDTToChuc': TruSoNDTToChuc,
                        'dataDienThoaiNDTToChuc': DienThoaiNDTToChuc,
                        'dataFaxNDTToChuc': FaxNDTToChuc,
                        'dataEmailNDTToChuc': EmailNDTToChuc,
                        'dataWebsiteNDTToChuc': WebsiteNDTToChuc,
                        'dataTyLeTVHopDanhNDTToChuc': TyLeTVHopDanhNDTToChuc,
                        'dataTyLeHoTenDaiDienNDTToChuc': HoTenDaiDienNDTToChuc,
                        'dataGioiTinhDaiDienNDTToChuc': GioiTinhDaiDienNDTToChuc,
                        'dataNgaySinhDaiDienNDTToChuc': NgaySinhDaiDienNDTToChuc,
                        'dataQuocTichDaiDienNDTToChuc': QuocTichDaiDienNDTToChuc,
                        'dataCMNDDaiDienNDTToChuc': CMNDDaiDienNDTToChuc,
                        'dataNgayCapCMNDDaiDienNDTToChuc': NgayCapCMNDDaiDienNDTToChuc,
                        'dataNoiCapCMNDDaiDienNDTToChuc': NoiCapCMNDDaiDienNDTToChuc,
                        'dataDiaChiTTDaiDienNDTToChuc': DiaChiTTDaiDienNDTToChuc,
                        'dataChoOHTDaiDienNDTToChuc': ChoOHTDaiDienNDTToChuc,
                        'dataDienThoaiDaiDienNDTToChuc': DienThoaiDaiDienNDTToChuc,
                        'dataFaxDaiDienNDTToChuc': FaxDaiDienNDTToChuc,
                        'dataEmailDaiDienNDTToChuc': EmailDaiDienNDTToChuc,
                    });
                }
                
                $("#tbNhaDauTu tbody").append("<tr id='tbNhaDauTu" + index + "'>" +
                    "<td>" + ten + "</td>" +
                    "<td>" + so + "</td>" +
                    "<td>" +
                    "<a class= 'btn btn-danger btn-sm' onclick = 'RemoveRowNhaDauTu(" + index + ");' > <i class='fa fa-trash-o'></i></a > " +
                    "<a class= 'btn btn-primary btn-sm' onclick = 'EditRowNhaDauTu(" + index + ");' > <i class='fa fa-pencil'></i></a > " +
                    "</td > " +
                    "</tr>");

                $('#hidIndexNhaDauTu').val(parseInt(index) + 1);

                var hidDSNhaDauTu = JSON.stringify(localNhaDauTu);
                $('#hidDSNhaDauTu').val(hidDSNhaDauTu);
            } else
            {
                if ($('#hidDSNhaDauTu').val() != "") {
                    var data = jQuery.parseJSON($('#hidDSNhaDauTu').val());
                    $.each(data, function (i, item) {
                        if (item.dataindex == $('#checkIndexEdit').val())
                        {
                            if (LoaiHinhNDT == 1) {
                                item.dataLoaiHinhNDT = LoaiHinhNDT;
                                item.dataHoTenNDTCaNhan = HoTenNDTCaNhan;
                                item.dataGioiTinhNDTCaNhan = GioiTinhNDTCaNhan;
                                item.dataNgaySinhNDTCaNhan = NgaySinhNDTCaNhan;
                                item.dataQuocTichNDTCaNhan = QuocTichNDTCaNhan;
                                item.dataCMNDNDTCaNhan = CMNDNDTCaNhan;
                                item.dataNgayCapCMNDNDTCaNhan = NgayCapCMNDNDTCaNhan;
                                item.dataNoiCapCMNDNDTCaNhan = NoiCapCMNDNDTCaNhan;
                                item.dataCTCNNDTCaNhan = CTCNNDTCaNhan;
                                item.dataSoCTCNNDTCaNhan = SoCTCNNDTCaNhan;
                                item.dataNgayCapCTCNNDTCaNhan = NgayCapCTCNNDTCaNhan;
                                item.dataNgayHetHanCTCNNDTCaNhan = NgayHetHanCTCNNDTCaNhan;
                                item.dataNoiCapCTCNNDTCaNhan = NoiCapCTCNNDTCaNhan;
                                item.dataDiaChiTTNDTCaNhan = DiaChiTTNDTCaNhan;
                                item.dataChoOHTNDTCaNhan = ChoOHTNDTCaNhan;
                                item.dataDienThoaiNDTCaNhan = DienThoaiNDTCaNhan;
                                item.dataFaxNDTCaNhan = FaxNDTCaNhan;
                                item.dataEmailNDTCaNhan = EmailNDTCaNhan;
                            }
                            else
                            {
                                item.dataLoaiHinhNDT = LoaiHinhNDT;
                                item.dataTenNDTToChuc = TenNDTToChuc;
                                item.dataGCNNDTToChuc = GCNNDTToChuc;
                                item.dataNgayCapGCNNDTToChuc = NgayCapGCNNDTToChuc;
                                item.dataNoiCapGCNNDTToChuc = NoiCapGCNNDTToChuc;
                                item.dataTruSoNDTToChuc = TruSoNDTToChuc;
                                item.dataDienThoaiNDTToChuc = DienThoaiNDTToChuc;
                                item.dataFaxNDTToChuc = FaxNDTToChuc;
                                item.dataEmailNDTToChuc = EmailNDTToChuc;
                                item.dataWebsiteNDTToChuc = WebsiteNDTToChuc;
                                item.dataTyLeTVHopDanhNDTToChuc = TyLeTVHopDanhNDTToChuc;
                                item.dataTyLeHoTenDaiDienNDTToChuc = HoTenDaiDienNDTToChuc;
                                item.dataGioiTinhDaiDienNDTToChuc = GioiTinhDaiDienNDTToChuc;
                                item.dataNgaySinhDaiDienNDTToChuc = NgaySinhDaiDienNDTToChuc;
                                item.dataQuocTichDaiDienNDTToChuc = QuocTichDaiDienNDTToChuc;
                                item.dataCMNDDaiDienNDTToChuc = CMNDDaiDienNDTToChuc;
                                item.dataNgayCapCMNDDaiDienNDTToChuc = NgayCapCMNDDaiDienNDTToChuc;
                                item.dataNoiCapCMNDDaiDienNDTToChuc = NoiCapCMNDDaiDienNDTToChuc;
                                item.dataDiaChiTTDaiDienNDTToChuc = DiaChiTTDaiDienNDTToChuc;
                                item.dataChoOHTDaiDienNDTToChuc = ChoOHTDaiDienNDTToChuc;
                                item.dataDienThoaiDaiDienNDTToChuc = DienThoaiDaiDienNDTToChuc;
                                item.dataFaxDaiDienNDTToChuc = FaxDaiDienNDTToChuc;
                                item.dataEmailDaiDienNDTToChuc = EmailDaiDienNDTToChuc;
                            }
                            
                        }
                    });
                    var hidDSNhaDauTu = JSON.stringify(data);
                    $('#hidDSNhaDauTu').val(hidDSNhaDauTu);

                    $("#tbNhaDauTu tbody").empty();
                    data = jQuery.parseJSON($('#hidDSNhaDauTu').val());
                    $.each(data, function (i, item) {
                        if (item.dataLoaiHinhNDT == 1) {
                            ten = item.dataHoTenNDTCaNhan;
                            so = item.dataCMNDNDTCaNhan;
                        } else {
                            ten = item.dataTenNDTToChuc;
                            so = item.dataGCNNDTToChuc;
                        }
                        $("#tbNhaDauTu tbody").append("<tr id='tbNhaDauTu" + item.dataindex + "'>" +
                            "<td>" + ten + "</td>" +
                            "<td>" + so + "</td>" +
                            "<td>" +
                            "<a class= 'btn btn-danger btn-sm' onclick = 'RemoveRowNhaDauTu(" + item.dataindex + ");' > <i class='fa fa-trash-o'></i></a > " +
                            "<a class= 'btn btn-primary btn-sm' onclick = 'EditRowNhaDauTu(" + item.dataindex + ");' > <i class='fa fa-pencil'></i></a > " +
                            "</td > " +
                            "</tr>");
                    });

                    $('#checkEdit').val(0);
                    $('#checkIndexEdit').val("");

                }
            }
        });
    },
    getProvinceByParent: function (HuyenId, XaID) {
        var url = URL_APPLICATION + '/Home/getProvinceByParent?parentid=' + HuyenId
        $.ajax({
            url: url,
            data: "",
            type: 'GET',
            success: function (response) {
                $("#XaDiaPhuong").html(response);
                if (XaID != 0) {
                    $("#XaDiaPhuong").val(XaID);
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    },
    deleteAllData: function (ids) {
        $.ajax({
            url: URL_APPLICATION + '/Admin/DangKyDauTuDuAn/DeleteMultiDangKyDauTuDuAn',
            data: {
                ids: ids
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.DangKyDauTuDuAn == true) {
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
            url: URL_APPLICATION + '/QuanLyGianHang/DeleteMultiDangKyDauTuDuAn',
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
    changeStatusQT: function (idDangKyDauTuDuAn, nextStatus) {
        $.ajax({
            url: URL_APPLICATION + '/DangKyDauTu/ChangeStatusQT',
            data: {
                ID: idDangKyDauTuDuAn,
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
        $('#filter_count_perpage').val(DangKyDauTuDuAnConfig.pageSize);
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
DangKyDauTuDuAn.init();