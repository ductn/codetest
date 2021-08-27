$(document).ready(function () {
    $('body').on('change', '#capCoQuan', function () {
        GetDataCoQuanBanHanh('capCoQuan', 'coQuanThucHien');
    });
    $('body').on('change', '#coQuanThucHien', function () {
        ContentController.init();
    });
});
var URL_APPLICATION = $('#URL_APPLICATION').val();
function GetDataCoQuanBanHanh(idparent, idsub) {
    var valP = $('#' + idparent).val();
    var url = URL_APPLICATION +  '/Content/GetDataCoQuanBanHanh';
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
                ContentController.init();
            }
        },
        error: function (err) {
            console.log(err);
        }
    });
}
var ContentConfig = {
    pageSize: 9,
    pageIndex: 1,
}
var ContentController = {
    init: function () {
        ContentController.loadData();
        ContentController.registerEvent();
    },
    registerEvent: function () {
        
    },
    loadData: function (changePageSize) {
        try {
            var DonViID = $('#coQuanThucHien').val();
            var json = {
                DonViID: DonViID,
                StatusId: 12,
                page: ContentConfig.pageIndex,
                pageSize: ContentConfig.pageSize
            };
            var url = URL_APPLICATION + '/Content/GetDataPaging';
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
                            try {
                                var d = item.CreatedDate.replace("/Date(", "").replace(")/", "") * 1;
                                var date = new Date(d);
                                var newDate = moment(date);
                                var NgayBanHanh = newDate.format('DD/MM/YYYY');
                                var UrlLink = URL_APPLICATION + "/tin-tuc/" + item.MetaTitle + "-" + item.ID + ".html";
                                var UrlImage = URL_APPLICATION + "/" + item.Image;
                                html += Mustache.render(template, {
                                    Image: UrlImage,
                                    NgayTao: NgayBanHanh,
                                    Name: item.Name,
                                    Link: UrlLink
                                });
                            } catch (e) {

                            }
                        });

                        $('#tblDataIndex').html(html);
                        $('#total_count_row').html(total);
                        $('#from_record').html((((ContentConfig.pageIndex - 1) * ContentConfig.pageSize) + 1));
                        $('#to_record').html((((ContentConfig.pageIndex - 1) * ContentConfig.pageSize) + grd_row));
                        $('#filter_count_perpage').val(ContentConfig.pageSize);
                        ContentController.paging(total, function () {
                            ContentController.loadData();
                        }, changePageSize);
                        ContentController.registerEvent();
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
        var totalPage = Math.ceil(totalRow / ContentConfig.pageSize);
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
                ContentConfig.pageIndex = page;
                setTimeout(callback, 200);
            }
        });
    }
}
ContentController.init();
