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
    public class SysProcedureController : BaseController
    {
        // GET: Admin/SysProcedure
        [HasCredential(RoleID = "INDEX_SYSPROCEDURE")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new SysProcedureDao();
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
            string controller = "SYSPROCEDURE";
            setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            return View(model);
        }

        [HttpGet]
        [HasCredential(RoleID = "ADD_SYSPROCEDURE")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [HasCredential(RoleID = "ADD_SYSPROCEDURE")]
        public ActionResult Create(SysProcedure model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                    new SysProcedureDao().Insert(model);
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
        [HasCredential(RoleID = "EDIT_SYSPROCEDURE")]
        public ActionResult Edit(int id)
        {
            var dao = new SysProcedureDao();
            var SysProcedure = dao.GetById(id);
            return View(SysProcedure);
        }

        [HttpPost]
        [HasCredential(RoleID = "EDIT_SYSPROCEDURE")]
        [ValidateInput(false)]
        public ActionResult Edit(SysProcedure model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                    new SysProcedureDao().Update(model);
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

        [HasCredential(RoleID = "VIEW_SYSPROCEDURE")]
        public ActionResult View(int id)
        {
            var SysProcedure = new SysProcedureDao().ViewDetail(id);
            ViewBag.SysProcedure = SysProcedure;
            return View(SysProcedure);
        }

        [HttpDelete]
        [HasCredential(RoleID = "DELETE_SYSPROCEDURE")]
        public ActionResult Delete(int id)
        {
            var dao = new SysProcedureDao();
            var SysProcedure = dao.Delete(id);
            return View(SysProcedure);
        }

        [HttpPost]
        [HasCredential(RoleID = "DELETEMULTI_SYSPROCEDURE")]
        public JsonResult DeleteMulti(string[] ids)
        {
            try
            {
                var result = false;
                foreach (var i in ids)
                {
                    int id = int.Parse(i.ToString());
                    result = new SysProcedureDao().Delete(id);
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
        public string getTitle(int id)
        {
            var dao = new SysProcedureDao();
            SysProcedure itemSearch = new SysProcedure();
            itemSearch = dao.GetById(id);
            string title = "";
            if (itemSearch != null)
            {
                title = itemSearch.Title;
            }
            return title;
        }
    }
}