var URL_APPLICATION = $('#URL_APPLICATION').val();
Number.prototype.format = function (n, x) {
    var re = '\\d(?=(\\d{' + (x || 3) + '})+' + (n > 0 ? '\\.' : '$') + ')';
    return this.toFixed(Math.max(0, ~~n)).replace(new RegExp(re, 'g'), '$&.');
};
function toggleDataSeries(e) {
    if (typeof (e.dataSeries.visible) === "undefined" || e.dataSeries.visible) {
        e.dataSeries.visible = false;
    }
    else {
        e.dataSeries.visible = true;
    }
    chart.render();
}
var monthHT = new Date().getMonth();
var NhaDauTuPortalConfig = {
    pageSize: 7,
    pageIndex: 1,
}
$("a[href='#viewBanDoNDT']:not([href='#])").click(function () {
    let target = $(this).attr("href");
    $('html,body').stop().animate({
        scrollTop: $(target).offset().top - 50
    }, 1000);
    event.preventDefault();
});
$("a[href='#pill-1NDT']:not([href='#])").click(function () {
    setTimeout(function () {
        $('html,body').stop().animate({
            scrollTop: $("#viewBanDoNDT").offset().top - 170
        }, 1000);
    }, 1000);
});
$("a[href='#viewBanDoNDT2']:not([href='#])").click(function () {
    let target = $(this).attr("href");
    $('html,body').stop().animate({
        scrollTop: $(target).offset().top - 50
    }, 1000);
    event.preventDefault();
});
$("a[href='#pill-11NDT']:not([href='#])").click(function () {
    setTimeout(function () {
        $('html,body').stop().animate({
            scrollTop: $("#viewBanDoNDT2").offset().top - 170
        }, 1000);
    }, 1000);
});
var NhaDauTuPortal = {
    init: function () {
        NhaDauTuPortal.loadData();
        NhaDauTuPortal.registerEvents();
    },
    registerEvents: function () {

        NhaDauTuPortal.getDataChartColumnAndLine(235, 4 , "TỈNH BẾN TRE");
        $('input[type=radio][name=rdLuyKeNhaDauTu]').change(function () {
            var val = this.value;
            var id = $(".tb-parentNDT").find("tr").find("td a.active2").attr("data-id");
            var title = $(".tb-parentNDT").find("tr").find("td a.active2").attr("data-title");
            $('input[type=radio][name=rdLuyKeNhaDauTu2]').removeAttr('checked');
            $("input[type=radio][name=rdLuyKeNhaDauTu2][value=" + val + "]").attr('checked', 'checked');
            if (val == 1) {
                NhaDauTuPortal.getDataChartColumnAndLine(id, 4, title);
            }
            if (val == 2) {
                NhaDauTuPortal.getDataChartColumnAndLineVon(id, 4, title);
            }
            if (val == 3) {
                NhaDauTuPortal.getDataChartColumnAndLineSoLD(id, 4, title);
            }
        });
        $('input[type=radio][name=rdLuyKeNhaDauTu2]').change(function () {
            var val = this.value;
            var id = $(".tb-parentNDT2").find("tr").find("td a.active2").attr("data-id");
            var title = $(".tb-parentNDT2").find("tr").find("td a.active2").attr("data-title");
            $('input[type=radio][name=rdLuyKeNhaDauTu]').removeAttr('checked');
            $("input[type=radio][name=rdLuyKeNhaDauTu][value=" + val + "]").attr('checked', 'checked');
            if (val == 1) {
                NhaDauTuPortal.getDataChartColumnAndLine(id, 4, title);
            }
            if (val == 2) {
                NhaDauTuPortal.getDataChartColumnAndLineVon(id, 4, title);
            }
            if (val == 3) {
                NhaDauTuPortal.getDataChartColumnAndLineSoLD(id, 4, title);
            }
        });
        $('.viewLuyKeQuyNDT').on('click', function () {
            $(".tb-parentNDT").find("tr").find("td a.active2").removeClass('active2');
            $(this).addClass('active2');
            var id = $(this).attr("data-id");
            $('#huyenNDTBanDoLuyKe').val(id);
            var title = $(".tb-parentNDT").find("tr").find("td a.active2").attr("data-title");
            $(".tb-parentNDT2").find("tr").find("td a.active2").removeClass('active2');
            $('.viewLuyKeQuyNDT2[data-id="' + id + '"]').addClass('active2');
            var val = $("input[type=radio][name=rdLuyKeNhaDauTu]:checked").val();
            if (val == 1) {
                NhaDauTuPortal.getDataChartColumnAndLine(id, 4, title);
            }
            if (val == 2) {
                NhaDauTuPortal.getDataChartColumnAndLineVon(id, 4, title);
            }
            if (val == 3) {
                NhaDauTuPortal.getDataChartColumnAndLineSoLD(id, 4, title);
            }
        });
        $('.viewLuyKeQuyNDT2').on('click', function () {
            $(".tb-parentNDT2").find("tr").find("td a.active2").removeClass('active2');
            $(this).addClass('active2');
            var id = $(this).attr("data-id");
            $('#huyenNDTBanDoLuyKe2').val(id);
            var title = $(".tb-parentNDT2").find("tr").find("td a.active2").attr("data-title");
            $(".tb-parentNDT").find("tr").find("td a.active2").removeClass('active2');
            $('.viewLuyKeQuyNDT[data-id="' + id + '"]').addClass('active2');
            var val = $("input[type=radio][name=rdLuyKeNhaDauTu2]:checked").val();
            if (val == 1) {
                NhaDauTuPortal.getDataChartColumnAndLine(id, 4, title);
            }
            if (val == 2) {
                NhaDauTuPortal.getDataChartColumnAndLineVon(id, 4, title);
            }
            if (val == 3) {
                NhaDauTuPortal.getDataChartColumnAndLineSoLD(id, 4, title);
            }
        });

        $("body").on('click', '#tblDataIndexNDT .ViewDetail', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            NhaDauTuPortal.getThongTinNhaDauTuInfor(id);
        });

        $('#huyenNDT').on('change', function () {
            var HuyenId = $('#huyenNDT').val();
            NhaDauTuPortal.getProvinceByParent(HuyenId, 'xaNDT');
            //var HuyenNDT = $('#huyenNDTBanDoLuyKe').val();
            //if (HuyenId != HuyenNDT) {
            //    $('#huyenNDTBanDoLuyKe').val(HuyenId);
            //}
            //var HuyenNDT2 = $('#huyenNDTBanDoLuyKe2').val();
            //if (HuyenId != HuyenNDT2) {
            //    $('#huyenNDTBanDoLuyKe2').val(HuyenId);
            //}
        });
        $('#huyenNDTBanDoLuyKe').on('change', function () {
            var HuyenId = $('#huyenNDTBanDoLuyKe').val();
            var HuyenNDT2 = $('#huyenNDTBanDoLuyKe2').val();
            if (HuyenId != HuyenNDT2) {
                $('#huyenNDTBanDoLuyKe2').val(HuyenId)
            }
            $(".tb-parentNDT").find("tr").find("td a[data-id='" + HuyenId + "']").click();
        });
        $('#huyenNDTBanDoLuyKe2').on('change', function () {
            var HuyenId = $('#huyenNDTBanDoLuyKe2').val();
            var HuyenNDT2 = $('#huyenNDTBanDoLuyKe').val();
            if (HuyenId != HuyenNDT2) {
                $('#huyenNDTBanDoLuyKe').val(HuyenId);
            }
            $(".tb-parentNDT2").find("tr").find("td a[data-id='" + HuyenId + "']").click();
        });

        $('#xaNDT').on('change', function () {
            NhaDauTuPortal.loadData(true);
        });
        $('#searchNDT').on('click', function () {
            NhaDauTuPortal.loadData(true);
        });
    },
    loadData: function (changePageSize) {
        var huyenID = $('#huyenNDT').val();
        var titleNDT = $("#huyenNDT :selected").text();
        if (huyenID == 0) {
            titleNDT = "Tỉnh bến tre";
        }
        var xaID = $('#xaNDT').val();
        if (xaID != 0 && xaID != null) {
            titleNDT = $("#xaNDT :selected").text()
        }
        var searh = $('#nameNDT').val();
        var duLieu = {
            huyenID: huyenID,
            xaID: xaID,
            searh: searh
        };
        $.ajax({
            url: URL_APPLICATION + '/Home/LoadDataNhaDauTu',
            type: 'GET',
            data: {
                strSearch: JSON.stringify(duLieu),
                page: NhaDauTuPortalConfig.pageIndex,
                pageSize: NhaDauTuPortalConfig.pageSize
            },
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    var data = response.data;
                    var html = '';
                    var template = $('#data-template-index-NDT').html();
                    $.each(data, function (i, item) {
                        html += Mustache.render(template, {
                            ID: item.Id,
                            TenNDT: item.TenNhaDauTu,
                            MaST: item.MaSoNhaDauTu,
                            DaiDienPL: item.NguoiDaiDienTheoPhapLuat,
                            DiaChi: item.DiaChiTruSoChinh
                        });
                    });
                    $('#tblDataIndexNDT').html(html);
                    $('.totalNDT').html(response.total.format());
                    $('.titleNDTCSDL').html(titleNDT);
                    NhaDauTuPortal.paging(response.total, function () {
                        NhaDauTuPortal.loadData();
                    }, changePageSize);
                    NhaDauTuPortal.registerEvent();
                }
            }
        });
    },
    paging: function (totalRow, callback, changePageSize) {
        var totalPage = Math.ceil(totalRow / NhaDauTuPortalConfig.pageSize);

        //Unbind pagination if it existed or click change pagesize
        if ($('#pagination-index-NDT a').length === 0 || changePageSize) {
            $('#pagination-index-NDT').empty();
            $('#pagination-index-NDT').removeData("twbs-pagination");
            $('#pagination-index-NDT').unbind("page");
        }

        $('#pagination-index-NDT').twbsPagination({
            totalPages: totalPage,
            visiblePages: 5,
            first: '<span aria-hidden="true">&laquo;</span>',
            next: 'Tiếp',
            last: '<span aria-hidden="true">&raquo;</span>',
            prev: 'Trước',
            onPageClick: function (event, page) {
                NhaDauTuPortalConfig.pageIndex = page;
                setTimeout(callback, 200);
            }
        });
    },
    getThongTinNhaDauTuInfor: function (id) {
        $.ajax({
            url: URL_APPLICATION + '/Home/GetThongTinNhaDauTuInfor',
            type: "POST",
            data: {
                id: id
            },
            dataType: "json",
            success: function (response) {
                if (response.status == true) {
                    var data = response.data;
                    var loaiHinh = "";
                    var KhuVucHD = "";
                    if (data.LoaiHinh == "1") {
                        LoaiHinh = "Trong nước";
                    }
                    if (data.LoaiHinh == "2") {
                        LoaiHinh = "Nước ngoài";
                    }
                    if (data.KhuVucHoatDong == "1") {
                        KhuVucHD = "Trong KCN";
                    }
                    if (data.KhuVucHoatDong == "2") {
                        KhuVucHD = "Ngoài KCN";
                    }
                    var str = "";
                    str += "<div class='my-3 mx-2'>";
                    str += "<p class='mb-2'><b>Nhà đầu tư / Dự án: </b>" + data.TenNhaDauTu + "</p>";
                    str += "<p class='mb-2'><b>Số CNĐT: </b>" + data.MaSoNhaDauTu + "</p>";
                    str += "<p class='mb-2'><b>Địa điểm thực hiện dự án: </b>" + data.DiaChiTruSoChinh + "</p>";
                    str += "<p class='mb-2'><b>Đại diện pháp lý: </b>" + data.NguoiDaiDienTheoPhapLuat + "</p>";
                    str += "<p class='mb-2'><b>Ngày cấp: </b>" + response.NgayDangKy + "</p>";
                    str += "<p class='mb-2'><b>Điện thoại: </b>" + data.DienThoai + "</p>";
                    str += "<p class='mb-2'><b>Loại hình: </b>" + loaiHinh+"</p>";
                    str += "<p class='mb-2'><b>Khu vực hoạt động: </b>" + KhuVucHD+"</p>";
                    str += "</div>";
                    $('#contentDetailMoDal').html(str);
                    $('#modalViewDetail').modal();
                }
            }
        })
    },
    getProvinceByParent: function(HuyenId, XaId) {
        var url = URL_APPLICATION + '/Home/getProvinceByParent?parentid=' + HuyenId;
        $.ajax({
            url: url,
            data: "",
            type: 'GET',
            success: function (response) {
                //console.log(response);
                $("#" + XaId).html(response);
                NhaDauTuPortal.loadData(true);
            },
            error: function (err) {
                //console.log(err);
            }
        });
    },
    getDataChartColumnAndLine: function (id, loaiHinh, titleDV) {
        var HuyenId = id;
        var XaPhuongId = 0;
        var FromDate = "";
        var ToDate = "";
        var LoaiHinh = loaiHinh;
        var LinhVucId = "0";
        var LoaiHinhTitle = "";
        if (LoaiHinh == 1) {
            LoaiHinhTitle = "HỘ KINH DOANH";
        }
        else if (LoaiHinh == 2) {
            LoaiHinhTitle = "HỢP TÁC XÃ";
        }
        else if (LoaiHinh == 4) {
            LoaiHinhTitle = "DỰ ÁN ĐẦU TƯ";
        }
        else {
            LoaiHinhTitle = "DOANH NGHIỆP";
        }
        $.ajax({
            url: URL_APPLICATION + '/Home/GetDataChartColumnAndLineDN',
            type: "POST",
            data: {
                HuyenId: HuyenId,
                XaPhuongId: XaPhuongId,
                LoaiHinh: LoaiHinh,
                FromDate: FromDate,
                ToDate: ToDate,
                LinhVucId: LinhVucId
            },
            dataType: "json",
            success: function (response) {
                if (response.status == true) {
                    var dataChartColumnNDT = response.dataChartColumnDN;
                    var dataChartLineForMonthNDT = response.dataChartLineForMonthDN;
                    var dataChartColumnQuy1NDT = response.dataChartColumnQuy1DN;
                    var dataChartColumnQuy2NDT = response.dataChartColumnQuy2DN;
                    var dataChartColumnQuy3NDT = response.dataChartColumnQuy3DN;
                    var dataChartColumnQuy4NDT = response.dataChartColumnQuy4DN;

                    var dataColumnNDT = [];
                    var dataLineNDT = [];
                    var dataLineForMonthNDT = [];
                    var dataColumnQuy1NDT = [];
                    var dataColumnQuy2NDT = [];
                    var dataColumnQuy3NDT = [];
                    var dataColumnQuy4NDT = [];

                    $.each(dataChartColumnNDT, function (i, item) {
                        var obj = { y: item.amount, label: item.title };
                        dataColumnNDT.push(obj);
                        var objLine = { y: item.amount, x: new Date(item.title, 0) };
                        dataLineNDT.push(objLine);
                    });

                    $.each(dataChartLineForMonthNDT, function (i, item) {
                        if (i <= monthHT) {
                            var obj = { y: item.amount, x: i + 1, label: "Tháng " + item.title };
                            dataLineForMonthNDT.push(obj);
                        }
                    });
                    $.each(dataChartColumnQuy1NDT, function (i, item) {
                        var obj = { label: item.title, y: item.amount };
                        dataColumnQuy1NDT.push(obj);
                    });
                    $.each(dataChartColumnQuy2NDT, function (i, item) {
                        var obj = { label: item.title, y: item.amount };
                        dataColumnQuy2NDT.push(obj);
                    });
                    $.each(dataChartColumnQuy3NDT, function (i, item) {
                        var obj = { label: item.title, y: item.amount };
                        dataColumnQuy3NDT.push(obj);
                    });
                    $.each(dataChartColumnQuy4NDT, function (i, item) {
                        var obj = { label: item.title, y: item.amount };
                        dataColumnQuy4NDT.push(obj);
                    });
                    //Line Chart Theo tháng
                    $('.tilteBDNDT2').html("BIỂU ĐỒ SỐ LƯỢNG THEO THÁNG");
                    var chartLineTheoThangNDT = new CanvasJS.Chart("chartLineTheoThangNDT", {
                        animationEnabled: true,
                        theme: "light2",
                        //title: {
                        //    text: "BIỂU ĐỒ SỐ LƯỢNG THEO THÁNG " + LoaiHinhTitle,
                        //    fontSize: 16,
                        //    fontFamily: "tahoma",
                        //    fontColor: "blue"
                        //},
                        //subtitles: [
                        //    {
                        //        text: titleDV.toUpperCase(),
                        //        fontSize: 16,
                        //        fontFamily: "tahoma",
                        //        fontColor: "red"
                        //    }
                        //],
                        axisX: {
                            valueFormatString: "MM",
                            intervalType: "month"
                        },
                        axisY: {
                            title: "DỰ ÁN ĐẦU TƯ",
                            includeZero: false

                        },
                        data: [{
                            type: "line",
                            indexLabelPlacement: "outside",
                            indexLabel: "{y}",
                            xValueFormatString: "MM/YYYY",
                            dataPoints: dataLineForMonthNDT
                        }]
                    });
                    chartLineTheoThangNDT.render();
                    //Chart colunm 4 cột theo quý
                    $('.tilteBDNDT').html("BIỂU ĐỒ SỐ LƯỢNG THEO QUÝ");
                    var chartColunmTheoQuyNDT = new CanvasJS.Chart("chartColunmTheoQuyNDT", {
                        animationEnabled: true,
                        //title: {
                        //    text: "BIỂU ĐỒ SỐ LƯỢNG THEO QUÝ " + LoaiHinhTitle,
                        //    fontSize: 16,
                        //    fontFamily: "tahoma",
                        //    fontColor: "blue"
                        //},
                        //subtitles: [
                        //    {
                        //        text: titleDV.toUpperCase(),
                        //        fontSize: 16,
                        //        fontFamily: "tahoma",
                        //        fontColor: "red"
                        //    }
                        //],
                        axisY: {
                            title: "DỰ ÁN ĐẦU TƯ",
                            includeZero: false

                        },
                        toolTip: {
                            shared: true
                        },
                        legend: {
                            cursor: "pointer",
                            itemclick: toggleDataSeries
                        },
                        data: [
                            {
                                type: "column",
                                indexLabelPlacement: "outside",
                                indexLabel: "{y}",
                                name: "Qúy 1",
                                legendText: "Qúy 1",
                                showInLegend: true,
                                dataPoints: dataColumnQuy1NDT
                            },
                            {
                                type: "column",
                                indexLabelPlacement: "outside",
                                indexLabel: "{y}",
                                name: "Qúy 2",
                                legendText: "Qúy 2",
                                showInLegend: true,
                                dataPoints: dataColumnQuy2NDT

                            },
                            {
                                type: "column",
                                indexLabelPlacement: "outside",
                                indexLabel: "{y}",
                                name: "Qúy 3",
                                legendText: "Qúy 3",
                                showInLegend: true,
                                dataPoints: dataColumnQuy3NDT

                            }
                            ,
                            {
                                type: "column",
                                indexLabelPlacement: "outside",
                                indexLabel: "{y}",
                                name: "Qúy 4",
                                legendText: "Qúy 4",
                                showInLegend: true,
                                dataPoints: dataColumnQuy4NDT

                            }
                        ]
                    });
                    chartColunmTheoQuyNDT.render();
                    //End chart colunm 4 cột theo quý
                }
            }
        })
    },
    getDataChartColumnAndLineVon: function (id, loaiHinh, titleDV) {
        var HuyenId = id;
        var XaPhuongId = 0;
        var FromDate = "";
        var ToDate = "";
        var LoaiHinh = loaiHinh;
        var LinhVucId = "0";
        var LoaiHinhTitle = "";
        if (LoaiHinh == 1) {
            LoaiHinhTitle = "HỘ KINH DOANH";
        }
        else if (LoaiHinh == 2) {
            LoaiHinhTitle = "HỢP TÁC XÃ";
        }
        else if (LoaiHinh == 4) {
            LoaiHinhTitle = "DỰ ÁN ĐẦU TƯ";
        }
        else {
            LoaiHinhTitle = "DOANH NGHIỆP";
        }
        $.ajax({
            url: URL_APPLICATION + '/Home/GetDataChartColumnAndLineVonDN',
            type: "POST",
            data: {
                HuyenId: HuyenId,
                XaPhuongId: XaPhuongId,
                LoaiHinh: LoaiHinh,
                FromDate: FromDate,
                ToDate: ToDate,
                LinhVucId: LinhVucId
            },
            dataType: "json",
            success: function (response) {
                if (response.status == true) {
                    var dataChartColumnNDT = response.dataChartColumnDN;
                    var dataChartLineForMonthNDT = response.dataChartLineForMonthDN;
                    var dataChartColumnQuy1NDT = response.dataChartColumnQuy1DN;
                    var dataChartColumnQuy2NDT = response.dataChartColumnQuy2DN;
                    var dataChartColumnQuy3NDT = response.dataChartColumnQuy3DN;
                    var dataChartColumnQuy4NDT = response.dataChartColumnQuy4DN;

                    var dataColumnNDT = [];
                    var dataLineNDT = [];
                    var dataLineForMonthNDT = [];
                    var dataColumnQuy1NDT = [];
                    var dataColumnQuy2NDT = [];
                    var dataColumnQuy3NDT = [];
                    var dataColumnQuy4NDT = [];

                    $.each(dataChartColumnNDT, function (i, item) {
                        var obj = { y: Math.round(item.amount / 1000000000), label: item.title };
                        dataColumnNDT.push(obj);
                        var objLine = { y: Math.round(item.amount / 1000000000), x: new Date(item.title, 0) };
                        dataLineNDT.push(objLine);
                    });

                    $.each(dataChartLineForMonthNDT, function (i, item) {
                        if (i <= monthHT) {
                            var obj = { y: Math.round(item.amount / 1000000000), x: i + 1, label: "Tháng " + item.title };
                            dataLineForMonthNDT.push(obj);
                        }
                    });

                    $.each(dataChartColumnQuy1NDT, function (i, item) {
                        var obj = { label: item.title, y: Math.round(item.amount / 1000000000) };
                        dataColumnQuy1NDT.push(obj);
                    });
                    $.each(dataChartColumnQuy2NDT, function (i, item) {
                        var obj = { label: item.title, y: Math.round(item.amount / 1000000000) };
                        dataColumnQuy2NDT.push(obj);
                    });
                    $.each(dataChartColumnQuy3NDT, function (i, item) {
                        var obj = { label: item.title, y: Math.round(item.amount / 1000000000) };
                        dataColumnQuy3NDT.push(obj);
                    });
                    $.each(dataChartColumnQuy4NDT, function (i, item) {
                        var obj = { label: item.title, y: Math.round(item.amount / 1000000000) };
                        dataColumnQuy4NDT.push(obj);
                    });
                    //Line Chart Theo tháng
                    $('.tilteBDNDT2').html("BIỂU ĐỒ VỐN THEO THÁNG");
                    var chartLineTheoThangNDT = new CanvasJS.Chart("chartLineTheoThangNDT", {
                        animationEnabled: true,
                        theme: "light2",
                        //title: {
                        //    text: "BIỂU ĐỒ VỐN THEO THÁNG " + LoaiHinhTitle,
                        //    fontSize: 16,
                        //    fontFamily: "tahoma",
                        //    fontColor: "blue"
                        //},
                        //subtitles: [
                        //    {
                        //        text: titleDV.toUpperCase(),
                        //        fontSize: 16,
                        //        fontFamily: "tahoma",
                        //        fontColor: "red"
                        //    }
                        //],
                        axisX: {
                            valueFormatString: "MM",
                            intervalType: "month"
                        },
                        axisY: {
                            title: "Tỷ đồng",
                            includeZero: false

                        },
                        data: [{
                            type: "line",
                            indexLabelPlacement: "outside",
                            indexLabel: "{y}",
                            xValueFormatString: "MM/YYYY",
                            dataPoints: dataLineForMonthNDT
                        }]
                    });
                    chartLineTheoThangNDT.render();
                    //Chart colunm 4 cột theo quý
                    $('.tilteBDNDT').html("BIỂU ĐỒ VỐN THEO QUÝ");
                    var chartColunmTheoQuyNDT = new CanvasJS.Chart("chartColunmTheoQuyNDT", {
                        animationEnabled: true,
                        //title: {
                        //    text: "BIỂU ĐỒ VỐN THEO QUÝ " + LoaiHinhTitle,
                        //    fontSize: 16,
                        //    fontFamily: "tahoma",
                        //    fontColor: "blue"
                        //},
                        //subtitles: [
                        //    {
                        //        text: titleDV.toUpperCase(),
                        //        fontSize: 16,
                        //        fontFamily: "tahoma",
                        //        fontColor: "red"
                        //    }
                        //],
                        axisY: {
                            title: "Tỷ đồng",
                            includeZero: false

                        },
                        toolTip: {
                            shared: true
                        },
                        legend: {
                            cursor: "pointer",
                            itemclick: toggleDataSeries
                        },
                        data: [
                            {
                                type: "column",
                                indexLabelPlacement: "outside",
                                indexLabel: "{y}",
                                name: "Qúy 1",
                                legendText: "Qúy 1",
                                showInLegend: true,
                                dataPoints: dataColumnQuy1NDT
                            },
                            {
                                type: "column",
                                indexLabelPlacement: "outside",
                                indexLabel: "{y}",
                                name: "Qúy 2",
                                legendText: "Qúy 2",
                                showInLegend: true,
                                dataPoints: dataColumnQuy2NDT

                            },
                            {
                                type: "column",
                                indexLabelPlacement: "outside",
                                indexLabel: "{y}",
                                name: "Qúy 3",
                                legendText: "Qúy 3",
                                showInLegend: true,
                                dataPoints: dataColumnQuy3NDT

                            }
                            ,
                            {
                                type: "column",
                                indexLabelPlacement: "outside",
                                indexLabel: "{y}",
                                name: "Qúy 4",
                                legendText: "Qúy 4",
                                showInLegend: true,
                                dataPoints: dataColumnQuy4NDT

                            }
                        ]
                    });
                    chartColunmTheoQuyNDT.render();

                    //End chart colunm 4 cột theo quý
                }
            }
        })
    },
    getDataChartColumnAndLineSoLD: function (id, loaiHinh, titleDV) {
        var HuyenId = id;
        var XaPhuongId = 0;
        var FromDate = "";
        var ToDate = "";
        var LoaiHinh = loaiHinh;
        var LinhVucId = "0";
        var LoaiHinhTitle = "";
        if (LoaiHinh == 1) {
            LoaiHinhTitle = "HỘ KINH DOANH";
        }
        else if (LoaiHinh == 2) {
            LoaiHinhTitle = "HỢP TÁC XÃ";
        }
        else if (LoaiHinh == 4) {
            LoaiHinhTitle = "DỰ ÁN ĐẦU TƯ";
        }
        else {
            LoaiHinhTitle = "DOANH NGHIỆP";
        }
        $.ajax({
            url: URL_APPLICATION + '/Home/GetDataChartColumnAndLineSoLD',
            type: "POST",
            data: {
                HuyenId: HuyenId,
                XaPhuongId: XaPhuongId,
                LoaiHinh: LoaiHinh,
                FromDate: FromDate,
                ToDate: ToDate,
                LinhVucId: LinhVucId
            },
            dataType: "json",
            success: function (response) {
                if (response.status == true) {
                    var dataChartColumnNDT = response.dataChartColumnNDT;
                    var dataChartLineForMonthNDT = response.dataChartLineForMonthDN;
                    var dataChartColumnQuy1NDT = response.dataChartColumnQuy1DN;
                    var dataChartColumnQuy2NDT = response.dataChartColumnQuy2DN;
                    var dataChartColumnQuy3NDT = response.dataChartColumnQuy3DN;
                    var dataChartColumnQuy4NDT = response.dataChartColumnQuy4DN;

                    var dataColumnNDT = [];
                    var dataLineNDT = [];
                    var dataLineForMonthNDT = [];
                    var dataColumnQuy1NDT = [];
                    var dataColumnQuy2NDT = [];
                    var dataColumnQuy3NDT = [];
                    var dataColumnQuy4NDT = [];

                    $.each(dataChartColumnNDT, function (i, item) {
                        var obj = { y: item.amount, label: item.title };
                        dataColumnNDT.push(obj);
                        var objLine = { y: item.amount, x: new Date(item.title, 0) };
                        dataLineNDT.push(objLine);
                    });

                    $.each(dataChartLineForMonthNDT, function (i, item) {
                        if (i <= monthHT) {
                            var obj = { y: item.amount, x: i + 1, label: "Tháng " + item.title };
                            dataLineForMonthNDT.push(obj);
                        }
                    });

                    $.each(dataChartColumnQuy1NDT, function (i, item) {
                        var obj = { label: item.title, y: item.amount };
                        dataColumnQuy1NDT.push(obj);
                    });
                    $.each(dataChartColumnQuy2NDT, function (i, item) {
                        var obj = { label: item.title, y: item.amount };
                        dataColumnQuy2NDT.push(obj);
                    });
                    $.each(dataChartColumnQuy3NDT, function (i, item) {
                        var obj = { label: item.title, y: item.amount };
                        dataColumnQuy3NDT.push(obj);
                    });
                    $.each(dataChartColumnQuy4NDT, function (i, item) {
                        var obj = { label: item.title, y: item.amount };
                        dataColumnQuy4NDT.push(obj);
                    });
                    //Line Chart Theo tháng
                    $('.tilteBDNDT2').html("BIỂU ĐỒ SỐ LAO ĐỘNG THEO THÁNG");
                    var chartLineTheoThangNDT = new CanvasJS.Chart("chartLineTheoThangNDT", {
                        animationEnabled: true,
                        theme: "light2",
                        //title: {
                        //    text: "BIỂU ĐỒ SỐ LAO ĐỘNG THEO THÁNG " + LoaiHinhTitle,
                        //    fontSize: 16,
                        //    fontFamily: "tahoma",
                        //    fontColor: "blue"
                        //},
                        //subtitles: [
                        //    {
                        //        text: titleDV.toUpperCase(),
                        //        fontSize: 16,
                        //        fontFamily: "tahoma",
                        //        fontColor: "red"
                        //    }
                        //],
                        axisX: {
                            valueFormatString: "MM",
                            intervalType: "month"
                        },
                        axisY: {
                            title: "Người",
                            includeZero: false

                        },
                        data: [{
                            type: "line",
                            indexLabelPlacement: "outside",
                            indexLabel: "{y}",
                            xValueFormatString: "MM/YYYY",
                            dataPoints: dataLineForMonthNDT
                        }]
                    });
                    chartLineTheoThangNDT.render();
                    //Chart colunm 4 cột theo quý
                    $('.tilteBDNDT').html("BIỂU ĐỒ SỐ LAO ĐỘNG THEO QUÝ");
                    var chartColunmTheoQuyNDT = new CanvasJS.Chart("chartColunmTheoQuyNDT", {
                        animationEnabled: true,
                        //title: {
                        //    text: "BIỂU ĐỒ SỐ LAO ĐỘNG THEO QUÝ " + LoaiHinhTitle,
                        //    fontSize: 16,
                        //    fontFamily: "tahoma",
                        //    fontColor: "blue"
                        //},
                        //subtitles: [
                        //    {
                        //        text: titleDV.toUpperCase(),
                        //        fontSize: 16,
                        //        fontFamily: "tahoma",
                        //        fontColor: "red"
                        //    }
                        //],
                        axisY: {
                            title: "Người",
                            includeZero: false

                        },
                        toolTip: {
                            shared: true
                        },
                        legend: {
                            cursor: "pointer",
                            itemclick: toggleDataSeries
                        },
                        data: [
                            {
                                type: "column",
                                indexLabelPlacement: "outside",
                                indexLabel: "{y}",
                                name: "Qúy 1",
                                legendText: "Qúy 1",
                                showInLegend: true,
                                dataPoints: dataColumnQuy1NDT
                            },
                            {
                                type: "column",
                                indexLabelPlacement: "outside",
                                indexLabel: "{y}",
                                name: "Qúy 2",
                                legendText: "Qúy 2",
                                showInLegend: true,
                                dataPoints: dataColumnQuy2NDT

                            },
                            {
                                type: "column",
                                indexLabelPlacement: "outside",
                                indexLabel: "{y}",
                                name: "Qúy 3",
                                legendText: "Qúy 3",
                                showInLegend: true,
                                dataPoints: dataColumnQuy3NDT

                            }
                            ,
                            {
                                type: "column",
                                indexLabelPlacement: "outside",
                                indexLabel: "{y}",
                                name: "Qúy 4",
                                legendText: "Qúy 4",
                                showInLegend: true,
                                dataPoints: dataColumnQuy4NDT

                            }
                        ]
                    });
                    chartColunmTheoQuyNDT.render();

                    //End chart colunm 4 cột theo quý
                }
            }
        })
    } 
}
NhaDauTuPortal.init();

