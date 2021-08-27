var URL_APPLICATION = $('#URL_APPLICATION').val();
var monthHT = new Date().getMonth();
function piechart_HTX2(id, dulieu) {
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
function trendline_HTX2(id) {
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
function columnchart_HTX2_Year(id, dulieu) {
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
function linechart_HTX2_Year(id, dulieu) {
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

function linechart_HTX2_Month(id, dulieu) {
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

function columnschart_HTX2_Quy(id, dulieu) {
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
var AppIndexHTXPortal = {
    init: function () {
        AppIndexHTXPortal.loadData();
        AppIndexHTXPortal.registerEvents();

    },
    registerEvents: function () {
        AppIndexHTXPortal.getBieuDoThongKeSoLuong();
        AppIndexHTXPortal.getBieuDoThongKeVon();
        AppIndexHTXPortal.getBieuDoThongKeLaoDong();
        $('#huyenHTXBieuDo').on('change', function () {
            AppIndexHTXPortal.getBieuDoThongKeSoLuong();
        });
        $('#huyenHTXBieuDo2').on('change', function () {
            AppIndexHTXPortal.getBieuDoThongKeVon();
        });
        $('#huyenHTXBieuDo3').on('change', function () {
            AppIndexHTXPortal.getBieuDoThongKeLaoDong();
        });
    },
    loadData: function (changePageSize) {

    },
    getBieuDoThongKeSoLuong: function () {
        var HuyenId = $('#huyenHTXBieuDo').val();
        var XaPhuongId = 0;
        $.ajax({
            url: URL_APPLICATION + '/ThongTinHopTacXa/GetDataBieuDo',
            type: "POST",
            data: {
                HuyenId: HuyenId,
                XaPhuongId: XaPhuongId
            },
            dataType: "json",
            success: function (response) {
                if (response.status == true) {
                    //Chart Von
                    var dataChartHTX = response.dataHKD;
                    var dataHTX = [];
                    $.each(dataChartHTX, function (i, item) {
                        var obj = [item.title, item.amount];
                        dataHTX.push(obj);
                    });
                    var tongHo = response.dataTongHKD;
                    $('#TongHTX').html(tongHo);
                    google.charts.load('current', { 'packages': ['corechart'] }).then(function () {
                        google.charts.setOnLoadCallback(piechart_HTX2('piechart_HTX2', dataHTX));
                    });
                }
            }
        })
        var obj = {
            HuyenId: HuyenId,
            XaPhuongId: 0,
            LoaiHinh: 2,
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
                    var dataChartColumnHTX = response.dataChartColumnDN;
                    var dataChartLineForMonthHTX = response.dataChartLineForMonthDN;
                    var dataChartColumnQuy1HTX = response.dataChartColumnQuy1DN;
                    var dataChartColumnQuy2HTX = response.dataChartColumnQuy2DN;
                    var dataChartColumnQuy3HTX = response.dataChartColumnQuy3DN;
                    var dataChartColumnQuy4HTX = response.dataChartColumnQuy4DN;

                    var dataColumnHTXYear = [["Year", "Năm", { role: "style" }],];
                    var dataLineHTXYear = [['Year', 'Năm',],];
                    var dataLineHTXMonth = [['Month', 'Tháng',],];
                    var dataColumnQuyHTX = [['', 'Quý 1', 'Quý 2', 'Quý 3', 'Quý 4'],];

                    $.each(dataChartColumnHTX, function (i, item) {
                        var obj = [item.title, item.amount];
                        var objColumn = [item.title, item.amount, ""];
                        dataLineHTXYear.push(obj);
                        dataColumnHTXYear.push(objColumn);
                    });
                    dataColumnQuyHTX.push([dataChartColumnQuy1HTX[0].title, dataChartColumnQuy1HTX[0].amount, dataChartColumnQuy2HTX[0].amount, dataChartColumnQuy3HTX[0].amount, dataChartColumnQuy4HTX[0].amount]);
                    dataColumnQuyHTX.push([dataChartColumnQuy1HTX[1].title, dataChartColumnQuy1HTX[1].amount, dataChartColumnQuy2HTX[1].amount, dataChartColumnQuy3HTX[1].amount, dataChartColumnQuy4HTX[1].amount]);
                    dataColumnQuyHTX.push([dataChartColumnQuy1HTX[2].title, dataChartColumnQuy1HTX[2].amount, dataChartColumnQuy2HTX[2].amount, dataChartColumnQuy3HTX[2].amount, dataChartColumnQuy4HTX[2].amount]);
                    dataColumnQuyHTX.push([dataChartColumnQuy1HTX[3].title, dataChartColumnQuy1HTX[3].amount, dataChartColumnQuy2HTX[3].amount, dataChartColumnQuy3HTX[3].amount, dataChartColumnQuy4HTX[3].amount]);
                    dataColumnQuyHTX.push([dataChartColumnQuy1HTX[4].title, dataChartColumnQuy1HTX[4].amount, dataChartColumnQuy2HTX[4].amount, dataChartColumnQuy3HTX[4].amount, dataChartColumnQuy4HTX[4].amount]);
                    $.each(dataChartLineForMonthHTX, function (i, item) {
                        var obj = [item.title, item.amount];
                        dataLineHTXMonth.push(obj);
                    });
                    google.charts.load('current', { 'packages': ['corechart'] }).then(function () {
                        google.charts.setOnLoadCallback(columnchart_HTX2_Year('columnchart_HTX2', dataColumnHTXYear));
                        google.charts.setOnLoadCallback(linechart_HTX2_Year('trendline_HTX2', dataLineHTXYear));
                        google.charts.setOnLoadCallback(columnschart_HTX2_Quy('columnschart_HTX2', dataColumnQuyHTX));
                        google.charts.setOnLoadCallback(linechart_HTX2_Month('linechart_HTX2', dataLineHTXMonth));
                    });
                }
            }
        })
    },
    getBieuDoThongKeVon: function () {
        var HuyenId = $('#huyenHTXBieuDo2').val();
        var XaPhuongId = 0;
        $.ajax({
            url: URL_APPLICATION + '/ThongTinHopTacXa/GetDataBieuDo',
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
                    $('#TongVonHTX').html(tongVon);
                    google.charts.load('current', { 'packages': ['corechart'] }).then(function () {
                        google.charts.setOnLoadCallback(piechart_HTX2('piechart_VHTX2', dataVon));
                    });
                }
            }
        })
        var obj = {
            HuyenId: HuyenId,
            XaPhuongId: 0,
            LoaiHinh: 2,
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
                    var dataChartColumnHTX = response.dataChartColumnDN;
                    var dataChartLineForMonthHTX = response.dataChartLineForMonthDN;
                    var dataChartColumnQuy1HTX = response.dataChartColumnQuy1DN;
                    var dataChartColumnQuy2HTX = response.dataChartColumnQuy2DN;
                    var dataChartColumnQuy3HTX = response.dataChartColumnQuy3DN;
                    var dataChartColumnQuy4HTX = response.dataChartColumnQuy4DN;

                    var dataColumnHTXYear = [["Year", "Năm", { role: "style" }],];
                    var dataLineHTXYear = [['Year', 'Năm',],];
                    var dataLineHTXMonth = [['Month', 'Tháng',],];
                    var dataColumnQuyHTX = [['', 'Quý 1', 'Quý 2', 'Quý 3', 'Quý 4'],];

                    $.each(dataChartColumnHTX, function (i, item) {
                        var obj = [item.title, item.amount / 1000000000];
                        var objColumn = [item.title, item.amount / 1000000000, ""];
                        dataLineHTXYear.push(obj);
                        dataColumnHTXYear.push(objColumn);
                    });
                    dataColumnQuyHTX.push([dataChartColumnQuy1HTX[0].title, dataChartColumnQuy1HTX[0].amount / 1000000000, dataChartColumnQuy2HTX[0].amount / 1000000000, dataChartColumnQuy3HTX[0].amount / 1000000000, dataChartColumnQuy4HTX[0].amount / 1000000000]);
                    dataColumnQuyHTX.push([dataChartColumnQuy1HTX[1].title, dataChartColumnQuy1HTX[1].amount / 1000000000, dataChartColumnQuy2HTX[1].amount / 1000000000, dataChartColumnQuy3HTX[1].amount / 1000000000, dataChartColumnQuy4HTX[1].amount / 1000000000]);
                    dataColumnQuyHTX.push([dataChartColumnQuy1HTX[2].title, dataChartColumnQuy1HTX[2].amount / 1000000000, dataChartColumnQuy2HTX[2].amount / 1000000000, dataChartColumnQuy3HTX[2].amount / 1000000000, dataChartColumnQuy4HTX[2].amount / 1000000000]);
                    dataColumnQuyHTX.push([dataChartColumnQuy1HTX[3].title, dataChartColumnQuy1HTX[3].amount / 1000000000, dataChartColumnQuy2HTX[3].amount / 1000000000, dataChartColumnQuy3HTX[3].amount / 1000000000, dataChartColumnQuy4HTX[3].amount / 1000000000]);
                    dataColumnQuyHTX.push([dataChartColumnQuy1HTX[4].title, dataChartColumnQuy1HTX[4].amount / 1000000000, dataChartColumnQuy2HTX[4].amount / 1000000000, dataChartColumnQuy3HTX[4].amount / 1000000000, dataChartColumnQuy4HTX[4].amount / 1000000000]);
                    $.each(dataChartLineForMonthHTX, function (i, item) {
                        var obj = [item.title, item.amount / 1000000000];
                        dataLineHTXMonth.push(obj);
                    });
                    google.charts.load('current', { 'packages': ['corechart'] }).then(function () {
                        google.charts.setOnLoadCallback(columnchart_HTX2_Year('columnchart_VHTX2', dataColumnHTXYear));
                        google.charts.setOnLoadCallback(linechart_HTX2_Year('trendline_VHTX2', dataLineHTXYear));
                        google.charts.setOnLoadCallback(columnschart_HTX2_Quy('columnschart_VHTX2', dataColumnQuyHTX));
                        google.charts.setOnLoadCallback(linechart_HTX2_Month('linechart_VHTX2', dataLineHTXMonth));
                    });
                }
            }
        })
    },
    getBieuDoThongKeLaoDong: function () {
        var HuyenId = $('#huyenHTXBieuDo3').val();
        var XaPhuongId = 0;
        var obj = {
            HuyenId: HuyenId,
            XaPhuongId: 0,
            LoaiHinh: 2,
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
                    var dataChartColumnHTX = response.dataChartColumnDN;
                    var dataChartLineForMonthHTX = response.dataChartLineForMonthDN;
                    var dataChartColumnQuy1HTX = response.dataChartColumnQuy1DN;
                    var dataChartColumnQuy2HTX = response.dataChartColumnQuy2DN;
                    var dataChartColumnQuy3HTX = response.dataChartColumnQuy3DN;
                    var dataChartColumnQuy4HTX = response.dataChartColumnQuy4DN;

                    var dataColumnHTXYear = [["Year", "Năm", { role: "style" }],];
                    var dataLineHTXYear = [['Year', 'Năm',],];
                    var dataLineHTXMonth = [['Month', 'Tháng',],];
                    var dataColumnQuyHTX = [['', 'Quý 1', 'Quý 2', 'Quý 3', 'Quý 4'],];

                    $.each(dataChartColumnHTX, function (i, item) {
                        var obj = [item.title, item.amount];
                        var objColumn = [item.title, item.amount, ""];
                        dataLineHTXYear.push(obj);
                        dataColumnHTXYear.push(objColumn);
                    });
                    dataColumnQuyHTX.push([dataChartColumnQuy1HTX[0].title, dataChartColumnQuy1HTX[0].amount, dataChartColumnQuy2HTX[0].amount, dataChartColumnQuy3HTX[0].amount, dataChartColumnQuy4HTX[0].amount]);
                    dataColumnQuyHTX.push([dataChartColumnQuy1HTX[1].title, dataChartColumnQuy1HTX[1].amount, dataChartColumnQuy2HTX[1].amount, dataChartColumnQuy3HTX[1].amount, dataChartColumnQuy4HTX[1].amount]);
                    dataColumnQuyHTX.push([dataChartColumnQuy1HTX[2].title, dataChartColumnQuy1HTX[2].amount, dataChartColumnQuy2HTX[2].amount, dataChartColumnQuy3HTX[2].amount, dataChartColumnQuy4HTX[2].amount]);
                    dataColumnQuyHTX.push([dataChartColumnQuy1HTX[3].title, dataChartColumnQuy1HTX[3].amount, dataChartColumnQuy2HTX[3].amount, dataChartColumnQuy3HTX[3].amount, dataChartColumnQuy4HTX[3].amount]);
                    dataColumnQuyHTX.push([dataChartColumnQuy1HTX[4].title, dataChartColumnQuy1HTX[4].amount, dataChartColumnQuy2HTX[4].amount, dataChartColumnQuy3HTX[4].amount, dataChartColumnQuy4HTX[4].amount]);
                    $('#TongLDHTX').html(new Intl.NumberFormat().format(dataChartColumnQuy4HTX[4].amount).replace(/,/g, "."));
                    $.each(dataChartLineForMonthHTX, function (i, item) {
                        var obj = [item.title, item.amount];
                        dataLineHTXMonth.push(obj);
                    });
                    google.charts.load('current', { 'packages': ['corechart'] }).then(function () {
                        google.charts.setOnLoadCallback(columnchart_HTX2_Year('columnchart_LDHTX2', dataColumnHTXYear));
                        google.charts.setOnLoadCallback(linechart_HTX2_Year('trendline_LDHTX2', dataLineHTXYear));
                        google.charts.setOnLoadCallback(columnschart_HTX2_Quy('columnschart_LDHTX2', dataColumnQuyHTX));
                        google.charts.setOnLoadCallback(linechart_HTX2_Month('linechart_LDHTX2', dataLineHTXMonth));
                    });
                }
            }
        })
    },
}
AppIndexHTXPortal.init();
window.onload = function () {
    AppIndexHTXPortal.init();
};

window.onresize = function () {
    AppIndexHTXPortal.init();
};
jQuery(document).on('shown.bs.tab', 'a[data-toggle="pill"]', function () {
    window.dispatchEvent(new Event('resize'));
});

