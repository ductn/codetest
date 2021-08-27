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
    public class StatusController : BaseController
    {
        // GET: Admin/Status
        [HasCredential(RoleID = "INDEX_STATUS")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new StatusDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            Status itStatus = new Status();
            var model = dao.ListAllPaging(itStatus, searchString, ref totalRecord, page, pageSize);

            ViewBag.SearchString = searchString;
            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "STATUS";

            //Dropdown DS quy trình
            var daoProcedure = new SysProcedureDao();
            List<SelectListItem> listQuyTrinh = new List<SelectListItem>();
            List<SysProcedure> lstProcedure = daoProcedure.ListAll(null);
            listQuyTrinh.Add(new SelectListItem { Value = "0", Text = "--Chọn--" });
            foreach (SysProcedure item in lstProcedure)
            {
                listQuyTrinh.Add(new SelectListItem { Value = item.Id + "", Text = item.Title });
            }
            //End Dropdown DS quy trình

            setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.IdSysProcedure = new SelectList(listQuyTrinh, "Value", "Text", itStatus.IdSysProcedure);
            ViewBag.ListStatus = model;
            return View(itStatus);
        }

        [HttpPost]
        [HasCredential(RoleID = "INDEX_STATUS")]
        public ActionResult Index(Status itStatus)
        {
            string searchString = "";
            int page = 1;
            int pageSize = 5;
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new StatusDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            var model = dao.ListAllPaging(itStatus, searchString, ref totalRecord, page, pageSize);

            ViewBag.SearchString = searchString;
            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "STATUS";

            //Dropdown DS quy trình
            var daoProcedure = new SysProcedureDao();
            List<SelectListItem> listQuyTrinh = new List<SelectListItem>();
            List<SysProcedure> lstProcedure = daoProcedure.ListAll(null);
            listQuyTrinh.Add(new SelectListItem { Value = "0", Text = "--Chọn--" });
            foreach (SysProcedure item in lstProcedure)
            {
                listQuyTrinh.Add(new SelectListItem { Value = item.Id + "", Text = item.Title });
            }
            //End Dropdown DS quy trình

            setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.IdSysProcedure = new SelectList(listQuyTrinh, "Value", "Text", itStatus.IdSysProcedure);
            ViewBag.ListStatus = model;
            return View(itStatus);
        }

        [HttpGet]
        [HasCredential(RoleID = "ADD_STATUS")]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [HasCredential(RoleID = "ADD_STATUS")]
        public ActionResult Create(Status model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                    //model.Creator = session.UserName;
                    //model.Created = DateTime.Now;
                    new StatusDao().Insert(model);
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
        [HasCredential(RoleID = "EDIT_STATUS")]
        public ActionResult Edit(int id)
        {
            var dao = new StatusDao();
            var Status = dao.GetById(id);
            SetViewBag();
            return View(Status);
        }

        [HttpPost]
        [HasCredential(RoleID = "EDIT_STATUS")]
        [ValidateInput(false)]
        public ActionResult Edit(Status model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                    //model.Modifier = session.UserName;
                    //model.Modified = DateTime.Now;
                    new StatusDao().Update(model);
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

        [HasCredential(RoleID = "VIEW_STATUS")]
        public ActionResult View(int id)
        {
            var Status = new StatusDao().ViewDetail(id);
            ViewBag.Status = Status;
            return View(Status);
        }

        [HttpDelete]
        [HasCredential(RoleID = "DELETE_STATUS")]
        public ActionResult Delete(int id)
        {
            var dao = new StatusDao();
            var Status = dao.Delete(id);
            return View(Status);
        }

        [HttpPost]
        [HasCredential(RoleID = "DELETEMULTI_STATUS")]
        public JsonResult DeleteMulti(string[] ids)
        {
            try
            {
                var result = false;
                foreach (var i in ids)
                {
                    int id = int.Parse(i.ToString());
                    result = new StatusDao().Delete(id);
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
        public string getTitle(int StatusId, int IdSysProcedure)
        {
            var dao = new StatusDao();
            Status itemSearch = new Status();
            itemSearch.StatusId = StatusId;
            itemSearch.IdSysProcedure = IdSysProcedure;
            itemSearch = dao.ListAll(itemSearch).FirstOrDefault();
            string title = "";
            if (itemSearch !=null)
            {
                title = itemSearch.Title;
            }
            return title;
        }
        public void SetViewBag(long? selectedId = null)
        {
            var daoProcedure = new SysProcedureDao();
            List<SelectListItem> listQuyTrinh = new List<SelectListItem>();
            List<SysProcedure> lstProcedure = daoProcedure.ListAll(null);
            foreach (SysProcedure item in lstProcedure)
            {
                listQuyTrinh.Add(new SelectListItem { Value = item.Id + "", Text = item.Title });
            }
            ViewBag.IdSysProcedure = new SelectList(listQuyTrinh, "Value", "Text", selectedId);
        }
    }
}