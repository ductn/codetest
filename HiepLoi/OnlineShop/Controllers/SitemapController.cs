using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class SitemapController : Controller
    {
        // GET: Store
        public ActionResult Index(int page = 1, int pageSize = 9)
        {
            //var dao = new StoreDao();
            //int totalRecord = 0;
            //Store itStore = new Store();
            //var model = new StoreDao().ListAllPaging(itStore, "", ref totalRecord, page, pageSize);

            //ViewBag.Total = totalRecord;
            //ViewBag.Page = page;

            //int maxPage = 5;
            //double totalPage = 0;
            //double tmp = (Convert.ToDouble(totalRecord) / Convert.ToDouble(pageSize));
            //totalPage = (double)Math.Ceiling(tmp);
            //ViewBag.TotalPage = totalPage;
            //ViewBag.MaxPage = maxPage;
            //ViewBag.First = 1;
            //ViewBag.Last = totalPage;
            //ViewBag.Next = page + 1;
            //ViewBag.Prev = page - 1;
            return View();
        }
    }
}