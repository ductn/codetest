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
    public class CountryController : BaseController
    {
        // GET: Admin/Country
        [HasCredential(RoleID = "INDEX_COUNTRY")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new CountryDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            Country itCountry = new Country();
            var model = dao.ListAllPaging(itCountry, searchString, ref totalRecord, page, pageSize);

            ViewBag.SearchString = searchString;
            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "COUNTRY";
            setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.ListCountry = model;
            return View(itCountry);
        }

        [HttpPost]
        [HasCredential(RoleID = "INDEX_COUNTRY")]
        public ActionResult Index(Country itCountry)
        {
            var dao = new CountryDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            string searchString = "";
            int page = 1;
            int pageSize = 5;
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var model = dao.ListAllPaging(itCountry, searchString, ref totalRecord, page, pageSize);

            ViewBag.SearchString = searchString;
            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "COUNTRY";
            setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.ListCountry = model;
            return View(itCountry);
        }

        [HttpGet]
        [HasCredential(RoleID = "ADD_COUNTRY")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [HasCredential(RoleID = "ADD_COUNTRY")]
        public ActionResult Create(Country model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                    model.Creator = session.UserName;
                    model.Created = DateTime.Now;
                    new CountryDao().Insert(model);
                    Session[CommonConstants.MSG_SUCCESS] = "Lưu thành công";
                }
                catch (Exception ex)
                {
                    Session[CommonConstants.MSG_ERROR] = "Lưu thất bại";
                    throw ex;
                }
                return RedirectToAction("Index");
            }
            else
            {
                Session[CommonConstants.MSG_ERROR] = "Lưu thất bại";
            }
            return View();
        }

        [HttpGet]
        [HasCredential(RoleID = "EDIT_COUNTRY")]
        public ActionResult Edit(int id)
        {
            var dao = new CountryDao();
            var Country = dao.GetById(id);
            return View(Country);
        }

        [HttpPost]
        [HasCredential(RoleID = "EDIT_COUNTRY")]
        [ValidateInput(false)]
        public ActionResult Edit(Country model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                    model.Modifier = session.UserName;
                    model.Modified = DateTime.Now;
                    new CountryDao().Update(model);
                    Session[CommonConstants.MSG_SUCCESS] = "Sửa thành công";
                }
                catch (Exception ex)
                {
                    Session[CommonConstants.MSG_ERROR] = "Sửa thất bại";
                    throw ex;
                }
            }
            else
            {
                Session[CommonConstants.MSG_ERROR] = "Sửa thất bại";
            }
            return RedirectToAction("Index");
        }

        [HasCredential(RoleID = "VIEW_COUNTRY")]
        public ActionResult View(int id)
        {
            var Country = new CountryDao().ViewDetail(id);
            ViewBag.Country = Country;
            return View(Country);
        }

        [HttpDelete]
        [HasCredential(RoleID = "DELETE_COUNTRY")]
        public ActionResult Delete(int id)
        {
            var dao = new CountryDao();
            var Country = dao.Delete(id);
            return View(Country);
        }

        [HttpPost]
        [HasCredential(RoleID = "DELETEMULTI_COUNTRY")]
        public JsonResult DeleteMulti(string[] ids)
        {
            try
            {
                var result = false;
                foreach (var i in ids)
                {
                    int id = int.Parse(i.ToString());
                    result = new CountryDao().Delete(id);
                }
                Session[CommonConstants.MSG_SUCCESS] = "Xóa thành công";
                return Json(new
                {
                    status = result
                });
            }
            catch (Exception ex)
            {
                Session[CommonConstants.MSG_ERROR] = "Xóa thất bại";
                return Json(new
                {
                    status = false,
                    message = ex.Message
                });

            }
        }
    }
}