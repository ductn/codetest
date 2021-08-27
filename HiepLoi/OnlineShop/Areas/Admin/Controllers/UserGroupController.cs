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
    public class UserGroupController : BaseController
    {
        // GET: Admin/UserGroup
        [HasCredential(RoleID = "INDEX_USERGROUP")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new UserGroupDao();
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
            string controller = "USERGROUP";
            setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            SetViewBag();
            return View(model);
        }

        [HttpGet]
        [HasCredential(RoleID = "ADD_USERGROUP")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [HasCredential(RoleID = "ADD_USERGROUP")]
        public ActionResult Create(UserGroup model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                    //model.Creator = session.UserName;
                    //model.Created = DateTime.Now;
                    new UserGroupDao().Insert(model);
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
            SetViewBag();
            return View();
        }

        [HttpGet]
        [HasCredential(RoleID = "EDIT_USERGROUP")]
        public ActionResult Edit(String id)
        {
            var dao = new UserGroupDao();
            var UserGroup = dao.GetById(id);
            SetViewBag();
            return View(UserGroup);
        }

        [HttpPost]
        [HasCredential(RoleID = "EDIT_USERGROUP")]
        [ValidateInput(false)]
        public ActionResult Edit(UserGroup model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                    //model.Modifier = session.UserName;
                    //model.Modified = DateTime.Now;
                    new UserGroupDao().Update(model);
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

        //[HasCredential(RoleID = "VIEW_USERGROUP")]
        //public ActionResult View(String id)
        //{
        //    var UserGroup = new UserGroupDao().ViewDetail(id);
        //    ViewBag.UserGroup = UserGroup;
        //    return View(UserGroup);
        //}

        [HttpDelete]
        [HasCredential(RoleID = "DELETE_USERGROUP")]
        public ActionResult Delete(String id)
        {
            var dao = new UserGroupDao();
            var UserGroup = dao.Delete(id);
            return View(UserGroup);
        }

        [HttpPost]
        [HasCredential(RoleID = "DELETEMULTI_USERGROUP")]
        public JsonResult DeleteMulti(string[] ids)
        {
            try
            {
                var result = false;
                foreach (var i in ids)
                {
                    String id = i.ToString();
                    result = new UserGroupDao().Delete(id);
                }
                Session[CommonConstants.MSG_SUCCESS] = "Xóa thành công";
                return Json(new
                {
                    UserGroup = result
                });
            }
            catch (Exception ex)
            {
                Session[CommonConstants.MSG_ERROR] = "Xóa thất bại";
                return Json(new
                {
                    UserGroup = false,
                    message = ex.Message
                });

            }
        }
        public void SetViewBag(long? selectedId = null)
        {

        }
    }
}