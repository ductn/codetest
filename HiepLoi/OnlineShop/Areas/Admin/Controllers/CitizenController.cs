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
    public class CitizenController : BaseController
    {
        // GET: Admin/Citizen
        [HasCredential(RoleID = "INDEX_CITIZEN")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new CitizenDao();
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
            string controller = "CITIZEN";
            setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            return View(model);
        }

        [HttpGet]
        [HasCredential(RoleID = "ADD_CITIZEN")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [HasCredential(RoleID = "ADD_CITIZEN")]
        public ActionResult Create(Citizen model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                    model.Creator = session.UserName;
                    model.Created = DateTime.Now;
                    new CitizenDao().Insert(model);
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
        [HasCredential(RoleID = "EDIT_CITIZEN")]
        public ActionResult Edit(int id)
        {
            var dao = new CitizenDao();
            var Citizen = dao.GetById(id);
            return View(Citizen);
        }

        [HttpPost]
        [HasCredential(RoleID = "EDIT_CITIZEN")]
        [ValidateInput(false)]
        public ActionResult Edit(Citizen model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                    model.Modifier = session.UserName;
                    model.Modified = DateTime.Now;
                    new CitizenDao().Update(model);
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

        [HasCredential(RoleID = "VIEW_CITIZEN")]
        public ActionResult View(int id)
        {
            var Citizen = new CitizenDao().ViewDetail(id);
            ViewBag.Citizen = Citizen;
            return View(Citizen);
        }

        [HttpDelete]
        [HasCredential(RoleID = "DELETE_CITIZEN")]
        public ActionResult Delete(int id)
        {
            var dao = new CitizenDao();
            var Citizen = dao.Delete(id);
            return View(Citizen);
        }

        [HttpPost]
        [HasCredential(RoleID = "DELETEMULTI_CITIZEN")]
        public JsonResult DeleteMulti(string[] ids)
        {
            try
            {
                var result = false;
                foreach (var i in ids)
                {
                    int id = int.Parse(i.ToString());
                    result = new CitizenDao().Delete(id);
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