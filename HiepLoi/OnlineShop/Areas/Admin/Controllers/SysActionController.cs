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
    public class SysActionController : BaseController
    {
        // GET: Admin/SysAction
        [HasCredential(RoleID = "INDEX_SYSACTION")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new SysActionDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            SysAction itSysAction = new SysAction();
            var model = dao.ListAllPaging(itSysAction, searchString, ref totalRecord, page, pageSize);

            ViewBag.SearchString = searchString;
            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "SYSACTION";

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
            ViewBag.IdSysProcedure = new SelectList(listQuyTrinh, "Value", "Text", itSysAction.IdSysProcedure);
            ViewBag.ListSysAction = model;
            return View(itSysAction);
        }

        [HttpPost]
        [HasCredential(RoleID = "INDEX_SYSACTION")]
        public ActionResult Index(SysAction itSysAction)
        {
            string searchString = "";
            int page = 1;
            int pageSize = 5;
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new SysActionDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            var model = dao.ListAllPaging(itSysAction, searchString, ref totalRecord, page, pageSize);

            ViewBag.SearchString = searchString;
            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "SYSACTION";

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
            ViewBag.IdSysProcedure = new SelectList(listQuyTrinh, "Value", "Text", itSysAction.IdSysProcedure);
            ViewBag.ListSysAction = model;
            return View(itSysAction);
        }

        [HttpGet]
        [HasCredential(RoleID = "ADD_SYSACTION")]
        public ActionResult Create(int IdSysProcedure)
        {
            SysAction item = new SysAction();
            item.IdSysProcedure = IdSysProcedure;
            SetViewBag(IdSysProcedure, null);
            return View(item);
        }

        [HttpPost]
        [ValidateInput(false)]
        [HasCredential(RoleID = "ADD_SYSACTION")]
        public ActionResult Create(SysAction model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                    model.UserGroups = model.lstUserGroup;
                    new SysActionDao().Insert(model);
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
        [HasCredential(RoleID = "EDIT_SYSACTION")]
        public ActionResult Edit(int id)
        {
            var dao = new SysActionDao();
            var SysAction = dao.GetById(id);
            SysAction.lstUserGroup = SysAction.UserGroups;
            int IdSysProcedure = SysAction.IdSysProcedure;
            SetViewBag(IdSysProcedure, null);
            return View(SysAction);
        }

        [HttpPost]
        [HasCredential(RoleID = "EDIT_SYSACTION")]
        [ValidateInput(false)]
        public ActionResult Edit(SysAction model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                    model.UserGroups = model.lstUserGroup;
                    new SysActionDao().Update(model);
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

        [HasCredential(RoleID = "VIEW_SYSACTION")]
        public ActionResult View(int id)
        {
            var SysAction = new SysActionDao().ViewDetail(id);
            ViewBag.SysAction = SysAction;
            return View(SysAction);
        }

        [HttpDelete]
        [HasCredential(RoleID = "DELETE_SYSACTION")]
        public ActionResult Delete(int id)
        {
            var dao = new SysActionDao();
            var SysAction = dao.Delete(id);
            return View(SysAction);
        }

        [HttpPost]
        [HasCredential(RoleID = "DELETEMULTI_SYSACTION")]
        public JsonResult DeleteMulti(string[] ids)
        {
            try
            {
                var result = false;
                foreach (var i in ids)
                {
                    int id = int.Parse(i.ToString());
                    result = new SysActionDao().Delete(id);
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
        public string getTitle(int ActionId)
        {
            var dao = new SysActionDao();
            var itemSearch = dao.GetById(ActionId);
            string title = "";
            if (itemSearch != null)
            {
                title = itemSearch.Title;
            }
            return title;
        }
        //public void SetViewBag(long? selectedId = null)
        //{
        //    var dao = new StatusDao();
        //    var daoProcedure = new SysProcedureDao();
        //    List<SelectListItem> list = new List<SelectListItem>();
        //    List<SelectListItem> listQuyTrinh = new List<SelectListItem>();
        //    List<SysProcedure> lstProcedure = daoProcedure.ListAll(null);
        //    List<Status> lstStatus = dao.ListAll(null);
        //    list.Add(new SelectListItem { Value = "0", Text = "--Chọn--" });
        //    foreach (Status item in lstStatus)
        //    {
        //        list.Add(new SelectListItem { Value = item.StatusId + "", Text = item.Title });
        //    }
        //    foreach(SysProcedure item in lstProcedure)
        //    {
        //        listQuyTrinh.Add(new SelectListItem { Value = item.Id + "", Text = item.Title });
        //    }
        //    ViewBag.CurrentStatus = new SelectList(list, "Value", "Text", selectedId);
        //    ViewBag.NextStatus = new SelectList(list, "Value", "Text", selectedId);
        //    ViewBag.IdSysProcedure = new SelectList(listQuyTrinh, "Value", "Text", selectedId);
        //}
        public void SetViewBag(int IdSysProcedure, long? selectedId = null)
        {
            var dao = new StatusDao();
            var daoProcedure = new SysProcedureDao();
            List<SelectListItem> list = new List<SelectListItem>();
            List<SelectListItem> listQuyTrinh = new List<SelectListItem>();
            List<SysProcedure> lstProcedure = daoProcedure.ListAll(null);
            List<Status> lstStatus;
            if (IdSysProcedure != 0)
            {
                Status itStatus = new Status();
                itStatus.IdSysProcedure = IdSysProcedure;
                lstStatus = dao.ListAll(itStatus);
            }
            else
            {
                lstStatus = dao.ListAll(null);
            }
             
            list.Add(new SelectListItem { Value = "0", Text = "--Chọn--" });
            foreach (Status item in lstStatus)
            {
                list.Add(new SelectListItem { Value = item.StatusId + "", Text = item.Title });
            }
            listQuyTrinh.Add(new SelectListItem { Value = "0", Text = "--Chọn--" });
            foreach (SysProcedure item in lstProcedure)
            {
                listQuyTrinh.Add(new SelectListItem { Value = item.Id + "", Text = item.Title });
            }
            var daoUserGroup = new UserGroupDao();
            List<SelectListItem> listUserGroup = new List<SelectListItem>();
            List<UserGroup> lstVaiTro = daoUserGroup.ListAll(null);
            foreach (UserGroup item in lstVaiTro)
            {
                listUserGroup.Add(new SelectListItem { Value = item.ID + "", Text = item.Name });
            }
            ViewBag.UserGroups = new SelectList(listUserGroup, "Value", "Text", selectedId);

            ViewBag.CurrentStatus = new SelectList(list, "Value", "Text", selectedId);
            ViewBag.NextStatus = new SelectList(list, "Value", "Text", selectedId);
            ViewBag.IdSysProcedure = new SelectList(listQuyTrinh, "Value", "Text", selectedId);
        }
    }
}