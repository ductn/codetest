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
    public class MenuController : BaseController
    {
        // GET: Admin/Menu
        [HasCredential(RoleID = "INDEX_MENU")]
        public ActionResult Index(string searchString, int parentid = 0, int page = 1, int pageSize = 10)
        {
            var dao = new MenuDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            Menu itMenu = new Menu();
            itMenu.ParentId = parentid;
            var model = dao.ListAllPaging(itMenu, parentid, ref totalRecord, searchString, page, pageSize);

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
            string controller = "MENU";
            setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.ListMenu = model;
            return View(itMenu);
        }
        [HttpPost]
        [HasCredential(RoleID = "INDEX_MENU")]
        public ActionResult Index(Menu itMenu)
        {
            var dao = new MenuDao();
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
            int? parentid = itMenu.ParentId;
            var model = dao.ListAllPaging(itMenu, parentid, ref totalRecord, searchString, page, pageSize);

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
            string controller = "MENU";
            setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.ListMenu = model;
            return View(itMenu);
        }

        [HttpGet]
        [HasCredential(RoleID = "ADD_MENU")]
        public ActionResult Create(int parentid = 0)
        {
            Menu entity = new Menu();
            SetViewBag(entity);
            return View();
        }
        [HasCredential(RoleID = "EDIT_MENU")]
        public ActionResult Edit(int id)
        {
            var Menu = new MenuDao().ViewDetail(id);
            SetViewBag(Menu);
            return View(Menu);
        }
        [HttpPost]
        [ValidateInput(false)]
        [HasCredential(RoleID = "EDIT_MENU")]
        public ActionResult Edit(Menu model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                    new MenuDao().Update(model);
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
        [HasCredential(RoleID = "ADD_MENU")]
        public ActionResult Create(Menu Menu)
        {
            if (ModelState.IsValid)
            {
                var dao = new MenuDao();

                long id = dao.Insert(Menu);
                if (id > 0)
                {
                    SetAlert("Thêm đơn vị thành công", "success");
                    return RedirectToAction("Index", "MENU");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm đơn vị không thành công");
                }
            }
            return View("Index");
        }
        [HttpDelete]
        [HasCredential(RoleID = "DELETE_MENU")]
        public ActionResult Delete(int id)
        {
            var dao = new MenuDao();
            var Menu = dao.Delete(id);
            return View(Menu);
        }

        [HttpPost]
        [HasCredential(RoleID = "DELETEMULTI_MENU")]
        public JsonResult DeleteMulti(string[] ids)
        {
            try
            {
                var result = false;
                foreach (var i in ids)
                {
                    int id = int.Parse(i.ToString());
                    result = new MenuDao().Delete(id);
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
        public void SetViewBag(Menu entity)
        {
            var dao = new MenuDao();
            List<SelectListItem> list = new List<SelectListItem>();
            List<Menu> lstMenu = dao.ListByParentId(entity.ParentId);
            list.Add(new SelectListItem { Value = "0", Text = "--Chọn--" });
            foreach (Menu item in lstMenu)
            {
                list.Add(new SelectListItem { Value = item.ID + "", Text = item.Text });
            }
            ViewBag.ParentId = new SelectList(list, "Value", "Text", entity.ParentId);

            List<SelectListItem> listTypeID = new List<SelectListItem>();
            listTypeID.Add(new SelectListItem { Value = "0", Text = "--Chọn--" });
            foreach (MenuType item in dao.ListMenuType())
            {
                listTypeID.Add(new SelectListItem { Value = item.ID + "", Text = item.Name});
            }
            ViewBag.TypeID = new SelectList(listTypeID, "Value", "Text", entity.TypeID);
        }
    }
}