var URL_APPLICATION = $('#URL_APPLICATION').val();
$(document).ready(function () {
    $('body').on('click', '#btnSerch', function () {
        NganhNgheCoDieuKienController.init();
    });
});
var NganhNgheCoDieuKienConfig = {
    pageSize: 10,
    pageIndex: 1,
}
var NganhNgheCoDieuKienController = {
    init: function () {
        NganhNgheCoDieuKienController.loadData();
        NganhNgheCoDieuKienController.registerEvent();
    },
    registerEvent: function () {
        
    },
    loadData: function (changePageSize) {
        try {
            var maNganhNghe = $('#maNganhNghe').val();
            var tenNganhNghe = $('#tenNganhNghe').val();
            var json = {
                maNganhNghe: maNganhNghe,
                tenNganhNghe: tenNganhNghe,
                page: NganhNgheCoDieuKienConfig.pageIndex,
                pageSize: NganhNgheCoDieuKienConfig.pageSize
            };
            var url = URL_APPLICATION + '/NganhNgheCoDieuKien/GetDataPaging';
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
                            var urlLink = URL_APPLICATION + "/chi-tiet-nganh-nghe-co-dieu-kien/" + item.Id;
                            html += Mustache.render(template, {
                                TenNganh: item.Name,
                                VanBanHuongDan: item.VanBanHuongDan,
                                urlLink: urlLink
                            });
                        });

                        $('#tblDataIndex').html(html);
                        $('#total_count_row').html(total);
                        $('#from_record').html((((NganhNgheCoDieuKienConfig.pageIndex - 1) * NganhNgheCoDieuKienConfig.pageSize) + 1));
                        $('#to_record').html((((NganhNgheCoDieuKienConfig.pageIndex - 1) * NganhNgheCoDieuKienConfig.pageSize) + grd_row));
                        $('#filter_count_perpage').val(NganhNgheCoDieuKienConfig.pageSize);
                        NganhNgheCoDieuKienController.paging(total, function () {
                            NganhNgheCoDieuKienController.loadData();
                        }, changePageSize);
                        NganhNgheCoDieuKienController.registerEvent();
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
        var totalPage = Math.ceil(totalRow / NganhNgheCoDieuKienConfig.pageSize);
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
                NganhNgheCoDieuKienConfig.pageIndex = page;
                setTimeout(callback, 200);
            }
        });
    }
}
NganhNgheCoDieuKienController.init();
