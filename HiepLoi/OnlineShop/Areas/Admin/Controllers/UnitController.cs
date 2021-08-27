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
    public class UnitController : BaseController
    {
        // GET: Admin/unit
        [HasCredential(RoleID = "INDEX_UNIT")]
        public ActionResult Index(string searchString, int parentid = 0, int page = 1, int pageSize = 10)
        {
            var dao = new UnitDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            Unit itUnit = new Unit();
            itUnit.ParentId = parentid;
            var model = dao.ListAllPaging(itUnit, parentid, ref totalRecord, searchString, page, pageSize);

            ViewBag.SearchString = searchString;
            ViewBag.ParentId = parentid;
            if (parentid > 0)
            {
                ViewBag.ParentIdOfParenId = dao.GetById(parentid).ParentId;
            }

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "UNIT";
            setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.ListUnit = model;
            return View(itUnit);
        }
        [HttpPost]
        [HasCredential(RoleID = "INDEX_UNIT")]
        public ActionResult Index(Unit itUnit)
        {
            var dao = new UnitDao();
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
            int? parentid = itUnit.ParentId;
            var model = dao.ListAllPaging(itUnit, parentid, ref totalRecord, searchString, page, pageSize);

            ViewBag.SearchString = searchString;
            ViewBag.ParentId = parentid;
            if (parentid > 0)
            {   
                ViewBag.ParentIdOfParenId = dao.GetById(parentid).ParentId;
            }

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "UNIT";
            setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.ListUnit = model;
            return View(itUnit);
        }

        [HttpGet]
        [HasCredential(RoleID = "ADD_UNIT")]
        public ActionResult Create(int parentid = 0)
        {
            SetViewBag(null, parentid);
            return View();
        }
        [HasCredential(RoleID = "EDIT_UNIT")]
        public ActionResult Edit(int id)
        {
            var unit = new UnitDao().ViewDetail(id);
            int parentid = unit.ParentId.Value;
            SetViewBag(null, parentid);
            return View(unit);
        }
        [HttpPost]
        [HasCredential(RoleID = "EDIT_UNIT")]
        [ValidateInput(false)]
        public ActionResult Edit(Unit model)
        {

            if (ModelState.IsValid)
            {
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
        [HttpPost]
        [HasCredential(RoleID = "ADD_UNIT")]
        public ActionResult Create(Unit unit)
        {
            if (ModelState.IsValid)
            {
                var dao = new UnitDao();

                long id = dao.Insert(unit);
                if (id > 0)
                {
                    SetAlert("Thêm đơn vị thành công", "success");
                    return RedirectToAction("Index", "Unit");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm đơn vị không thành công");
                }
            }
            return View("Index");
        }
        [HttpDelete]
        [HasCredential(RoleID = "DELETE_UNIT")]
        public ActionResult Delete(int id)
        {
            var dao = new UnitDao();
            var Unit = dao.Delete(id);
            return View(Unit);
        }

        [HttpPost]
        [HasCredential(RoleID = "DELETEMULTI_UNIT")]
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
        public void SetViewBag(int? selectedId = null, int parentid = 0)
        {
            var dao = new UnitDao();
            List<SelectListItem> list = new List<SelectListItem>();
            List<Unit> lstUnit = dao.ListByParentId(parentid);
            list.Add(new SelectListItem { Value = "0", Text = "--Chọn--" });
            foreach (Unit item in lstUnit)
            {
                list.Add(new SelectListItem { Value = item.Id + "", Text = item.Title });
            }
            //SelectList sl = new SelectList(dao.ListByParentId(parentid), "Id", "Title", selectedId);
            //ViewBag.ParentId = sl;
            ViewBag.ParentId = new SelectList(list, "Value", "Text", selectedId);
        }
        public string getTitle(string code)
        {
            string title = "";
            if (code != "0")
            {
                var dao = new UnitDao();
                Unit itemSearch = new Unit();
                itemSearch.Code = code;
                List<Unit> lstUnit = dao.ListAll(itemSearch);
                if (lstUnit.Count > 0) {
                    title = lstUnit.First().Title;
                }
            }
            return title;
        }
        [HttpPost]
        public JsonResult ChangeShowOnHome(int id)
        {
            var result = new UnitDao().ChangeShowOnHome(id);
            return Json(new
            {
                status = result
            });
        }
        public string getTitleById(int Id)
        {
            string title = "";
            try
            {
                if (Id != 0)
                {
                    var dao = new UnitDao();
                    Unit itemSearch = new Unit();
                    itemSearch.Id = Id;
                    List<Unit> lstUnit = dao.ListAll(itemSearch);
                    if (lstUnit.Count > 0)
                    {
                        title = lstUnit.First().Title;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return title;
        }
    }
}