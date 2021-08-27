var URL_APPLICATION = $('#URL_APPLICATION').val();
var ThongTinHoKinhDoanh = {
    init: function () {

        //ThongTinHoKinhDoanh.loadProvince();
        ThongTinHoKinhDoanh.registerEvent();
        ThongTinHoKinhDoanh.changeBieuDoI();
    },
    registerEvent: function () {
        $('.ViewDetail').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            ThongTinHoKinhDoanh.getThongTinHoKinhDoanhInfor(id);
        });
        $('#HuyenId').off('change').on('change', function () {
            $('#searchFormThongTinHoKinhDoanh').submit();
        });
        $('#XaPhuongId').off('change').on('change', function () {
            $('#searchFormThongTinHoKinhDoanh').submit();
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
            url: URL_APPLICATION + '/ThongTinHoKinhDoanh/LoadProvince',
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
            url: URL_APPLICATION + '/ThongTinHoKinhDoanh/LoadDistrict',
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
    getThongTinHoKinhDoanhInfor: function (id) {
        $.ajax({
            url: URL_APPLICATION + '/ThongTinHoKinhDoanh/GetThongTinHoKinhDoanhInfor',
            type: "POST",
            data: {
                id: id
            },
            dataType: "json",
            success: function (response) {
                if (response.status == true) {
                    var data = response.data;
                    $('#View_NameChinh').html(data.Biz_VietName);
                    $('#View_Name').html(data.Biz_VietName);
                    $('#View_LoaiHinh').html("Hộ kinh doanh");
                    $('#View_MST').html(data.CertifiedCode);
                    $('#View_DiaChi').html(data.Biz_HeadOffice);
                    $('#View_DaiDien').html(data.Ow_Name);
                    $('#View_NgayHoatDong').html(response.NgayDangKy);
                    $('#View_SDT').html(data.Biz_Tel);
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
                url: URL_APPLICATION + '/ThongTinHoKinhDoanh/GetDataBieuDo',
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
ThongTinHoKinhDoanh.init();
