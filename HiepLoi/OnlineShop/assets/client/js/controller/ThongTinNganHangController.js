var URL_APPLICATION = $('#URL_APPLICATION').val();
var ThongTinNganHangConfig = {
    pageSize: 9,
    pageIndex: 1,
}
var ThongTinNganHangController = {
    init: function () {
        ThongTinNganHangController.loadData();
        ThongTinNganHangController.registerEvent();
    },
    registerEvent: function () {
        
    },
    loadData: function (changePageSize) {
        try {
            var json = {
                page: ThongTinNganHangConfig.pageIndex,
                pageSize: ThongTinNganHangConfig.pageSize
            };
            var url = URL_APPLICATION + '/ThongTinNganHang/GetDataPaging';
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
                            html += Mustache.render(template, {
                                Image: Image,
                                TieuDe: item.TieuDe,
                                SDT: item.SDT,
                                DiaChi: item.DiaChi,
                                Email: item.Email,
                            });
                        });

                        $('#tblDataIndex').html(html);
                        $('#total_count_row').html(total);
                        $('#from_record').html((((ThongTinNganHangConfig.pageIndex - 1) * ThongTinNganHangConfig.pageSize) + 1));
                        $('#to_record').html((((ThongTinNganHangConfig.pageIndex - 1) * ThongTinNganHangConfig.pageSize) + grd_row));
                        $('#filter_count_perpage').val(ThongTinNganHangConfig.pageSize);
                        ThongTinNganHangController.paging(total, function () {
                            ThongTinNganHangController.loadData();
                        }, changePageSize);
                        ThongTinNganHangController.registerEvent();
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
        var totalPage = Math.ceil(totalRow / ThongTinNganHangConfig.pageSize);
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
                ThongTinNganHangConfig.pageIndex = page;
                setTimeout(callback, 200);
            }
        });
    }
}
ThongTinNganHangController.init();
