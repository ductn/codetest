﻿var URL_APPLICATION = $('#URL_APPLICATION').val();
var monthHT = new Date().getMonth();
function piechart_DN2(id, dulieu) {
    var data = new google.visualization.DataTable();
    data.addColumn('string', 'Pizza');
    data.addColumn('number', 'Populartiy');
    data.addRows(dulieu);
    var options = {
        title: 'Biểu đồ phân bố theo lĩnh vực / ngành',
        titleTextStyle: { bold: true, italic: false, fontSize: '14' },
        textPosition: 'out',
        legend: { position: 'labeled', alignment: 'center' },
        pieSliceText: 'none',
        width: '100%',
        chartArea: { width: '95%' },
    };

    var chart = new google.visualization.PieChart(document.getElementById(id));
    chart.draw(data, options);
}
function trendline_DN2(id) {
    var data = google.visualization.arrayToDataTable([
        ['X', 'Y'],
        [0, 4],
        [1, 2],
        [2, 4],
        [3, 6],
        [4, 4]
    ]);

    var options = {
        title: 'Biểu đồ tăng trưởng các năm theo line',
        titleTextStyle: { bold: true, italic: false, fontSize: '14' },
        legend: 'none',
        colors: ['red'],
        pointShape: 'diamond',
        width: '100%', chartArea: { width: '90%' },
        trendlines: {
            0: {
                type: 'polynomial',
                degree: 3,
                visibleInLegend: true,
                pointSize: 20, // Set the size of the trendline dots.
                opacity: 0.1
            }
        }
    };
    var chart = new google.visualization.ScatterChart(document.getElementById(id));


    chart.draw(data, options);
}
function columnchart_DN2_Year(id, dulieu) {
    var data = google.visualization.arrayToDataTable(dulieu);

    var view = new google.visualization.DataView(data);
    view.setColumns([0, 1,
        {
            calc: "stringify",
            sourceColumn: 1,
            type: "string",
            role: "annotation"
        },
        2]);

    var options = {
        title: "Biểu đồ lũy kế hàng năm theo năm ",
        titleTextStyle: { bold: true, italic: false, fontSize: '14', alignment: 'center' },
        chartArea: { width: "80%" },
        legend: { position: "none" },
    };
    var chart = new google.visualization.ColumnChart(document.getElementById(id));
    chart.draw(view, options);
}
function linechart_DN2_Year(id, dulieu) {
    var data = google.visualization.arrayToDataTable(dulieu);

    var options = {

        title: 'Biểu đồ lũy kế hàng năm theo năm dạng line',
        titleTextStyle: { bold: true, italic: false, fontSize: '14' },
        curveType: 'function',
        legend: { position: 'bottom' },
        chartArea: { width: "80%" },
        width: '100%',
        colors: ['red']
    };

    var chart = new google.visualization.LineChart(document.getElementById(id));

    chart.draw(data, options);
}

function linechart_DN2_Month(id, dulieu) {
    var data = google.visualization.arrayToDataTable(dulieu);

    var options = {

        title: 'Biểu đồ tăng trưởng trong năm',
        titleTextStyle: { bold: true, italic: false, fontSize: '14' },
        curveType: 'function',
        legend: { position: 'bottom' },
        chartArea: { width: "80%" },
        width: '100%',
        colors: ['red']
    };

    var chart = new google.visualization.LineChart(document.getElementById(id));

    chart.draw(data, options);
}

