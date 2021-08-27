var URL_APPLICATION = $('#URL_APPLICATION').val();
window.closeModal = function () {
    $('#modalAction').modal('hide');
    location.reload();
};
jQuery.validator.addMethod("specialChars", function (value, element) {
    var regex = /[!@#$%^&*\=\[\]{}]/;
    var id = element.id;
    if (regex.test(value)) {
        $("#" + id).focus();
        return false;  // FAIL validation when REGEX matches
    } else {
        return true;   // PASS validation otherwise
    };
}, "please use only alphanumeric or alphabetic characters");
jQuery.validator.addMethod("valNot0", function (value, element) {
    var id = element.id;
    if (value == 0 || value == "0") {
        $("#" + id).focus();
        return false;  // FAIL validation when REGEX matches
    } else {
        return true;   // PASS validation otherwise
    };
}, "please use valNot0");
jQuery.validator.addMethod("focusRequire", function (value, element) {
    var id = element.id;
    if (value == null || value == "") {
        $("#" + id).focus();
        return false;
    } else {
        return true;   // PASS validation otherwise
    };
}, "please use focusRequire");
function sendFile(file) {
    var formData = new FormData();
    formData.append('file', $('#fileUpload')[0].files[0]);
    $.ajax({
        type: 'post',
        url: URL_APPLICATION + '/Handler/fileUploader.ashx',
        data: formData,
        success: function (Home) {
            if (Home != 'error') {
                var my_path = Home;
                $("#myUploadedImg").attr("src", URL_APPLICATION + my_path);
                $("#Avatar").val(my_path);
            }
        },
        processData: false,
        HomeType: false,
        error: function () {
            alert("Lỗi!");
        }
    });
}
var _URL = window.URL || window.webkitURL;
$("#fileUpload").on('change', function () {
    var file, img;
    if ((file = this.files[0])) {
        img = new Image();
        img.onload = function () {
            sendFile(file);
        };
        img.onerror = function () {
            alert("Not a valid file:" + file.type);
        };
        img.src = _URL.createObjectURL(file);
    }
});
var autoSizeIframe = function autoSizeIframe() {
    try {
        var mainWrapperHeight = $('body').height();
        var ViewViecCuaToi = $("#popup-view-Home", parent.document.body);
        ViewViecCuaToi.find('iframe').height(mainWrapperHeight);
    } catch (e) {
        // TODO: handle exception
    }
};
var monthHT = new Date().getMonth();
var Year = new Date().getFullYear();
var HomeConfig = {
    pageSize: $.cookie('pageSize') ? $.cookie('pageSize') : 5,
    pageIndex: 1,
}
var Home = {
    init: function () {
        Home.loadData();
        Home.registerEvents();
    },
    registerEvents: function () {
        $('#formTinTuc').validate({
            rules: {
                ID: { required: true, specialChars: true },
                Name: { required: true, specialChars: true }
            },
            messages: {
                ID: { required: "Vui lòng nhập mã", specialChars: "Định dạng không hợp lệ" },
                Name: { required: "Vui lòng nhập tên", specialChars: "Định dạng không hợp lệ" }
            }
        }); 
        $('.btnChuyen').off('click').on('click', function () {
            $("#formTinTuc").submit();
            $("#formChinhSachUuDai").submit();
            //parent.document.location.reload();
        });
        $('.btn-chuyenTinTuc').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            var controller = $(this).data('controller');
            var iframe = "";
            iframe += "<iframe src='" + $('#URL_APPLICATION').val() + "/Admin/" + controller + "/Action/" + id + "' width='100%' height='450px' style='border:none;'></iframe>";
            $('#content-tintuc').html(iframe); 
            $('#modalTinTucAction').modal();
        });
        $('.btn-chuyenChinhSach').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            var controller = $(this).data('controller');
            var iframe = "";
            iframe += "<iframe src='" + $('#URL_APPLICATION').val() + "/Admin/" + controller + "/Action/" + id + "' width='100%' height='450px' style='border:none;'></iframe>";
            $('#content-chinhsach').html(iframe);
            $('#modalChinhSachAction').modal();
        });
        $('.btn-LanhDao-Search').off('click').on('click', function () {
            $("#XaPhuongId").val($("#XaPhuongId").val());// gán giá trị cho biến ẩn
            //$("#searchFromLanhDao").submit();
            var rdDN = $("#rdDN").is(":checked");
            var rdVon = $("#rdVon").is(":checked");
            var rdLaoDong = $("#rdLaoDong").is(":checked");
            Home.getDataBieuDoTron();
            if (rdDN == true) {
                Home.getDataChartColumnAndLineDN();
            }
            if (rdVon == true) {
                Home.getDataChartColumnAndLineVonDN();
            }
            if (rdLaoDong == true) {
                Home.getDataChartColumnAndLineSoLD();
            }
            Home.getTableHoatDong();

        });
        $('#HuyenId').on('change', function () {
            var HuyenId = $('#HuyenId').val();
            Home.getProvinceByParent(HuyenId);
        });
        Home.getDataBieuDoTron();
        Home.getDataChartColumnAndLineDN();
        Home.getTableHoatDong();
        //Home.getDataChartColumnAndLineVonDN();
        //Home.getDataChartColumnAndLineSoLD();
        $('.btn-delete').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            bootbox.confirm("Bạn có chắc chắn muốn xóa bản ghi này không?", function (result) {
                Home.deleteData(id);
            });
        });
        $('#btnDeleteAll').off('click').on('click', function () {
            var values = $(".checkSingle:checked").map(function () { return $(this).data('id'); }).get();
            if (values != "" && values != null) {
                $('#CheckAll').prop('checked', false);
                bootbox.confirm("Bạn có chắc chắn muốn xóa danh sách bản ghi này không?", function (result) {
                    Home.deleteAllData(values);
                });
            } else {
                bootbox.alert("Chọn bản ghi cần xóa");
            }
        });
    },
    deleteAllData: function (ids) {
        $.ajax({
            url: URL_APPLICATION + '/Admin/Home/DeleteMulti',
            data: {
                ids: ids
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.Home == true) {
                    //bootbox.alert("Xóa thành công", function () {
                    window.location.href = window.location.href;
                    //});
                } else {
                    bootbox.alert(response.message);
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    },
    deleteData: function (id) {
        var ids = [];
        ids[0] = id;
        $.ajax({
            url: URL_APPLICATION + '/Admin/Home/DeleteMulti',
            data: {
                ids: ids
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    window.location.href = window.location.href;
                } else {
                    bootbox.alert(response.message);
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    },
    getProvinceByParent: function (HuyenId) {
        var url = URL_APPLICATION + '/Admin/Province/getProvinceByParent?parentid=' + HuyenId
        $.ajax({
            url: url,
            data: "",
            type: 'GET',
            success: function (response) {
                $("#XaPhuongId").html(response);
            },
            error: function (err) {
                console.log(err);
            }
        });
    },
    getDataBieuDoTron: function () {

        var HuyenId = $("#HuyenId").val();
        var XaPhuongId = $("#XaPhuongId").val();
        var FromDate = $("#FromDate").val();
        var ToDate = $("#ToDate").val();
        var LoaiHinh = $("#LoaiHinh").val();
        if (XaPhuongId == '' || XaPhuongId == null) {
            XaPhuongId = 0;
        }
        var LoaiHinhTitle = "";
        var ToanTinh = "";
        var Url = '/Admin/Home/GetDataBieuDoDN';
        if (LoaiHinh == 1) {
            Url = '/Admin/Home/GetDataBieuDoHKD';
            LoaiHinhTitle = "HỘ KINH DOANH";
        }
        else if (LoaiHinh == 2) {
            Url = '/Admin/Home/GetDataBieuDoHTX';
            LoaiHinhTitle = "HỢP TÁC XÃ";
        }
        else {
            Url = '/Admin/Home/GetDataBieuDoDN';
            LoaiHinhTitle = "DOANH NGHIỆP";
        }
        if (HuyenId == "0") {
            ToanTinh = " TỈNH BẾN TRE ";
        } else {
            if (XaPhuongId == 0) {
                ToanTinh = $("#HuyenId option:selected").text().toUpperCase();
            } else {
                ToanTinh = $("#XaPhuongId option:selected").text().toUpperCase();
            }
        }
        $("#headerHo").html("<span style='color:blue;'>TỶ LỆ (%) " + LoaiHinhTitle + " THEO LĨNH VỰC : </span><span style='color:red;'>" + ToanTinh +"</span>");
        $("#headerVon").html("<span style='color:blue;'>TỶ LỆ (%) VỐN " + LoaiHinhTitle + " THEO LĨNH VỰC : </span><span style='color:red;'>" + ToanTinh +"</span>");
        
        $.ajax({
            url: URL_APPLICATION + Url,
            type: "POST",
            data: {
                HuyenId: HuyenId,
                XaPhuongId: XaPhuongId,
                LoaiHinh: LoaiHinh,
                FromDate: FromDate,
                ToDate: ToDate
            },
            dataType: "json",
            success: function (response) {
                if (response.status == true) {
                    var dataChartVon = response.dataVon;
                    var tongVon = response.dataTongVonHKD;
                    var tongHo = response.dataTongHKD;
                    $("#headerTongVon").text("Tổng vốn trên địa bàn: " + tongVon + " triệu đồng");
                    $("#headerTongHo").text("Tổng : " + tongHo + "");
                    var dataVon = [];
                    $.each(dataChartVon, function (i, item) {
                        var obj = { y: item.amount, label: item.title };
                        dataVon.push(obj);
                    });
                    var chartVon = new CanvasJS.Chart("chartVon", {
                        animationEnabled: true,
                        theme: "light2",
                        exportEnabled: false,
                        animationEnabled: true,
                        title: {
                            text: "",
                        },
                        data: [{
                            type: "pie",
                            startAngle: 25,
                            toolTipContent: "<b>{label}</b>: {y}%",
                            indexLabelFontSize: 16,
                            yValueFormatString: "##0.00\"%\"",
                            indexLabel: "{label}",
                            dataPoints: dataVon
                        }]
                    });
                    chartVon.render();
                    //End chart Vốn
                    //Chart HKD
                    var dataChartHKD = response.dataHKD;
                    var dataHKD = [];
                    $.each(dataChartHKD, function (i, item) {
                        var obj = { y: item.amount, label: item.title };
                        dataHKD.push(obj);
                    });
                    var chartHo = new CanvasJS.Chart("chartHo", {
                        animationEnabled: true,
                        theme: "light2",
                        exportEnabled: false,
                        animationEnabled: true,
                        title: {
                            text: ""
                        },
                        data: [{
                            type: "pie",
                            startAngle: 25,
                            toolTipContent: "<b>{label}</b>: {y}%",
                            yValueFormatString: "##0.00\"%\"",
                            indexLabelFontSize: 16,
                            indexLabel: "{label}",
                            dataPoints: dataHKD
                        }]
                    });
                    chartHo.render();
                }
            }
        })
    },
    getDataChartColumnAndLineDN: function () {
        var HuyenId = $("#HuyenId").val();
        var XaPhuongId = $("#XaPhuongId").val();
        var FromDate = $("#FromDate").val();
        var ToDate = $("#ToDate").val();
        var LoaiHinh = $("#LoaiHinh").val();
        var LinhVucId = $("#LinhVuc").val();
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
        if (XaPhuongId == '' || XaPhuongId == null) {
            XaPhuongId = 0;
        }
        var ToanTinh = "";
        if (HuyenId == "0") {
            ToanTinh = " TỈNH BẾN TRE ";
        } else {
            if (XaPhuongId == 0) {
                ToanTinh = $("#HuyenId option:selected").text().toUpperCase();
            } else {
                ToanTinh = $("#XaPhuongId option:selected").text().toUpperCase();
            }
        }
        var obj = {
            HuyenId: HuyenId,
            XaPhuongId: XaPhuongId,
            LoaiHinh: LoaiHinh,
            FromDate: "",
            ToDate: "",
            LinhVucId: LinhVucId
        };
        $.ajax({
            url: URL_APPLICATION + '/Admin/Home/GetDataChartColumnAndLineDN',
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
                    //Chart single Colunm
                    $("#headerChartColumnHo").html("<span style='color:blue;'>BIỂU ĐỒ " + LoaiHinhTitle + " LŨY KẾ ĐẾN NĂM " + Year + " THEO NĂM : <span><span style='color:red;'>" + ToanTinh+ "</span>");
                    var chartColunmHo = new CanvasJS.Chart("chartColunmHo", {
                        animationEnabled: true,
                        theme: "light2", // "light1", "light2", "dark1", "dark2"
                        title: {
                            text: ""
                        },
                        axisY: {
                            title: LoaiHinhTitle
                           
                        },
                        data: [{
                            type: "column",
                            indexLabelPlacement: "outside",
                            indexLabel: "{y}",
                            showInLegend: true,
                            legendMarkerColor: "grey",
                            legendText: "Năm",
                            dataPoints: dataColumnDN
                        }]
                    });
                    chartColunmHo.render();
                    //Line Chart
                    $("#headerChartLineHo").html("<span style='color:blue;'>BIỂU ĐỒ " + LoaiHinhTitle + " LŨY KẾ ĐẾN NĂM " + Year + " THEO NĂM : <span><span style='color:red;'>" + ToanTinh + "</span>");
                    var chartLineHo = new CanvasJS.Chart("chartLineHo", {
                        animationEnabled: true,
                        theme: "light2",
                        title: {
                            text: ""
                        },
                        axisX: {
                            valueFormatString: "YYYY",
                            interval: 1,
                            intervalType: "year"
                        },
                        axisY: {
                            title: LoaiHinhTitle,
                            includeZero: false

                        },
                        data: [{
                            type: "line",
                            indexLabelPlacement: "outside",
                            indexLabel: "{y}",
                            indexLabelFontSize: 16,
                            xValueFormatString: "YYYY",
                            dataPoints: dataLineDN
                        }]
                    });
                    chartLineHo.render();
                    //Line Chart Theo tháng
                    $("#headerChartLineTheoThangHo").html("<span style='color:blue;'>BIỂU ĐỒ " + LoaiHinhTitle + " TỪ NĂM " + Year + " ĐẾN NAY THEO THÁNG : </span><span style='color:red;'>" + ToanTinh + "</span>");
                    var chartLineTheoThangHo = new CanvasJS.Chart("chartLineTheoThangHo", {
                        animationEnabled: true,
                        theme: "light2",
                        title: {
                            text: ""
                        },
                        axisY: {
                            title: LoaiHinhTitle,
                            includeZero: false

                        },
                        data: [{
                            type: "line",
                            indexLabelPlacement: "outside",
                            indexLabel: "{y}",
                            indexLabelFontSize: 16,
                            xValueFormatString: "MM/YYYY",
                            dataPoints: dataLineForMonthDN
                        }]
                    });
                    chartLineTheoThangHo.render();
                    //Chart colunm 4 cột theo quý
                    $("#headerChartColunmTheoQuyHo").html("<span style='color:blue;'>BIỂU ĐỒ " + LoaiHinhTitle + " LŨY KẾ ĐẾN NĂM " + Year + " THEO QUÝ : <span><span style='color:red;'>" + ToanTinh + "</span>");
                    var chartColunmTheoQuyHo = new CanvasJS.Chart("chartColunmTheoQuyHo", {
                        animationEnabled: true,
                        title: {
                            text: ""
                        },
                        axisY: {
                            title: LoaiHinhTitle,
                            includeZero: false,
                            titleFontFamily: "tahoma",
                            titleFontSize: 18,
                            labelFontWeight: "bold"

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
                    chartColunmTheoQuyHo.render();

                    function toggleDataSeries(e) {
                        if (typeof (e.dataSeries.visible) === "undefined" || e.dataSeries.visible) {
                            e.dataSeries.visible = false;
                        }
                        else {
                            e.dataSeries.visible = true;
                        }
                        chartColunmTheoQuyHo.render();
                    }

                    //End chart colunm 4 cột theo quý
                }
            }
        })
    },
    getDataChartColumnAndLineVonDN: function () {
        var HuyenId = $("#HuyenId").val();
        var XaPhuongId = $("#XaPhuongId").val();
        var FromDate = $("#FromDate").val();
        var ToDate = $("#ToDate").val();
        var LoaiHinh = $("#LoaiHinh").val();
        var LinhVucId = $("#LinhVuc").val();
        var LoaiHinhTitle = "";
        if (LoaiHinh == 1) {
            LoaiHinhTitle = "VỐN HỘ KINH DOANH";
        }
        else if (LoaiHinh == 2) {
            LoaiHinhTitle = "VỐN HỢP TÁC XÃ";
        }
        else {
            LoaiHinhTitle = "VỐN DOANH NGHIỆP";
        }
        if (XaPhuongId == '' || XaPhuongId == null) {
            XaPhuongId = 0;
        }
        var ToanTinh = "";
        if (HuyenId == "0") {
            ToanTinh = " TỈNH BẾN TRE ";
        } else {
            if (XaPhuongId == 0) {
                ToanTinh = $("#HuyenId option:selected").text().toUpperCase();
            } else {
                ToanTinh = $("#XaPhuongId option:selected").text().toUpperCase();
            }
        }
        var obj = {
            HuyenId: HuyenId,
            XaPhuongId: XaPhuongId,
            LoaiHinh: LoaiHinh,
            FromDate: "",
            ToDate: "",
            LinhVucId: LinhVucId
        };
        $.ajax({
            url: URL_APPLICATION + '/Admin/Home/GetDataChartColumnAndLineVonDN',
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
                        var objLine = { y: Math.round(item.amount/1000000000), x: new Date(item.title, 0) };
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
                    //console.log(dataColumnQuy4DN);
                    //Chart single Colunm
                    $("#headerChartColumnHo").html("<span style='color:blue;'>BIỂU ĐỒ " + LoaiHinhTitle + " TỪ NĂM " + Year + " ĐẾN NAY THEO THÁNG : </span><span style='color:red;'>" + ToanTinh + "</span>");
                    var chartColunmHo = new CanvasJS.Chart("chartColunmHo", {
                        animationEnabled: true,
                        theme: "light2", // "light1", "light2", "dark1", "dark2"
                        title: {
                            text: ""
                        },
                        axisY: {
                            title: "Tỷ đồng"
                        },
                        data: [{
                            type: "column",
                            indexLabelPlacement: "outside",
                            indexLabel: "{y}",
                            showInLegend: true,
                            legendMarkerColor: "grey",
                            legendText: "Năm",
                            dataPoints: dataColumnDN
                        }]
                    });
                    chartColunmHo.render();
                    //Line Chart
                    $("#headerChartLineHo").html("<span style='color:blue;'>BIỂU ĐỒ " + LoaiHinhTitle + " TỪ NĂM " + Year + " ĐẾN NAY THEO THÁNG : </span><span style='color:red;'>" + ToanTinh + "</span>");
                    var chartLineHo = new CanvasJS.Chart("chartLineHo", {
                        animationEnabled: true,
                        theme: "light2",
                        title: {
                            text: ""
                        },
                        axisX: {
                            valueFormatString: "YYYY",
                            interval: 1,
                            intervalType: "year"
                        },
                        axisY: {
                            title: "Tỷ đồng",
                            includeZero: false

                        },
                        data: [{
                            type: "line",
                            indexLabelPlacement: "outside",
                            indexLabel: "{y}",
                            indexLabelFontSize: 16,
                            xValueFormatString: "YYYY",
                            dataPoints: dataLineDN
                        }]
                    });
                    chartLineHo.render();
                    //Line Chart Theo tháng
                    $("#headerChartLineTheoThangHo").html("<span style='color:blue;'>BIỂU ĐỒ " + LoaiHinhTitle + " TỪ NĂM " + Year + " ĐẾN NAY THEO THÁNG : </span><span style='color:red;'>" + ToanTinh + "</span>");
                    var chartLineTheoThangHo = new CanvasJS.Chart("chartLineTheoThangHo", {
                        animationEnabled: true,
                        theme: "light2",
                        title: {
                            text: ""
                        },
                        axisY: {
                            title: "Tỷ đồng",
                            includeZero: false

                        },
                        data: [{
                            type: "line",
                            indexLabelPlacement: "outside",
                            indexLabel: "{y}",
                            indexLabelFontSize: 16,
                            xValueFormatString: "MM/YYYY",
                            dataPoints: dataLineForMonthDN
                        }]
                    });
                    chartLineTheoThangHo.render();
                    //Chart colunm 4 cột theo quý
                    $("#headerChartColunmTheoQuyHo").html("<span style='color:blue;'>BIỂU ĐỒ " + LoaiHinhTitle + " LŨY KẾ ĐẾN NĂM " + Year + " THEO QUÝ : <span><span style='color:red;'>" + ToanTinh + "</span>");
                    var chartColunmTheoQuyHo = new CanvasJS.Chart("chartColunmTheoQuyHo", {
                        animationEnabled: true,
                        title: {
                            text: ""
                        },
                        axisY: {
                            title: "Tỷ đồng",
                            includeZero: false,
                            titleFontFamily: "tahoma",
                            titleFontSize: 18,
                            labelFontWeight: "bold"

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
                    chartColunmTheoQuyHo.render();

                    function toggleDataSeries(e) {
                        if (typeof (e.dataSeries.visible) === "undefined" || e.dataSeries.visible) {
                            e.dataSeries.visible = false;
                        }
                        else {
                            e.dataSeries.visible = true;
                        }
                        chartColunmTheoQuyHo.render();
                    }

                    //End chart colunm 4 cột theo quý
                }
            }
        })
    },
    getDataChartColumnAndLineSoLD: function () {
        var HuyenId = $("#HuyenId").val();
        var XaPhuongId = $("#XaPhuongId").val();
        var FromDate = $("#FromDate").val();
        var ToDate = $("#ToDate").val();
        var LoaiHinh = $("#LoaiHinh").val();
        var LinhVucId = $("#LinhVuc").val();
        var LoaiHinhTitle = "";
        if (LoaiHinh == 1) {
            LoaiHinhTitle = "SỐ LAO ĐỘNG HỘ KINH DOANH";
        }
        else if (LoaiHinh == 2) {
            LoaiHinhTitle = "SỐ LAO ĐỘNG HỢP TÁC XÃ";
        }
        else {
            LoaiHinhTitle = "SỐ LAO ĐỘNG DOANH NGHIỆP";
        }
        if (XaPhuongId == '' || XaPhuongId == null) {
            XaPhuongId = 0;
        }
        var ToanTinh = "";
        if (HuyenId == "0") {
            ToanTinh = " TỈNH BẾN TRE ";
        } else {
            if (XaPhuongId == 0) {
                ToanTinh = $("#HuyenId option:selected").text().toUpperCase();
            } else {
                ToanTinh = $("#XaPhuongId option:selected").text().toUpperCase();
            }
        }
        var obj = {
            HuyenId: HuyenId,
            XaPhuongId: XaPhuongId,
            LoaiHinh: LoaiHinh,
            FromDate: "",
            ToDate: "",
            LinhVucId: LinhVucId
        };
        $.ajax({
            url: URL_APPLICATION + '/Admin/Home/GetDataChartColumnAndLineSoLD',
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
                    //console.log(dataColumnQuy4DN);
                    //Chart single Colunm
                    $("#headerChartColumnHo").html("<span style='color:blue;'>BIỂU ĐỒ " + LoaiHinhTitle + " LŨY KẾ ĐẾN NĂM " + Year + " THEO NĂM : <span><span style='color:red;'>" + ToanTinh + "</span>");
                    var chartColunmHo = new CanvasJS.Chart("chartColunmHo", {
                        animationEnabled: true,
                        theme: "light2", // "light1", "light2", "dark1", "dark2"
                        title: {
                            text: ""
                        },
                        axisY: {
                            title: "Người"
                        },
                        data: [{
                            type: "column",
                            indexLabelPlacement: "outside",
                            indexLabel: "{y}",
                            showInLegend: true,
                            legendMarkerColor: "grey",
                            legendText: "Năm",
                            dataPoints: dataColumnDN
                        }]
                    });
                    chartColunmHo.render();
                    //Line Chart
                    $("#headerChartLineHo").html("<span style='color:blue;'>BIỂU ĐỒ " + LoaiHinhTitle + " LŨY KẾ ĐẾN NĂM " + Year + " THEO NĂM : <span><span style='color:red;'>" + ToanTinh + "</span>");
                    var chartLineHo = new CanvasJS.Chart("chartLineHo", {
                        animationEnabled: true,
                        theme: "light2",
                        title: {
                            text: ""
                        },
                        axisX: {
                            valueFormatString: "YYYY",
                            interval: 1,
                            intervalType: "year"
                        },
                        axisY: {
                            title: "Người",
                            includeZero: false

                        },
                        data: [{
                            type: "line",
                            indexLabelPlacement: "outside",
                            indexLabel: "{y}",
                            indexLabelFontSize: 16,
                            xValueFormatString: "YYYY",
                            dataPoints: dataLineDN
                        }]
                    });
                    chartLineHo.render();
                    //Line Chart Theo tháng
                    $("#headerChartLineTheoThangHo").html("<span style='color:blue;'>BIỂU ĐỒ " + LoaiHinhTitle + " TỪ NĂM " + Year + " ĐẾN NAY THEO THÁNG : </span><span style='color:red;'>" + ToanTinh + "</span>");
                    var chartLineTheoThangHo = new CanvasJS.Chart("chartLineTheoThangHo", {
                        animationEnabled: true,
                        theme: "light2",
                        title: {
                            text: ""
                        },
                        axisY: {
                            title: "Người",
                            includeZero: false

                        },
                        data: [{
                            type: "line",
                            indexLabelPlacement: "outside",
                            indexLabel: "{y}",
                            indexLabelFontSize: 16,
                            xValueFormatString: "MM/YYYY",
                            dataPoints: dataLineForMonthDN
                        }]
                    });
                    chartLineTheoThangHo.render();
                    //Chart colunm 4 cột theo quý
                    $("#headerChartColunmTheoQuyHo").html("<span style='color:blue;'>BIỂU ĐỒ " + LoaiHinhTitle + " LŨY KẾ ĐẾN NĂM " + Year + " THEO QUÝ : <span><span style='color:red;'>" + ToanTinh + "</span>");
                    var chartColunmTheoQuyHo = new CanvasJS.Chart("chartColunmTheoQuyHo", {
                        animationEnabled: true,
                        title: {
                            text: ""
                        },
                        axisY: {
                            title: "Người",
                            includeZero: false,
                            titleFontFamily: "tahoma",
                            titleFontSize: 18,
                            labelFontWeight: "bold"

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
                    chartColunmTheoQuyHo.render();

                    function toggleDataSeries(e) {
                        if (typeof (e.dataSeries.visible) === "undefined" || e.dataSeries.visible) {
                            e.dataSeries.visible = false;
                        }
                        else {
                            e.dataSeries.visible = true;
                        }
                        chartColunmTheoQuyHo.render();
                    }

                    //End chart colunm 4 cột theo quý
                }
            }
        })
    },
    getTableHoatDong: function () {
        var LoaiHinh = $("#LoaiHinh").val();
        if (LoaiHinh == 1) {
            $("#headerTableHoatDong").html("<span style='color:blue;'>TÌNH HÌNH HOẠT ĐỘNG HỘ KINH DOANH</span>");
        }
        else if (LoaiHinh == 2) {
            $("#headerTableHoatDong").html("<span style='color:blue;'>TÌNH HÌNH HOẠT ĐỘNG HỢP TÁC XÃ</span>");
        }
        else {
            $("#headerTableHoatDong").html("<span style='color:blue;'>TÌNH HÌNH HOẠT ĐỘNG DOANH NGHIỆP</span>");
        }
        $.ajax({
            url: URL_APPLICATION + '/Admin/Home/GetTableHoatDong',
            type: "POST",
            data: {
                LoaiHinh: LoaiHinh
            },
            dataType: "json",
            success: function (response) {
                if (response.status == true) {
                    var thead = "";
                    if (LoaiHinh == 1) {
                        thead = "<tr class='lanhdao text-center'>" +
                            "<th width='25%' > Địa bàn</th >" +
                            "<th width = '15%' > Cấp mới</th > " +
                            "<th width = '15%' > Cấp đổi</th > " +
                            "<th width = '15%' > Cấp lại</th > " +
                            "<th width = '15%' > Tạm ngưng</th > " +
                            "<th width = '15%' > Chấm dứt</th > " +
                            "</tr >";
                    } else {
                        thead = "<tr class='lanhdao text-center'>" +
                                    "<th width='75%'>Địa bàn</th>" +
                                    "<th width='25%'>Đang hoạt động</th>" +
                                "</tr>";
                    }
                    var tableContent = response.dataTableContent;
                    $(".lanhdao thead").html(thead);
                    $(".lanhdao tbody").html(tableContent);
                }
            }
        })
    },
    loadData: function () {
        $('#filter_count_perpage').val(HomeConfig.pageSize);
    },
    removeParams: function (sParam) {
        var url = window.location.href.split('?')[0] + '?';
        var sPageURL = decodeURIComponent(window.location.search.substring(1)),
            sURLVariables = sPageURL.split('&'),
            sParameterName,
            i;
        for (i = 0; i < sURLVariables.length; i++) {
            sParameterName = sURLVariables[i].split('=');
            if (sParameterName[0] != sParam) {
                url = url + sParameterName[0] + '=' + sParameterName[1] + '&'
            }
        }
        return url.substring(0, url.length - 1);
    }
}
Home.init();