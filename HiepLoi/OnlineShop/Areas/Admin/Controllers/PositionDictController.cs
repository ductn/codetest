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
    public class PositionDictController : BaseController
    {
        // GET: Admin/PositionDict
        [HasCredential(RoleID = "INDEX_POSITIONDICT")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new PositionDictDao();
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
            string controller = "POSITIONDICT";
            setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            return View(model);
        }

        [HttpGet]
        [HasCredential(RoleID = "ADD_POSITIONDICT")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [HasCredential(RoleID = "ADD_POSITIONDICT")]
        public ActionResult Create(PositionDict model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                    model.Creator = session.UserName;
                    model.Created = DateTime.Now;
                    new PositionDictDao().Insert(model);
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
        [HasCredential(RoleID = "EDIT_POSITIONDICT")]
        public ActionResult Edit(int id)
        {
            var dao = new PositionDictDao();
            var PositionDict = dao.GetById(id);
            return View(PositionDict);
        }

        [HttpPost]
        [HasCredential(RoleID = "EDIT_POSITIONDICT")]
        [ValidateInput(false)]
        public ActionResult Edit(PositionDict model)
        {
           
            if (ModelState.IsValid)
            {
                try
                {
                    var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                    model.Modifier = session.UserName;
                    model.Modified = DateTime.Now;
                    new PositionDictDao().Update(model);
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

        [HasCredential(RoleID = "VIEW_POSITIONDICT")]
        public ActionResult View(int id)
        {
            var PositionDict = new PositionDictDao().ViewDetail(id);
            ViewBag.PositionDict = PositionDict;
            return View(PositionDict);
        }

        [HttpDelete]
        [HasCredential(RoleID = "DELETE_POSITIONDICT")]
        public ActionResult Delete(int id)
        {
            var dao = new PositionDictDao();
            var PositionDict = dao.Delete(id);
            return View(PositionDict);
        }

        [HttpPost]
        [HasCredential(RoleID = "DELETEMULTI_POSITIONDICT")]
        public JsonResult DeleteMulti(string[] ids)
        {
            try
            {
                var result = false;
                foreach (var i in ids)
                {
                    int id = int.Parse(i.ToString());
                    result = new PositionDictDao().Delete(id);
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