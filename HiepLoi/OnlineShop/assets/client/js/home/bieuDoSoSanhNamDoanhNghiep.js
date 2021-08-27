var URL_APPLICATION = $('#URL_APPLICATION').val();
var bieuDoSoSanhNamDoanhNghiepPortal = {
    init: function () {
        bieuDoSoSanhNamDoanhNghiepPortal.loadData();
        bieuDoSoSanhNamDoanhNghiepPortal.registerEvents();
    },
    registerEvents: function () {
        $('#huyenSSDN').on('change', function () {
            bieuDoSoSanhNamDoanhNghiepPortal.loadData();
        });
        $('#namDuocSSDN').on('change', function () {
            bieuDoSoSanhNamDoanhNghiepPortal.loadData();
        });
        $('#namSSDN').on('change', function () {
            bieuDoSoSanhNamDoanhNghiepPortal.loadData();
        });
    },
    loadData: function (changePageSize) {

        var HuyenId = $('#huyenSSDN').val();
        var namDuocSoSanh = $('#namDuocSSDN').val();
        var namSoSanh = $('#namSSDN').val();
        var obj = {
            HuyenId: HuyenId,
            namDuocSoSanh: namDuocSoSanh,
            namSoSanh: namSoSanh
        };
        $.ajax({
            url: URL_APPLICATION + '/Home/GetSoSanhNam',
            type: "POST",
            data: obj,
            dataType: "json",
            success: function (response) {
                if (response.status == true) {
                    var soDNNamDuocSoSanh = response.soDNNamDuocSoSanh;
                    var soDNNamSoSanh = response.soDNNamSoSanh;
                    var soVonHoaNamDuocSoSanh = Math.round(response.soVonHoaNamDuocSoSanh / 1000000000);
                    var soVonHoaNamSoSanh = Math.round(response.soVonHoaNamSoSanh / 1000000000);
                    var soLDNamDuocSoSanh = response.soLDNamDuocSoSanh;
                    var soLDNamSoSanh = response.soLDNamSoSanh;
                    if (soDNNamDuocSoSanh < soDNNamSoSanh) {
                        $('#ttsoDNNam').html("Tăng");
                    } else {
                        $('#ttsoDNNam').html("Giảm");
                    }
                    if (soVonHoaNamDuocSoSanh < soVonHoaNamSoSanh) {
                        $('#ttsoVonHoaNam').html("Tăng");
                    } else {
                        $('#ttsoVonHoaNam').html("Giảm");
                    }
                    if (soLDNamDuocSoSanh < soLDNamSoSanh) {
                        $('#ttsoLDNam').html("Tăng");
                    } else {
                        $('#ttsoLDNam').html("Giảm");
                    }
                    if (soDNNamDuocSoSanh != 0) {
                        var pt = (Math.abs(soDNNamDuocSoSanh - soDNNamSoSanh) / soDNNamDuocSoSanh * 100).toFixed(2);
                        $('#ptsoDNNam').html(pt);
                    } else {
                        $('#ptsoDNNam').html(0);
                    }
                    if (soVonHoaNamDuocSoSanh != 0) {
                        var pt = (Math.abs(soVonHoaNamDuocSoSanh - soVonHoaNamSoSanh) / soVonHoaNamDuocSoSanh * 100).toFixed(2);
                        $('#ptsoVonHoaNam').html(pt);
                    } else {
                        $('#ptsoVonHoaNam').html(0);
                    }
                    if (soLDNamDuocSoSanh != 0) {
                        var pt = (Math.abs(soLDNamDuocSoSanh - soLDNamSoSanh) / soLDNamDuocSoSanh * 100).toFixed(2);
                        $('#ptsoLDNam').html(pt);
                    } else {
                        $('#ptsoLDNam').html(0);
                    }
                    $('#soDNNamDuocSoSanh').html(soDNNamDuocSoSanh);
                    $('#soDNNamSoSanh').html(soDNNamSoSanh);
                    $('#soVonHoaNamDuocSoSanh').html(soVonHoaNamDuocSoSanh);
                    $('#soVonHoaNamSoSanh').html(soVonHoaNamSoSanh);
                    $('#soLDNamDuocSoSanh').html(soLDNamDuocSoSanh);
                    $('#soLDNamSoSanh').html(soLDNamSoSanh);
                    var chartDN = new CanvasJS.Chart("chartContainerSSDN", {
                        animationEnabled: true,
                        theme: "light2",
                        title: {
                            text: "Số lượng",
                            fontSize: 16,
                            fontFamily: "tahoma",
                            fontColor: "blue"
                        },
                        axisY: {
                            title: "Doanh nghiệp",
                            includeZero: false

                        },
                        data: [{
                            type: "column",
                            indexLabel: "{y}",
                            dataPoints: [
                                { y: response.soDNNamDuocSoSanh, label: "Năm " + response.namDuocSoSanh+"" },
                                { y: response.soDNNamSoSanh, label: "Năm " + response.namSoSanh +"" },
                            ]
                        }]
                    });
                    chartDN.render();
                    var chartVonDN = new CanvasJS.Chart("chartContainerSSVonDN", {
                        animationEnabled: true,
                        theme: "light2",
                        title: {
                            text: "Vốn",
                            fontSize: 16,
                            fontFamily: "tahoma",
                            fontColor: "blue"
                        },
                        axisY: {
                            title: "Tỷ đồng",
                            includeZero: false

                        },
                        data: [{
                            type: "column",
                            indexLabel: "{y}",
                            dataPoints: [
                                { y: soVonHoaNamDuocSoSanh, label: "Năm " + response.namDuocSoSanh + "" },
                                { y: soVonHoaNamSoSanh, label: "Năm " + response.namSoSanh + "" },
                            ]
                        }]
                    });
                    chartVonDN.render();
                    var chartSLDDN = new CanvasJS.Chart("chartContainerSSSLDDN", {
                        animationEnabled: true,
                        theme: "light2",
                        title: {
                            text: "Số lao động",
                            fontSize: 16,
                            fontFamily: "tahoma",
                            fontColor: "blue"
                        },
                        axisY: {
                            title: "Người",
                            includeZero: false

                        },
                        data: [{
                            type: "column",
                            indexLabel: "{y}",
                            dataPoints: [
                                { y: response.soLDNamDuocSoSanh, label: "Năm " + response.namDuocSoSanh + "" },
                                { y: response.soLDNamSoSanh, label: "Năm " + response.namSoSanh + "" },
                            ]
                        }]
                    });
                    chartSLDDN.render();
                }
            }
        })
        
    },
}
bieuDoSoSanhNamDoanhNghiepPortal.init();

