var URL_APPLICATION = $('#URL_APPLICATION').val();
var HopTacXaPortalConfig = {
    pageSize: 7,
    pageIndex: 1,
}
$("a[href='#viewBanDoHTX']:not([href='#])").click(function () {
    let target = $(this).attr("href");
    $('html,body').stop().animate({
        scrollTop: $(target).offset().top - 50
    }, 1000);
    event.preventDefault();
});
$("a[href='#pill-1HTX']:not([href='#])").click(function () {
    setTimeout(function () {
        $('html,body').stop().animate({
            scrollTop: $("#viewBanDoHTX").offset().top - 170
        }, 1000);
    }, 1000);
});
$("a[href='#viewBanDoHTX2']:not([href='#])").click(function () {
    let target = $(this).attr("href");
    $('html,body').stop().animate({
        scrollTop: $(target).offset().top - 50
    }, 1000);
    event.preventDefault();
});
$("a[href='#pill-11HTX']:not([href='#])").click(function () {
    setTimeout(function () {
        $('html,body').stop().animate({
            scrollTop: $("#viewBanDoHTX2").offset().top - 170
        }, 1000);
    }, 1000);
});
var monthHT = new Date().getMonth();
var HopTacXaPortal = {
    init: function () {
        HopTacXaPortal.loadData();
        HopTacXaPortal.registerEvents();
    },
    registerEvents: function () {
        HopTacXaPortal.getDataChartColumnAndLine(235, 2, "TỈNH BẾN TRE");
        $('input[type=radio][name=rdLuyKeHopTacXa]').change(function () {
            var val = this.value;
            var id = $(".tb-parentHTX").find("tr").find("td a.active2").attr("data-id");
            var title = $(".tb-parentHTX").find("tr").find("td a.active2").attr("data-title");
            $('input[type=radio][name=rdLuyKeHopTacXa2]').removeAttr('checked');
            $("input[type=radio][name=rdLuyKeHopTacXa2][value=" + val + "]").attr('checked', 'checked');
            if (val == 1) {
                HopTacXaPortal.getDataChartColumnAndLine(id, 2, title);
            }
            if (val == 2) {
                HopTacXaPortal.getDataChartColumnAndLineVon(id, 2, title);
            }
            if (val == 3) {
                HopTacXaPortal.getDataChartColumnAndLineSoLD(id, 2, title);
            }
        });
        $('input[type=radio][name=rdLuyKeHopTacXa2]').change(function () {
            var val = this.value;
            var id = $(".tb-parentHTX2").find("tr").find("td a.active2").attr("data-id");
            var title = $(".tb-parentHTX2").find("tr").find("td a.active2").attr("data-title");
            $('input[type=radio][name=rdLuyKeHopTacXa]').removeAttr('checked');
            $("input[type=radio][name=rdLuyKeHopTacXa][value=" + val + "]").attr('checked', 'checked');
            if (val == 1) {
                HopTacXaPortal.getDataChartColumnAndLine(id, 2, title);
            }
            if (val == 2) {
                HopTacXaPortal.getDataChartColumnAndLineVon(id, 2, title);
            }
            if (val == 3) {
                HopTacXaPortal.getDataChartColumnAndLineSoLD(id, 2, title);
            }
        });
        $('.viewLuyKeQuyHTX').on('click', function () {
            $(".tb-parentHTX").find("tr").find("td a.active2").removeClass('active2');
            $(this).addClass('active2');
            var id = $(this).attr("data-id");
            $('#huyenHTXBanDoLuyKe').val(id);
            var title = $(".tb-parentHTX").find("tr").find("td a.active2").attr("data-title");
            $(".tb-parentHTX2").find("tr").find("td a.active2").removeClass('active2');
            $('.viewLuyKeQuyHTX2[data-id="' + id + '"]').addClass('active2');
            var val = $("input[type=radio][name=rdLuyKeHopTacXa]:checked").val();
            if (val == 1) {
                HopTacXaPortal.getDataChartColumnAndLine(id, 2, title);
            }
            if (val == 2) {
                HopTacXaPortal.getDataChartColumnAndLineVon(id, 2, title);
            }
            if (val == 3) {
                HopTacXaPortal.getDataChartColumnAndLineSoLD(id, 2, title);
            }
        });
        $('.viewLuyKeQuyHTX2').on('click', function () {
            $(".tb-parentHTX2").find("tr").find("td a.active2").removeClass('active2');
            $(this).addClass('active2');
            var id = $(this).attr("data-id");
            $('#huyenHTXBanDoLuyKe2').val(id);
            var title = $(".tb-parentHTX2").find("tr").find("td a.active2").attr("data-title");
            $(".tb-parentHTX").find("tr").find("td a.active2").removeClass('active2');
            $('.viewLuyKeQuyHTX[data-id="' + id + '"]').addClass('active2');
            var val = $("input[type=radio][name=rdLuyKeHopTacXa2]:checked").val();
            if (val == 1) {
                HopTacXaPortal.getDataChartColumnAndLine(id, 2, title);
            }
            if (val == 2) {
                HopTacXaPortal.getDataChartColumnAndLineVon(id, 2, title);
            }
            if (val == 3) {
                HopTacXaPortal.getDataChartColumnAndLineSoLD(id, 2, title);
            }
        });

        $("body").on('click', '#tblDataIndexHTX .ViewDetail', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            HopTacXaPortal.getThongTinHopTacXaInfor(id);
        });

        $('#huyenHTX').on('change', function () {
            var HuyenId = $('#huyenHTX').val();
            HopTacXaPortal.getProvinceByParent(HuyenId, 'xaHTX');
            //var HuyenHTX = $('#huyenHTXBanDoLuyKe').val();
            //if (HuyenId != HuyenHTX) {
            //    $('#huyenHTXBanDoLuyKe').val(HuyenId);
            //}
            //var HuyenHTX2 = $('#huyenHTXBanDoLuyKe2').val();
            //if (HuyenId != HuyenHTX2) {
            //    $('#huyenHTXBanDoLuyKe2').val(HuyenId);
            //}
        });
        $('#huyenHTXBanDoLuyKe').on('change', function () {
            var HuyenId = $('#huyenHTXBanDoLuyKe').val();
            var HuyenHTX2 = $('#huyenHTXBanDoLuyKe2').val();
            if (HuyenId != HuyenHTX2) {
                $('#huyenHTXBanDoLuyKe2').val(HuyenId)
            }
            $(".tb-parentHTX").find("tr").find("td a[data-id='" + HuyenId + "']").click();
        });
        $('#huyenHTXBanDoLuyKe2').on('change', function () {
            var HuyenId = $('#huyenHTXBanDoLuyKe2').val();
            var HuyenHTX2 = $('#huyenHTXBanDoLuyKe').val();
            if (HuyenId != HuyenHTX2) {
                $('#huyenHTXBanDoLuyKe').val(HuyenId);
            }
            $(".tb-parentHTX2").find("tr").find("td a[data-id='" + HuyenId + "']").click();
        });
        $('#xaHTX').on('change', function () {
            HopTacXaPortal.loadData(true);
        });
        $('#searchHTX').on('click', function () {
            HopTacXaPortal.loadData(true);
        });
    },
    loadData: function (changePageSize) {
        var huyenID = $('#huyenHTX').val();
        var titleHTX = $("#huyenHTX :selected").text();
        if (huyenID == 0) {
            titleHTX = "Tỉnh bến tre";
        }
        var xaID = $('#xaHTX').val();
        if (xaID != 0 && xaID != null) {
            titleDN = $("#xaDN :selected").text()
        }
        var searh = $('#nameHTX').val();
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
            url: URL_APPLICATION + '/Home/LoadDataHopTacXa',
            type: 'GET',
            data: {
                strSearch: JSON.stringify(duLieu),
                page: HopTacXaPortalConfig.pageIndex,
                pageSize: HopTacXaPortalConfig.pageSize
            },
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    var data = response.data;
                    var html = '';
                    var template = $('#data-template-index-HTX').html();
                    $.each(data, function (i, item) {
                        html += Mustache.render(template, {
                            ID: item.HopTacXaId,
                            TenHTX: item.Biz_VietName,
                            MaST: item.HopTacXaCode,
                            DaiDienPL: item.Ow_Name,
                            DiaChi: item.Biz_HeadOffice
                        });
                    });
                    $('#tblDataIndexHTX').html(html);
                    $('.totalHTX').html(response.total.format());
                    $('.titleHTXCSDL').html(titleHTX);
                    HopTacXaPortal.paging(response.total, function () {
                        HopTacXaPortal.loadData();
                    }, changePageSize);
                    HopTacXaPortal.registerEvent();
                }
            }
        });
    },
    paging: function (totalRow, callback, changePageSize) {
        var totalPage = Math.ceil(totalRow / HopTacXaPortalConfig.pageSize);
        //Unbind pagination if it existed or click change pagesize
        if ($('#pagination-index-HTX a').length === 0 || changePageSize) {
            $('#pagination-index-HTX').empty();
            $('#pagination-index-HTX').removeData("twbs-pagination");
            $('#pagination-index-HTX').unbind("page");
        }

        $('#pagination-index-HTX').twbsPagination({
            totalPages: totalPage,
            visiblePages: 5,
            first: '<span aria-hidden="true">&laquo;</span>',
            next: 'Tiếp',
            last: '<span aria-hidden="true">&raquo;</span>',
            prev: 'Trước',
            onPageClick: function (event, page) {
                HopTacXaPortalConfig.pageIndex = page;
                setTimeout(callback, 200);
            }
        });
    },
    getThongTinHopTacXaInfor: function (id) {
        $.ajax({
            url: URL_APPLICATION + '/ThongTinHopTacXa/GetThongTinHopTacXaInfor',
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
                    $('#View_LoaiHinh').html("Hợp tác xã");
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
                HopTacXaPortal.loadData(true);
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
                    $('.tilteBDHTX2').html("BIỂU ĐỒ SỐ LƯỢNG THEO THÁNG");
                    var chartLineTheoThangHTX = new CanvasJS.Chart("chartLineTheoThangHTX", {
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
                            title: "Hợp tác xã",
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
                    chartLineTheoThangHTX.render();
                    //Chart colunm 4 cột theo quý
                    $('.tilteBDHTX').html("BIỂU ĐỒ SỐ LƯỢNG THEO QUÝ");
                    var chartColunmTheoQuyHTX = new CanvasJS.Chart("chartColunmTheoQuyHTX", {
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
                            title: "Hợp tác xã",
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
                    chartColunmTheoQuyHTX.render();
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
                    $('.tilteBDHTX2').html("BIỂU ĐỒ VỐN THEO THÁNG");
                    var chartLineTheoThangHTX = new CanvasJS.Chart("chartLineTheoThangHTX", {
                        animationEnabled: true,
                        theme: "light2",
                        //title: {
                        //    text: "BIỂU ĐỒ VỐN THEO THÁNG " + LoaiHinhTitle + " ",
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
                    chartLineTheoThangHTX.render();
                    //Chart colunm 4 cột theo quý
                    $('.tilteBDHTX').html("BIỂU ĐỒ VỐN THEO QUÝ");
                    var chartColunmTheoQuyHTX = new CanvasJS.Chart("chartColunmTheoQuyHTX", {
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
                    chartColunmTheoQuyHTX.render();

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
                    $('.tilteBDHTX2').html("BIỂU ĐỒ SỐ LAO ĐỘNG THEO THÁNG");
                    var chartLineTheoThangHTX = new CanvasJS.Chart("chartLineTheoThangHTX", {
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
                    chartLineTheoThangHTX.render();
                    //Chart colunm 4 cột theo quý
                    $('.tilteBDHTX').html("BIỂU ĐỒ SỐ LAO ĐỘNG THEO QUÝ");
                    var chartColunmTheoQuyHTX = new CanvasJS.Chart("chartColunmTheoQuyHTX", {
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
                    chartColunmTheoQuyHTX.render();

                    //End chart colunm 4 cột theo quý
                }
            }
        })
    } 
    
}
HopTacXaPortal.init();

