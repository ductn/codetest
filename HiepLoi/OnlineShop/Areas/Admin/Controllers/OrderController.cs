using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClsCommon;
using CommonConstants = ClsCommon.CommonConstants;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Globalization;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class OrderController : BaseController
    {
        // GET: Admin/Contact
        [HasCredential(RoleID = "INDEX_ORDER")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new OrderDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            Order itOrder = new Order();
            var model = dao.ListAllPaging(itOrder, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "ORDER";
            setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.ListOrder = model;
            SetViewBag();
            return View(itOrder);
        }

        [HttpPost]
        [HasCredential(RoleID = "INDEX_ORDER")]
        public ActionResult Index(Order itOrder)
        {
            string searchString = "";
            int page = 1;
            int pageSize = 5;
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new OrderDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            var model = dao.ListAllPaging(itOrder, searchString, ref totalRecord, page, pageSize);


            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "ORDER";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListOrder = model;
            SetViewBag();
            return View(itOrder);
        }

        //Đơn hàng đang soạn
        [HasCredential(RoleID = "CHOXULY_ORDER")]
        public ActionResult ListChoXuLy(string searchString, int page = 1, int pageSize = 5)
        {
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new OrderDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            Order itOrder = new Order();
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];

            itOrder.Status = 0;
            var model = dao.ListAllPaging(itOrder, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "ORDER";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListOrder = model;
            SetViewBag();
            return View(itOrder);
        }

        [HttpPost]
        [HasCredential(RoleID = "CHOXULY_ORDER")]
        public ActionResult ListChoXuLy(Order itOrder)
        {
            string searchString = "";
            int page = 1;
            int pageSize = 5;
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new OrderDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            itOrder.Status = 0;
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];

            var model = dao.ListAllPaging(itOrder, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "ORDER";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListOrder = model;
            SetViewBag();
            return View(itOrder);
        }
        //End Đơn hàng đang soạn

        //Đơn hàng chờ duyệt
        [HasCredential(RoleID = "DAXULY_ORDER")]
        public ActionResult ListDaXuLy(string searchString, int page = 1, int pageSize = 5)
        {
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new OrderDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            Order itOrder = new Order();
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            List<Int32> lstStatus = new List<Int32>();
            lstStatus.Add(1);
            lstStatus.Add(2);
            itOrder.ListStatusId = lstStatus;
            var model = dao.ListAllPaging(itOrder, searchString, ref totalRecord, page, pageSize);
            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "ORDER";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListOrder = model;
            SetViewBag();
            return View(itOrder);
        }
        [HttpPost]
        [HasCredential(RoleID = "DAXULY_ORDER")]
        public ActionResult ListDaXuLy(Order itOrder)
        {
            string searchString = "";
            int page = 1;
            int pageSize = 5;
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new OrderDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            var model = dao.ListAllPaging(itOrder, searchString, ref totalRecord, page, pageSize).Where(x => x.Status != 0);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "ORDER";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListOrder = model;
            SetViewBag();
            return View(itOrder);
        }
        //End Đơn hàng chờ duyệt
        public ActionResult View(int ID)
        {
            OrderDetail itOrderDetail = new OrderDetail();
            itOrderDetail.OrderID = ID;
            var model = new OrderDetailDao().ListAll(itOrderDetail);
            var order = new OrderDao().GetById(ID);
            ViewBag.order = order;
            return View(model);
        }
        public void SetViewBag(long? selectedId = null)
        {
            var daoStore = new StoreDao();
            Store itStore = new Store();
            List<SelectListItem> listStore = new List<SelectListItem>();
            List<Store> lstStore = daoStore.ListAll(itStore);
            listStore.Add(new SelectListItem { Value = "0", Text = "--Chọn--" });
            foreach (Store item in lstStore)
            {
                listStore.Add(new SelectListItem { Value = item.StoreId + "", Text = item.Title });
            }
            ViewBag.StoreId = new SelectList(listStore, "Value", "Text", selectedId);
        }

    }
}