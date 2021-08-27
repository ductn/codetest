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
    public class MaterialController : BaseController
    {
        // GET: Admin/Material
        [HasCredential(RoleID = "INDEX_MATERIAL")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult LoadData(string strMaterial, int page = 1, int pageSize = 10)
        {
            var dao = new MaterialDao();

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            Material Material = serializer.Deserialize<Material>(strMaterial);
            int totalRecord = 0;
            int ParentId = 0;
            var model = dao.ListAllPaging(Material,ref totalRecord, page, pageSize);

            return Json(new
            {
                data = model,
                total = totalRecord,
                parentid = ParentId,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [HasCredential(RoleID = "SAVE_MATERIAL")]
        public JsonResult SaveData(string strMaterial)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            Material Material = serializer.Deserialize<Material>(strMaterial);
            var dao = new MaterialDao();
            bool status = false;
            string message = string.Empty;
            //add new entity if id  = 0
            if (Material.Id == 0)
            {
                try
                {
                    var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                    if (session != null)
                    {
                        Material.CreatedBy = session.UserName;
                    }
                    dao.Insert(Material);
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
                var entity = dao.GetById(Material.Id);
                entity.Code = Material.Code;
                entity.Name = Material.Name;
                entity.Desription = Material.Desription;
                entity.Sort = Material.Sort;
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
            var dao = new MaterialDao();
            var entity = dao.ViewDetail(id);
            return Json(new
            {
                data = entity,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [HasCredential(RoleID = "DELETE_MATERIAL")]
        public JsonResult Delete(int id)
        {
            try
            {
                var result = new MaterialDao().Delete(id);
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
        [HasCredential(RoleID = "DELETEMULTI_MATERIAL")]
        public JsonResult DeleteMulti(string[] ids)
        {
            try
            {
                var result = false;
                foreach (var i in ids)
                {
                    int id = int.Parse(i.ToString());
                    result = new MaterialDao().Delete(id);
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
        [HasCredential(RoleID = "EDIT_MATERIAL")]
        public JsonResult ChangeStatus(int id)
        {
            var result = new MaterialDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }

    }
}