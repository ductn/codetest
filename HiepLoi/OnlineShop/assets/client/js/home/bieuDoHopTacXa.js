var URL_APPLICATION = $('#URL_APPLICATION').val();
Number.prototype.format = function (n, x) {
    var re = '\\d(?=(\\d{' + (x || 3) + '})+' + (n > 0 ? '\\.' : '$') + ')';
    return this.toFixed(Math.max(0, ~~n)).replace(new RegExp(re, 'g'), '$&.');
};

var BieuDoHopTacXaPortal = {
    init: function () {
        BieuDoHopTacXaPortal.getBieuDoThongKe();
        BieuDoHopTacXaPortal.loadData();
        BieuDoHopTacXaPortal.registerEvents();
    },
    registerEvents: function () {
        $('#huyenHTX').on('change', function () {
            var HuyenId = $('#huyenHTXBieuDo').val();
            var HuyenHTX = $('#huyenHTX').val();
            if (HuyenId != HuyenHTX) {
                $('#huyenHTXBieuDo').val(HuyenHTX).trigger("change");
            }
        });
        $('#xaHTX').on('change', function () {
            var XaId = $('#xaHTXBieuDo').val();
            var XaHTX = $('#xaHTX').val();
            if (XaId != XaHTX) {
                $('#xaHTXBieuDo').val(XaHTX).trigger("change");
            }
        });
        $('#huyenHTXBieuDo').on('change', function () {
            var HuyenId = $('#huyenHTXBieuDo').val();
            var HuyenHTX = $('#huyenHTX').val();
            if (HuyenId != HuyenHTX) {
                $('#huyenHTX').val(HuyenId).trigger("change");
            }
            $("#htmlBanDoHTXI").html('<div id="containerHTX" style="width: 100%; height: 500px"></div>');
            $("#htmlBanDoHTXII").html('<div id="containerHTX2" style="width: 100%; height: 500px"></div>');
            BieuDoHopTacXaPortal.getProvinceByParent(HuyenId, 'xaHTXBieuDo');
        });
        $('#xaHTXBieuDo').on('change', function () {
            var XaId = $('#xaHTXBieuDo').val();
            var XaHTX = $('#xaHTX').val();
            if (XaId != XaHTX) {
                $('#xaHTX').val(XaId).trigger("change");
            }
            $("#htmlBanDoHTXI").html('<div id="containerHTX" style="width: 100%; height: 500px"></div>');
            $("#htmlBanDoHTXII").html('<div id="containerHTX2" style="width: 100%; height: 500px"></div>');
            BieuDoHopTacXaPortal.getBieuDoThongKe(true);
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
                BieuDoHopTacXaPortal.getBieuDoThongKe(true);
            },
            error: function (err) {
                //console.log(err);
            }
        });
    },
    getBieuDoThongKe: function () {
        var HuyenId = $('#huyenHTXBieuDo').val();
        var XaPhuongId = $('#xaHTXBieuDo').val();
        //Chart 2 
        anychart.onDocumentReady(function () {
            $.ajax({
                url: URL_APPLICATION + '/ThongTinHopTacXa/GetDataBieuDo',
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
                            chartVon.container('containerHTX');
                            chartVon.draw();

                        }
                        else {
                            chartVon.container('containerHTX');
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
                            chartHKD.container('containerHTX2');
                            chartHKD.draw();

                        }
                        else {
                            chartHKD.container('containerHTX2');
                            chartHKD.draw();
                        }
                        //End Chart HKD
                    }
                }
            })


        });
    },
}
BieuDoHopTacXaPortal.init();

