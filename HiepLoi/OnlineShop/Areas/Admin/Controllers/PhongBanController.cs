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
    public class PhongBanController : BaseController
    {
        // GET: Admin/PHONGBAN
        [HasCredential(RoleID = "INDEX_PHONGBAN")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new UnitDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            var model = dao.ListAllPagingPhongBan(searchString, ref totalRecord, page, pageSize);

            ViewBag.SearchString = searchString;
            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "PHONGBAN";
            setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            return View(model);
        }

        [HttpGet]
        [HasCredential(RoleID = "ADD_PHONGBAN")]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [HasCredential(RoleID = "ADD_PHONGBAN")]
        public ActionResult Create(Unit model)
        {
            if (ModelState.IsValid)
            {
                SetViewBag();
                try
                {
                    var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                    new UnitDao().Insert(model);
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
        [HasCredential(RoleID = "EDIT_PHONGBAN")]
        public ActionResult Edit(int id)
        {
            SetViewBag();
            var dao = new UnitDao();
            var PHONGBAN = dao.GetById(id);
            return View(PHONGBAN);
        }

        [HttpPost]
        [HasCredential(RoleID = "EDIT_PHONGBAN")]
        [ValidateInput(false)]
        public ActionResult Edit(Unit model)
        {

            if (ModelState.IsValid)
            {
                SetViewBag();
                try
                {
                    var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                    new UnitDao().Update(model);
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

        [HasCredential(RoleID = "VIEW_PHONGBAN")]
        public ActionResult View(int id)
        {
            var PHONGBAN = new UnitDao().ViewDetail(id);
            ViewBag.PHONGBAN = PHONGBAN;
            return View(PHONGBAN);
        }

        [HttpDelete]
        [HasCredential(RoleID = "DELETE_PHONGBAN")]
        public ActionResult Delete(int id)
        {
            var dao = new UnitDao();
            var PHONGBAN = dao.Delete(id);
            return View(PHONGBAN);
        }

        [HttpPost]
        [HasCredential(RoleID = "DELETEMULTI_PHONGBAN")]
        public JsonResult DeleteMulti(string[] ids)
        {
            try
            {
                var result = false;
                foreach (var i in ids)
                {
                    int id = int.Parse(i.ToString());
                    result = new UnitDao().Delete(id);
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

        public void SetViewBag(int? selectedId = null)
        {
            var dao = new UnitDao();
            List<SelectListItem> list = new List<SelectListItem>();
            List<Unit> lstUnit = dao.ListByParentId(0);
            //list.Add(new SelectListItem { Value = "0", Text = "--Chọn--" });
            foreach (Unit item in lstUnit)
            {
                list.Add(new SelectListItem { Value = item.Id + "", Text = item.Title });
            }
            //SelectList sl = new SelectList(dao.ListByParentId(parentid), "Id", "Title", selectedId);
            //ViewBag.ParentId = sl;
            ViewBag.ParentId = new SelectList(list, "Value", "Text", selectedId);
        }
    }
}