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
    public class SliderController : BaseController
    {
        // GET: Admin/Slide
        [HasCredential(RoleID = "INDEX_SLIDER")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new SliderDao();
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
            string controller = "SLIDER";
            setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.MainDomain = CommonConstants.DomainName;
            return View(model);
        }

        [HttpGet]
        [HasCredential(RoleID = "ADD_SLIDER")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [HasCredential(RoleID = "ADD_SLIDER")]
        public ActionResult Create(Slider model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                    new SliderDao().Insert(model);
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
        [HasCredential(RoleID = "EDIT_SLIDER")]
        public ActionResult Edit(int id)
        {
            var dao = new SliderDao();
            var Slide = dao.GetById(id);
            return View(Slide);
        }

        [HttpPost]
        [HasCredential(RoleID = "EDIT_SLIDER")]
        [ValidateInput(false)]
        public ActionResult Edit(Slider model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                    new SliderDao().Update(model);
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

        [HasCredential(RoleID = "VIEW_SLIDER")]
        public ActionResult View(int id)
        {
            var Slide = new SliderDao().ViewDetail(id);
            ViewBag.Slide = Slide;
            ViewBag.MainDomain = CommonConstants.DomainName;
            return View(Slide);
        }

        [HttpDelete]
        [HasCredential(RoleID = "DELETE_SLIDER")]
        public ActionResult Delete(int id)
        {
            var dao = new SliderDao();
            var Slide = dao.Delete(id);
            return View(Slide);
        }

        [HttpPost]
        [HasCredential(RoleID = "DELETEMULTI_SLIDER")]
        public JsonResult DeleteMulti(string[] ids)
        {
            try
            {
                var result = false;
                foreach (var i in ids)
                {
                    int id = int.Parse(i.ToString());
                    result = new SliderDao().Delete(id);
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