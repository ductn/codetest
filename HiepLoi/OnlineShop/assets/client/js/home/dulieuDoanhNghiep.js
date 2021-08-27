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
var DoanhNghiepPortalConfig = {
    pageSize: 7,
    pageIndex: 1,
}
var monthHT = new Date().getMonth();
$("a[href='#viewBanDoDN']:not([href='#])").click(function () {
    let target = $(this).attr("href");
    $('html,body').stop().animate({
        scrollTop: $(target).offset().top - 50
    }, 1000);
    event.preventDefault();
});
$("a[href='#pill-1']:not([href='#])").click(function () {
    setTimeout(function () {
        var id = $('.viewLuyKeQuyDN:first-child').attr("data-id");
        $(".viewLuyKeQuyDN[data-id='" + id + "']").trigger("click");
    }, 1000);
});
$("a[href='#viewBanDoDN2']:not([href='#])").click(function () {
    let target = $(this).attr("href");
    $('html,body').stop().animate({
        scrollTop: $(target).offset().top - 50
    }, 1000);
    event.preventDefault();
});
$("a[href='#pill-11']:not([href='#])").click(function () {
    setTimeout(function () {
        var id = $('.viewLuyKeQuyDN2:first-child').attr("data-id");
        $(".viewLuyKeQuyDN2[data-id='" + id + "']").trigger("click");
    }, 1000);
});
$(document).on('click', '.viewBanDoHanhChinh', function () {
    $('#contentBanDoHanhChinh').attr("src", $('#contentBanDoHanhChinh').attr("src"));
    $('#modalBanDoHanhChinh').modal("show");
});
var DoanhNghiepPortal = {
    init: function () {
        DoanhNghiepPortal.loadData();
        DoanhNghiepPortal.registerEvents();
    },
    registerEvents: function () {

        DoanhNghiepPortal.getDataChartColumnAndLine(235, 3 , "TỈNH BẾN TRE");
        $('input[type=radio][name=rdLuyKeDoanhNghiep]').change(function () {
            var val = this.value;
            var id = $(".tb-parentDN").find("tr").find("td a.active2").attr("data-id");
            var title = $(".tb-parentDN").find("tr").find("td a.active2").attr("data-title");
            $('input[type=radio][name=rdLuyKeDoanhNghiep2]').removeAttr('checked');
            $("input[type=radio][name=rdLuyKeDoanhNghiep2][value=" + val + "]").attr('checked', 'checked');
            if (val == 1) {
                DoanhNghiepPortal.getDataChartColumnAndLine(id, 3, title);
            }
            if (val == 2) {
                DoanhNghiepPortal.getDataChartColumnAndLineVon(id, 3, title);
            }
            if (val == 3) {
                DoanhNghiepPortal.getDataChartColumnAndLineSoLD(id, 3, title);
            }
        });
        $('input[type=radio][name=rdLuyKeDoanhNghiep2]').change(function () {
            var val = this.value;
            var id = $(".tb-parentDN2").find("tr").find("td a.active2").attr("data-id");
            var title = $(".tb-parentDN2").find("tr").find("td a.active2").attr("data-title");
            $('input[type=radio][name=rdLuyKeDoanhNghiep]').removeAttr('checked');
            $("input[type=radio][name=rdLuyKeDoanhNghiep][value=" + val + "]").attr('checked', 'checked');
            if (val == 1) {
                DoanhNghiepPortal.getDataChartColumnAndLine(id, 3, title);
            }
            if (val == 2) {
                DoanhNghiepPortal.getDataChartColumnAndLineVon(id, 3, title);
            }
            if (val == 3) {
                DoanhNghiepPortal.getDataChartColumnAndLineSoLD(id, 3, title);
            }
        });
        $('.viewLuyKeQuyDN').on('click', function () {
            $(".tb-parentDN").find("tr").find("td a.active2").removeClass('active2');
            $(this).addClass('active2');
            var id = $(this).attr("data-id");
            $('#huyenDNBanDoLuyKe').val(id);
            var title = $(".tb-parentDN").find("tr").find("td a.active2").attr("data-title");
            $(".tb-parentDN2").find("tr").find("td a.active2").removeClass('active2');
            $('.viewLuyKeQuyDN2[data-id="' + id + '"]').addClass('active2');
            var val = $("input[type=radio][name=rdLuyKeDoanhNghiep]:checked").val();
            if (val == 1) {
                DoanhNghiepPortal.getDataChartColumnAndLine(id, 3, title);
            }
            if (val == 2) {
                DoanhNghiepPortal.getDataChartColumnAndLineVon(id, 3, title);
            }
            if (val == 3) {
                DoanhNghiepPortal.getDataChartColumnAndLineSoLD(id, 3, title);
            }
        });
        $('.viewLuyKeQuyDN2').on('click', function () {
            $(".tb-parentDN2").find("tr").find("td a.active2").removeClass('active2');
            $(this).addClass('active2');
            var id = $(this).attr("data-id");
            $('#huyenDNBanDoLuyKe2').val(id);
            var title = $(".tb-parentDN2").find("tr").find("td a.active2").attr("data-title");
            $(".tb-parentDN").find("tr").find("td a.active2").removeClass('active2');
            $('.viewLuyKeQuyDN[data-id="' + id + '"]').addClass('active2');
            var val = $("input[type=radio][name=rdLuyKeDoanhNghiep2]:checked").val();
            if (val == 1) {
                DoanhNghiepPortal.getDataChartColumnAndLine(id, 3, title);
            }
            if (val == 2) {
                DoanhNghiepPortal.getDataChartColumnAndLineVon(id, 3, title);
            }
            if (val == 3) {
                DoanhNghiepPortal.getDataChartColumnAndLineSoLD(id, 3, title);
            }
        });

        $("body").on('click', '#tblDataIndexDN .ViewDetail', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            DoanhNghiepPortal.getThongTinDoanhNghiepInfor(id);
        });

        $('#huyenDN').on('change', function () {
            var HuyenId = $('#huyenDN').val();
            DoanhNghiepPortal.getProvinceByParent(HuyenId, 'xaDN');
            //var HuyenDN = $('#huyenDNBanDoLuyKe').val();
            //if (HuyenId != HuyenDN) {
            //    $('#huyenDNBanDoLuyKe').val(HuyenId);
            //}
            //var HuyenDN2 = $('#huyenDNBanDoLuyKe2').val();
            //if (HuyenId != HuyenDN2) {
            //    $('#huyenDNBanDoLuyKe2').val(HuyenId);
            //}
        });
        $('#huyenDNBanDoLuyKe').on('change', function () {
            var HuyenId = $('#huyenDNBanDoLuyKe').val();
            var HuyenDN2 = $('#huyenDNBanDoLuyKe2').val();
            if (HuyenId != HuyenDN2) {
                $('#huyenDNBanDoLuyKe2').val(HuyenId)
            }
            $(".tb-parentDN").find("tr").find("td a[data-id='" + HuyenId + "']").click();
        });
        $('#huyenDNBanDoLuyKe2').on('change', function () {
            var HuyenId = $('#huyenDNBanDoLuyKe2').val();
            var HuyenDN2 = $('#huyenDNBanDoLuyKe').val();
            if (HuyenId != HuyenDN2) {
                $('#huyenDNBanDoLuyKe').val(HuyenId);
            }
            $(".tb-parentDN2").find("tr").find("td a[data-id='" + HuyenId + "']").click();
        });
        $('#xaDN').on('change', function () {
            DoanhNghiepPortal.loadData(true);
        });
        $('#searchDN').on('click', function () {
            DoanhNghiepPortal.loadData(true);
        });
    },
    loadData: function (changePageSize) {
        var huyenID = $('#huyenDN').val();
        var titleDN = $("#huyenDN :selected").text();
        if (huyenID == 0) {
            titleDN = "Tỉnh bến tre";
        }
        var xaID = $('#xaDN').val();
        if (xaID != 0 && xaID != null) {
            titleDN = $("#xaDN :selected").text()
        }
        var searh = $('#nameDN').val();
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
            url: URL_APPLICATION + '/Home/LoadDataDoanhNghiep',
            type: 'GET',
            data: {
                strSearch: JSON.stringify(duLieu),
                page: DoanhNghiepPortalConfig.pageIndex,
                pageSize: DoanhNghiepPortalConfig.pageSize
            },
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    var data = response.data;
                    var html = '';
                    var template = $('#data-template-index-dn').html();
                    $.each(data, function (i, item) {
                        var DaiDienPL = "";
                        if (item.NguoiDaiDienTheoPhapLuat == null || item.NguoiDaiDienTheoPhapLuat == "") {
                            DaiDienPL = item.ChuSoHuu;
                        } else {
                            DaiDienPL = item.NguoiDaiDienTheoPhapLuat;
                        }
                        html += Mustache.render(template, {
                            ID: item.Id,
                            TenDN: item.TenDoanhNghiep,
                            MaST: item.MaSoDoanhNghiep,
                            DaiDienPL: DaiDienPL,
                            DiaChi: item.DiaChiTruSoChinh
                        });
                    });
                    $('#tblDataIndexDN').html(html);
                    $('.totalDN').html(response.total.format());
                    $('.titleDNCSDL').html(titleDN);
                    DoanhNghiepPortal.paging(response.total, function () {
                        DoanhNghiepPortal.loadData();
                    }, changePageSize);
                    DoanhNghiepPortal.registerEvent();
                }
            }
        });
    },
    paging: function (totalRow, callback, changePageSize) {
        var totalPage = Math.ceil(totalRow / DoanhNghiepPortalConfig.pageSize);

        //Unbind pagination if it existed or click change pagesize
        if ($('#pagination-index a').length === 0 || changePageSize) {
            $('#pagination-index').empty();
            $('#pagination-index').removeData("twbs-pagination");
            $('#pagination-index').unbind("page");
        }

        $('#pagination-index').twbsPagination({
            totalPages: totalPage,
            visiblePages: 5,
            first: '<span aria-hidden="true">&laquo;</span>',
            next: 'Tiếp',
            last: '<span aria-hidden="true">&raquo;</span>',
            prev: 'Trước',
            onPageClick: function (event, page) {
                DoanhNghiepPortalConfig.pageIndex = page;
                setTimeout(callback, 200);
            }
        });
    },
    getThongTinDoanhNghiepInfor: function (id) {
        $.ajax({
            url: URL_APPLICATION + '/ThongTinDoanhNghiep/GetThongTinDoanhNghiepInfor',
            type: "POST",
            data: {
                id: id
            },
            dataType: "json",
            success: function (response) {
                if (response.status == true) {
                    var data = response.data;
                    $('#View_NameChinh').html(data.TenDoanhNghiep);
                    $('#View_Name').html(data.TenDoanhNghiep);
                    $('#View_LoaiHinh').html("Doanh nghiệp");
                    $('#View_MST').html(data.MaSoDoanhNghiep);
                    $('#View_DiaChi').html(data.DiaChiTruSoChinh);
                    $('#View_DaiDien').html(data.NguoiDaiDienTheoPhapLuat);
                    $('#View_NgayHoatDong').html(response.NgayDangKy);
                    $('#View_SDT').html(data.DienThoai);
                    $('#View_TrangThai').html(data.TrangThai);
                    $('#modalSocial').modal('show');
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
                DoanhNghiepPortal.loadData(true);
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
                        if (i<= monthHT) {
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
                    $('.tilteBDDN2').html("BIỂU ĐỒ SỐ LƯỢNG THEO THÁNG");
                    var chartLineTheoThangDN = new CanvasJS.Chart("chartLineTheoThangDN", {
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
                            title: "Doanh nghiệp",
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
                    chartLineTheoThangDN.render();
                    //Chart colunm 4 cột theo quý
                    $('.tilteBDDN').html("BIỂU ĐỒ SỐ LƯỢNG THEO QUÝ");
                    var chartColunmTheoQuyDN = new CanvasJS.Chart("chartColunmTheoQuyDN", {
                        animationEnabled: true,
                        //title: {
                        //    text: "BIỂU ĐỒ SỐ LƯỢNG THEO QUÝ " + LoaiHinhTitle ,
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
                            title: "Doanh nghiệp",
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
                    chartColunmTheoQuyDN.render();
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
                        if (i<= monthHT) {
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
                    $('.tilteBDDN2').html("BIỂU ĐỒ VỐN THEO THÁNG");
                    var chartLineTheoThangDN = new CanvasJS.Chart("chartLineTheoThangDN", {
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
                    chartLineTheoThangDN.render();
                    //Chart colunm 4 cột theo quý
                    $('.tilteBDDN').html("BIỂU ĐỒ VỐN THEO QUÝ");
                    var chartColunmTheoQuyDN = new CanvasJS.Chart("chartColunmTheoQuyDN", {
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
                    chartColunmTheoQuyDN.render();

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
                        if (i<= monthHT) {
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
                    $('.tilteBDDN2').html("BIỂU ĐỒ SỐ LAO ĐỘNG THEO THÁNG");
                    var chartLineTheoThangDN = new CanvasJS.Chart("chartLineTheoThangDN", {
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
                    chartLineTheoThangDN.render();
                    //Chart colunm 4 cột theo quý
                    $('.tilteBDDN').html("BIỂU ĐỒ SỐ LAO ĐỘNG THEO QUÝ");
                    var chartColunmTheoQuyDN = new CanvasJS.Chart("chartColunmTheoQuyDN", {
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
                    chartColunmTheoQuyDN.render();

                    //End chart colunm 4 cột theo quý
                }
            }
        })
    } 
}
DoanhNghiepPortal.init();

