var URL_APPLICATION = $('#URL_APPLICATION').val();
var CategoryConfig = {
    pageSize: 9,
    pageIndex: 1,
}
var CategoryController = {
    init: function () {
        CategoryController.loadData();
        CategoryController.registerEvent();
    },
    registerEvent: function () {
        
    },
    loadSanPhamNganhHang: function (UnitId, CategoryId, ProductCategoryParentId) {
        try {
            var CATEGORY_ID = CategoryId;
            var UNIT_ID = UnitId;
            var PRODUCTCATEGORY_PARENTID = ProductCategoryParentId;
            var PRODUCTCATEGORY_ID = jQuery('#PRODUCTCATEGORY_ID').val();
            var json = {
                DomainName: URL_APPLICATION + "/san-pham",
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
                        $('body').find('#SanPhamNganhHang' + PRODUCTCATEGORY_PARENTID).html(html);
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
                DomainName: URL_APPLICATION + "/san-pham",
                CategoryId: CATEGORY_ID,
                UnitId: UNIT_ID,
                ProductCategoryParentId: PRODUCTCATEGORY_PARENTID,
                ProductCategoryId: PRODUCTCATEGORY_ID
            };
            var url = URL_APPLICATION + '/Product/APIProductCategoryUnit';
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
                        var ProductCategoryUnitViewModels = response.ProductCategoryUnitViewModels;
                        //var total = response.total;
                        $.each(ProductCategoryUnitViewModels, function (i, item) {
                            try {
                                var NganhHangID = item.ProductCategoryId;
                                var TenNganhHang = item.ProductCategoryName;
                                var UrlXemTatCa = URL_APPLICATION + "/nhanh-san-pham/" + item.CategoryMetaTitle + "-" + item.CategoryID + "/" + item.MetaTitleUnit + "-" + item.UnitId + "/" + item.ProductCategoryMetaTitle + "-" + item.ProductCategoryId;
                                html += Mustache.render(TemplateNganhHang, {
                                    NganhHangID: NganhHangID,
                                    TenNganhHang: TenNganhHang,
                                    UrlXemTatCa: UrlXemTatCa
                                });
                            } catch (e) {

                            }
                        });

                        $('#tblDataNganhHang').html(html);
                        $.each(ProductCategoryUnitViewModels, function (i, item) {
                            CategoryController.loadSanPhamNganhHang(item.UnitId, item.CategoryID, item.ProductCategoryId);
                        });
                        //$('#total_count_row').html(total);
                        //$('#from_record').html((((SanPhamConfig.pageIndex - 1) * SanPhamConfig.pageSize) + 1));
                        //$('#to_record').html((((SanPhamConfig.pageIndex - 1) * SanPhamConfig.pageSize) + grd_row));
                        //$('#filter_count_perpage').val(SanPhamConfig.pageSize);
                        //SanPhamController.paging(total, function () {
                        //    SanPhamController.loadData();
                        //}, changePageSize);
                        CategoryController.registerEvent();
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
        var totalPage = Math.ceil(totalRow / CategoryConfig.pageSize);
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
                CategoryConfig.pageIndex = page;
                setTimeout(callback, 200);
            }
        });
    }
}
CategoryController.init();
