var URL_APPLICATION = $('#URL_APPLICATION').val();
var LangNghePortalConfig = {
    pageSize: 7,
    pageIndex: 1,
}
var LangNghePortal = {
    init: function () {
        LangNghePortal.loadData();
        LangNghePortal.registerEvents();
    },
    registerEvents: function () {
        $('#huyenLangNghe').on('change', function () {
            var HuyenId = $('#huyenLangNghe').val();
            LangNghePortal.getProvinceByParent(HuyenId, 'xaLangNghe');
        });
        $('#xaLangNghe').on('change', function () {
            LangNghePortal.loadData(true);
        });
        $("body").on('click', '#tblDataIndexLangNghe .ViewDetail', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            LangNghePortal.getThongTinLangNgheInfor(id);
        });
        $('#searchLangNghe').on('click', function () {
            LangNghePortal.loadData(true);
        });
    },
    loadData: function (changePageSize) {
        var huyenID = $('#huyenLangNghe').val();
        var titleLangNghe = $("#huyenLangNghe :selected").text();
        if (huyenID == 0) {
            titleLangNghe = "Tỉnh bến tre";
        }
        var xaID = $('#xaLangNghe').val();
        if (xaID != 0 && xaID != null) {
            titleLangNghe = $("#xaLangNghe :selected").text()
        }
        var searh = $('#nameLangNghe').val();
        var url = new URL(window.location.href);
        var c = url.searchParams.get("searchString");
        if (c != null && c != "") {
            searh = c;
        }
        var duLieu = {
            huyenID: huyenID,
            xaID: xaID,
            searh: searh
        };
        $.ajax({
            url: URL_APPLICATION + '/Home/LoadDataLangNghe',
            type: 'GET',
            data: {
                strSearch: JSON.stringify(duLieu),
                page: LangNghePortalConfig.pageIndex,
                pageSize: LangNghePortalConfig.pageSize
            },
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    var data = response.data;
                    var html = '';
                    var template = $('#data-template-index-LangNghe').html();
                    $.each(data, function (i, item) {
                        var loaiLN = "";
                        if (item.LoaiLangNghe == "1") {
                            loaiLN = "Làng nghề";
                        }
                        if (item.LoaiLangNghe == "2") {
                            loaiLN = "LN truyền thống";
                        }
                        html += Mustache.render(template, {
                            ID: item.Id,
                            TenLangNghe: item.TenLangNghe,
                            LoaiLangNghe: loaiLN,
                            SoHoThamGia: new Intl.NumberFormat().format(item.SoHoThamGia).replace(/,/g, "."),
                            SoNguoiThamGia: new Intl.NumberFormat().format(item.SoNguoiThamGia).replace(/,/g, ".")
                        });
                    });
                    $('#tblDataIndexLangNghe').html(html);
                    $('.totalLangNghe').html(response.total.format());
                    $('.titleLangNgheCSDL').html(titleLangNghe);
                    LangNghePortal.paging(response.total, function () {
                        LangNghePortal.loadData();
                    }, changePageSize);
                    LangNghePortal.registerEvent();
                }
            }
        });
    },
    paging: function (totalRow, callback, changePageSize) {
        var totalPage = Math.ceil(totalRow / LangNghePortalConfig.pageSize);

        //Unbind pagination if it existed or click change pagesize
        if ($('#pagination-index-LangNghe a').length === 0 || changePageSize) {
            $('#pagination-index-LangNghe').empty();
            $('#pagination-index-LangNghe').removeData("twbs-pagination");
            $('#pagination-index-LangNghe').unbind("page");
        }

        $('#pagination-index-LangNghe').twbsPagination({
            totalPages: totalPage,
            visiblePages: 5,
            first: '<span aria-hidden="true">&laquo;</span>',
            next: 'Tiếp',
            last: '<span aria-hidden="true">&raquo;</span>',
            prev: 'Trước',
            onPageClick: function (event, page) {
                LangNghePortalConfig.pageIndex = page;
                setTimeout(callback, 200);
            }
        });
    },
    getThongTinLangNgheInfor: function (id) {
        $.ajax({
            url: URL_APPLICATION + '/LangNghe/GetLangNgheInfor',
            type: "POST",
            data: {
                id: id
            },
            dataType: "json",
            success: function (response) {
                if (response.status == true) {
                    var data = response.data;
                    var loaiLN = "";
                    if (data.LoaiLangNghe == "1") {
                        loaiLN = "Làng nghề";
                    }
                    if (data.LoaiLangNghe == "2") {
                        loaiLN = "LN truyền thống";
                    }
                    var str = "";
                    str += "<div class='my-3 mx-2'>";
                    str += "<p class='mb-2'><b>Tên làng nghề: </b>" + data.TenLangNghe+ "</p>";
                    str += "<p class='mb-2'><b>Làng nghề công nhận: </b>" + loaiLN + " </p>";
                    str += "<p class='mb-2'><b>Số hộ tham gia (hộ): </b>" + new Intl.NumberFormat().format(data.SoHoThamGia).replace(/,/g, ".") + "</p>";
                    str += "<p class='mb-2'><b>Số người tham gia (lao động): </b>" + new Intl.NumberFormat().format(data.SoNguoiThamGia).replace(/,/g, ".") + "</p>";
                    str += "<p class='mb-2'><b>Thu nhập bình quân (trd/ld/tháng): </b>" + new Intl.NumberFormat().format(data.ThuNhapBinhQuan).replace(/,/g, ".") + "</p>";
                    str += "</div>";
                    $('#contentDetailMoDal').html(str);
                    $('#modalViewDetail').modal();
                }
            }
        })
    },
    getProvinceByParent: function(HuyenId, XaId) {
        var url = URL_APPLICATION + '/Home/getProvinceByParent?parentid=' + HuyenId;
        $.ajax({
            url: url,
            data: "",
            type: 'GET',
            success: function (response) {
                //console.log(response);
                $("#" + XaId).html(response);
                LangNghePortal.loadData(true);
            },
            error: function (err) {
                //console.log(err);
            }
        });
    }
}
LangNghePortal.init();

