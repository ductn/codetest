var URL_APPLICATION = $('#URL_APPLICATION').val();
var NganhHangConfig = {
    pageSize: 9,
    pageIndex: 1,
}
var NganhHangController = {
    init: function () {
        NganhHangController.loadData();
        NganhHangController.registerEvent();
    },
    registerEvent: function () {
        
    },
    loadSanPhamNganhHang: function (UnitId) {
        try {
            var CATEGORY_ID = jQuery('#CATEGORY_ID').val();
            var UNIT_ID = UnitId;
            var PRODUCTCATEGORY_PARENTID = jQuery('#PRODUCTCATEGORY_PARENTID').val();
            var PRODUCTCATEGORY_ID = jQuery('#PRODUCTCATEGORY_ID').val();
            var json = {
                DomainName: URL_APPLICATION + "/san-pham/danh-sach",
                CategoryId: CATEGORY_ID,
                UnitId: UNIT_ID,
                ProductCategoryParentId: PRODUCTCATEGORY_PARENTID,
                ProductCategoryId: PRODUCTCATEGORY_ID
            };
            var url = URL_APPLICATION + '/Product/APIProduct';
            $.ajax({
                url: url,
                type: 'POST',
                data: {
                    NganhHang: JSON.stringify(json)
                },
                dataType: "json",
                success: function (response) {
                    if (response.status == true) {
                        var grd_row = '';
                        var html = '';
                        var TemplateSanPham = $('#data-template-sanpham').html();
                        var ProductViews = response.ProductViews;
                        $.each(ProductViews, function (i, item) {
                            try {
                                var link = URL_APPLICATION + "/chi-tiet/" + item.MetaTitle + "-" + item.ID;
                                var images = URL_APPLICATION + item.Images;
                                var Name = item.Name;
                                var CateName = item.CateName;
                                var Price = (item.Price.HasValue ? item.Price.Value.ToString("N0") : "Liên hệ");
                                var GioHang = '/them-gio-hang?productId=' +  item.ID + '&quantity=1';
                                html += Mustache.render(TemplateSanPham, {
                                    link: link,
                                    images: images,
                                    Name: Name,
                                    CateName: CateName,
                                    Price: Price,
                                    GioHang: GioHang
                                });
                            } catch (e) {

                            }
                        });
                        $('body').find('#SanPhamNganhHang' + UNIT_ID).html(html);
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
    loadData: function (changePageSize) {
        try {
            var CATEGORY_ID = jQuery('#CATEGORY_ID').val();
            var UNIT_ID = jQuery('#UNIT_ID').val();
            var PRODUCTCATEGORY_PARENTID = jQuery('#PRODUCTCATEGORY_PARENTID').val();
            var PRODUCTCATEGORY_ID = jQuery('#PRODUCTCATEGORY_ID').val();
            var json = {
                DomainName: URL_APPLICATION + "/san-pham-nganh-hang",
                CategoryId: CATEGORY_ID,
                UnitId: UNIT_ID,
                ProductCategoryParentId: PRODUCTCATEGORY_PARENTID,
                ProductCategoryId: PRODUCTCATEGORY_ID
            };
            var url = URL_APPLICATION + '/Product/APICategoryUnit';
            $.ajax({
                url: url,
                type: 'POST',
                data: {
                    NganhHang: JSON.stringify(json)
                },
                dataType: "json",
                success: function (response) {
                    if (response.status == true) {
                        var grd_row = '';
                        var html = '';
                        var TemplateNganhHang = $('#data-template-nganhhang').html();
                        var CategoryUnitViewModels = response.CategoryUnitViewModels;
                        //var total = response.total;
                        $.each(CategoryUnitViewModels, function (i, item) {
                            try {
                                var NganhHangID = item.UnitId;
                                var TenNganhHang = item.UnitTitle;
                                var UrlXemTatCa = URL_APPLICATION + "/nhanh-san-pham/" + item.CategoryMetaTitle + "-" + item.CategoryID +"/"+ item.MetaTitleUnit + "-" + item.UnitId;
                                html += Mustache.render(TemplateNganhHang, {
                                    NganhHangID: NganhHangID,
                                    TenNganhHang: TenNganhHang,
                                    UrlXemTatCa: UrlXemTatCa
                                });
                            } catch (e) {

                            }
                        });

                        $('#tblDataNganhHang').html(html);
                        $.each(CategoryUnitViewModels, function (i, item) {
                            NganhHangController.loadSanPhamNganhHang(item.UnitId);
                        });
                        //$('#total_count_row').html(total);
                        //$('#from_record').html((((SanPhamConfig.pageIndex - 1) * SanPhamConfig.pageSize) + 1));
                        //$('#to_record').html((((SanPhamConfig.pageIndex - 1) * SanPhamConfig.pageSize) + grd_row));
                        //$('#filter_count_perpage').val(SanPhamConfig.pageSize);
                        //SanPhamController.paging(total, function () {
                        //    SanPhamController.loadData();
                        //}, changePageSize);
                        NganhHangController.registerEvent();
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
        var totalPage = Math.ceil(totalRow / NganhHangConfig.pageSize);
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
                NganhHangConfig.pageIndex = page;
                setTimeout(callback, 200);
            }
        });
    }
}
NganhHangController.init();
