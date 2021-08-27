var URL_APPLICATION = $('#URL_APPLICATION').val();
var monthHT = new Date().getMonth();
function piechart_HKD2(id, dulieu) {
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
function trendline_HKD2(id) {
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
function columnchart_HKD2_Year(id, dulieu) {
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
function linechart_HKD2_Year(id, dulieu) {
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

function linechart_HKD2_Month(id, dulieu) {
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

function columnschart_HKD2_Quy(id,dulieu) {
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
var AppIndexHKDPortal = {
    init: function () {
        AppIndexHKDPortal.loadData();
        AppIndexHKDPortal.registerEvents();
       
    },
    registerEvents: function () {
        AppIndexHKDPortal.getBieuDoThongKeSoLuong();
        AppIndexHKDPortal.getBieuDoThongKeVon();
        AppIndexHKDPortal.getBieuDoThongKeLaoDong();
        $('#huyenHKDBieuDo').on('change', function () {
            AppIndexHKDPortal.getBieuDoThongKeSoLuong();
        });
        $('#huyenHKDBieuDo2').on('change', function () {
            AppIndexHKDPortal.getBieuDoThongKeVon();
        });
        $('#huyenHKDBieuDo3').on('change', function () {
            AppIndexHKDPortal.getBieuDoThongKeLaoDong();
        });
    },
    loadData: function (changePageSize) {

    },
    getBieuDoThongKeSoLuong: function () {
        var HuyenId = $('#huyenHKDBieuDo').val();
        var XaPhuongId = 0;
        $.ajax({
            url: URL_APPLICATION + '/ThongTinHoKinhDoanh/GetDataBieuDo',
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
                    $('#TongHKD').html(tongHo);
                    google.charts.load('current', { 'packages': ['corechart'] }).then(function () {
                        google.charts.setOnLoadCallback(piechart_HKD2('piechart_HKD2', dataHKD));
                    });
                }
            }
        })
        var obj = {
            HuyenId: HuyenId,
            XaPhuongId: 0,
            LoaiHinh: 1,
            FromDate: "",
            ToDate: "",
            LinhVucId: 0
        };
        $.ajax({
            url: URL_APPLICATION + '/Admin/Home/GetDataChartColumnAndLineDN',
            type: "POST",
            data: obj,
            dataType: "json",
            success: function (response) {
                if (response.status == true) {
                    var dataChartColumnHKD = response.dataChartColumnDN;
                    var dataChartLineForMonthHKD = response.dataChartLineForMonthDN;
                    var dataChartColumnQuy1HKD = response.dataChartColumnQuy1DN;
                    var dataChartColumnQuy2HKD = response.dataChartColumnQuy2DN;
                    var dataChartColumnQuy3HKD = response.dataChartColumnQuy3DN;
                    var dataChartColumnQuy4HKD = response.dataChartColumnQuy4DN;

                    var dataColumnHKDYear = [["Year", "Density", { role: "style" }],];
                    var dataLineHKDYear = [['Year', 'Line',],];
                    var dataLineHKDMonth = [['Month', 'Line',],];
                    var dataColumnQuyHKD = [['', 'Quý 1', 'Quý 2', 'Quý 3', 'Quý 4'],];

                    $.each(dataChartColumnHKD, function (i, item) {
                        var obj = [item.title, item.amount];
                        var objColumn = [item.title, item.amount,""];
                        dataLineHKDYear.push(obj);
                        dataColumnHKDYear.push(objColumn);
                    });
                    dataColumnQuyHKD.push([dataChartColumnQuy1HKD[0].title, dataChartColumnQuy1HKD[0].amount, dataChartColumnQuy2HKD[0].amount, dataChartColumnQuy3HKD[0].amount, dataChartColumnQuy4HKD[0].amount]);
                    dataColumnQuyHKD.push([dataChartColumnQuy1HKD[1].title, dataChartColumnQuy1HKD[1].amount, dataChartColumnQuy2HKD[1].amount, dataChartColumnQuy3HKD[1].amount, dataChartColumnQuy4HKD[1].amount]);
                    dataColumnQuyHKD.push([dataChartColumnQuy1HKD[2].title, dataChartColumnQuy1HKD[2].amount, dataChartColumnQuy2HKD[2].amount, dataChartColumnQuy3HKD[2].amount, dataChartColumnQuy4HKD[2].amount]);
                    dataColumnQuyHKD.push([dataChartColumnQuy1HKD[3].title, dataChartColumnQuy1HKD[3].amount, dataChartColumnQuy2HKD[3].amount, dataChartColumnQuy3HKD[3].amount, dataChartColumnQuy4HKD[3].amount]);
                    dataColumnQuyHKD.push([dataChartColumnQuy1HKD[4].title, dataChartColumnQuy1HKD[4].amount, dataChartColumnQuy2HKD[4].amount, dataChartColumnQuy3HKD[4].amount, dataChartColumnQuy4HKD[4].amount]);
                    $.each(dataChartLineForMonthHKD, function (i, item) {
                        var obj = [item.title, item.amount];
                        dataLineHKDMonth.push(obj);
                    });
                    google.charts.load('current', { 'packages': ['corechart'] }).then(function () {
                        google.charts.setOnLoadCallback(columnchart_HKD2_Year('columnchart_HKD2', dataColumnHKDYear));
                        google.charts.setOnLoadCallback(linechart_HKD2_Year('trendline_HKD2', dataLineHKDYear));
                        google.charts.setOnLoadCallback(columnschart_HKD2_Quy('columnschart_HKD2', dataColumnQuyHKD));
                        google.charts.setOnLoadCallback(linechart_HKD2_Month('linechart_HKD2', dataLineHKDMonth));
                    });
                }
            }
        })
    },
    getBieuDoThongKeVon: function () {
        var HuyenId = $('#huyenHKDBieuDo2').val();
        var XaPhuongId = 0;
        $.ajax({
            url: URL_APPLICATION + '/ThongTinHoKinhDoanh/GetDataBieuDo',
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
                    $('#TongVonHKD').html(tongVon);
                    google.charts.load('current', { 'packages': ['corechart'] }).then(function () {
                        google.charts.setOnLoadCallback(piechart_HKD2('piechart_VHKD2', dataVon));
                    });
                }
            }
        })
        var obj = {
            HuyenId: HuyenId,
            XaPhuongId: 0,
            LoaiHinh: 1,
            FromDate: "",
            ToDate: "",
            LinhVucId: 0
        };

        $.ajax({
            url: URL_APPLICATION + '/Admin/Home/GetDataChartColumnAndLineVonDN',
            type: "POST",
            data: obj,
            dataType: "json",
            success: function (response) {
                if (response.status == true) {
                    var dataChartColumnHKD = response.dataChartColumnDN;
                    var dataChartLineForMonthHKD = response.dataChartLineForMonthDN;
                    var dataChartColumnQuy1HKD = response.dataChartColumnQuy1DN;
                    var dataChartColumnQuy2HKD = response.dataChartColumnQuy2DN;
                    var dataChartColumnQuy3HKD = response.dataChartColumnQuy3DN;
                    var dataChartColumnQuy4HKD = response.dataChartColumnQuy4DN;

                    var dataColumnHKDYear = [["Year", "Density", { role: "style" }],];
                    var dataLineHKDYear = [['Year', 'Line',],];
                    var dataLineHKDMonth = [['Month', 'Line',],];
                    var dataColumnQuyHKD = [['', 'Quý 1', 'Quý 2', 'Quý 3', 'Quý 4'],];

                    $.each(dataChartColumnHKD, function (i, item) {
                        var obj = [item.title, item.amount / 1000000000];
                        var objColumn = [item.title, item.amount / 1000000000, ""];
                        dataLineHKDYear.push(obj);
                        dataColumnHKDYear.push(objColumn);
                    });
                    dataColumnQuyHKD.push([dataChartColumnQuy1HKD[0].title, dataChartColumnQuy1HKD[0].amount / 1000000000, dataChartColumnQuy2HKD[0].amount / 1000000000, dataChartColumnQuy3HKD[0].amount / 1000000000, dataChartColumnQuy4HKD[0].amount / 1000000000]);
                    dataColumnQuyHKD.push([dataChartColumnQuy1HKD[1].title, dataChartColumnQuy1HKD[1].amount / 1000000000, dataChartColumnQuy2HKD[1].amount / 1000000000, dataChartColumnQuy3HKD[1].amount / 1000000000, dataChartColumnQuy4HKD[1].amount / 1000000000]);
                    dataColumnQuyHKD.push([dataChartColumnQuy1HKD[2].title, dataChartColumnQuy1HKD[2].amount / 1000000000, dataChartColumnQuy2HKD[2].amount / 1000000000, dataChartColumnQuy3HKD[2].amount / 1000000000, dataChartColumnQuy4HKD[2].amount / 1000000000]);
                    dataColumnQuyHKD.push([dataChartColumnQuy1HKD[3].title, dataChartColumnQuy1HKD[3].amount / 1000000000, dataChartColumnQuy2HKD[3].amount / 1000000000, dataChartColumnQuy3HKD[3].amount / 1000000000, dataChartColumnQuy4HKD[3].amount / 1000000000] );
                    dataColumnQuyHKD.push([dataChartColumnQuy1HKD[4].title, dataChartColumnQuy1HKD[4].amount / 1000000000, dataChartColumnQuy2HKD[4].amount / 1000000000, dataChartColumnQuy3HKD[4].amount / 1000000000, dataChartColumnQuy4HKD[4].amount / 1000000000] );
                    $.each(dataChartLineForMonthHKD, function (i, item) {
                        var obj = [item.title, item.amount / 1000000000];
                        dataLineHKDMonth.push(obj);
                    });
                    google.charts.load('current', { 'packages': ['corechart'] }).then(function () {
                        google.charts.setOnLoadCallback(columnchart_HKD2_Year('columnchart_VHKD2', dataColumnHKDYear));
                        google.charts.setOnLoadCallback(linechart_HKD2_Year('trendline_VHKD2', dataLineHKDYear));
                        google.charts.setOnLoadCallback(columnschart_HKD2_Quy('columnschart_VHKD2', dataColumnQuyHKD));
                        google.charts.setOnLoadCallback(linechart_HKD2_Month('linechart_VHKD2', dataLineHKDMonth));
                    });
                }
            }
        })
    },
    getBieuDoThongKeLaoDong: function () {
        var HuyenId = $('#huyenHKDBieuDo3').val();
        var XaPhuongId = 0;
        var obj = {
            HuyenId: HuyenId,
            XaPhuongId: 0,
            LoaiHinh: 1,
            FromDate: "",
            ToDate: "",
            LinhVucId: 0
        };

        $.ajax({
            url: URL_APPLICATION + '/Admin/Home/GetDataChartColumnAndLineSoLD',
            type: "POST",
            data: obj,
            dataType: "json",
            success: function (response) {
                if (response.status == true) {
                    var dataChartColumnHKD = response.dataChartColumnDN;
                    var dataChartLineForMonthHKD = response.dataChartLineForMonthDN;
                    var dataChartColumnQuy1HKD = response.dataChartColumnQuy1DN;
                    var dataChartColumnQuy2HKD = response.dataChartColumnQuy2DN;
                    var dataChartColumnQuy3HKD = response.dataChartColumnQuy3DN;
                    var dataChartColumnQuy4HKD = response.dataChartColumnQuy4DN;

                    var dataColumnHKDYear = [["Year", "Density", { role: "style" }],];
                    var dataLineHKDYear = [['Year', 'Line',],];
                    var dataLineHKDMonth = [['Month', 'Line',],];
                    var dataColumnQuyHKD = [['', 'Quý 1', 'Quý 2', 'Quý 3', 'Quý 4'],];

                    $.each(dataChartColumnHKD, function (i, item) {
                        var obj = [item.title, item.amount];
                        var objColumn = [item.title, item.amount, ""];
                        dataLineHKDYear.push(obj);
                        dataColumnHKDYear.push(objColumn);
                    });
                    dataColumnQuyHKD.push([dataChartColumnQuy1HKD[0].title, dataChartColumnQuy1HKD[0].amount, dataChartColumnQuy2HKD[0].amount, dataChartColumnQuy3HKD[0].amount, dataChartColumnQuy4HKD[0].amount]);
                    dataColumnQuyHKD.push([dataChartColumnQuy1HKD[1].title, dataChartColumnQuy1HKD[1].amount, dataChartColumnQuy2HKD[1].amount, dataChartColumnQuy3HKD[1].amount, dataChartColumnQuy4HKD[1].amount]);
                    dataColumnQuyHKD.push([dataChartColumnQuy1HKD[2].title, dataChartColumnQuy1HKD[2].amount, dataChartColumnQuy2HKD[2].amount, dataChartColumnQuy3HKD[2].amount, dataChartColumnQuy4HKD[2].amount]);
                    dataColumnQuyHKD.push([dataChartColumnQuy1HKD[3].title, dataChartColumnQuy1HKD[3].amount, dataChartColumnQuy2HKD[3].amount, dataChartColumnQuy3HKD[3].amount, dataChartColumnQuy4HKD[3].amount]);
                    dataColumnQuyHKD.push([dataChartColumnQuy1HKD[4].title, dataChartColumnQuy1HKD[4].amount, dataChartColumnQuy2HKD[4].amount, dataChartColumnQuy3HKD[4].amount, dataChartColumnQuy4HKD[4].amount]);
                    $('#TongLDHKD').html(new Intl.NumberFormat().format(dataChartColumnQuy4HKD[4].amount).replace(/,/g, "."));
                    $.each(dataChartLineForMonthHKD, function (i, item) {
                        var obj = [item.title, item.amount];
                        dataLineHKDMonth.push(obj);
                    });
                    google.charts.load('current', { 'packages': ['corechart'] }).then(function () {
                        google.charts.setOnLoadCallback(columnchart_HKD2_Year('columnchart_LDHKD2', dataColumnHKDYear));
                        google.charts.setOnLoadCallback(linechart_HKD2_Year('trendline_LDHKD2', dataLineHKDYear));
                        google.charts.setOnLoadCallback(columnschart_HKD2_Quy('columnschart_LDHKD2', dataColumnQuyHKD));
                        google.charts.setOnLoadCallback(linechart_HKD2_Month('linechart_LDHKD2', dataLineHKDMonth));
                    });
                }
            }
        })
    },
}
AppIndexHKDPortal.init();
window.onload = function () {
    AppIndexHKDPortal.init();
};

window.onresize = function () {
    AppIndexHKDPortal.init();
};
jQuery(document).on('shown.bs.tab', 'a[data-toggle="pill"]', function () {
    window.dispatchEvent(new Event('resize'));
});

