using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Common
{
    public static class CommonConstants
    {
        public static int TINH_ID = 235; //Tỉnh Bến Tre
        public static string USER_SESSION = "USER_SESSION";
        public static string SESSION_CREDENTIALS = "SESSION_CREDENTIALS";
        public static string SESSION_SYSFUNCTION = "SESSION_SYSFUNCTION";
        public static string CartSession = "CartSession";
        public static string MSG_SUCCESS = "msgSuccess";
        public static string MSG_ERROR = "msgError";

        public static string MEMBER_GROUP = "MEMBER";
        public static string ADMIN_GROUP = "ADMIN";
        public static string MOD_GROUP = "MOD";
        public static string BIENTAP_GROUP = "BIENTAP";
        public static string PHEDUYET_GROUP = "PHEDUYET";
        public static string QLGIANHANG_GROUP = "QLGIANHANG";
        public static string QLLIENHE_GROUP = "QLLIENHE";
        public static string QLPHANQUYEN_GROUP = "QLPHANQUYEN";
        public static string QLCONGKHAI_GROUP = "QLCONGKHAI";
        public static string LANHDAOSO_GROUP = "LANHDAOSO";
        public static string CurrentCulture { set; get; }
        public static int QUYTRINH_TINTUC = 1;
        public static int QUYTRINH_CHINHSACHUUDAI = 2;
        public static int QUYTRINH_GIANHANG = 3;
        public static int QUYTRINH_SANPHAM = 4;
        public static int QUYTRINH_LIENHENGUOIDUNG = 5;
        public static int QUYTRINH_LIENHEGIANHANG = 6;
        //Module hệ thống
        public static string CONTENT_MODULE = "5";
        public static string CHINHSACHUUDAI_MODULE = "6";
        public static string SANPHAM_MODULE = "7";
        public static string GIANHANG_MODULE = "8";
        public static string LIENHENGUOIDUNG_MODULE = "9";
        public static string LIENHEGIANHANG_MODULE = "10";
        //End module hệ thống
        //Trạng thái tin tức
        public static int TINTTUC_DANGSOAN = 1;
        public static int TINTTUC_TRINHDUYET = 2;
        public static int TINTTUC_XUATBAN = 3;
        public static int TINTTUC_TRAVE = 4;
        public static int TINTTUC_KHONGDUYET = 5;
        public static int TINTTUC_THUHOI = 6;
        public static int TINTTUC_CONGKHAI = 12;
        //End trạng thái tin tức
        //Trạng thái chính sách ưu đãi
        public static int CHINHSACHUUDAI_DANGSOAN = 1;
        public static int CHINHSACHUUDAI_TRINHDUYET = 2;
        public static int CHINHSACHUUDAI_XUATBAN = 3;
        public static int CHINHSACHUUDAI_TRAVE = 4;
        public static int CHINHSACHUUDAI_KHONGDUYET = 5;
        public static int CHINHSACHUUDAI_THUHOI = 6;
        public static int CHINHSACHUUDAI_CONGKHAI = 12;
        //End Trạng thái chính sách ưu đãi
        //Trạng thái cửa hàng
        public static int GIANHANG_MOIDANGKY = 1;
        public static int GIANHANG_CHODUYET = 2;
        public static int GIANHANG_DUOCDUYET = 3;
        public static int GIANHANG_KHONGDUYET = 4;
        public static int GIANHANG_CAPNHAT = 5;
        public static int GIANHANG_CHOBOSUNG7 = 7;
        public static int GIANHANG_CHOBOSUNG8 = 8;
        //End Trạng thái cửa hàng
        //Trạng thái sản phẩm
        public static int SANPHAM_MOIDANGKY = 1;
        public static int SANPHAM_CHODUYET = 2;
        public static int SANPHAM_DUOCDUYET = 3;
        public static int SANPHAM_KHONGDUYET = 4;
        public static int SANPHAM_CAPNHAT = 5;
        public static int SANPHAM_CHOBOSUNG7 = 7;
        public static int SANPHAM_CHOBOSUNG8 = 8;
        //End Trạng thái sản phẩm
        //Trạng thái liên hệ người dùng
        public static int LIENHENGUOIDUNG_CHOTIEPNHAN = 1;
        public static int LIENHENGUOIDUNG_DANGXULY = 2;
        public static int LIENHENGUOIDUNG_DUOCDUYET = 3;
        public static int LIENHENGUOIDUNG_KHONGDUYET = 4;
        //End Trạng thái liên hệ người dùng
        //Trạng thái liên hệ gian hàng
        public static int LIENHEGIANHANG_CHOTIEPNHAN = 1;
        public static int LIENHEGIANHANG_DANGXULY = 2;
        public static int LIENHEGIANHANG_DUOCDUYET = 3;
        public static int LIENHEGIANHANG_KHONGDUYET = 4;
        //End Trạng thái liên hệ gian hàng
        //public static string MAINDOMAIN = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);//local
        public static string MAINDOMAIN = "http://csdldoanhnghiep.giaiphapdientu.vn";//local
    }
}