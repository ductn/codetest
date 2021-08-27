var URL_APPLICATION = $('#URL_APPLICATION').val();
Number.prototype.format = function (n, x) {
    var re = '\\d(?=(\\d{' + (x || 3) + '})+' + (n > 0 ? '\\.' : '$') + ')';
    return this.toFixed(Math.max(0, ~~n)).replace(new RegExp(re, 'g'), '$&.');
};

var BieuDoHoKinhDoanhPortal = {
    init: function () {
        BieuDoHoKinhDoanhPortal.getBieuDoThongKe();
        BieuDoHoKinhDoanhPortal.loadData();
        BieuDoHoKinhDoanhPortal.registerEvents();
    },
    registerEvents: function () {
        $('#huyenHKD').on('change', function () {
            var HuyenId = $('#huyenHKDBieuDo').val();
            var HuyenHKD = $('#huyenHKD').val();
            if (HuyenId != HuyenHKD) {
                $('#huyenHKDBieuDo').val(HuyenHKD).trigger("change");
            }
        });
        $('#xaHKD').on('change', function () {
            var XaId = $('#xaHKDBieuDo').val();
            var XaHKD = $('#xaHKD').val();
            if (XaId != XaHKD) {
                $('#xaHKDBieuDo').val(XaHKD).trigger("change");
            }
        });
        $('#huyenHKDBieuDo').on('change', function () {
            var HuyenId = $('#huyenHKDBieuDo').val();
            var HuyenHKD = $('#huyenHKD').val();
            if (HuyenId != HuyenHKD) {
                $('#huyenHKD').val(HuyenId).trigger("change");
            }
            $("#htmlBanDoHKTI").html('<div id="containerHKT" style="width: 100%; height: 500px"></div>');
            $("#htmlBanDoHKTII").html('<div id="containerHKT2" style="width: 100%; height: 500px"></div>');
            BieuDoHoKinhDoanhPortal.getProvinceByParent(HuyenId, 'xaHKDBieuDo');
        });
        $('#xaHKDBieuDo').on('change', function () {
            var XaId = $('#xaHKDBieuDo').val();
            var XaHKD = $('#xaHKD').val();
            if (XaId != XaHKD) {
                $('#xaHKD').val(XaId).trigger("change");
            }
            $("#htmlBanDoHKTI").html('<div id="containerHKT" style="width: 100%; height: 500px"></div>');
            $("#htmlBanDoHKTII").html('<div id="containerHKT2" style="width: 100%; height: 500px"></div>');
            BieuDoHoKinhDoanhPortal.getBieuDoThongKe(true);
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
                BieuDoHoKinhDoanhPortal.getBieuDoThongKe(true);
            },
            error: function (err) {
                //console.log(err);
            }
        });
    },
    getBieuDoThongKe: function () {
        var HuyenId = $('#huyenHKDBieuDo').val();
        var XaPhuongId = $('#xaHKDBieuDo').val();
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
                        chartVon.title("Tổng vốn trên địa bàn: " + response.dataTongVonHKD + " tỷ đồng");
                        chartVon.data(dataVon);
                        if ($(window).width() > 768) {
                            // set legend position
                            chartVon.legend().position("right");
                            // set items layout
                            chartVon.legend().itemsLayout("vertical");
                            chartVon.container('containerHKT');
                            chartVon.draw();

                        }
                        else {
                            chartVon.container('containerHKT');
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
                            chartHKD.container('containerHKT2');
                            chartHKD.draw();

                        }
                        else {
                            chartHKD.container('containerHKT2');
                            chartHKD.draw();
                        }
                        //End Chart HKD
                    }
                }
            })


        });
    },
}
BieuDoHoKinhDoanhPortal.init();

