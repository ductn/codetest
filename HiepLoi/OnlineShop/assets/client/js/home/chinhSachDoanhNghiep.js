var URL_APPLICATION = $('#URL_APPLICATION').val();
var ChinhSachDoanhNghiepPortal = {
    pageSize: 7,
    pageIndex: 1,
}
var ChinhSachDoanhNghiepPortal = {
    init: function () {
        ChinhSachDoanhNghiepPortal.loadData();
        ChinhSachDoanhNghiepPortal.registerEvents();
    },
    registerEvents: function () {
        $('#huyenDN').on('change', function () {
            ChinhSachDoanhNghiepPortal.init();
        });
    },
    loadData: function (changePageSize) {
        var huyenID = $('#huyenDN').val();
        //var searh = $('#nameDNChinhSach').val();
        var searh = "";
        var duLieu = {
            huyenID: huyenID,
            searh: searh
        };
        $.ajax({
            url: URL_APPLICATION + '/ThongTinDoanhNghiep/LoadChinhSachUuDai',
            type: 'GET',
            data: {
                strSearch: JSON.stringify(duLieu),
                page: DoanhNghiepPortalConfig.pageIndex,
                pageSize: DoanhNghiepPortalConfig.pageSize
            },
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    var data = response.data;
                    var html = '';
                    var template = $('#data-template-index-dnChinhSach').html();
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
                    $('#tblDataIndexDNChinhSach').html(html);
                    $('.totalDNChinhSach').html(response.total.format());
                    ChinhSachDoanhNghiepPortal.paging(response.total, function () {
                        ChinhSachDoanhNghiepPortal.loadData();
                    }, changePageSize);
                    ChinhSachDoanhNghiepPortal.registerEvent();
                }
            }
        });
    },
    paging: function (totalRow, callback, changePageSize) {
        var totalPage = Math.ceil(totalRow / DoanhNghiepPortalConfig.pageSize);

        //Unbind pagination if it existed or click change pagesize
        if ($('#pagination-index-dnChinhSach a').length === 0 || changePageSize) {
            $('#pagination-index-dnChinhSach').empty();
            $('#pagination-index-dnChinhSach').removeData("twbs-pagination");
            $('#pagination-index-dnChinhSach').unbind("page");
        }

        $('#pagination-index-dnChinhSach').twbsPagination({
            totalPages: totalPage,
            visiblePages: 5,
            first: '<span aria-hidden="true">&laquo;</span>',
            next: 'Tiếp',
            last: '<span aria-hidden="true">&raquo;</span>',
            prev: 'Trước',
            onPageClick: function (event, page) {
                DoanhNghiepPortalConfig.pageIndex = page;
                setTimeout(callback, 200);
            }
        });
    },
}
ChinhSachDoanhNghiepPortal.init();