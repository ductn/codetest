var URL_APPLICATION = $('#URL_APPLICATION').val();
function GetDaTa(idcontent) {
    var url = URL_APPLICATION + '/BieuMauDangKyDauTu/GetData';
    var obj = {ParentId: 0 };
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
                    var obj = {ParentId: item.Id };
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
var BieuMauDangKyDauTuController = {
    init: function () {
        BieuMauDangKyDauTuController.registerEvent();
    },
    registerEvent: function () {
       
        GetDaTa('contentDauTu');

    },
}
BieuMauDangKyDauTuController.init();
