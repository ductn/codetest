var URL_APPLICATION = $('#URL_APPLICATION').val();
var ChinhSachHopTacXaPortal = {
    pageSize: 7,
    pageIndex: 1,
}
var ChinhSachHopTacXaPortal = {
    init: function () {
        ChinhSachHopTacXaPortal.loadData();
        ChinhSachHopTacXaPortal.registerEvents();
    },
    registerEvents: function () {
        $('#huyenHTX').on('change', function () {
            ChinhSachHopTacXaPortal.init();
        });
    },
    loadData: function (changePageSize) {
        var huyenID = $('#huyenHTX').val();
        //var searh = $('#nameHTXChinhSach').val();
        var searh = "";
        var duLieu = {
            huyenID: huyenID,
            searh: searh
        };
        $.ajax({
            url: URL_APPLICATION + '/ThongTinHopTacXa/LoadChinhSachUuDai',
            type: 'GET',
            data: {
                strSearch: JSON.stringify(duLieu),
                page: HopTacXaPortalConfig.pageIndex,
                pageSize: HopTacXaPortalConfig.pageSize
            },
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    var data = response.data;
                    var html = '';
                    var template = $('#data-template-index-HTXChinhSach').html();
                    $.each(data, function (i, item) {
                        var d = item.NgayBanHanh.replace("/Date(", "").replace(")/", "") * 1;
                        var date = new Date(d);
                        var newDate = moment(date);
                        var NgayBanHanh = newDate.format('DD/MM/YYYY');
                        var UrlFile = "";
                        if (item.TapTinDinhKem != null && item.TapTinDinhKem != "") {
                            UrlFile = URL_APPLICATION + JSON.parse(item.TapTinDinhKem.replace(/'/g, '"'))[0].urldownload;
                        }
                        html += Mustache.render(template, {
                            SoHieuVanBan: item.SoHieuVanBan,
                            NgayBanHanh: NgayBanHanh,
                            TrichYeu: item.TrichYeu,
                            UrlFile: UrlFile
                        });
                    });
                    $('#tblDataIndexHTXChinhSach').html(html);
                    $('.totalHTXChinhSach').html(response.total.format());
                    ChinhSachHopTacXaPortal.paging(response.total, function () {
                        ChinhSachHopTacXaPortal.loadData();
                    }, changePageSize);
                    ChinhSachHopTacXaPortal.registerEvent();
                }
            }
        });
    },
    paging: function (totalRow, callback, changePageSize) {
        var totalPage = Math.ceil(totalRow / HopTacXaPortalConfig.pageSize);

        //Unbind pagination if it existed or click change pagesize
        if ($('#pagination-index-HTXChinhSach a').length === 0 || changePageSize) {
            $('#pagination-index-HTXChinhSach').empty();
            $('#pagination-index-HTXChinhSach').removeData("twbs-pagination");
            $('#pagination-index-HTXChinhSach').unbind("page");
        }

        $('#pagination-index-HTXChinhSach').twbsPagination({
            totalPages: totalPage,
            visiblePages: 5,
            first: '<span aria-hidden="true">&laquo;</span>',
            next: 'Tiếp',
            last: '<span aria-hidden="true">&raquo;</span>',
            prev: 'Trước',
            onPageClick: function (event, page) {
                HopTacXaPortalConfig.pageIndex = page;
                setTimeout(callback, 200);
            }
        });
    },
}
ChinhSachHopTacXaPortal.init();