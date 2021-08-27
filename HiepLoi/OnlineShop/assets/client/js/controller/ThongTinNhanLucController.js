var URL_APPLICATION = $('#URL_APPLICATION').val();
var ThongTinNhanLucConfig = {
    pageSize: 9,
    pageIndex: 1,
}
var ThongTinNhanLucController = {
    init: function () {
        ThongTinNhanLucController.loadData();
        ThongTinNhanLucController.registerEvent();
    },
    registerEvent: function () {
        
    },
    loadData: function (changePageSize) {
        try {
            var json = {
                page: ThongTinNhanLucConfig.pageIndex,
                pageSize: ThongTinNhanLucConfig.pageSize
            };
            var url = URL_APPLICATION + '/ThongTinNhanLuc/GetDataPaging';
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
                        $.each(items, function (i, item) {
                            var Image = URL_APPLICATION + item.Image;
                            var UrlDocThem = URL_APPLICATION + "/chi-tiet-thong-tin-nhan-luc/" + item.QueryString + "-" + item.Id + ".html" ;
                            var d = item.NgayXuatBan.replace("/Date(", "").replace(")/", "") * 1;
                            var date = new Date(d);
                            var newDate = moment(date);
                            var NgayXuatBan = newDate.format('DD/MM/YYYY');
                            html += Mustache.render(template, {
                                Image: Image,
                                NgayXuatBan: NgayXuatBan,
                                TieuDe: item.TieuDe,
                                UrlDocThem: UrlDocThem,
                            });
                        });

                        $('#tblDataIndex').html(html);
                        $('#total_count_row').html(total);
                        $('#from_record').html((((ThongTinNhanLucConfig.pageIndex - 1) * ThongTinNhanLucConfig.pageSize) + 1));
                        $('#to_record').html((((ThongTinNhanLucConfig.pageIndex - 1) * ThongTinNhanLucConfig.pageSize) + grd_row));
                        $('#filter_count_perpage').val(ThongTinNhanLucConfig.pageSize);
                        ThongTinNhanLucController.paging(total, function () {
                            ThongTinNhanLucController.loadData();
                        }, changePageSize);
                        ThongTinNhanLucController.registerEvent();
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
        var totalPage = Math.ceil(totalRow / ThongTinNhanLucConfig.pageSize);
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
                ThongTinNhanLucConfig.pageIndex = page;
                setTimeout(callback, 200);
            }
        });
    }
}
ThongTinNhanLucController.init();
