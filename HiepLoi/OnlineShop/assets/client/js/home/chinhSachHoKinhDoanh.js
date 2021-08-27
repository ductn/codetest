var URL_APPLICATION = $('#URL_APPLICATION').val();
var ChinhSachHoKinhDoanhPortal = {
    pageSize: 7,
    pageIndex: 1,
}
var ChinhSachHoKinhDoanhPortal = {
    init: function () {
        ChinhSachHoKinhDoanhPortal.loadData();
        ChinhSachHoKinhDoanhPortal.registerEvents();
    },
    registerEvents: function () {
        $('#huyenHKD').on('change', function () {
            ChinhSachHoKinhDoanhPortal.init();
        });
    },
    loadData: function (changePageSize) {
        var huyenID = $('#huyenHKD').val();
        //var searh = $('#nameHKDChinhSach').val();
        var searh = "";
        var duLieu = {
            huyenID: huyenID,
            searh: searh
        };
        $.ajax({
            url: URL_APPLICATION + '/ThongTinHoKinhDoanh/LoadChinhSachUuDai',
            type: 'GET',
            data: {
                strSearch: JSON.stringify(duLieu),
                page: HoKinhDoanhPortalConfig.pageIndex,
                pageSize: HoKinhDoanhPortalConfig.pageSize
            },
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    var data = response.data;
                    var html = '';
                    var template = $('#data-template-index-HKDChinhSach').html();
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
                    $('#tblDataIndexHKDChinhSach').html(html);
                    $('.totalHKDChinhSach').html(response.total.format());
                    ChinhSachHoKinhDoanhPortal.paging(response.total, function () {
                        ChinhSachHoKinhDoanhPortal.loadData();
                    }, changePageSize);
                    ChinhSachHoKinhDoanhPortal.registerEvent();
                }
            }
        });
    },
    paging: function (totalRow, callback, changePageSize) {
        var totalPage = Math.ceil(totalRow / HoKinhDoanhPortalConfig.pageSize);

        //Unbind pagination if it existed or click change pagesize
        if ($('#pagination-index-HKDChinhSach a').length === 0 || changePageSize) {
            $('#pagination-index-HKDChinhSach').empty();
            $('#pagination-index-HKDChinhSach').removeData("twbs-pagination");
            $('#pagination-index-HKDChinhSach').unbind("page");
        }

        $('#pagination-index-HKDChinhSach').twbsPagination({
            totalPages: totalPage,
            visiblePages: 5,
            first: '<span aria-hidden="true">&laquo;</span>',
            next: 'Tiếp',
            last: '<span aria-hidden="true">&raquo;</span>',
            prev: 'Trước',
            onPageClick: function (event, page) {
                HoKinhDoanhPortalConfig.pageIndex = page;
                setTimeout(callback, 200);
            }
        });
    },
}
ChinhSachHoKinhDoanhPortal.init();