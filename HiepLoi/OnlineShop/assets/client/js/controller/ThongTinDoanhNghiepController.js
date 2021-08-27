var URL_APPLICATION = $('#URL_APPLICATION').val();
var ThongTinDoanhNghiep = {
    init: function () {

        //ThongTinDoanhNghiep.loadProvince();
        ThongTinDoanhNghiep.registerEvent();
        ThongTinDoanhNghiep.changeBieuDoI();
    },
    registerEvent: function () {
        $('.ViewDetail').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            ThongTinDoanhNghiep.getThongTinDoanhNghiepInfor(id);
        });
        $('#HuyenId').off('change').on('change', function () {
            $('#searchFormThongTinDoanhNghiep').submit();
        });
        $('#XaPhuongId').off('change').on('change', function () {
            $('#searchFormThongTinDoanhNghiep').submit();
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
            url: URL_APPLICATION + '/ThongTinDoanhNghiep/LoadProvince',
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
            url: URL_APPLICATION + '/ThongTinDoanhNghiep/LoadDistrict',
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
                $("#XaPhuongId").html(response);
                if (XaID != 0) {
                    $("#XaPhuongId").val(XaID);
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    },
    getThongTinDoanhNghiepInfor: function (id) {
        $.ajax({
            url: URL_APPLICATION + '/ThongTinDoanhNghiep/GetThongTinDoanhNghiepInfor',
            type: "POST",
            data: {
                id: id
            },
            dataType: "json",
            success: function (response) {
                if (response.status == true) {
                    var data = response.data;
                    $('#View_NameChinh').html(data.TenDoanhNghiep);
                    $('#View_Name').html(data.TenDoanhNghiep);
                    $('#View_LoaiHinh').html("Doanh nghiệp");
                    $('#View_MST').html(data.MaSoDoanhNghiep);
                    $('#View_DiaChi').html(data.DiaChiTruSoChinh);
                    $('#View_DaiDien').html(data.NguoiDaiDienTheoPhapLuat);
                    $('#View_NgayHoatDong').html(response.NgayDangKy);
                    $('#View_SDT').html(data.DienThoai);
                    $('#View_TrangThai').html("Đang hoạt động");
                    $('#modalSocial').modal('show');
                }
            }
        })
    },
    changeBieuDoI: function () {
        var HuyenId = $('#HuyenId').val();
        var XaPhuongId = $('#XaPhuongId').val();
        //Chart 2 
        anychart.onDocumentReady(function () {
            $.ajax({
                url: URL_APPLICATION + '/ThongTinDoanhNghiep/GetDataBieuDo',
                type: "POST",
                data: {
                    HuyenId: HuyenId,
                    XaPhuongId: XaPhuongId
                },
                dataType: "json",
                success: function (response) {
                    if (response.status == true) {
                        //Chart Von
                        var dataChartVon = response.dataVon;
                        var dataVon = [];
                        $.each(dataChartVon, function (i, item) {
                            var obj = { x: item.title, value: item.amount };
                            dataVon.push(obj);
                        });
                        var chartVon = anychart.pie();
                        chartVon.title("Tổng vốn trên địa bàn: " + response.dataTongVonHKD+" đồng");
                        chartVon.data(dataVon);
                        if ($(window).width() > 768) {
                            // set legend position
                            chartVon.legend().position("right");
                            // set items layout
                            chartVon.legend().itemsLayout("vertical");
                            chartVon.container('container');
                            chartVon.draw();

                        }
                        else {
                            chartVon.container('container');
                            chartVon.draw();
                        }
                        //End chart Vốn
                        //Chart HKD
                        var dataChartHKD = response.dataHKD;
                        var dataHKD = [];
                        $.each(dataChartHKD, function (i, item) {
                            var obj = { x: item.title, value: item.amount };
                            dataHKD.push(obj);
                        });
                        var chartHKD = anychart.pie();
                        chartHKD.title("Tổng số hộ trên địa bàn: " + response.dataTongHKD +"");
                        chartHKD.data(dataHKD);
                        if ($(window).width() > 768) {
                            // set legend position
                            chartHKD.legend().position("right");
                            // set items layout
                            chartHKD.legend().itemsLayout("vertical");
                            chartHKD.container('container2');
                            chartHKD.draw();

                        }
                        else {
                            chartHKD.container('container2');
                            chartHKD.draw();
                        }
                        //End Chart HKD
                    }
                }
            })
            

        });
    },
}
ThongTinDoanhNghiep.init();
