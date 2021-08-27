using Model.Dao;
using Model.EF;
using ClsCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace OnlineShop.Areas.Admin.Controllers
{
    public class LogHeThongController : BaseController
    {
        // GET: Admin/LogHeThong
        [HasCredential(RoleID = "INDEX_LOGHETHONG")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new LogHeThongDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            var model = dao.ListAllPaging(searchString, ref totalRecord, page, pageSize);

            ViewBag.SearchString = searchString;
            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "LOGHETHONG";
            setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            return View(model);
        }
    }
}