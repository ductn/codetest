var URL_APPLICATION = $('#URL_APPLICATION').val();
Number.prototype.format = function (n, x) {
    var re = '\\d(?=(\\d{' + (x || 3) + '})+' + (n > 0 ? '\\.' : '$') + ')';
    return this.toFixed(Math.max(0, ~~n)).replace(new RegExp(re, 'g'), '$&.');
};

var BieuDoDoanhNghiepPortal = {
    init: function () {
        BieuDoDoanhNghiepPortal.getBieuDoThongKe();
        BieuDoDoanhNghiepPortal.loadData();
        BieuDoDoanhNghiepPortal.registerEvents();
    },
    registerEvents: function () {
        $('#huyenDN').on('change', function () {
            var HuyenId = $('#huyenDNBieuDo').val();
            var HuyenDN = $('#huyenDN').val();
            if (HuyenId != HuyenDN) {
                $('#huyenDNBieuDo').val(HuyenDN).trigger("change");
            }
        });
        $('#xaDN').on('change', function () {
            var XaId = $('#xaDNBieuDo').val();
            var XaDN = $('#xaDN').val();
            if (XaId != XaDN) {
                $('#xaDNBieuDo').val(XaDN).trigger("change");
            }
        });
        $('#huyenDNBieuDo').on('change', function () {
            var HuyenId = $('#huyenDNBieuDo').val();
            var HuyenDN = $('#huyenDN').val();
            if (HuyenId != HuyenDN) {
                $('#huyenDN').val(HuyenId).trigger("change");
            }
            $("#htmlBanDoI").html('<div id="container" style="width: 100%; height: 500px"></div>');
            $("#htmlBanDoII").html('<div id="container2" style="width: 100%; height: 500px"></div>');
            BieuDoDoanhNghiepPortal.getProvinceByParent(HuyenId, 'xaDNBieuDo');
        });
        $('#xaDNBieuDo').on('change', function () {
            var XaId = $('#xaDNBieuDo').val();
            var XaDN = $('#xaDN').val();
            if (XaId != XaDN) {
                $('#xaDN').val(XaId).trigger("change");
            }
            $("#htmlBanDoI").html('<div id="container" style="width: 100%; height: 500px"></div>');
            $("#htmlBanDoII").html('<div id="container2" style="width: 100%; height: 500px"></div>');
            BieuDoDoanhNghiepPortal.getBieuDoThongKe(true);
        });
    },
    loadData: function (changePageSize) {
       
    },
    getProvinceByParent: function (HuyenId, XaId) {
        var url = URL_APPLICATION + '/Home/getProvinceByParent?parentid=' + HuyenId;
        $.ajax({
            url: url,
            data: "",
            type: 'GET',
            success: function (response) {
                //console.log(response);
                $("#" + XaId).html(response);
                BieuDoDoanhNghiepPortal.getBieuDoThongKe(true);
            },
            error: function (err) {
                //console.log(err);
            }
        });
    },
    getBieuDoThongKe: function () {
        var HuyenId = $('#huyenDNBieuDo').val();
        var XaPhuongId = $('#xaDNBieuDo').val();
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
                        chartVon.title("Tổng vốn trên địa bàn: " + response.dataTongVonHKD + " tỷ đồng");
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
                        chartHKD.title("Tổng số hộ trên địa bàn: " + response.dataTongHKD + "");
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
BieuDoDoanhNghiepPortal.init();

