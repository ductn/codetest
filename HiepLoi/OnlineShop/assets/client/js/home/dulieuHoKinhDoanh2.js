var URL_APPLICATION = $('#URL_APPLICATION').val();
var HoKinhDoanhPortalConfig = {
    pageSize: 7,
    pageIndex: 1,
}
$("a[href='#viewBanDoHKD']:not([href='#])").click(function () {
    let target = $(this).attr("href");
    $('html,body').stop().animate({
        scrollTop: $(target).offset().top - 50
    }, 1000);
    event.preventDefault();
});
$("a[href='#pill-1HKT']:not([href='#])").click(function () {
    setTimeout(function () {
        $('html,body').stop().animate({
            scrollTop: $("#viewBanDoHKD").offset().top - 170
        }, 1000);
    }, 1000);
});
$("a[href='#viewBanDoHKD2']:not([href='#])").click(function () {
    let target = $(this).attr("href");
    $('html,body').stop().animate({
        scrollTop: $(target).offset().top - 50
    }, 1000);
    event.preventDefault();
});
$("a[href='#pill-11HKT']:not([href='#])").click(function () {
    setTimeout(function () {
        $('html,body').stop().animate({
            scrollTop: $("#viewBanDoHKD2").offset().top - 170
        }, 1000);
    }, 1000);
});
var monthHT = new Date().getMonth();
var HoKinhDoanhPortal = {
    init: function () {
        HoKinhDoanhPortal.loadData();
        HoKinhDoanhPortal.registerEvents();
    },
    registerEvents: function () {

        HoKinhDoanhPortal.getDataChartColumnAndLine(235, 1, "TỈNH BẾN TRE");
        $('input[type=radio][name=rdLuyKeHoKinhDoanh]').change(function () {
            var val = this.value;
            var id = $(".tb-parentHKD").find("tr").find("td a.active2").attr("data-id");
            var title = $(".tb-parentHKD").find("tr").find("td a.active2").attr("data-title");
            $('input[type=radio][name=rdLuyKeHoKinhDoanh2]').removeAttr('checked');
            $("input[type=radio][name=rdLuyKeHoKinhDoanh2][value=" + val + "]").attr('checked', 'checked');
            if (val == 1) {
                HoKinhDoanhPortal.getDataChartColumnAndLine(id, 1, title);
            }
            if (val == 2) {
                HoKinhDoanhPortal.getDataChartColumnAndLineVon(id, 1, title);
            }
            if (val == 3) {
                HoKinhDoanhPortal.getDataChartColumnAndLineSoLD(id, 1, title);
            }
        });
        $('input[type=radio][name=rdLuyKeHoKinhDoanh2]').change(function () {
            var val = this.value;
            var id = $(".tb-parentHKD2").find("tr").find("td a.active2").attr("data-id");
            var title = $(".tb-parentHKD2").find("tr").find("td a.active2").attr("data-title");
            $('input[type=radio][name=rdLuyKeHoKinhDoanh]').removeAttr('checked');
            $("input[type=radio][name=rdLuyKeHoKinhDoanh][value=" + val + "]").attr('checked', 'checked');
            if (val == 1) {
                HoKinhDoanhPortal.getDataChartColumnAndLine(id, 1, title);
            }
            if (val == 2) {
                HoKinhDoanhPortal.getDataChartColumnAndLineVon(id, 1, title);
            }
            if (val == 3) {
                HoKinhDoanhPortal.getDataChartColumnAndLineSoLD(id, 1, title);
            }
        });
        $('.viewLuyKeQuyHKD').on('click', function () {
            $(".tb-parentHKD").find("tr").find("td a.active2").removeClass('active2');
            $(this).addClass('active2');
            var id = $(this).attr("data-id");
            $('#huyenHKDBanDoLuyKe').val(id);
            var title = $(".tb-parentHKD").find("tr").find("td a.active2").attr("data-title");
            $(".tb-parentHKD2").find("tr").find("td a.active2").removeClass('active2');
            $('.viewLuyKeQuyHKD2[data-id="' + id + '"]').addClass('active2');
            var val = $("input[type=radio][name=rdLuyKeHoKinhDoanh]:checked").val();
            if (val == 1) {
                HoKinhDoanhPortal.getDataChartColumnAndLine(id, 1, title);
            }
            if (val == 2) {
                HoKinhDoanhPortal.getDataChartColumnAndLineVon(id, 1, title);
            }
            if (val == 3) {
                HoKinhDoanhPortal.getDataChartColumnAndLineSoLD(id, 1, title);
            }
        });
        $('.viewLuyKeQuyHKD2').on('click', function () {
            $(".tb-parentHKD2").find("tr").find("td a.active2").removeClass('active2');
            $(this).addClass('active2');
            var id = $(this).attr("data-id");
            $('#huyenHKDBanDoLuyKe2').val(id);
            var title = $(".tb-parentHKD2").find("tr").find("td a.active2").attr("data-title");
            $(".tb-parentHKD").find("tr").find("td a.active2").removeClass('active2');
            $('.viewLuyKeQuyHKD[data-id="' + id + '"]').addClass('active2');
            var val = $("input[type=radio][name=rdLuyKeHoKinhDoanh2]:checked").val();
            if (val == 1) {
                HoKinhDoanhPortal.getDataChartColumnAndLine(id, 1, title);
            }
            if (val == 2) {
                HoKinhDoanhPortal.getDataChartColumnAndLineVon(id, 1, title);
            }
            if (val == 3) {
                HoKinhDoanhPortal.getDataChartColumnAndLineSoLD(id, 1, title);
            }
        });

        $("body").on('click', '#tblDataIndexHKD .ViewDetail', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            HoKinhDoanhPortal.getThongTinHoKinhDoanhInfor(id);
        });

        $('#huyenHKD').on('change', function () {
            var HuyenId = $('#huyenHKD').val();
            HoKinhDoanhPortal.getProvinceByParent(HuyenId, 'xaHKD');
            //var HuyenHKD = $('#huyenHKDBanDoLuyKe').val();
            //if (HuyenId != HuyenHKD) {
            //    $('#huyenHKDBanDoLuyKe').val(HuyenId);
            //}
            //var HuyenHKD2 = $('#huyenHKDBanDoLuyKe2').val();
            //if (HuyenId != HuyenHKD2) {
            //    $('#huyenHKDBanDoLuyKe2').val(HuyenId);
            //}
        });
        $('#huyenHKDBanDoLuyKe').on('change', function () {
            var HuyenId = $('#huyenHKDBanDoLuyKe').val();
            var HuyenHKD2 = $('#huyenHKDBanDoLuyKe2').val();
            if (HuyenId != HuyenHKD2) {
                $('#huyenHKDBanDoLuyKe2').val(HuyenId)
            }
            $(".tb-parentHKD").find("tr").find("td a[data-id='" + HuyenId + "']").click();
        });
        $('#huyenHKDBanDoLuyKe2').on('change', function () {
            var HuyenId = $('#huyenHKDBanDoLuyKe2').val();
            var HuyenHKD2 = $('#huyenHKDBanDoLuyKe').val();
            if (HuyenId != HuyenHKD2) {
                $('#huyenHKDBanDoLuyKe').val(HuyenId);
            }
            $(".tb-parentHKD2").find("tr").find("td a[data-id='" + HuyenId + "']").click();
        });
        $('#xaHKD').on('change', function () {
            HoKinhDoanhPortal.loadData(true);
        });
        $('#searchHKD').on('click', function () {
            HoKinhDoanhPortal.loadData(true);
        });
    },
    loadData: function (changePageSize) {
        var huyenID = $('#huyenHKD').val();
        var titleHKD = $("#huyenHKD :selected").text();
        if (huyenID == 0) {
            titleHKD = "Tỉnh bến tre";
        }
        var xaID = $('#xaHKD').val();
        if (xaID != 0 && xaID != null) {
            titleDN = $("#xaDN :selected").text()
        }
        var searh = $('#nameHKD').val();
        var url = new URL(window.location.href);
        var c = url.searchParams.get("searchString");
        if (c != null && c != "") {
            searh = c;
        }
        var duLieu = {
            huyenID: huyenID,
            xaID: xaID,
            searh: searh
        };
        $.ajax({
            url: URL_APPLICATION + '/Home/LoadDataHoKinhDoanh',
            type: 'GET',
            data: {
                strSearch: JSON.stringify(duLieu),
                page: HoKinhDoanhPortalConfig.pageIndex,
                pageSize: HoKinhDoanhPortalConfig.pageSize
            },
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    var data = response.data;
                    var html = '';
                    var template = $('#data-template-index-HKD').html();
                    $.each(data, function (i, item) {
                        html += Mustache.render(template, {
                            ID: item.CertifiedID,
                            TenHKD: item.Biz_VietName,
                            MaST: item.CertifiedCode,
                            DaiDienPL: item.Ow_Name,
                            DiaChi: item.Biz_HeadOffice
                        });
                    });
                    $('#tblDataIndexHKD').html(html);
                    $('.totalHKD').html(response.total.format());
                    $('.titleHKDCSDL').html(titleHKD);
                    HoKinhDoanhPortal.paging(response.total, function () {
                        HoKinhDoanhPortal.loadData();
                    }, changePageSize);
                    HoKinhDoanhPortal.registerEvent();
                }
            }
        });
    },
    paging: function (totalRow, callback, changePageSize) {
        var totalPage = Math.ceil(totalRow / HoKinhDoanhPortalConfig.pageSize);

        //Unbind pagination if it existed or click change pagesize
        if ($('#pagination-index-HKD a').length === 0 || changePageSize) {
            $('#pagination-index-HKD').empty();
            $('#pagination-index-HKD').removeData("twbs-pagination");
            $('#pagination-index-HKD').unbind("page");
        }

        $('#pagination-index-HKD').twbsPagination({
            totalPages: totalPage,
            visiblePages: 5,
            first: '<span aria-hidden="true">&laquo;</span>',
            next: 'Tiếp',
            last: '<span aria-hidden="true">&raquo;</span>',
            prev: 'Trước',
            onPageClick: function (event, page) {
                HoKinhDoanhPortalConfig.pageIndex = page;
                setTimeout(callback, 200);
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
    getProvinceByParent: function (HuyenId, XaId) {
        var url = URL_APPLICATION + '/Home/getProvinceByParent?parentid=' + HuyenId;
        $.ajax({
            url: url,
            data: "",
            type: 'GET',
            success: function (response) {
                //console.log(response);
                $("#" + XaId).html(response);
                HoKinhDoanhPortal.loadData(true);
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
                    var dataChartColumnDN = response.dataChartColumnDN;
                    var dataChartLineForMonthDN = response.dataChartLineForMonthDN;
                    var dataChartColumnQuy1DN = response.dataChartColumnQuy1DN;
                    var dataChartColumnQuy2DN = response.dataChartColumnQuy2DN;
                    var dataChartColumnQuy3DN = response.dataChartColumnQuy3DN;
                    var dataChartColumnQuy4DN = response.dataChartColumnQuy4DN;

                    var dataColumnDN = [];
                    var dataLineDN = [];
                    var dataLineForMonthDN = [];
                    var dataColumnQuy1DN = [];
                    var dataColumnQuy2DN = [];
                    var dataColumnQuy3DN = [];
                    var dataColumnQuy4DN = [];

                    $.each(dataChartColumnDN, function (i, item) {
                        var obj = { y: item.amount, label: item.title };
                        dataColumnDN.push(obj);
                        var objLine = { y: item.amount, x: new Date(item.title, 0) };
                        dataLineDN.push(objLine);
                    });

                    $.each(dataChartLineForMonthDN, function (i, item) {
                        if (i <= monthHT) {
                            var obj = { y: item.amount, x: i + 1, label: "Tháng " + item.title };
                            dataLineForMonthDN.push(obj);
                        }
                    });

                    $.each(dataChartColumnQuy1DN, function (i, item) {
                        var obj = { label: item.title, y: item.amount };
                        dataColumnQuy1DN.push(obj);
                    });
                    $.each(dataChartColumnQuy2DN, function (i, item) {
                        var obj = { label: item.title, y: item.amount };
                        dataColumnQuy2DN.push(obj);
                    });
                    $.each(dataChartColumnQuy3DN, function (i, item) {
                        var obj = { label: item.title, y: item.amount };
                        dataColumnQuy3DN.push(obj);
                    });
                    $.each(dataChartColumnQuy4DN, function (i, item) {
                        var obj = { label: item.title, y: item.amount };
                        dataColumnQuy4DN.push(obj);
                    });
                    //Line Chart Theo tháng
                    $('.tilteBDHKD2').html("BIỂU ĐỒ SỐ LƯỢNG THEO THÁNG");
                    var chartLineTheoThangHKD = new CanvasJS.Chart("chartLineTheoThangHKD", {
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
                            title: "Hộ kinh doanh",
                            includeZero: false

                        },
                        data: [{
                            type: "line",
                            indexLabelPlacement: "outside",
                            indexLabel: "{y}",
                            xValueFormatString: "MM/YYYY",
                            dataPoints: dataLineForMonthDN
                        }]
                    });
                    chartLineTheoThangHKD.render();
                    //Chart colunm 4 cột theo quý
                    $('.tilteBDHKD').html("BIỂU ĐỒ SỐ LƯỢNG THEO QUÝ");
                    var chartColunmTheoQuyHKD = new CanvasJS.Chart("chartColunmTheoQuyHKD", {
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
                            title: "Hộ kinh doanh",
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
                                dataPoints: dataColumnQuy1DN
                            },
                            {
                                type: "column",
                                indexLabelPlacement: "outside",
                                indexLabel: "{y}",
                                name: "Qúy 2",
                                legendText: "Qúy 2",
                                showInLegend: true,
                                dataPoints: dataColumnQuy2DN

                            },
                            {
                                type: "column",
                                indexLabelPlacement: "outside",
                                indexLabel: "{y}",
                                name: "Qúy 3",
                                legendText: "Qúy 3",
                                showInLegend: true,
                                dataPoints: dataColumnQuy3DN

                            }
                            ,
                            {
                                type: "column",
                                indexLabelPlacement: "outside",
                                indexLabel: "{y}",
                                name: "Qúy 4",
                                legendText: "Qúy 4",
                                showInLegend: true,
                                dataPoints: dataColumnQuy4DN

                            }
                        ]
                    });
                    chartColunmTheoQuyHKD.render();
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
                    var dataChartColumnDN = response.dataChartColumnDN;
                    var dataChartLineForMonthDN = response.dataChartLineForMonthDN;
                    var dataChartColumnQuy1DN = response.dataChartColumnQuy1DN;
                    var dataChartColumnQuy2DN = response.dataChartColumnQuy2DN;
                    var dataChartColumnQuy3DN = response.dataChartColumnQuy3DN;
                    var dataChartColumnQuy4DN = response.dataChartColumnQuy4DN;

                    var dataColumnDN = [];
                    var dataLineDN = [];
                    var dataLineForMonthDN = [];
                    var dataColumnQuy1DN = [];
                    var dataColumnQuy2DN = [];
                    var dataColumnQuy3DN = [];
                    var dataColumnQuy4DN = [];

                    $.each(dataChartColumnDN, function (i, item) {
                        var obj = { y: Math.round(item.amount / 1000000000), label: item.title };
                        dataColumnDN.push(obj);
                        var objLine = { y: Math.round(item.amount / 1000000000), x: new Date(item.title, 0) };
                        dataLineDN.push(objLine);
                    });

                    $.each(dataChartLineForMonthDN, function (i, item) {
                        if (i <= monthHT) {
                            var obj = { y: Math.round(item.amount / 1000000000), x: i + 1, label: "Tháng " + item.title };
                            dataLineForMonthDN.push(obj);
                        }
                    });

                    $.each(dataChartColumnQuy1DN, function (i, item) {
                        var obj = { label: item.title, y: Math.round(item.amount / 1000000000) };
                        dataColumnQuy1DN.push(obj);
                    });
                    $.each(dataChartColumnQuy2DN, function (i, item) {
                        var obj = { label: item.title, y: Math.round(item.amount / 1000000000) };
                        dataColumnQuy2DN.push(obj);
                    });
                    $.each(dataChartColumnQuy3DN, function (i, item) {
                        var obj = { label: item.title, y: Math.round(item.amount / 1000000000) };
                        dataColumnQuy3DN.push(obj);
                    });
                    $.each(dataChartColumnQuy4DN, function (i, item) {
                        var obj = { label: item.title, y: Math.round(item.amount / 1000000000) };
                        dataColumnQuy4DN.push(obj);
                    });
                    //Line Chart Theo tháng
                    $('.tilteBDHKD2').html("BIỂU ĐỒ VỐN THEO THÁNG");
                    var chartLineTheoThangHKD = new CanvasJS.Chart("chartLineTheoThangHKD", {
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
                            dataPoints: dataLineForMonthDN
                        }]
                    });
                    chartLineTheoThangHKD.render();
                    //Chart colunm 4 cột theo quý
                    $('.tilteBDHKD').html("BIỂU ĐỒ VỐN THEO QUÝ");
                    var chartColunmTheoQuyHKD = new CanvasJS.Chart("chartColunmTheoQuyHKD", {
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
                                dataPoints: dataColumnQuy1DN
                            },
                            {
                                type: "column",
                                indexLabelPlacement: "outside",
                                indexLabel: "{y}",
                                name: "Qúy 2",
                                legendText: "Qúy 2",
                                showInLegend: true,
                                dataPoints: dataColumnQuy2DN

                            },
                            {
                                type: "column",
                                indexLabelPlacement: "outside",
                                indexLabel: "{y}",
                                name: "Qúy 3",
                                legendText: "Qúy 3",
                                showInLegend: true,
                                dataPoints: dataColumnQuy3DN

                            }
                            ,
                            {
                                type: "column",
                                indexLabelPlacement: "outside",
                                indexLabel: "{y}",
                                name: "Qúy 4",
                                legendText: "Qúy 4",
                                showInLegend: true,
                                dataPoints: dataColumnQuy4DN

                            }
                        ]
                    });
                    chartColunmTheoQuyHKD.render();

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
                    var dataChartColumnDN = response.dataChartColumnDN;
                    var dataChartLineForMonthDN = response.dataChartLineForMonthDN;
                    var dataChartColumnQuy1DN = response.dataChartColumnQuy1DN;
                    var dataChartColumnQuy2DN = response.dataChartColumnQuy2DN;
                    var dataChartColumnQuy3DN = response.dataChartColumnQuy3DN;
                    var dataChartColumnQuy4DN = response.dataChartColumnQuy4DN;

                    var dataColumnDN = [];
                    var dataLineDN = [];
                    var dataLineForMonthDN = [];
                    var dataColumnQuy1DN = [];
                    var dataColumnQuy2DN = [];
                    var dataColumnQuy3DN = [];
                    var dataColumnQuy4DN = [];

                    $.each(dataChartColumnDN, function (i, item) {
                        var obj = { y: item.amount, label: item.title };
                        dataColumnDN.push(obj);
                        var objLine = { y: item.amount, x: new Date(item.title, 0) };
                        dataLineDN.push(objLine);
                    });

                    $.each(dataChartLineForMonthDN, function (i, item) {
                        if (i <= monthHT) {
                            var obj = { y: item.amount, x: i + 1, label: "Tháng " + item.title };
                            dataLineForMonthDN.push(obj);
                        }
                    });

                    $.each(dataChartColumnQuy1DN, function (i, item) {
                        var obj = { label: item.title, y: item.amount };
                        dataColumnQuy1DN.push(obj);
                    });
                    $.each(dataChartColumnQuy2DN, function (i, item) {
                        var obj = { label: item.title, y: item.amount };
                        dataColumnQuy2DN.push(obj);
                    });
                    $.each(dataChartColumnQuy3DN, function (i, item) {
                        var obj = { label: item.title, y: item.amount };
                        dataColumnQuy3DN.push(obj);
                    });
                    $.each(dataChartColumnQuy4DN, function (i, item) {
                        var obj = { label: item.title, y: item.amount };
                        dataColumnQuy4DN.push(obj);
                    });
                    //Line Chart Theo tháng
                    $('.tilteBDHKD2').html("BIỂU ĐỒ SỐ LAO ĐỘNG THEO THÁNG");
                    var chartLineTheoThangHKD = new CanvasJS.Chart("chartLineTheoThangHKD", {
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
                            dataPoints: dataLineForMonthDN
                        }]
                    });
                    chartLineTheoThangHKD.render();
                    //Chart colunm 4 cột theo quý
                    $('.tilteBDHKD').html("BIỂU ĐỒ SỐ LAO ĐỘNG THEO QUÝ");
                    var chartColunmTheoQuyHKD = new CanvasJS.Chart("chartColunmTheoQuyHKD", {
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
                                dataPoints: dataColumnQuy1DN
                            },
                            {
                                type: "column",
                                indexLabelPlacement: "outside",
                                indexLabel: "{y}",
                                name: "Qúy 2",
                                legendText: "Qúy 2",
                                showInLegend: true,
                                dataPoints: dataColumnQuy2DN

                            },
                            {
                                type: "column",
                                indexLabelPlacement: "outside",
                                indexLabel: "{y}",
                                name: "Qúy 3",
                                legendText: "Qúy 3",
                                showInLegend: true,
                                dataPoints: dataColumnQuy3DN

                            }
                            ,
                            {
                                type: "column",
                                indexLabelPlacement: "outside",
                                indexLabel: "{y}",
                                name: "Qúy 4",
                                legendText: "Qúy 4",
                                showInLegend: true,
                                dataPoints: dataColumnQuy4DN

                            }
                        ]
                    });
                    chartColunmTheoQuyHKD.render();

                    //End chart colunm 4 cột theo quý
                }
            }
        })
    } 
}
HoKinhDoanhPortal.init();

