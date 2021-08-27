using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*x}", new { x = @".*\.asmx(/.*)?" });
            routes.IgnoreRoute("{*botdetect}",
            new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });

            // This line added to also ignore .ashx files.
            routes.IgnoreRoute("{resource}.ashx/{*pathInfo}");
            routes.MapRoute(
                name: "Danh Sach Dang Giam Gia",
                url: "san-pham-dang-giam-gia.html",
                defaults: new { controller = "Product", action = "DanhSachDangGiamGia", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                name: "san pham top ban chay",
                url: "san-pham-top-ban-chay.html",
                defaults: new { controller = "Product", action = "DanhSachTopNganhHang", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                name: "san pham khuyen mai",
                url: "san-pham-khuyen-mai.html",
                defaults: new { controller = "Product", action = "DanhSachSanPhamKhuyenMai", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                name: "san pham nhan hang",
                url: "san-pham-nhan-hang/{metatitleunit}-{UnitId}",
                defaults: new { controller = "Product", action = "NhanhNhanHang", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                name: "Nhanh San Pham 1",
                url: "nhanh-san-pham/{metatitle}-{cateId}",
                defaults: new { controller = "Product", action = "NhanhSanPham", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                name: "Nhanh San Pham 2",
                url: "nhanh-san-pham/{metatitle}-{cateId}/{metatitleunit}-{UnitId}",
                defaults: new { controller = "Product", action = "NhanhSanPham", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                name: "Nhanh San Pham 3",
                url: "nhanh-san-pham/{metatitle}-{cateId}/{metatitleunit}-{UnitId}/{metatitleproductcategoryparent}-{ProductCategoryParentId}",
                defaults: new { controller = "Product", action = "NhanhSanPham", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                name: "Nhanh San Pham 4",
                url: "nhanh-san-pham/{metatitle}-{cateId}/{metatitleunit}-{UnitId}/{metatitleproductcategoryparent}-{ProductCategoryParentId}/{metatitleproductcategoryt}-{ProductCategoryId}",
                defaults: new { controller = "Product", action = "NhanhSanPham", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );

            routes.MapRoute(
                name: "San Pham",
                url: "san-pham/danh-sach",
                defaults: new { controller = "Product", action = "SanPham", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                name: "San Pham 1",
                url: "san-pham/danh-sach/{metatitle}-{cateId}",
                defaults: new { controller = "Product", action = "SanPham", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                name: "San Pham 2",
                url: "san-pham/danh-sach/{metatitle}-{cateId}/{metatitleunit}-{UnitId}",
                defaults: new { controller = "Product", action = "SanPham", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                name: "San Pham 3",
                url: "san-pham/danh-sach/{metatitle}-{cateId}/{metatitleunit}-{UnitId}/{metatitleproductcategoryparent}-{ProductCategoryParentId}",
                defaults: new { controller = "Product", action = "SanPham", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                name: "San Pham 4",
                url: "san-pham/danh-sach/{metatitle}-{cateId}/{metatitleunit}-{UnitId}/{metatitleproductcategoryparent}-{ProductCategoryParentId}/{metatitleproductcategoryt}-{ProductCategoryId}",
                defaults: new { controller = "Product", action = "SanPham", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                name: "Nganh Hang 1",
                url: "san-pham-nganh-hang/{metatitle}-{cateId}",
                defaults: new { controller = "Product", action = "NganhHang", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                name: "Nganh Hang 2",
                url: "san-pham-nganh-hang/{metatitle}-{cateId}/{metatitleunit}-{UnitId}",
                defaults: new { controller = "Product", action = "NganhHang", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                name: "Nganh Hang 3",
                url: "san-pham-nganh-hang/{metatitle}-{cateId}/{metatitleunit}-{UnitId}/{metatitleproductcategoryparent}-{ProductCategoryParentId}",
                defaults: new { controller = "Product", action = "NganhHang", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                name: "Nganh Hang 4",
                url: "san-pham-nganh-hang/{metatitle}-{cateId}/{metatitleunit}-{UnitId}/{metatitleproductcategoryparent}-{ProductCategoryParentId}/{metatitleproductcategoryt}-{ProductCategoryId}",
                defaults: new { controller = "Product", action = "NganhHang", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                name: "Product Category 2",
                url: "san-pham/{metatitle}-{cateId}/{metatitleunit}-{UnitId}",
                defaults: new { controller = "Product", action = "Category", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                name: "Product Category 3",
                url: "san-pham/{metatitle}-{cateId}/{metatitleunit}-{UnitId}/{metatitleproductcategoryparent}-{ProductCategoryParentId}",
                defaults: new { controller = "Product", action = "Category", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                name: "Product Category 4",
                url: "san-pham/{metatitle}-{cateId}/{metatitleunit}-{UnitId}/{metatitleproductcategoryparent}-{ProductCategoryParentId}/{metatitleproductcategoryt}-{ProductCategoryId}",
                defaults: new { controller = "Product", action = "Category", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                name: "Product Detail",
                url: "chi-tiet/{metatitle}-{id}",
                defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                 name: "Tags",
                 url: "tag/{tagId}",
                 defaults: new { controller = "Content", action = "Tag", id = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                 name: "Content Detail",
                 url: "tin-tuc/{metatitle}-{id}.html",
                 defaults: new { controller = "Content", action = "Detail", id = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                 name: "Store Detail",
                 url: "gian-hang/{metatitle}-{id}.html",
                 defaults: new { controller = "Store", action = "Detail", id = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                 name: "Store Product",
                 url: "gian-hang/{metatitle}-{id}/san-pham.html",
                 defaults: new { controller = "Store", action = "SanPham", id = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                 name: "Store Map",
                 url: "gian-hang/{metatitle}-{id}/ban-do.html",
                 defaults: new { controller = "Store", action = "BanDo", id = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                 name: "Store ShoppingCart",
                 url: "gian-hang/{metatitle}-{id}/gio-hang.html",
                 defaults: new { controller = "Store", action = "GioHang", id = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                name: "Store TraCuuDonHang",
                url: "quan-ly-don-hang-khach.html",
                defaults: new { controller = "Store", action = "TraCuuDonHang", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
           );
            routes.MapRoute(
                 name: "Store ChiTietDonHang",
                 url: "chi-tiet-don-hang.html",
                 defaults: new { controller = "Store", action = "ChiTietDonHang", id = UrlParameter.Optional, idSp = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                 name: "Store ChiTietSanPham",
                 url: "gian-hang/{metatitle}-{id}/{metatitle2}-{idSp}.html",
                 defaults: new { controller = "Store", action = "ChiTietSanPham", id = UrlParameter.Optional, idSp = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                 name: "QuanLyGianHang GianHangMoi",
                 url: "gian-hang-moi.html",
                 defaults: new { controller = "QuanLyGianHang", action = "GianHangMoi", id = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                name: "QuanLyGianHang GianHangMoiApp",
                url: "gian-hang-moi-app.html",
                defaults: new { controller = "QuanLyGianHang", action = "GianHangMoiApp", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
           );
            routes.MapRoute(
                 name: "QuanLyGianHang GianHangChoBoSung",
                 url: "gian-hang-cho-bo-sung.html",
                 defaults: new { controller = "QuanLyGianHang", action = "GianHangChoBoSung", id = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                 name: "QuanLyGianHang GianHangChoBoSungApp",
                 url: "gian-hang-cho-bo-sung-app.html",
                 defaults: new { controller = "QuanLyGianHang", action = "GianHangChoBoSungApp", id = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                 name: "QuanLyGianHang GianHangChoPheDuyet",
                 url: "gian-hang-cho-phe-duyet.html",
                 defaults: new { controller = "QuanLyGianHang", action = "GianHangChoPheDuyet", id = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                 name: "QuanLyGianHang GianHangChoPheDuyetApp",
                 url: "gian-hang-cho-phe-duyet-app.html",
                 defaults: new { controller = "QuanLyGianHang", action = "GianHangChoPheDuyetApp", id = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                 name: "QuanLyGianHang GianHangDangCapNhat",
                 url: "gian-hang-dang-cap-nhat.html",
                 defaults: new { controller = "QuanLyGianHang", action = "GianHangDangCapNhat", id = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                name: "QuanLyGianHang GianHangDangCapNhatApp",
                url: "gian-hang-dang-cap-nhat-app.html",
                defaults: new { controller = "QuanLyGianHang", action = "GianHangDangCapNhatApp", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
           );
            routes.MapRoute(
                 name: "QuanLyGianHang GianHangDuocDuyet",
                 url: "gian-hang-duoc-duyet.html",
                 defaults: new { controller = "QuanLyGianHang", action = "GianHangDuocDuyet", id = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                 name: "QuanLyGianHang GianHangDuocDuyetApp",
                 url: "gian-hang-duoc-duyet-app.html",
                 defaults: new { controller = "QuanLyGianHang", action = "GianHangDuocDuyetApp", id = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                 name: "QuanLyGianHang GianHangKhongDuyet",
                 url: "gian-hang-khong-duyet.html",
                 defaults: new { controller = "QuanLyGianHang", action = "GianHangKhongDuyet", id = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                 name: "QuanLyGianHang GianHangKhongDuyetApp",
                 url: "gian-hang-khong-duyet-app.html",
                 defaults: new { controller = "QuanLyGianHang", action = "GianHangKhongDuyetApp", id = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                 name: "DangKyDauTu TaoMoiDuAn",
                 url: "tao-moi-dang-ky-du-an.html",
                 defaults: new { controller = "DangKyDauTu", action = "Create", id = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                 name: "DangKyDauTu CapNhatDuAn",
                 url: "cap-nhat-dau-tu-du-an.html",
                 defaults: new { controller = "DangKyDauTu", action = "Edit", id = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                 name: "DangKyDauTu DSDangKy",
                 url: "du-an-moi-dang-ky.html",
                 defaults: new { controller = "DangKyDauTu", action = "DangKy", id = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                 name: "DangKyDauTu DSBoSung",
                 url: "du-an-cho-bo-sung.html",
                 defaults: new { controller = "DangKyDauTu", action = "BoSung", id = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                 name: "DangKyDauTu DSChoDuyet",
                 url: "du-an-cho-phe-duyet.html",
                 defaults: new { controller = "DangKyDauTu", action = "ChoDuyet", id = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                 name: "DangKyDauTu DSDuocDuyet",
                 url: "du-an-duoc-phe-duyet.html",
                 defaults: new { controller = "DangKyDauTu", action = "DuocDuyet", id = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                 name: "DangKyDauTu DSKhongDuyet",
                 url: "du-an-khong-duoc-duyet.html",
                 defaults: new { controller = "DangKyDauTu", action = "KhongDuyet", id = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                 name: "QuanLyGianHang SanPhamMoi",
                 url: "san-pham-moi.html",
                 defaults: new { controller = "QuanLyGianHang", action = "SanPhamMoi", id = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                 name: "QuanLyGianHang SanPhamMoiApp",
                 url: "san-pham-moi-app.html",
                 defaults: new { controller = "QuanLyGianHang", action = "SanPhamMoiApp", id = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                 name: "QuanLyGianHang SanPhamChoBoSung",
                 url: "san-pham-cho-bo-sung.html",
                 defaults: new { controller = "QuanLyGianHang", action = "SanPhamChoBoSung", id = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                 name: "QuanLyGianHang SanPhamChoBoSungApp",
                 url: "san-pham-cho-bo-sung-app.html",
                 defaults: new { controller = "QuanLyGianHang", action = "SanPhamChoBoSungApp", id = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                 name: "QuanLyGianHang SanPhamChoPheDuyet",
                 url: "san-pham-cho-phe-duyet.html",
                 defaults: new { controller = "QuanLyGianHang", action = "SanPhamChoPheDuyet", id = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                name: "QuanLyGianHang SanPhamChoPheDuyetApp",
                url: "san-pham-cho-phe-duyet-app.html",
                defaults: new { controller = "QuanLyGianHang", action = "SanPhamChoPheDuyetApp", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
           );
            routes.MapRoute(
                 name: "QuanLyGianHang SanPhamDangCapNhat",
                 url: "san-pham-dang-cap-nhat.html",
                 defaults: new { controller = "QuanLyGianHang", action = "SanPhamDangCapNhat", id = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                 name: "QuanLyGianHang SanPhamDangCapNhatApp",
                 url: "san-pham-dang-cap-nhat-app.html",
                 defaults: new { controller = "QuanLyGianHang", action = "SanPhamDangCapNhatApp", id = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                 name: "QuanLyGianHang SanPhamDuocDuyet",
                 url: "san-pham-duoc-duyet.html",
                 defaults: new { controller = "QuanLyGianHang", action = "SanPhamDuocDuyet", id = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                 name: "QuanLyGianHang SanPhamDuocDuyetApp",
                 url: "san-pham-duoc-duyet-app.html",
                 defaults: new { controller = "QuanLyGianHang", action = "SanPhamDuocDuyetApp", id = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                 name: "QuanLyGianHang SanPhamKhongDuyet",
                 url: "san-pham-khong-duyet.html",
                 defaults: new { controller = "QuanLyGianHang", action = "SanPhamKhongDuyet", id = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                 name: "QuanLyGianHang SanPhamKhongDuyetApp",
                 url: "san-pham-khong-duyet-app.html",
                 defaults: new { controller = "QuanLyGianHang", action = "SanPhamKhongDuyetApp", id = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                 name: "QuanLyGianHang DonHangChoXuLy",
                 url: "don-hang-cho-xu-ly.html",
                 defaults: new { controller = "QuanLyGianHang", action = "DonHangChoXuLy", id = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                 name: "QuanLyGianHang DonHangChoXuLyApp",
                 url: "don-hang-cho-xu-ly-app.html",
                 defaults: new { controller = "QuanLyGianHang", action = "DonHangChoXuLyApp", id = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                 name: "QuanLyGianHang DonHangDaXuLy",
                 url: "don-hang-da-xu-ly.html",
                 defaults: new { controller = "QuanLyGianHang", action = "DonHangDaXuLy", id = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                 name: "QuanLyGianHang DonHangDaXuLyApp",
                 url: "don-hang-da-xu-ly-app.html",
                 defaults: new { controller = "QuanLyGianHang", action = "DonHangDaXuLyApp", id = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                 name: "QuanLyGianHang ThongTinDonHang",
                 url: "thong-tin-don-hang.html",
                 defaults: new { controller = "QuanLyGianHang", action = "ThongTinDonHang", id = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                 name: "QuanLyGianHang ThongTinDonHangApp",
                 url: "thong-tin-don-hang-app.html",
                 defaults: new { controller = "QuanLyGianHang", action = "ThongTinDonHangApp", id = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                 name: "QuanLyGianHang TaoMoiGianHang",
                 url: "tao-moi-gian-hang.html",
                 defaults: new { controller = "QuanLyGianHang", action = "TaoMoiGianHang", id = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                name: "QuanLyGianHang TaoMoiGianHangApp",
                url: "tao-moi-gian-hang-app.html",
                defaults: new { controller = "QuanLyGianHang", action = "TaoMoiGianHangApp", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
           );
            routes.MapRoute(
                 name: "QuanLyGianHang CapNhatGianHang",
                 url: "cap-nhat-gian-hang.html",
                 defaults: new { controller = "QuanLyGianHang", action = "CapNhatGianHang", id = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                 name: "QuanLyGianHang CapNhatGianHangApp",
                 url: "cap-nhat-gian-hang-app.html",
                 defaults: new { controller = "QuanLyGianHang", action = "CapNhatGianHangApp", id = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                name: "QuanLyGianHang CapNhatSanPham",
                url: "cap-nhat-san-pham.html",
                defaults: new { controller = "QuanLyGianHang", action = "CapNhatSanPham", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
           );
            routes.MapRoute(
               name: "QuanLyGianHang CapNhatSanPhamApp",
               url: "cap-nhat-san-pham-app.html",
               defaults: new { controller = "QuanLyGianHang", action = "CapNhatSanPhamApp", id = UrlParameter.Optional },
               namespaces: new[] { "OnlineShop.Controllers" }
          );
            routes.MapRoute(
                 name: "QuanLyGianHang TaoMoiSanPham",
                 url: "tao-moi-san-pham.html",
                 defaults: new { controller = "QuanLyGianHang", action = "TaoMoiSanPham", id = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                name: "QuanLyGianHang TaoMoiSanPhamApp",
                url: "tao-moi-san-pham-app.html",
                defaults: new { controller = "QuanLyGianHang", action = "TaoMoiSanPhamApp", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
           );
            routes.MapRoute(
                 name: "QuanLyGianHang ThongTinCaNhan",
                 url: "thong-tin-ca-nhan.html",
                 defaults: new { controller = "QuanLyGianHang", action = "ThongTinCaNhan", id = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                name: "QuanLyGianHang ThongTinCaNhanApp",
                url: "thong-tin-ca-nhan-app.html",
                defaults: new { controller = "QuanLyGianHang", action = "ThongTinCaNhanApp", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
           );
            routes.MapRoute(
                 name: "QuanLyGianHang DoiMatKhau",
                 url: "doi-mat-khau.html",
                 defaults: new { controller = "QuanLyGianHang", action = "DoiMatKhau", id = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                 name: "QuanLyGianHang DoiMatKhauApp",
                 url: "doi-mat-khau-app.html",
                 defaults: new { controller = "QuanLyGianHang", action = "DoiMatKhauApp", id = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                 name: "QuanLyGianHang LienHe",
                 url: "lien-he-gian-hang.html",
                 defaults: new { controller = "QuanLyGianHang", action = "LienHe", id = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                 name: "QuanLyGianHang LienHeApp",
                 url: "lien-he-gian-hang-app.html",
                 defaults: new { controller = "QuanLyGianHang", action = "LienHeApp", id = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                 name: "VanBanPhapLuat Detail",
                 url: "van-ban-phap-luat/{metatitle}-{id}.html",
                 defaults: new { controller = "VBPQDocument", action = "Detail", id = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                 name: "About",
                 url: "gioi-thieu.html",
                 defaults: new { controller = "About", action = "Index", id = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                  name: "Contact",
                  url: "lien-he.html",
                  defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional },
                  namespaces: new[] { "OnlineShop.Controllers" }
            );

            routes.MapRoute(
               name: "News",
               url: "tin-tuc.html",
               defaults: new { controller = "Content", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
               name: "thongtindoanhnghiep",
               url: "thong-tin-doanh-nghiep.html",
               defaults: new { controller = "ThongTinDoanhNghiep", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
               name: "thongtindoanhnghiep chinhsach",
               url: "thong-tin-doanh-nghiep/chinh-sach.html",
               defaults: new { controller = "ThongTinDoanhNghiep", action = "TabChinhSachUuDai", id = UrlParameter.Optional },
               namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
               name: "thongtindoanhnghiep bieudo",
               url: "thong-tin-doanh-nghiep/bieu-do.html",
               defaults: new { controller = "ThongTinDoanhNghiep", action = "TabBieuDo", id = UrlParameter.Optional },
               namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
               name: "thongtinhoptacxa",
               url: "thong-tin-hop-tac-xa.html",
               defaults: new { controller = "ThongTinHopTacXa", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
               name: "thongtinhoptacxa chinhsach",
               url: "thong-tin-hop-tac-xa/chinh-sach.html",
               defaults: new { controller = "ThongTinHopTacXa", action = "TabChinhSachUuDai", id = UrlParameter.Optional },
               namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
               name: "thongtinhoptacxa bieudo",
               url: "thong-tin-hop-tac-xa/bieu-do.html",
               defaults: new { controller = "ThongTinHopTacXa", action = "TabBieuDo", id = UrlParameter.Optional },
               namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
               name: "thongtinhokinhdoanh",
               url: "thong-tin-ho-kinh-doanh.html",
               defaults: new { controller = "ThongTinHoKinhDoanh", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
               name: "thongtinhokinhdoanh chinhsach",
               url: "thong-tin-ho-kinh-doanh/chinh-sach.html",
               defaults: new { controller = "ThongTinHoKinhDoanh", action = "TabChinhSachUuDai", id = UrlParameter.Optional },
               namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
               name: "thongtinhokinhdoanh bieudo",
               url: "thong-tin-ho-kinh-doanh/bieu-do.html",
               defaults: new { controller = "ThongTinHoKinhDoanh", action = "TabBieuDo", id = UrlParameter.Optional },
               namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
              name: "BieuMauDangKyKinhDoanh Portal",
              url: "bieu-mau-kinh-doanh.html",
              defaults: new { controller = "BieuMauDangKyKinhDoanh", action = "Index", id = UrlParameter.Optional },
              namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
             name: "BieuMauDangKyDauTu Portal",
             url: "bieu-mau-dau-tu.html",
             defaults: new { controller = "BieuMauDangKyDauTu", action = "Index", id = UrlParameter.Optional },
             namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                name: "ChinhSachUuDai Portal",
                url: "chinh-sach-uu-dai.html",
                defaults: new { controller = "ChinhSachUuDai", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
              );
            routes.MapRoute(
               name: "ChinhSachUuDai PortalApp",
               url: "chinh-sach-uu-dai-app.html",
               defaults: new { controller = "ChinhSachUuDai", action = "IndexApp", id = UrlParameter.Optional },
               namespaces: new[] { "OnlineShop.Controllers" }
             );
            routes.MapRoute(
              name: "ChinhSachUuDai ConfigPortalApp",
              url: "config-chinh-sach-uu-dai-app.html",
              defaults: new { controller = "ChinhSachUuDai", action = "ConfigApp", id = UrlParameter.Optional },
              namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                name: "Config ChinhSachUuDaiApp",
                url: "cau-hinh-chinh-sach-uu-dai-app.html",
                defaults: new { controller = "ChinhSachUuDai", action = "ConfigChinhSachUuDai", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
              name: "Update ConfigUuDaiApp",
              url: "update-config-uu-dai-app.html",
              defaults: new { controller = "ChinhSachUuDai", action = "UpdateConfigUuDai", id = UrlParameter.Optional },
              namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
               name: "NganhNgheCoDieuKien Portal",
               url: "nganh-nghe-kinh-doanh-co-dieu-kien.html",
               defaults: new { controller = "NganhNgheCoDieuKien", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "OnlineShop.Controllers" }
             );
            routes.MapRoute(
               name: "NganhNgheCoDieuKien Portal ChiTiet",
               url: "chi-tiet-nganh-nghe-co-dieu-kien/{id}",
               defaults: new { controller = "NganhNgheCoDieuKien", action = "Detail", id = UrlParameter.Optional },
               namespaces: new[] { "OnlineShop.Controllers" }
             );
            routes.MapRoute(
               name: "NoiDungCanBiet Portal",
               url: "nhung-dieu-can-biet.html",
               defaults: new { controller = "NoiDungCanBiet", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "OnlineShop.Controllers" }
             );
            routes.MapRoute(
               name: "ThongTinNganHang Portal",
               url: "thong-tin-ngan-hang.html",
               defaults: new { controller = "ThongTinNganHang", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "OnlineShop.Controllers" }
             );
            routes.MapRoute(
               name: "ThongTinNhanLuc Portal",
               url: "thong-tin-nhan-luc.html",
               defaults: new { controller = "ThongTinNhanLuc", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "OnlineShop.Controllers" }
             );
            routes.MapRoute(
              name: "ThongTinNhanLuc Portal Detail",
              url: "chi-tiet-thong-tin-nhan-luc/{metatitle}-{id}.html",
              defaults: new { controller = "ThongTinNhanLuc", action = "Detail", id = UrlParameter.Optional },
              namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
              name: "404",
              url: "404.html",
              defaults: new { controller = "Home", action = "ERR404", id = UrlParameter.Optional },
              namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
              name: "BieuDoDNApp",
              url: "bieu-do-dn-app.html",
              defaults: new { controller = "Home", action = "BieuDoDNApp", id = UrlParameter.Optional },
              namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
              name: "SupportApp",
              url: "support-app.html",
              defaults: new { controller = "Home", action = "SupportApp", id = UrlParameter.Optional },
              namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
              name: "MarketingApp",
              url: "marketing-app.html",
              defaults: new { controller = "Home", action = "MarketingApp", id = UrlParameter.Optional },
              namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
              name: "DKDoanhNghiepApp",
              url: "dk-doanh-nghiep.html",
              defaults: new { controller = "Home", action = "DKDoanhNghiepApp", id = UrlParameter.Optional },
              namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
              name: "DKNguoiDungApp",
              url: "dk-nguoi-dung.html",
              defaults: new { controller = "Home", action = "DKNguoiDungApp", id = UrlParameter.Optional },
              namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
             name: "ParentView",
             url: "ParentView",
             defaults: new { controller = "Home", action = "ParentView", id = UrlParameter.Optional },
             namespaces: new[] { "OnlineShop.Controllers" }
           );
            routes.MapRoute(
            name: "KiemTraTaiKhoanView",
            url: "KiemTraTaiKhoanView",
            defaults: new { controller = "User", action = "KiemTraTaiKhoanView", id = UrlParameter.Optional },
            namespaces: new[] { "OnlineShop.Controllers" }
          );
            routes.MapRoute(
               name: "Store",
               url: "van-ban-phap-luat.html",
               defaults: new { controller = "VBPQDocument", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
               name: "HoiDap",
               url: "hoi-dap.html",
               defaults: new { controller = "HoiDap", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
               name: "DuAnDauTu",
               url: "du-an-dau-tu.html",
               defaults: new { controller = "DuAnDauTu", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
               name: "LangNghe",
               url: "lang-nghe.html",
               defaults: new { controller = "LangNghe", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
               name: "SiteMap",
               url: "sitemap.html",
               defaults: new { controller = "Sitemap", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
               name: "VanBanPhapQuy",
               url: "gian-hang.html",
               defaults: new { controller = "Store", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                name: "Login-member",
                url: "dang-nhap.html",
                defaults: new { controller = "User", action = "Login", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
               name: "Search",
               url: "tim-kiem",
               defaults: new { controller = "Product", action = "Search", id = UrlParameter.Optional },
               namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                name: "Register",
                url: "dang-ky.html",
                defaults: new { controller = "User", action = "Register", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                name: "DangKyApp",
                url: "dang-ky-app.html",
                defaults: new { controller = "User", action = "DangKyApp", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                name: "ForgotPassword",
                url: "quen-mat-khau.html",
                defaults: new { controller = "User", action = "ForgotPassword", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                name: "QuenMatKhauApp",
                url: "quen-mat-khau-app.html",
                defaults: new { controller = "User", action = "QuenMatKhauApp", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                name: "UpdatePassword Protal ",
                url: "cap-nhat-mat-khau/{userid}/{token}.html",
                defaults: new { controller = "User", action = "UpdatePassword", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                name: "AcceptRegisterAccount Protal ",
                url: "xac-nhan-dang-ky/{UserName}.html",
                defaults: new { controller = "User", action = "AcceptRegister", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                name: "AcceptAccountNhaDauTuRegister Protal ",
                url: "xac-nhan-dang-ky-tai-khoan-nha-dau-tu/{UserName}.html",
                defaults: new { controller = "User", action = "AcceptAccountNhaDauTuRegister", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                 name: "Payment",
                 url: "thanh-toan",
                 defaults: new { controller = "Cart", action = "Payment", id = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                name: "Add Cart",
                url: "them-gio-hang",
                defaults: new { controller = "Cart", action = "AddItem", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                  name: "Payment Success",
                  url: "hoan-thanh",
                  defaults: new { controller = "Cart", action = "Success", id = UrlParameter.Optional },
                  namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                name: "Login-admin",
                url: "Login",
                defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );
        }
    }
}
