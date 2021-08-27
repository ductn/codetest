using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClsCommon;
using OnlineShop.Models;
using Newtonsoft.Json;

namespace OnlineShop.Controllers
{
    public class StoreController : Controller
    {
        // GET: Store
        public ActionResult Index(string searchString, int page = 1, int pageSize = 9)
        {
            var dao = new StoreDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            Store itStore = new Store();
            itStore.StatusId = CommonConstants.GIANHANG_DUOCDUYET;
            var model = dao.ListAllPaging(itStore, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            bool checkLogin = false;
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session != null)
            {
                ViewBag.UserName = session.UserName;
                checkLogin = true;
            }
            ViewBag.checkLogin = checkLogin;
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.ListStore = model;
            return View(itStore);
        }

        public ActionResult Detail(long id)
        {
            var model = new StoreDao().GetByID(id);
            if (model.StoreImage != "" && model.StoreImage != "0" && model.StoreImage != null)
            {
                dynamic jsonFile = JsonConvert.DeserializeObject(model.StoreImage);
                ViewBag.jsonStoreImg = jsonFile;
                ViewBag.checkShowStoreImge = true;
            }
            else
            {
                ViewBag.jsonStoreImg = false;
            }
            if (model.CountView !=null )
            {
                model.CountView = model.CountView + 1;
            }
            else
            {
                model.CountView = 1;
            }
            new StoreDao().UpdateCountView(model);
            //Gian hàng cùng ngành
            var itSearch = new Store();
            itSearch.LinhVucKinhDoanhId = model.LinhVucKinhDoanhId;
            ViewBag.MoreStore = new StoreDao().ListAll(itSearch).Skip(0).Take(3);
            //End Gian hàng cùng ngành

            //Sản phẩm mới
            var itSearchProduct = new Product();
            itSearchProduct.StoreId = model.StoreId;
            itSearchProduct.IsHot = true;
            ViewBag.MoreProduct = new ProductDao().ListAll(itSearchProduct).Skip(0).Take(3);
            //End Sản phẩm mới
            return View(model);
        }
        public ActionResult SanPham(long id, int page = 1, int pageSize = 9)
        {
            var model = new StoreDao().GetByID(id);
            if (model.StoreImage != "" && model.StoreImage != "0" && model.StoreImage != null)
            {
                dynamic jsonFile = JsonConvert.DeserializeObject(model.StoreImage);
                ViewBag.jsonStoreImg = jsonFile;
            }
            else
            {
                ViewBag.jsonStoreImg = "";
            }

            //Sản phẩm mới
            var itSearchProduct = new Product();
            itSearchProduct.StoreId = model.StoreId;
            itSearchProduct.IsHot = true;
            ViewBag.MoreProduct = new ProductDao().ListAll(itSearchProduct).Skip(0).Take(3);
            //End Sản phẩm mới

            //Sản phẩm đang bán
            int totalRecord = 0;
            var ListAllProduct = new ProductDao().ListAllPaging(itSearchProduct, "", ref totalRecord, page, pageSize);
            ViewBag.Total = totalRecord;
            ViewBag.Page = page;

            int maxPage = 5;
            double totalPage = 0;
            double tmp = (Convert.ToDouble(totalRecord) / Convert.ToDouble(pageSize));
            totalPage = (double)Math.Ceiling(tmp);
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;
            ViewBag.ListAllProduct = ListAllProduct;
            //End Sản phẩm đang bán
            return View(model);
        }
        public ActionResult ChiTietSanPham(int id, long idSp, int page = 1, int pageSize = 9)
        {
            var product = new ProductDao().ViewDetail(idSp);
            if (product.MoreImages != "" && product.MoreImages != "0" && product.MoreImages != null)
            {
                dynamic jsonFile = JsonConvert.DeserializeObject(product.MoreImages);
                ViewBag.checkShowProductImg = true;
                ViewBag.jsonProductImg = jsonFile;
            }
            else
            {
                ViewBag.checkShowProductImg = false;
                ViewBag.jsonProductImgImg = "";
            }
            long? storeId = 0;
            storeId = product.StoreId;
            var store = new StoreDao().GetByID(storeId);


            //More product
            Product itProduct = new Product();
            itProduct.StoreId = id;
            int totalRecord = 0;
            var ListAllProduct = new ProductDao().ListAllPaging(itProduct, "", ref totalRecord, page, pageSize);
            ViewBag.Total = totalRecord;
            ViewBag.Page = page;

            int maxPage = 5;
            double totalPage = 0;
            double tmp = (Convert.ToDouble(totalRecord) / Convert.ToDouble(pageSize));
            totalPage = (double)Math.Ceiling(tmp);
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;
            ViewBag.ListAllProduct = ListAllProduct;
            ViewBag.SoLuongSanPham = product.Quantity;
            //End More product

            //Sản phẩm mới
            var itSearchProduct = new Product();
            itSearchProduct.StoreId = id;
            itSearchProduct.IsHot = true;
            ViewBag.MoreProduct = new ProductDao().ListAll(itSearchProduct).Skip(0).Take(3);
            //End Sản phẩm mới

            ViewBag.store = store;
            return View(product);
        }
        public ActionResult BanDo(long id)
        {
            var model = new StoreDao().GetByID(id);
            ViewBag.MoreStore = new StoreDao().ListAll(null).Skip(0).Take(3);
            return View(model);
        }
        public ActionResult GioHang(long id)
        {
            string Khach = (string)Session["UserKhachMua"];
            if (Khach == null) 
            {
                Khach = "underfind404";
            }
            var itGh = new GioHang();
            itGh.UserKhach = Khach;
            itGh.Status = 0;
            var lstGh = new GioHangDao().ListAll(itGh);
            ViewBag.lstGh = lstGh;
            ViewBag.CountGioHang = lstGh.Count();
            ViewBag.UserKhachMua = Khach;
            return View();
        }
        [HttpGet]
        public ActionResult TraCuuDonHang(string searchString = null, int page = 1,int pageSize = 5)
        {
            string Khach = (string)Session["UserKhachMua"];
            if (Khach == null)
            {
                Khach = "underfind404";
            }
            //Sản phẩm mới
            var itSearchProduct = new Product();
            itSearchProduct.IsHot = true;
            ViewBag.MoreProduct = new ProductDao().ListAll(itSearchProduct).Skip(0).Take(6);
            //End Sản phẩm mới
            //Gio Hang
            int totalRecord = 0;
            var itDonHang = new Order();
            if(searchString != null && searchString != "")
            {
                itDonHang.ShipMobile = searchString;
            }
            else
            {
                itDonHang.UserKhach = Khach;
            }
            var lstGh = new OrderDao().ListAllPaging(itDonHang, searchString, ref totalRecord, page, pageSize);
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.Page = page;

            int maxPage = 5;
            double totalPage = 0;
            double tmp = (Convert.ToDouble(totalRecord) / Convert.ToDouble(pageSize));
            totalPage = (double)Math.Ceiling(tmp);
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;
            ViewBag.lstGh = lstGh;
            ViewBag.CountGioHang = lstGh.Count();
            ViewBag.UserKhachMua = Khach;
            return View();
        }
        [HttpGet]
        public ActionResult ChiTietDonHang(int id)
        {
            //Sản phẩm mới
            var itSearchProduct = new Product();
            itSearchProduct.IsHot = true;
            ViewBag.MoreProduct = new ProductDao().ListAll(itSearchProduct).Skip(0).Take(6);
            //End Sản phẩm mới
            var order = new OrderDao().ViewDetail(id);
            var dao = new OrderDetailDao();
            var model = new OrderDetail();
            model.OrderID = id;
            var lstOrderDetail = dao.ListAll(model);
            ViewBag.lstOrderDetail = lstOrderDetail;
            ViewBag.order = order;
            return View();
        }
        [ChildActionOnly]
        public ActionResult MainMenuGianHang()
        {
            string Khach = (string)Session["UserKhachMua"];
            if (Khach == null)
            {
                Khach = "underfind404";
            }
            var itGh = new GioHang();
            itGh.UserKhach = Khach;
            itGh.Status = 0;
            var lstGh = new GioHangDao().ListAll(itGh);
            int countGioHang = 0;
            if (lstGh != null)
            {
                countGioHang = lstGh.Count();
            }
            ViewBag.countGioHang = countGioHang;
            var id = RouteData.Values["id"];
            var itStore = new StoreDao().GetByID(Convert.ToInt32(id));
            return PartialView(itStore);
        }
        [ChildActionOnly]
        public PartialViewResult HeaderCartGianHang()
        {
            if (Session["UserKhachMua"] == null)
            {
                Session["UserKhachMua"] = "UserKhachMua" + DateTime.Now.ToFileTimeUtc();
            }
            var id = RouteData.Values["id"];
            var itStore = new StoreDao().GetByID(Convert.ToInt32(id));
            return PartialView(itStore);
        }
        public Product getProduct(int Id)
        {
            Product item = new ProductDao().GetByID(Id);
            return item;
        }

        [HttpGet]
        public ActionResult checkSotre(int userId)
        {
            bool status = false;
            Store store = new Store();
            store.UserId = userId;
            var data = new StoreDao().ListAll(store).FirstOrDefault();
            if(data != null)
            {
                status = true;
            }
            return Json(new
            {
                data = data,
                status = status,
            }, JsonRequestBehavior.AllowGet);
        }
    }
}