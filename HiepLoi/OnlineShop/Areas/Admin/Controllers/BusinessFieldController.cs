using BotDetect;
using Model.Dao;
using Model.EF;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class BusinessFieldController : BaseController
    {
        // GET: Admin/BusinessField
        [HasCredential(RoleID = "INDEX_BUSINESSFIELD")]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult LoadData(string strBusinessField, int page = 1, int pageSize = 10)
        {
            var dao = new BusinessFieldDao();

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            BusinessField businessField = serializer.Deserialize<BusinessField>(strBusinessField);

            var model = dao.ListAllPaging(businessField, page, pageSize);
            int totalRow = dao.ListAllPaging(businessField, 0, 0).Count();

            return Json(new
            {
                data = model,
                total = totalRow,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [HasCredential(RoleID = "SAVE_BUSINESSFIELD")]
        public JsonResult SaveData(string strBusinessField)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            BusinessField businessfield = serializer.Deserialize<BusinessField>(strBusinessField);
            var dao = new BusinessFieldDao();
            bool status = false;
            if (businessfield.Status == "True")
            {
                businessfield.Status = "1";
            }
            else
            {
                businessfield.Status = "0";
            }
            string message = string.Empty;
            //add new entity if id  = 0
            if (businessfield.Id == 0)
            {
                try
                {
                    dao.Insert(businessfield);
                    status = true;
                }
                catch (Exception ex)
                {

                    status = false;
                    message = ex.Message;
                }
            }
            else
            {
                //update entity if <> 0
                var entity = dao.GetById(businessfield.Id);
                entity.Name = businessfield.Name;
                entity.Code = businessfield.Code;
                entity.Status = businessfield.Status;
                try
                {
                    dao.Update(entity);
                    status = true;
                }
                catch (Exception ex)
                {

                    status = false;
                    message = ex.Message;
                }

            }
            return Json(new
            {
                status = status,
                message = message
            });
        }
        [HttpGet]
        public JsonResult GetDetail(int id)
        {
            var dao = new BusinessFieldDao();
            var entity = dao.ViewDetail(id);

            return Json(new
            {
                data = entity,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [HasCredential(RoleID = "DELETE_BUSINESSFIELD")]
        public JsonResult Delete(int id)
        {
            try
            {
                var result = new BusinessFieldDao().Delete(id);
                return Json(new
                {
                    status = result
                });
            }
            catch (Exception ex)
            {

                return Json(new
                {
                    status = false,
                    message = ex.Message
                });

            }
        }
        [HttpPost]
        [HasCredential(RoleID = "EDIT_SYSFUNCTION")]
        public JsonResult ChangeStatus(int id)
        {
            var result = new BusinessFieldDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}