function columnschart_DN2_Quy(id,dulieu) {
    // Some raw data (not necessarily accurate)
    var data = google.visualization.arrayToDataTable(dulieu);

    var view = new google.visualization.DataView(data);
    view.setColumns([0, 1,
        {
            calc: "stringify",
            sourceColumn: 1,
            type: "string",
            role: "annotation"
        },
        2]);


    var options = {
        title: 'Biểu đồ lũy kế hàng năm theo quý',
        titleTextStyle: { bold: true, italic: false, fontSize: '14' },
        width: '100%',
        seriesType: 'bars',
        legend: { position: 'bottom', maxLines: 4 },
        bar: { groupWidth: "80%" },
        chartArea: { width: "80%" }
    };

    var chart = new google.visualization.ComboChart(document.getElementById(id));
    chart.draw(data, options);
}
var AppIndexPortal = {
    init: function () {
        AppIndexPortal.loadData();
        AppIndexPortal.registerEvents();
       
    },
    registerEvents: function () {
        AppIndexPortal.getBieuDoThongKeSoLuong();
        AppIndexPortal.getBieuDoThongKeVon();
        AppIndexPortal.getBieuDoThongKeLaoDong();
        $('#huyenDNBieuDo').on('change', function () {
            AppIndexPortal.getBieuDoThongKeSoLuong();
        });
        $('#huyenDNBieuDo2').on('change', function () {
            AppIndexPortal.getBieuDoThongKeVon();
        });
        $('#huyenDNBieuDo3').on('change', function () {
            AppIndexPortal.getBieuDoThongKeLaoDong();
        });
    },
    loadData: function (changePageSize) {

    },
    getBieuDoThongKeSoLuong: function () {
        var HuyenId = $('#huyenDNBieuDo').val();
        var XaPhuongId = 0;
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
                    var dataChartHKD = response.dataHKD;
                    var dataHKD = [];
                    $.each(dataChartHKD, function (i, item) {
                        var obj = [item.title, item.amount];
                        dataHKD.push(obj);
                    });
                    var tongHo = response.dataTongHKD;
                    $('#TongDN').html(tongHo);
                    google.charts.load('current', { 'packages': ['corechart'] }).then(function () {
                        google.charts.setOnLoadCallback(piechart_DN2('piechart_DN2', dataHKD));
                    });
                }
            }
        })
        var obj = {
            HuyenId: HuyenId,
            XaPhuongId: 0,
            LoaiHinh: 3,
            FromDate: "",
            ToDate: "",
            LinhVucId: 0
        };
        $.ajax({
            url: URL_APPLICATION + '/Home/GetDataChartColumnAndLineDN',
            type: "POST",
            data: obj,
            dataType: "json",
            success: function (response) {
                if (response.status == true) {
                    var dataChartColumnDN = response.dataChartColumnDN;
                    var dataChartLineForMonthDN = response.dataChartLineForMonthDN;
                    var dataChartColumnQuy1DN = response.dataChartColumnQuy1DN;
                    var dataChartColumnQuy2DN = response.dataChartColumnQuy2DN;
                    var dataChartColumnQuy3DN = response.dataChartColumnQuy3DN;
                    var dataChartColumnQuy4DN = response.dataChartColumnQuy4DN;

                    var dataColumnDNYear = [["Year", "Density", { role: "style" }],];
                    var dataLineDNYear = [['Year', 'Line',],];
                    var dataLineDNMonth = [['Month', 'Line',],];
                    var dataColumnQuyDN = [['', 'Quý 1', 'Quý 2', 'Quý 3', 'Quý 4'],];

                    $.each(dataChartColumnDN, function (i, item) {
                        var obj = [item.title, item.amount];
                        var objColumn = [item.title, item.amount,""];
                        dataLineDNYear.push(obj);
                        dataColumnDNYear.push(objColumn);
                    });
                    dataColumnQuyDN.push([dataChartColumnQuy1DN[0].title, dataChartColumnQuy1DN[0].amount, dataChartColumnQuy2DN[0].amount, dataChartColumnQuy3DN[0].amount, dataChartColumnQuy4DN[0].amount]);
                    dataColumnQuyDN.push([dataChartColumnQuy1DN[1].title, dataChartColumnQuy1DN[1].amount, dataChartColumnQuy2DN[1].amount, dataChartColumnQuy3DN[1].amount, dataChartColumnQuy4DN[1].amount]);
                    dataColumnQuyDN.push([dataChartColumnQuy1DN[2].title, dataChartColumnQuy1DN[2].amount, dataChartColumnQuy2DN[2].amount, dataChartColumnQuy3DN[2].amount, dataChartColumnQuy4DN[2].amount]);
                    dataColumnQuyDN.push([dataChartColumnQuy1DN[3].title, dataChartColumnQuy1DN[3].amount, dataChartColumnQuy2DN[3].amount, dataChartColumnQuy3DN[3].amount, dataChartColumnQuy4DN[3].amount]);
                    dataColumnQuyDN.push([dataChartColumnQuy1DN[4].title, dataChartColumnQuy1DN[4].amount, dataChartColumnQuy2DN[4].amount, dataChartColumnQuy3DN[4].amount, dataChartColumnQuy4DN[4].amount]);
                    $.each(dataChartLineForMonthDN, function (i, item) {
                        var obj = [item.title, item.amount];
                        dataLineDNMonth.push(obj);
                    });
                    google.charts.load('current', { 'packages': ['corechart'] }).then(function () {
                        google.charts.setOnLoadCallback(columnchart_DN2_Year('columnchart_DN2', dataColumnDNYear));
                        google.charts.setOnLoadCallback(linechart_DN2_Year('trendline_DN2', dataLineDNYear));
                        google.charts.setOnLoadCallback(columnschart_DN2_Quy('columnschart_DN2', dataColumnQuyDN));
                        google.charts.setOnLoadCallback(linechart_DN2_Month('linechart_DN2', dataLineDNMonth));
                    });
                }
            }
        })
    },
    getBieuDoThongKeVon: function () {
        var HuyenId = $('#huyenDNBieuDo2').val();
        var XaPhuongId = 0;
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
                        var obj = [item.title, item.amount];
                        dataVon.push(obj);
                    });
                    var tongVon = response.dataTongVonHKD;
                    $('#TongVonDN').html(tongVon);
                    google.charts.load('current', { 'packages': ['corechart'] }).then(function () {
                        google.charts.setOnLoadCallback(piechart_DN2('piechart_VDN2', dataVon));
                    });
                }
            }
        })
        var obj = {
            HuyenId: HuyenId,
            XaPhuongId: 0,
            LoaiHinh: 3,
            FromDate: "",
            ToDate: "",
            LinhVucId: 0
        };

        $.ajax({
            url: URL_APPLICATION + '/Home/GetDataChartColumnAndLineVonDN',
            type: "POST",
            data: obj,
            dataType: "json",
            success: function (response) {
                if (response.status == true) {
                    var dataChartColumnDN = response.dataChartColumnDN;
                    var dataChartLineForMonthDN = response.dataChartLineForMonthDN;
                    var dataChartColumnQuy1DN = response.dataChartColumnQuy1DN;
                    var dataChartColumnQuy2DN = response.dataChartColumnQuy2DN;
                    var dataChartColumnQuy3DN = response.dataChartColumnQuy3DN;
                    var dataChartColumnQuy4DN = response.dataChartColumnQuy4DN;

                    var dataColumnDNYear = [["Year", "Density", { role: "style" }],];
                    var dataLineDNYear = [['Year', 'Line',],];
                    var dataLineDNMonth = [['Month', 'Line',],];
                    var dataColumnQuyDN = [['', 'Quý 1', 'Quý 2', 'Quý 3', 'Quý 4'],];

                    $.each(dataChartColumnDN, function (i, item) {
                        var obj = [item.title, item.amount / 1000000000];
                        var objColumn = [item.title, item.amount / 1000000000, ""];
                        dataLineDNYear.push(obj);
                        dataColumnDNYear.push(objColumn);
                    });
                    dataColumnQuyDN.push([dataChartColumnQuy1DN[0].title, dataChartColumnQuy1DN[0].amount / 1000000000, dataChartColumnQuy2DN[0].amount / 1000000000, dataChartColumnQuy3DN[0].amount / 1000000000, dataChartColumnQuy4DN[0].amount / 1000000000]);
                    dataColumnQuyDN.push([dataChartColumnQuy1DN[1].title, dataChartColumnQuy1DN[1].amount / 1000000000, dataChartColumnQuy2DN[1].amount / 1000000000, dataChartColumnQuy3DN[1].amount / 1000000000, dataChartColumnQuy4DN[1].amount / 1000000000]);
                    dataColumnQuyDN.push([dataChartColumnQuy1DN[2].title, dataChartColumnQuy1DN[2].amount / 1000000000, dataChartColumnQuy2DN[2].amount / 1000000000, dataChartColumnQuy3DN[2].amount / 1000000000, dataChartColumnQuy4DN[2].amount / 1000000000]);
                    dataColumnQuyDN.push([dataChartColumnQuy1DN[3].title, dataChartColumnQuy1DN[3].amount / 1000000000, dataChartColumnQuy2DN[3].amount / 1000000000, dataChartColumnQuy3DN[3].amount / 1000000000, dataChartColumnQuy4DN[3].amount / 1000000000] );
                    dataColumnQuyDN.push([dataChartColumnQuy1DN[4].title, dataChartColumnQuy1DN[4].amount / 1000000000, dataChartColumnQuy2DN[4].amount / 1000000000, dataChartColumnQuy3DN[4].amount / 1000000000, dataChartColumnQuy4DN[4].amount / 1000000000] );
                    $.each(dataChartLineForMonthDN, function (i, item) {
                        var obj = [item.title, item.amount / 1000000000];
                        dataLineDNMonth.push(obj);
                    });
                    google.charts.load('current', { 'packages': ['corechart'] }).then(function () {
                        google.charts.setOnLoadCallback(columnchart_DN2_Year('columnchart_VDN2', dataColumnDNYear));
                        google.charts.setOnLoadCallback(linechart_DN2_Year('trendline_VDN2', dataLineDNYear));
                        google.charts.setOnLoadCallback(columnschart_DN2_Quy('columnschart_VDN2', dataColumnQuyDN));
                        google.charts.setOnLoadCallback(linechart_DN2_Month('linechart_VDN2', dataLineDNMonth));
                    });
                }
            }
        })
    },
    getBieuDoThongKeLaoDong: function () {
        var HuyenId = $('#huyenDNBieuDo3').val();
        var XaPhuongId = 0;
        var obj = {
            HuyenId: HuyenId,
            XaPhuongId: 0,
            LoaiHinh: 3,
            FromDate: "",
            ToDate: "",
            LinhVucId: 0
        };

        $.ajax({
            url: URL_APPLICATION + '/Home/GetDataChartColumnAndLineSoLD',
            type: "POST",
            data: obj,
            dataType: "json",
            success: function (response) {
                if (response.status == true) {
                    var dataChartColumnDN = response.dataChartColumnDN;
                    var dataChartLineForMonthDN = response.dataChartLineForMonthDN;
                    var dataChartColumnQuy1DN = response.dataChartColumnQuy1DN;
                    var dataChartColumnQuy2DN = response.dataChartColumnQuy2DN;
                    var dataChartColumnQuy3DN = response.dataChartColumnQuy3DN;
                    var dataChartColumnQuy4DN = response.dataChartColumnQuy4DN;

                    var dataColumnDNYear = [["Year", "Density", { role: "style" }],];
                    var dataLineDNYear = [['Year', 'Line',],];
                    var dataLineDNMonth = [['Month', 'Line',],];
                    var dataColumnQuyDN = [['', 'Quý 1', 'Quý 2', 'Quý 3', 'Quý 4'],];

                    $.each(dataChartColumnDN, function (i, item) {
                        var obj = [item.title, item.amount];
                        var objColumn = [item.title, item.amount, ""];
                        dataLineDNYear.push(obj);
                        dataColumnDNYear.push(objColumn);
                    });
                    dataColumnQuyDN.push([dataChartColumnQuy1DN[0].title, dataChartColumnQuy1DN[0].amount, dataChartColumnQuy2DN[0].amount, dataChartColumnQuy3DN[0].amount, dataChartColumnQuy4DN[0].amount]);
                    dataColumnQuyDN.push([dataChartColumnQuy1DN[1].title, dataChartColumnQuy1DN[1].amount, dataChartColumnQuy2DN[1].amount, dataChartColumnQuy3DN[1].amount, dataChartColumnQuy4DN[1].amount]);
                    dataColumnQuyDN.push([dataChartColumnQuy1DN[2].title, dataChartColumnQuy1DN[2].amount, dataChartColumnQuy2DN[2].amount, dataChartColumnQuy3DN[2].amount, dataChartColumnQuy4DN[2].amount]);
                    dataColumnQuyDN.push([dataChartColumnQuy1DN[3].title, dataChartColumnQuy1DN[3].amount, dataChartColumnQuy2DN[3].amount, dataChartColumnQuy3DN[3].amount, dataChartColumnQuy4DN[3].amount]);
                    dataColumnQuyDN.push([dataChartColumnQuy1DN[4].title, dataChartColumnQuy1DN[4].amount, dataChartColumnQuy2DN[4].amount, dataChartColumnQuy3DN[4].amount, dataChartColumnQuy4DN[4].amount]);
                    $('#TongLDDN').html(new Intl.NumberFormat().format(dataChartColumnQuy4DN[4].amount).replace(/,/g, "."));
                    $.each(dataChartLineForMonthDN, function (i, item) {
                        var obj = [item.title, item.amount];
                        dataLineDNMonth.push(obj);
                    });
                    google.charts.load('current', { 'packages': ['corechart'] }).then(function () {
                        google.charts.setOnLoadCallback(columnchart_DN2_Year('columnchart_LDDN2', dataColumnDNYear));
                        google.charts.setOnLoadCallback(linechart_DN2_Year('trendline_LDDN2', dataLineDNYear));
                        google.charts.setOnLoadCallback(columnschart_DN2_Quy('columnschart_LDDN2', dataColumnQuyDN));
                        google.charts.setOnLoadCallback(linechart_DN2_Month('linechart_LDDN2', dataLineDNMonth));
                    });
                }
            }
        })
    },
}
AppIndexPortal.init();
window.onload = function () {
    AppIndexPortal.init();
};

window.onresize = function () {
    AppIndexPortal.init();
};
jQuery(document).on('shown.bs.tab', 'a[data-toggle="pill"]', function () {
    window.dispatchEvent(new Event('resize'));
});

