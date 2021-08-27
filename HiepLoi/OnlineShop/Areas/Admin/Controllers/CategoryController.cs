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
    public class CategoryController : BaseController
    {
        // GET: Admin/Category
        [HasCredential(RoleID = "INDEX_CATEGORY")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new CategoryDao();
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
            string controller = "CATEGORY";
            setPermission(controller);
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            return View(model);
        }

        [HttpGet]
        [HasCredential(RoleID = "ADD_CATEGORY")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [HasCredential(RoleID = "ADD_CATEGORY")]
        public ActionResult Create(Category model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                    model.CreatedBy = session.UserName;
                    model.CreatedDate = DateTime.Now;
                    if (!string.IsNullOrEmpty(model.Name))
                    {
                        model.MetaTitle = StringHelper.ToUnsignString(model.Name);
                    }
                    model.ShowOnHome = false;
                    model.Language = "VN";
                    new CategoryDao().Insert(model);
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
        [HasCredential(RoleID = "EDIT_CATEGORY")]
        public ActionResult Edit(int id)
        {
            var dao = new CategoryDao();
            var Category = dao.GetById(id);
            return View(Category);
        }

        [HttpPost]
        [HasCredential(RoleID = "EDIT_CATEGORY")]
        [ValidateInput(false)]
        public ActionResult Edit(Category model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                    model.ModifiedBy = session.UserName;
                    model.ModifiedDate = DateTime.Now;
                    if (!string.IsNullOrEmpty(model.Name))
                    {
                        model.MetaTitle = StringHelper.ToUnsignString(model.Name);
                    }
                    model.ShowOnHome = false;
                    model.Language = "VN";
                    new CategoryDao().Update(model);
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

        [HasCredential(RoleID = "VIEW_CATEGORY")]
        public ActionResult View(int id)
        {
            var Category = new CategoryDao().ViewDetail(id);
            ViewBag.Category = Category;
            return View(Category);
        }

        public string getName(int id)
        {
            string name = "";
            try
            {
                if (id != 0)
                {
                    var Category = new CategoryDao().ViewDetail(id);
                    if (Category != null)
                    {
                        name = Category.Name;
                    }
                }
            }
            catch (Exception)
            {

            }
            return name;
        }

        [HttpDelete]
        [HasCredential(RoleID = "DELETE_CATEGORY")]
        public ActionResult Delete(int id)
        {
            var dao = new CategoryDao();
            var Category = dao.Delete(id);
            return View(Category);
        }

        [HttpPost]
        [HasCredential(RoleID = "DELETEMULTI_CATEGORY")]
        public JsonResult DeleteMulti(string[] ids)
        {
            try
            {
                var result = false;
                foreach (var i in ids)
                {
                    int id = int.Parse(i.ToString());
                    result = new CategoryDao().Delete(id);
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

        [HttpPost]
        [HasCredential(RoleID = "EDIT_USER")]
        public JsonResult ChangeStatus(long id)
        {
            var result = new CategoryDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }

        [HttpPost]
        public JsonResult ChangeGoiYMuaSam(long id)
        {
            var result = new CategoryDao().ChangeGoiYMuaSam(id);
            return Json(new
            {
                status = result
            });
        }

        [HttpPost]
        public JsonResult loadSelectUnit(int Id)
        {
            Unit obj = new Unit();
            var emtity = new UnitDao().ListAll(obj);

            return Json(new
            {
                data = emtity,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetDetailCategoryUnit(int id)
        {
            var dao = new CategoryUnitDao();
            var entity = dao.ListByCategoryIdViewModel(id);
            var TitleCategory = new CategoryDao().ViewDetail((long)id).Name;
            return Json(new
            {
                data = entity,
                TitleCategory = TitleCategory,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SaveCategoryUnit(string json)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            CategoryUnit categoryUnit = serializer.Deserialize<CategoryUnit>(json);
            var dao = new CategoryUnitDao();
            bool status = false;
            string message = string.Empty;

            var model = dao.ListByCategoryUnit(categoryUnit.CategoryId, categoryUnit.UnitId);
            if (model == null)
            {
                try
                {
                    CategoryUnit entity = new CategoryUnit();
                    entity.CategoryId = categoryUnit.CategoryId;
                    entity.UnitId = categoryUnit.UnitId;
                    dao.Insert(entity);
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
                try
                {
                    model.CategoryId = categoryUnit.CategoryId;
                    model.UnitId = categoryUnit.UnitId;
                    dao.Update(model);
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

        [HttpPost]
        public JsonResult deleteCategoryUnit(int CategoryUnitId)
        {
            try
            {
                var result = new CategoryUnitDao().Delete((long)CategoryUnitId);
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
    }
}