
$(document).ready(function () {
    var URL_APPLICATION = $('#URL_APPLICATION').val();
    function GetDataCauHoi() {
        var json = {};
        var url = URL_APPLICATION + '/HoiDap/GetDataCauHoi';
        $.ajax({
            url: url,
            type: 'POST',
            data: json,
            dataType: "json",
            success: function (response) {
                if (response.status == true) {
                    var items = response.data;
                    var html = '';
                    $.each(items, function (i, item) {
                        var id = item.Id;
                        var title = item.Title;
                        var promise = GetDataTraLoi(id);
                        var titleSub = '';
                        var template = $('#data-template-index').html();
                        promise.then(function (data) {
                            var itemSubs = data.data;
                            $.each(itemSubs, function (iSub, itemSub) {
                                titleSub += itemSub.Title + " <br/><br/>";
                            });
                            html += Mustache.render(template, {
                                Id: id,
                                titleParent: title,
                                titleSub: titleSub
                            });
                            $('#accordionEx').html(html);
                        });
                    });
                }
            },
            error: function (data, Status) {
                console.log(data.responseText);
            }
        });
    }
    function GetDataTraLoi(id) {
        var json = {
            idCauHoi: id
        };
        var url = URL_APPLICATION + '/HoiDap/GetDataTraLoi';
        return $.ajax({
            url: url,
            type: 'POST',
            data: json,
            dataType: "json",

        });
    }
    GetDataCauHoi();
});
