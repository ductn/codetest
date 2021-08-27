using Model.Dao;
using Model.EF;
using ClsCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class UtilityController : BaseController
    {
        // GET: Admin/Utility
        [HasCredential(RoleID = "INDEX_UTILITY")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult LoadData(string strUtility, int page = 1, int pageSize = 10)
        {
            var dao = new UtilityDao();

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            Utility Utility = serializer.Deserialize<Utility>(strUtility);
            int totalRecord = 0;
            int ParentId = 0;
            var model = dao.ListAllPaging(Utility, ref totalRecord, page, pageSize);

            return Json(new
            {
                data = model,
                total = totalRecord,
                parentid = ParentId,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [HasCredential(RoleID = "SAVE_UTILITY")]
        public JsonResult SaveData(string strUtility)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            Utility Utility = serializer.Deserialize<Utility>(strUtility);
            var dao = new UtilityDao();
            bool status = false;
            string message = string.Empty;
            //add new entity if id  = 0
            if (Utility.Id == 0)
            {
                try
                {
                    var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                    if (session != null)
                    {
                        Utility.CreatedBy = session.UserName;
                    }
                    dao.Insert(Utility);
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
                var entity = dao.GetById(Utility.Id);
                entity.Code = Utility.Code;
                entity.Name = Utility.Name;
                entity.Desription = Utility.Desription;
                entity.Sort = Utility.Sort;
                try
                {
                    var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                    if (session != null)
                    {
                        entity.ModifiedBy = session.UserName;
                    }
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
            var dao = new UtilityDao();
            var entity = dao.ViewDetail(id);
            return Json(new
            {
                data = entity,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [HasCredential(RoleID = "DELETE_UTILITY")]
        public JsonResult Delete(int id)
        {
            try
            {
                var result = new UtilityDao().Delete(id);
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
        [HasCredential(RoleID = "DELETEMULTI_UTILITY")]
        public JsonResult DeleteMulti(string[] ids)
        {
            try
            {
                var result = false;
                foreach (var i in ids)
                {
                    int id = int.Parse(i.ToString());
                    result = new UtilityDao().Delete(id);
                }
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
        [HasCredential(RoleID = "EDIT_UTILITY")]
        public JsonResult ChangeStatus(int id)
        {
            var result = new UtilityDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}