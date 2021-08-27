$(document).ready(function () {
    $('body').on('change', '#capCoQuan', function () {
        GetDataCoQuanBanHanh('capCoQuan', 'coQuanThucHien');
    });
    $('body').on('change', '#coQuanThucHien', function () {
        ChinhSachUuDaiController.init();
    });
});
var URL_APPLICATION = $('#URL_APPLICATION').val();
function GetDataCoQuanBanHanh(idparent, idsub) {
    var valP = $('#' + idparent).val();
    var url = URL_APPLICATION +  '/ChinhSachUuDai/GetDataCoQuanBanHanh';
    var json = { IsDiaPhuong: valP };
    $.ajax({
        url: url,
        type: 'POST',
        data: json,
        dataType: "json",
        success: function (response) {
            if (response.status == true) {
                var data = response.data;
                var option = "";
                if (data != null && data != "") {
                    $.each(data, function (i, item) {
                        option += '<option value="' + item.Code + '">' + item.Title + '</option>';
                    });
                } else {
                    option += '<option value="0">--Chọn--</option>';
                }
                $('#' + idsub).html(option);
                ChinhSachUuDaiController.init();
            }
        },
        error: function (err) {
            console.log(err);
        }
    });
}
var ChinhSachUuDaiConfig = {
    pageSize: 5,
    pageIndex: 1,
}
var ChinhSachUuDaiController = {
    init: function () {
        ChinhSachUuDaiController.loadData();
        ChinhSachUuDaiController.registerEvent();
    },
    registerEvent: function () {
        
    },
    loadData: function (changePageSize) {
        try {
            var DonViID = $('#coQuanThucHien').val();
            var username = null;
            var chkUs = $('#username').val();
            if (chkUs != null && chkUs != "") {
                username = chkUs;
            }
            var json = {
                DonViID: DonViID,
                username: username,
                StatusId: 12,
                page: ChinhSachUuDaiConfig.pageIndex,
                pageSize: ChinhSachUuDaiConfig.pageSize
            };
            var url = URL_APPLICATION + '/ChinhSachUuDai/GetDataPaging';
            $.ajax({
                url: url,
                type: 'POST',
                data: json,
                dataType: "json",
                success: function (response) {
                    if (response.status == true) {
                        var grd_row = '';
                        var html = '';
                        var template = $('#data-template-index').html();
                        var items = response.data;
                        var total = response.total;
                        var icount = 1;
                        $.each(items, function (i, item) {
                            var d = item.NgayBanHanh.replace("/Date(", "").replace(")/", "") * 1;
                            var date = new Date(d);
                            var newDate = moment(date);
                            var NgayBanHanh = newDate.format('DD/MM/YYYY');
                            var UrlFile = "";
                            if (item.TapTinDinhKem != null && item.TapTinDinhKem != ""){
                                UrlFile = URL_APPLICATION + JSON.parse(item.TapTinDinhKem.replace(/'/g, '"'))[0].urldownload;
                            }
                            html += Mustache.render(template, {
                                STT: icount,
                                SoHieu: item.SoHieuVanBan,
                                NgayBanHanh: NgayBanHanh,
                                TrichYeu: item.TrichYeu,
                                UrlFile: UrlFile
                            });
                            icount++;
                        });

                        $('#tblDataIndex').html(html);
                        $('#total_count_row').html(total);
                        $('#from_record').html((((ChinhSachUuDaiConfig.pageIndex - 1) * ChinhSachUuDaiConfig.pageSize) + 1));
                        $('#to_record').html((((ChinhSachUuDaiConfig.pageIndex - 1) * ChinhSachUuDaiConfig.pageSize) + grd_row));
                        $('#filter_count_perpage').val(ChinhSachUuDaiConfig.pageSize);
                        ChinhSachUuDaiController.paging(total, function () {
                            ChinhSachUuDaiController.loadData();
                        }, changePageSize);
                        ChinhSachUuDaiController.registerEvent();
                    }
                },
                error: function (data, Status) {
                    console.log(data.responseText);
                }
            });
        } catch (e) {
            // TODO: handle exception
        }
    },
    paging: function (totalRow, callback, changePageSize) {
        var totalPage = Math.ceil(totalRow / ChinhSachUuDaiConfig.pageSize);
        //Unbind pagination if it existed or click change pagesize
        if ($('#pagination-index a').length === 0 || totalPage === 0) {
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
                ChinhSachUuDaiConfig.pageIndex = page;
                setTimeout(callback, 200);
            }
        });
    }
}
ChinhSachUuDaiController.init();
