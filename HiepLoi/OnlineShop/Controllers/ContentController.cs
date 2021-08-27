using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class ContentController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetDataPaging(int StatusId, string DonViID, int page, int pageSize)
        {
            Content itContent = new Content();
            itContent.StatusId = StatusId;
            var dao = new ContentDao();
            if(DonViID != "0" && DonViID != null)
            {
                itContent.DonViID = DonViID;
            }
            int totalRecord = 0;
            string searchString = null;
            var lstDaTa = dao.ListAllPaging(itContent, searchString, ref totalRecord, page, pageSize);
            return Json(new
            {
                data = lstDaTa,
                total = totalRecord,
                status = true
            }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetDataCoQuanBanHanh(int IsDiaPhuong)
        {
            var dao = new UnitDao();
            var lstDaTa = dao.ListByIsDiaPhuong(IsDiaPhuong);
            return Json(new
            {
                data = lstDaTa,
                status = true
            }, JsonRequestBehavior.AllowGet);

        }
        // GET: Content
        //public ActionResult Index(int page = 1, int pageSize = 9)
        //{
        //    var dao = new ContentDao();
        //    int totalRecord = 0;
        //    Content itContent = new Content();
        //    itContent.StatusId = ClsCommon.CommonConstants.TINTTUC_CONGKHAI;
        //    var model = new ContentDao().ListAllPaging(itContent, "", ref totalRecord, page, pageSize);
        //    //int totalRecord = 0;
        //    //totalRecord = model.ToList().Count();
        //    ViewBag.Total = totalRecord;
        //    ViewBag.Page = page;

        //    int maxPage = 5;
        //    double totalPage = 0;
        //    double tmp = (Convert.ToDouble(totalRecord) / Convert.ToDouble(pageSize));
        //    totalPage = (double)Math.Ceiling(tmp);
        //    ViewBag.TotalPage = totalPage;
        //    ViewBag.MaxPage = maxPage;
        //    ViewBag.First = 1;
        //    ViewBag.Last = totalPage;
        //    ViewBag.Next = page + 1;
        //    ViewBag.Prev = page - 1;
        //    return View(model);
        //}

        public ActionResult Detail(long id)
        {
            var model = new ContentDao().GetByID(id);
            model.ViewCount += 1;
            new ContentDao().UpdateCountView(model);
            ViewBag.MoreContent = new ContentDao().ListAll(null).Skip(0).Take(3);
            //ViewBag.Tags = new ContentDao().ListTag(id);
            return View(model);
        }

        //public ActionResult Tag(string tagId, int page = 1, int pageSize = 10)
        //{
        //    var model = new ContentDao().ListAllByTag(tagId, page, pageSize);
        //    int totalRecord = 0;

        //    ViewBag.Total = totalRecord;
        //    ViewBag.Page = page;

        //    ViewBag.Tag = new ContentDao().GetTag(tagId);
        //    int maxPage = 5;
        //    int totalPage = 0;

        //    totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
        //    ViewBag.TotalPage = totalPage;
        //    ViewBag.MaxPage = maxPage;
        //    ViewBag.First = 1;
        //    ViewBag.Last = totalPage;
        //    ViewBag.Next = page + 1;
        //    ViewBag.Prev = page - 1;
        //    return View(model);
        //}
    }
}