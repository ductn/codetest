var URL_APPLICATION = $('#URL_APPLICATION').val();
var bieuDoDinhHuongDoanhNghiepPortal = {
    init: function () {
        bieuDoDinhHuongDoanhNghiepPortal.registerEvents();
        bieuDoDinhHuongDoanhNghiepPortal.loadData();
    },
    registerEvents: function () {
        $('#frmCapNhatMTN').validate({
            rules: {
                soVon: { required: true },
                soLuong: { required: true },
                soLD: { required: true }
            },
            messages: {
                soVon: { required: "Vui lòng nhập số vốn" },
                soLuong: { required: "Vui lòng nhập số lượng" },
                soLD: { required: "Vui lòng nhập số lao động" }
            }
        }); 
        var today = new Date();
        var year = today.getFullYear();
        $('#namThamChieu').val(year - 1);
        var str = "";
        var tong = year + 10;
        for (i = year; i < tong; i++) {
            str += '<option value="' + i + '">' + i +'</option>';
        }
        $('#mucTieuNam').html(str);

        $('body').on('change', '#namThamChieu', function () {
            var gt = parseInt($('#namThamChieu').val()) + 1;
            var tong = gt + 10;
            var str = "";
            for (i = gt; i < tong; i++) {
                str += '<option value="' + i + '">' + i + '</option>';
            }
            $('#mucTieuNam').html(str);
            bieuDoDinhHuongDoanhNghiepPortal.loadData();
        });
        $('body').on('change', '#mucTieuNam', function () {
            bieuDoDinhHuongDoanhNghiepPortal.loadData();
        });
        $('body').on('change', '#namMucTieu', function () {
            var namMucTieu = $('#namMucTieu').val();
            var obj = {
                namMucTieu: namMucTieu
            };
            $.ajax({
                url: URL_APPLICATION + '/Home/DuLieuMucTieuNam',
                type: "POST",
                data: obj,
                dataType: "json",
                success: function (response) {
                    $('#soLuong').val(response.SoLuong);
                    $('#soVon').val(response.SoVon);
                    $('#soLD').val(response.SoLaoDong);
                }
            });
        });
        $('body').on('click', '#taoMucTieuNam', function () {
            var namMucTieu = $('#namMucTieu').val();
            var obj = {
                namMucTieu: namMucTieu
            };
            $.ajax({
                url: URL_APPLICATION + '/Home/DuLieuMucTieuNam',
                type: "POST",
                data: obj,
                dataType: "json",
                success: function (response) {
                    $('#soLuong').val(response.SoLuong);
                    $('#soVon').val(response.SoVon);
                    $('#soLD').val(response.SoLaoDong);

                    $('#modalNamMucTieu').modal();
                    $('body').on('click', '#capNhatNMT', function () {
                        if ($('#frmCapNhatMTN').valid()) {
                            var namMucTieu = $('#namMucTieu').val();
                            var soLuong = $('#soLuong').val();
                            var soVon = $('#soVon').val();
                            var soLD = $('#soLD').val();
                            var url = URL_APPLICATION + "/Home/CapNhatMucTieuNam";
                            var obj = {
                                namMucTieu: namMucTieu,
                                soLuong: soLuong,
                                soVon: soVon,
                                soLD: soLD
                            };
                            $.ajax({
                                url: url,
                                type: "POST",
                                data: obj,
                                dataType: "json",
                                success: function (response) {
                                    if (response.status == true) {
                                        $('#modalNamMucTieu').modal('hide');
                                        bieuDoDinhHuongDoanhNghiepPortal.loadData();
                                    }
                                }
                            })
                        }
                    });

                }
            });
            
        });
    },
    loadData: function (changePageSize) {
        var namThamChieu = $('#namThamChieu').val();
        var mucTieuNam = $('#mucTieuNam').val();
        var obj = {
            namMucTieu: mucTieuNam,
            namThamChieu: namThamChieu
        };
        $.ajax({
            url: URL_APPLICATION + '/Home/GetDinhHuongPhatTrien',
            type: "POST",
            data: obj,
            dataType: "json",
            success: function (response) {
                var soDNNamThamChieu = response.soDNNamThamChieu;
                var soDNNamMucTieu = response.soDNNamMucTieu;
                var soVonHoaNamThamChieu = Math.round(response.soVonHoaNamThamChieu / 1000000000);
                var soVonHoaNamMucTieu = Math.round(response.soVonHoaNamMucTieu);
                var soLDNamThamChieu = response.soLDNamThamChieu;
                var soLDNamMucTieu = response.soLDNamMucTieu;
                if (soDNNamThamChieu < soDNNamMucTieu) {
                    $('#ttsoDNNamDinhHuong').html("Tăng");
                } else {
                    $('#ttsoDNNamDinhHuong').html("Giảm");
                }
                if (soVonHoaNamThamChieu < soVonHoaNamMucTieu) {
                    $('#ttsoVonHoaNamDinhHuong').html("Tăng");
                } else {
                    $('#ttsoVonHoaNamDinhHuong').html("Giảm");
                }
                if (soLDNamThamChieu < soLDNamMucTieu) {
                    $('#ttsoLDNamDinhHuong').html("Tăng");
                } else {
                    $('#ttsoLDNamDinhHuong').html("Giảm");
                }
                if (soDNNamThamChieu != 0) {
                    var pt = (Math.abs(soDNNamThamChieu - soDNNamMucTieu) / soDNNamThamChieu * 100).toFixed(2);
                    $('#ptsoDNNamDinhHuong').html(pt);
                } else {
                    $('#ptsoDNNamDinhHuong').html(0);
                }
                if (soVonHoaNamThamChieu != 0) {
                    var pt = (Math.abs(soVonHoaNamThamChieu - soVonHoaNamMucTieu) / soVonHoaNamThamChieu * 100).toFixed(2);
                    $('#ptsoVonHoaNamDinhHuong').html(pt);
                } else {
                    $('#ptsoVonHoaNamDinhHuong').html(0);
                }
                if (soLDNamThamChieu != 0) {
                    var pt = (Math.abs(soLDNamThamChieu - soLDNamMucTieu) / soLDNamThamChieu * 100).toFixed(2);
                    $('#ptsoLDNamDinhHuong').html(pt);
                } else {
                    $('#ptsoLDNamDinhHuong').html(0);
                }
                $('#soDNNamThamChieu').html(soDNNamThamChieu);
                $('#soDNNamMucTieu').html(soDNNamMucTieu);
                $('#soVonHoaNamThamChieu').html(soVonHoaNamThamChieu);
                $('#soVonHoaNamMucTieu').html(soVonHoaNamMucTieu);
                $('#soLDNamThamChieu').html(soLDNamThamChieu);
                $('#soLDNamMucTieu').html(soLDNamMucTieu);
                var obj2 = {
                    namMucTieu: mucTieuNam
                };
                $.ajax({
                    url: URL_APPLICATION + '/Home/GetNamMucTieu',
                    type: "POST",
                    data: obj2,
                    dataType: "json",
                    success: function (response) {
                        var chitieuSoLuong = parseInt(soDNNamMucTieu);
                        var chitieuSoVon = parseInt(soVonHoaNamMucTieu);
                        var chitieuSoLD = parseInt(soLDNamMucTieu);
                        var dadatSoLuong = response.soDNNamMucTieu;
                        var dadatSoVon = Math.round(response.soVonHoaNamMucTieu / 1000000000);
                        var dadatSoLD = response.soLDNamMucTieu;
                        $('#namHienTaiMucTieu').html(mucTieuNam);
                        $('#duLieuDinhHuongSoLuongHT').html(dadatSoLuong);
                        $('#duLieuDinhHuongSoVonHT').html(dadatSoVon);
                        $('#duLieuDinhHuongSoLDHT').html(dadatSoLD);
                        if (chitieuSoLuong != 0) {
                            var pt = (dadatSoLuong / chitieuSoLuong * 100).toFixed(2);
                            $('#ptDinhHuongSoLuongHT').html(pt);
                            if (pt < 100) {
                                $('#ttDinhHuongSoLuongHT').html("Chậm so với mục tiêu");
                            }
                            else if (pt == 100) {
                                $('#ttDinhHuongSoLuongHT').html("Đạt so với mục tiêu");
                            } else {
                                $('#ttDinhHuongSoLuongHT').html("Vượt so với mục tiêu");
                            }
                        } else {
                            $('#ptDinhHuongSoLuongHT').html(0);
                        }
                       
                        if (chitieuSoVon != 0) {
                            var pt = (dadatSoVon / chitieuSoVon * 100).toFixed(2);
                            $('#ptDinhHuongSoVonHT').html((dadatSoVon / chitieuSoVon * 100).toFixed(2));
                            if (pt < 100) {
                                $('#ttDinhHuongSoVonHT').html("Chậm so với mục tiêu");
                            }
                            else if (pt == 100) {
                                $('#ttDinhHuongSoVonHT').html("Đạt so với mục tiêu");
                            } else {
                                $('#ttDinhHuongSoVonHT').html("Vượt so với mục tiêu");
                            }
                        } else {
                            $('#ptDinhHuongSoVonHT').html(0);
                        }
                        
                        if (chitieuSoLD != 0) {
                            var pt = (dadatSoLD / chitieuSoLD * 100).toFixed(2);
                            $('#ptDinhHuongSoLDHT').html((dadatSoLD / chitieuSoLD * 100).toFixed(2));
                            if (pt < 100) {
                                $('#ttDinhHuongSoLDHT').html("Chậm so với mục tiêu");
                            }
                            else if (pt == 100) {
                                $('#ttDinhHuongSoLDHT').html("Đạt so với mục tiêu");
                            } else {
                                $('#ttDinhHuongSoLDHT').html("Vượt so với mục tiêu");
                            }
                        } else {
                            $('#ptDinhHuongSoLDHT').html(0);
                        }
                       
                        var chartDN = new CanvasJS.Chart("chartContainerDHMTDN", {
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
                                    { y: chitieuSoLuong, label: "Chỉ tiêu" },
                                    { y: dadatSoLuong, label: "Đã đạt" },
                                ]
                            }]
                        });
                        chartDN.render();
                        var chartVonDN = new CanvasJS.Chart("chartContainerDHMTVonDN", {
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
                                    { y: chitieuSoVon, label: "Chỉ tiêu" },
                                    { y: dadatSoVon, label: "Đã đạt" },
                                ]
                            }]
                        });
                        chartVonDN.render();
                        var chartSLDDN = new CanvasJS.Chart("chartContainerDHMTSLDDN", {
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
                                    { y: chitieuSoLD, label: "Chỉ tiêu" },
                                    { y: dadatSoLD, label: "Đã đạt" },
                                ]
                            }]
                        });
                        chartSLDDN.render();

                    }
                })
                
            }
        })
        
    },
}
bieuDoDinhHuongDoanhNghiepPortal.init();

