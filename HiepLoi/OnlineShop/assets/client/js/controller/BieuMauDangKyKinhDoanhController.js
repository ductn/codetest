var URL_APPLICATION = $('#URL_APPLICATION').val();
function GetDaTa(loaiBieuMau, idcontent) {
    var url = URL_APPLICATION +  '/BieuMauDangKyKinhDoanh/GetData';
    var obj = { loaiBieuMau: loaiBieuMau, ParentId: 0 };
    $.ajax({
        url: url,
        type: 'POST',
        data: obj,
        dataType: "json",
        success: function (response) {
            if (response.status == true) {
                var data = response.data;
                $.each(data, function (i, item) {
                    var htmlParent = "";
                    var htmlSub = "";
                    var obj = { loaiBieuMau: loaiBieuMau, ParentId: item.Id };
                    $.ajax({
                        url: url,
                        type: 'POST',
                        data: obj,
                        dataType: "json",
                        success: function (response) {
                            if (response.status == true) {
                                htmlParent = '<tr class="bg-row"><td colspan="3" class="p-3 count-key">' + item.Title + '</td><tr>';
                                var data = response.data;
                                $.each(data, function (iSub, itemSub) {
                                    htmlSub += '<tr><td class="text-center count"></td><td class="text-left">' + itemSub.Title + '</td>';
                                    htmlSub += '<td class="text-center" >'
                                    if (itemSub.LinkFile != null && itemSub.LinkFile != '') {
                                        htmlSub += '<a href = "' + URL_APPLICATION + "/" + itemSub.LinkFile + '" > <img style="width: 30px;height: 30px;" alt="" src="' + URL_APPLICATION + '/MediaUploader/pdf.png"></a>';
                                    }
                                    htmlSub += '</td ><tr>';
                                });
                                $('#' + idcontent).append(htmlParent + htmlSub);
                            }
                        },
                        error: function (err) {
                            console.log(err);
                        }
                    });
                });
            }
        },
        error: function (err) {
            console.log(err);
        }
    });
}

function GetDaTaII(loaiBieuMau, idcontent) {
    var url = URL_APPLICATION + '/BieuMauDangKyKinhDoanh/GetData';
    var obj = { loaiBieuMau: loaiBieuMau, ParentId: 0 };
    $.ajax({
        url: url,
        type: 'POST',
        data: obj,
        dataType: "json",
        success: function (response) {
            if (response.status == true) {
                var data = response.data;
                $.each(data, function (i, item) {
                    var htmlParent = '<h5 class="text-success text-uppercase mt-4"><i class="fa fa-angle-double-right mr-3"></i>' + item.Title +'</h5>';
                    var htmlItem = "";
                    var obj = { loaiBieuMau: loaiBieuMau, ParentId: item.Id };
                    $.ajax({
                        url: url,
                        type: 'POST',
                        data: obj,
                        dataType: "json",
                        success: function (response) {
                            if (response.status == true) {
                                var data = response.data;
                                $.each(data, function (iSub, itemSub) {
                                    var obj = { loaiBieuMau: loaiBieuMau, ParentId: itemSub.Id };
                                    var htmlSub2 = "";
                                    var htmlSub = "";
                                    $.ajax({
                                        url: url,
                                        type: 'POST',
                                        data: obj,
                                        dataType: "json",
                                        success: function (response) {
                                            if (response.status == true) {
                                                htmlSub = '<td colspan="3" class="p-3 text-center">' + itemSub.Title + '</td><tr>';
                                                var data = response.data;
                                                $.each(data, function (iSub2, itemSub2) {
                                                    htmlSub2 += '<tr><td class="text-center count"></td><td class="text-left">' + itemSub2.Title + '</td>';
                                                    htmlSub2 += '<td class="text-center" >'
                                                    if (itemSub2.LinkFile != null && itemSub2.LinkFile != '') {
                                                        htmlSub2 += '<a href = "' + URL_APPLICATION + "/" + itemSub2.LinkFile + '" > <img style="width: 30px;height: 30px;" alt="" src="' + URL_APPLICATION + '/MediaUploader/pdf.png"></a>';
                                                    }
                                                    htmlSub2 += '</td ><tr>';
                                                });
                                                htmlItem += '<div class="table border bg-white tb-list mt-3 table-striped ">';
                                                    htmlItem += '<table class="table border bg-white mt-3 table-striped document">';
                                                        htmlItem += '<thead>';
                                                            htmlItem += '<tr class="font-weight-bold">';
                                                                htmlItem += '<th class="text-center">STT</th>';
                                                                htmlItem += '<th>Loại hình</th>';
                                                                htmlItem += '<th class="text-center">Ký hiệu</th>';
                                                            htmlItem += '</tr>';
                                                        htmlItem += '</thead>';
                                                        htmlItem += '<tbody>';
                                                            htmlItem += '<tr class="bg-row">';
                                                                htmlItem += htmlSub;
                                                            htmlItem += '</tr>';
                                                            htmlItem += '<tr>';
                                                                htmlItem += htmlSub2;
                                                            htmlItem += '</tr>';
                                                        htmlItem += '</tbody>';
                                                    htmlItem += '</table>';
                                                htmlItem += '</div>';
                                            }
                                        },
                                        error: function (err) {
                                            console.log(err);
                                        }
                                    });
                                });
                            }
                        },
                        error: function (err) {
                            console.log(err);
                        }
                    });
                    setTimeout(function () {
                        $('#' + idcontent).append(htmlParent + htmlItem);
                    }, 3000);
                });
            }
        },
        error: function (err) {
            console.log(err);
        }
    });
}
var BieuMauDangKyKinhDoanhController = {
    init: function () {
        BieuMauDangKyKinhDoanhController.registerEvent();
    },
    registerEvent: function () {
       
        var loaiBieuMau = '1';
        GetDaTa(loaiBieuMau, "contentBM");
        loaiBieuMau = '2';
        GetDaTa(loaiBieuMau, "contentDKM");
        loaiBieuMau = '3';
        GetDaTaII(loaiBieuMau, "contentTD");
        loaiBieuMau = '4';
        GetDaTa(loaiBieuMau, "contentGTTN");

    },
}
BieuMauDangKyKinhDoanhController.init();
