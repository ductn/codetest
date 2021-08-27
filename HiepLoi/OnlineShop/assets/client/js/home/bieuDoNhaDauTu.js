var URL_APPLICATION = $('#URL_APPLICATION').val();
Number.prototype.format = function (n, x) {
    var re = '\\d(?=(\\d{' + (x || 3) + '})+' + (n > 0 ? '\\.' : '$') + ')';
    return this.toFixed(Math.max(0, ~~n)).replace(new RegExp(re, 'g'), '$&.');
};

var BieuDoNhaDauTuPortal = {
    init: function () {
        BieuDoNhaDauTuPortal.getBieuDoThongKe();
        BieuDoNhaDauTuPortal.loadData();
        BieuDoNhaDauTuPortal.registerEvents();
    },
    registerEvents: function () {
        $('#huyenNDT').on('change', function () {
            var HuyenId = $('#huyenNDTBieuDo').val();
            var HuyenNDT = $('#huyenNDT').val();
            if (HuyenId != HuyenNDT) {
                $('#huyenNDTBieuDo').val(HuyenNDT).trigger("change");
            }
        });
        $('#xaNDT').on('change', function () {
            var XaId = $('#xaNDTBieuDo').val();
            var XaNDT = $('#xaNDT').val();
            if (XaId != XaNDT) {
                $('#xaNDTBieuDo').val(XaNDT).trigger("change");
            }
        });
        $('#huyenNDTBieuDo').on('change', function () {
            var HuyenId = $('#huyenNDTBieuDo').val();
            var HuyenNDT = $('#huyenNDT').val();
            if (HuyenId != HuyenNDT) {
                $('#huyenNDT').val(HuyenId).trigger("change");
            }
            $("#htmlBanDoNDTI").html('<div id="containerNDT" style="width: 100%; height: 500px"></div>');
            $("#htmlBanDoNDTII").html('<div id="containerNDT2" style="width: 100%; height: 500px"></div>');
            BieuDoNhaDauTuPortal.getProvinceByParent(HuyenId, 'xaNDTBieuDo');
        });
        $('#xaNDTBieuDo').on('change', function () {
            var XaId = $('#xaNDTBieuDo').val();
            var XaNDT = $('#xaNDT').val();
            if (XaId != XaNDT) {
                $('#xaNDT').val(XaId).trigger("change");
            }
            $("#htmlBanDoNDTI").html('<div id="containerNDT" style="width: 100%; height: 500px"></div>');
            $("#htmlBanDoNDTII").html('<div id="containerNDT2" style="width: 100%; height: 500px"></div>');
            BieuDoNhaDauTuPortal.getBieuDoThongKe(true);
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
                BieuDoNhaDauTuPortal.getBieuDoThongKe(true);
            },
            error: function (err) {
                //console.log(err);
            }
        });
    },
    getBieuDoThongKe: function () {
        var HuyenId = $('#huyenNDTBieuDo').val();
        var XaPhuongId = $('#xaNDTBieuDo').val();
        //Chart 2 
        anychart.onDocumentReady(function () {
            $.ajax({
                url: URL_APPLICATION + '/Home/GetDataBieuDoNDT',
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
                            chartVon.container('containerNDT');
                            chartVon.draw();

                        }
                        else {
                            chartVon.container('containerNDT');
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
                            chartHKD.container('containerNDT2');
                            chartHKD.draw();

                        }
                        else {
                            chartHKD.container('containerNDT2');
                            chartHKD.draw();
                        }
                        //End Chart HKD
                    }
                }
            })


        });
    },
}
BieuDoNhaDauTuPortal.init();

