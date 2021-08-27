using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;
using Model.Dao;
using ClsCommon;
using PagedList;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class BusinessPartnerController : BaseController
    {
        // GET: Admin/BusinessPartner
        [HasCredential(RoleID = "INDEX_BUSINESSPARTNER")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new BusinessPartnerDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            BusinessPartner entity = new BusinessPartner();
            var model = dao.ListAllPaging(entity, searchString, ref totalRecord, page, pageSize);

            ViewBag.SearchString = searchString;
            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "BusinessPartner";
            setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.ListBusinessPartner = model;
            return View(entity);
        }
        [HttpPost]
        [HasCredential(RoleID = "INDEX_BUSINESSPARTNER")]
        public ActionResult Index(BusinessPartner entity)
        {
            var dao = new BusinessPartnerDao();
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
            var model = dao.ListAllPaging(entity, searchString, ref totalRecord, page, pageSize);

            ViewBag.SearchString = searchString;

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "BusinessPartner";
            setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.ListBusinessPartner = model;
            return View(entity);
        }

        [HttpGet]
        [HasCredential(RoleID = "ADD_BUSINESSPARTNER")]
        public ActionResult Create(int parentid = 0)
        {
            return View();
        }
        [HasCredential(RoleID = "EDIT_BUSINESSPARTNER")]
        public ActionResult Edit(int id)
        {
            var entity = new BusinessPartnerDao().ViewDetail(id);
            return View(entity);
        }
        [HttpPost]
        [ValidateInput(false)]
        [HasCredential(RoleID = "EDIT_BUSINESSPARTNER")]
        public ActionResult Edit(BusinessPartner model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                    model.QueryString = StringHelper.ToUnsignString(model.TieuDe);
                    new BusinessPartnerDao().Update(model);
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
        [HttpPost]
        [ValidateInput(false)]
        [HasCredential(RoleID = "ADD_BUSINESSPARTNER")]
        public ActionResult Create(BusinessPartner entity)
        {
            if (ModelState.IsValid)
            {
                var dao = new BusinessPartnerDao();
                entity.QueryString = StringHelper.ToUnsignString(entity.TieuDe);
                entity.NgayXuatBan = DateTime.Now;
                long id = dao.Insert(entity);
                if (id > 0)
                {
                    SetAlert("Thêm thông tin ngân hàng thành công", "success");
                    return RedirectToAction("Index", "BusinessPartner");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm thông tin ngân hàng không thành công");
                }
            }
            return View("Index");
        }
        [HttpDelete]
        [HasCredential(RoleID = "DELETE_BUSINESSPARTNER")]
        public ActionResult Delete(int id)
        {
            var dao = new BusinessPartnerDao();
            var entity = dao.Delete(id);
            return View(entity);
        }

        [HttpPost]
        [HasCredential(RoleID = "DELETEMULTI_BUSINESSPARTNER")]
        public JsonResult DeleteMulti(string[] ids)
        {
            try
            {
                var result = false;
                foreach (var i in ids)
                {
                    int id = int.Parse(i.ToString());
                    result = new BusinessPartnerDao().Delete(id);
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

        [HasCredential(RoleID = "VIEW_BUSINESSPARTNER")]
        public ActionResult View(int id)
        {
            var entity = new BusinessPartnerDao().ViewDetail(id);
            ViewBag.BusinessPartner = entity;
            return View(entity);
        }
    }
}