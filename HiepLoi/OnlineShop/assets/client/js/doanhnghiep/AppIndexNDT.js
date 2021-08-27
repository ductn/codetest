var URL_APPLICATION = $('#URL_APPLICATION').val();
var monthHT = new Date().getMonth();
function piechart_NDT2(id, dulieu) {
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
function trendline_NDT2(id) {
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
function columnchart_NDT2_Year(id, dulieu) {
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
function linechart_NDT2_Year(id, dulieu) {
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

function linechart_NDT2_Month(id, dulieu) {
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

function columnschart_NDT2_Quy(id, dulieu) {
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
var AppIndexNDTPortal = {
    init: function () {
        AppIndexNDTPortal.loadData();
        AppIndexNDTPortal.registerEvents();

    },
    registerEvents: function () {
        AppIndexNDTPortal.getBieuDoThongKeSoLuong();
        AppIndexNDTPortal.getBieuDoThongKeVon();
        //AppIndexNDTPortal.getBieuDoThongKeLaoDong();
        $('#huyenNDTBieuDo').on('change', function () {
            AppIndexNDTPortal.getBieuDoThongKeSoLuong();
        });
        $('#huyenNDTBieuDo2').on('change', function () {
            AppIndexNDTPortal.getBieuDoThongKeVon();
        });
        $('#huyenNDTBieuDo3').on('change', function () {
            AppIndexNDTPortal.getBieuDoThongKeLaoDong();
        });
    },
    loadData: function (changePageSize) {

    },
    getBieuDoThongKeSoLuong: function () {
        var HuyenId = $('#huyenNDTBieuDo').val();
        var XaPhuongId = 0;
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
                    var dataChartNDT = response.dataHKD;
                    var dataNDT = [];
                    $.each(dataChartNDT, function (i, item) {
                        var obj = [item.title, item.amount];
                        dataNDT.push(obj);
                    });
                    var tongHo = response.dataTongHKD;
                    $('#TongNDT').html(tongHo);
                    google.charts.load('current', { 'packages': ['corechart'] }).then(function () {
                        google.charts.setOnLoadCallback(piechart_NDT2('piechart_NDT2', dataNDT));
                    });
                }
            }
        })
        var obj = {
            HuyenId: HuyenId,
            XaPhuongId: 0,
            LoaiHinh: 4,
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
                    var dataChartColumnNDT = response.dataChartColumnDN;
                    var dataChartLineForMonthNDT = response.dataChartLineForMonthDN;
                    var dataChartColumnQuy1NDT = response.dataChartColumnQuy1DN;
                    var dataChartColumnQuy2NDT = response.dataChartColumnQuy2DN;
                    var dataChartColumnQuy3NDT = response.dataChartColumnQuy3DN;
                    var dataChartColumnQuy4NDT = response.dataChartColumnQuy4DN;

                    var dataColumnNDTYear = [["Year", "Năm", { role: "style" }],];
                    var dataLineNDTYear = [['Year', 'Năm',],];
                    var dataLineNDTMonth = [['Month', 'Tháng',],];
                    var dataColumnQuyNDT = [['', 'Quý 1', 'Quý 2', 'Quý 3', 'Quý 4'],];

                    $.each(dataChartColumnNDT, function (i, item) {
                        var obj = [item.title, item.amount];
                        var objColumn = [item.title, item.amount, ""];
                        dataLineNDTYear.push(obj);
                        dataColumnNDTYear.push(objColumn);
                    });
                    dataColumnQuyNDT.push([dataChartColumnQuy1NDT[0].title, dataChartColumnQuy1NDT[0].amount, dataChartColumnQuy2NDT[0].amount, dataChartColumnQuy3NDT[0].amount, dataChartColumnQuy4NDT[0].amount]);
                    dataColumnQuyNDT.push([dataChartColumnQuy1NDT[1].title, dataChartColumnQuy1NDT[1].amount, dataChartColumnQuy2NDT[1].amount, dataChartColumnQuy3NDT[1].amount, dataChartColumnQuy4NDT[1].amount]);
                    dataColumnQuyNDT.push([dataChartColumnQuy1NDT[2].title, dataChartColumnQuy1NDT[2].amount, dataChartColumnQuy2NDT[2].amount, dataChartColumnQuy3NDT[2].amount, dataChartColumnQuy4NDT[2].amount]);
                    dataColumnQuyNDT.push([dataChartColumnQuy1NDT[3].title, dataChartColumnQuy1NDT[3].amount, dataChartColumnQuy2NDT[3].amount, dataChartColumnQuy3NDT[3].amount, dataChartColumnQuy4NDT[3].amount]);
                    dataColumnQuyNDT.push([dataChartColumnQuy1NDT[4].title, dataChartColumnQuy1NDT[4].amount, dataChartColumnQuy2NDT[4].amount, dataChartColumnQuy3NDT[4].amount, dataChartColumnQuy4NDT[4].amount]);
                    $.each(dataChartLineForMonthNDT, function (i, item) {
                        var obj = [item.title, item.amount];
                        dataLineNDTMonth.push(obj);
                    });
                    google.charts.load('current', { 'packages': ['corechart'] }).then(function () {
                        google.charts.setOnLoadCallback(columnchart_NDT2_Year('columnchart_NDT2', dataColumnNDTYear));
                        google.charts.setOnLoadCallback(linechart_NDT2_Year('trendline_NDT2', dataLineNDTYear));
                        google.charts.setOnLoadCallback(columnschart_NDT2_Quy('columnschart_NDT2', dataColumnQuyNDT));
                        google.charts.setOnLoadCallback(linechart_NDT2_Month('linechart_NDT2', dataLineNDTMonth));
                    });
                }
            }
        })
    },
    getBieuDoThongKeVon: function () {
        var HuyenId = $('#huyenNDTBieuDo2').val();
        var XaPhuongId = 0;
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
                        var obj = [item.title, item.amount];
                        dataVon.push(obj);
                    });
                    var tongVon = response.dataTongVonHKD;
                    $('#TongVonNDT').html(tongVon);
                    google.charts.load('current', { 'packages': ['corechart'] }).then(function () {
                        google.charts.setOnLoadCallback(piechart_NDT2('piechart_VNDT2', dataVon));
                    });
                }
            }
        })
        var obj = {
            HuyenId: HuyenId,
            XaPhuongId: 0,
            LoaiHinh: 4,
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
                    var dataChartColumnNDT = response.dataChartColumnDN;
                    var dataChartLineForMonthNDT = response.dataChartLineForMonthDN;
                    var dataChartColumnQuy1NDT = response.dataChartColumnQuy1DN;
                    var dataChartColumnQuy2NDT = response.dataChartColumnQuy2DN;
                    var dataChartColumnQuy3NDT = response.dataChartColumnQuy3DN;
                    var dataChartColumnQuy4NDT = response.dataChartColumnQuy4DN;

                    var dataColumnNDTYear = [["Year", "Năm", { role: "style" }],];
                    var dataLineNDTYear = [['Year', 'Năm',],];
                    var dataLineNDTMonth = [['Month', 'Tháng',],];
                    var dataColumnQuyNDT = [['', 'Quý 1', 'Quý 2', 'Quý 3', 'Quý 4'],];

                    $.each(dataChartColumnNDT, function (i, item) {
                        var obj = [item.title, item.amount / 1000000000];
                        var objColumn = [item.title, item.amount / 1000000000, ""];
                        dataLineNDTYear.push(obj);
                        dataColumnNDTYear.push(objColumn);
                    });
                    dataColumnQuyNDT.push([dataChartColumnQuy1NDT[0].title, dataChartColumnQuy1NDT[0].amount / 1000000000, dataChartColumnQuy2NDT[0].amount / 1000000000, dataChartColumnQuy3NDT[0].amount / 1000000000, dataChartColumnQuy4NDT[0].amount / 1000000000]);
                    dataColumnQuyNDT.push([dataChartColumnQuy1NDT[1].title, dataChartColumnQuy1NDT[1].amount / 1000000000, dataChartColumnQuy2NDT[1].amount / 1000000000, dataChartColumnQuy3NDT[1].amount / 1000000000, dataChartColumnQuy4NDT[1].amount / 1000000000]);
                    dataColumnQuyNDT.push([dataChartColumnQuy1NDT[2].title, dataChartColumnQuy1NDT[2].amount / 1000000000, dataChartColumnQuy2NDT[2].amount / 1000000000, dataChartColumnQuy3NDT[2].amount / 1000000000, dataChartColumnQuy4NDT[2].amount / 1000000000]);
                    dataColumnQuyNDT.push([dataChartColumnQuy1NDT[3].title, dataChartColumnQuy1NDT[3].amount / 1000000000, dataChartColumnQuy2NDT[3].amount / 1000000000, dataChartColumnQuy3NDT[3].amount / 1000000000, dataChartColumnQuy4NDT[3].amount / 1000000000]);
                    dataColumnQuyNDT.push([dataChartColumnQuy1NDT[4].title, dataChartColumnQuy1NDT[4].amount / 1000000000, dataChartColumnQuy2NDT[4].amount / 1000000000, dataChartColumnQuy3NDT[4].amount / 1000000000, dataChartColumnQuy4NDT[4].amount / 1000000000]);
                    $.each(dataChartLineForMonthNDT, function (i, item) {
                        var obj = [item.title, item.amount / 1000000000];
                        dataLineNDTMonth.push(obj);
                    });
                    google.charts.load('current', { 'packages': ['corechart'] }).then(function () {
                        google.charts.setOnLoadCallback(columnchart_NDT2_Year('columnchart_VNDT2', dataColumnNDTYear));
                        google.charts.setOnLoadCallback(linechart_NDT2_Year('trendline_VNDT2', dataLineNDTYear));
                        google.charts.setOnLoadCallback(columnschart_NDT2_Quy('columnschart_VNDT2', dataColumnQuyNDT));
                        google.charts.setOnLoadCallback(linechart_NDT2_Month('linechart_VNDT2', dataLineNDTMonth));
                    });
                }
            }
        })
    },
    getBieuDoThongKeLaoDong: function () {
        var HuyenId = $('#huyenNDTBieuDo3').val();
        var XaPhuongId = 0;
        var obj = {
            HuyenId: HuyenId,
            XaPhuongId: 0,
            LoaiHinh: 4,
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
                    var dataChartColumnNDT = response.dataChartColumnDN;
                    var dataChartLineForMonthNDT = response.dataChartLineForMonthDN;
                    var dataChartColumnQuy1NDT = response.dataChartColumnQuy1DN;
                    var dataChartColumnQuy2NDT = response.dataChartColumnQuy2DN;
                    var dataChartColumnQuy3NDT = response.dataChartColumnQuy3DN;
                    var dataChartColumnQuy4NDT = response.dataChartColumnQuy4DN;

                    var dataColumnNDTYear = [["Year", "Năm", { role: "style" }],];
                    var dataLineNDTYear = [['Year', 'Năm',],];
                    var dataLineNDTMonth = [['Month', 'Tháng',],];
                    var dataColumnQuyNDT = [['', 'Quý 1', 'Quý 2', 'Quý 3', 'Quý 4'],];

                    $.each(dataChartColumnNDT, function (i, item) {
                        var obj = [item.title, item.amount];
                        var objColumn = [item.title, item.amount, ""];
                        dataLineNDTYear.push(obj);
                        dataColumnNDTYear.push(objColumn);
                    });
                    dataColumnQuyNDT.push([dataChartColumnQuy1NDT[0].title, dataChartColumnQuy1NDT[0].amount, dataChartColumnQuy2NDT[0].amount, dataChartColumnQuy3NDT[0].amount, dataChartColumnQuy4NDT[0].amount]);
                    dataColumnQuyNDT.push([dataChartColumnQuy1NDT[1].title, dataChartColumnQuy1NDT[1].amount, dataChartColumnQuy2NDT[1].amount, dataChartColumnQuy3NDT[1].amount, dataChartColumnQuy4NDT[1].amount]);
                    dataColumnQuyNDT.push([dataChartColumnQuy1NDT[2].title, dataChartColumnQuy1NDT[2].amount, dataChartColumnQuy2NDT[2].amount, dataChartColumnQuy3NDT[2].amount, dataChartColumnQuy4NDT[2].amount]);
                    dataColumnQuyNDT.push([dataChartColumnQuy1NDT[3].title, dataChartColumnQuy1NDT[3].amount, dataChartColumnQuy2NDT[3].amount, dataChartColumnQuy3NDT[3].amount, dataChartColumnQuy4NDT[3].amount]);
                    dataColumnQuyNDT.push([dataChartColumnQuy1NDT[4].title, dataChartColumnQuy1NDT[4].amount, dataChartColumnQuy2NDT[4].amount, dataChartColumnQuy3NDT[4].amount, dataChartColumnQuy4NDT[4].amount]);
                    $('#TongLDNDT').html(new Intl.NumberFormat().format(dataChartColumnQuy4NDT[4].amount).replace(/,/g, "."));
                    $.each(dataChartLineForMonthNDT, function (i, item) {
                        var obj = [item.title, item.amount];
                        dataLineNDTMonth.push(obj);
                    });
                    google.charts.load('current', { 'packages': ['corechart'] }).then(function () {
                        google.charts.setOnLoadCallback(columnchart_NDT2_Year('columnchart_LDNDT2', dataColumnNDTYear));
                        google.charts.setOnLoadCallback(linechart_NDT2_Year('trendline_LDNDT2', dataLineNDTYear));
                        google.charts.setOnLoadCallback(columnschart_NDT2_Quy('columnschart_LDNDT2', dataColumnQuyNDT));
                        google.charts.setOnLoadCallback(linechart_NDT2_Month('linechart_LDNDT2', dataLineNDTMonth));
                    });
                }
            }
        })
    },
}
AppIndexNDTPortal.init();
window.onload = function () {
    AppIndexNDTPortal.init();
};

window.onresize = function () {
    AppIndexNDTPortal.init();
};
jQuery(document).on('shown.bs.tab', 'a[data-toggle="pill"]', function () {
    window.dispatchEvent(new Event('resize'));
});

