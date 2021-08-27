using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClsCommon;
using CommonConstants = ClsCommon.CommonConstants;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Globalization;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ContactStoreController : BaseController
    {
        // GET: Admin/Contact
        [HasCredential(RoleID = "INDEX_CONTACTSTORE")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new ContactStoreDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            ContactStore itContactStore = new ContactStore();
            var model = dao.ListAllPaging(itContactStore, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "CONTACTSTORE";
            setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.ListContactStore = model;
            return View(itContactStore);
        }

        [HttpPost]
        [HasCredential(RoleID = "INDEX_CONTACTSTORE")]
        public ActionResult Index(ContactStore itContactStore)
        {
            string searchString = "";
            int page = 1;
            int pageSize = 5;
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new ContactStoreDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            var model = dao.ListAllPaging(itContactStore, searchString, ref totalRecord, page, pageSize);


            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "CONTACTSTORE";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListContactStore = model;
            return View(itContactStore);
        }

        //Gian hàng đang soạn
        [HasCredential(RoleID = "CHOTIEPNHAN_CONTACTSTORE")]
        public ActionResult ListChoTiepNhan(string searchString, int page = 1, int pageSize = 5)
        {
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new ContactStoreDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            ContactStore itContactStore = new ContactStore();
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];

            itContactStore.StatusId = CommonConstants.LIENHEGIANHANG_CHOTIEPNHAN;
            var model = dao.ListAllPaging(itContactStore, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "CONTACTSTORE";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListContactStore = model;
            return View(itContactStore);
        }

        [HttpPost]
        [HasCredential(RoleID = "CHOTIEPNHAN_CONTACTSTORE")]
        public ActionResult ListChoTiepNhan(ContactStore itContactStore)
        {
            string searchString = "";
            int page = 1;
            int pageSize = 5;
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new ContactStoreDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            itContactStore.StatusId = CommonConstants.LIENHEGIANHANG_CHOTIEPNHAN;
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];

            var model = dao.ListAllPaging(itContactStore, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "CONTACTSTORE";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListContactStore = model;
            return View(itContactStore);
        }
        //End Gian hàng đang soạn

        //Gian hàng chờ duyệt
        [HasCredential(RoleID = "DANGXULY_CONTACTSTORE")]
        public ActionResult ListDangXuLy(string searchString, int page = 1, int pageSize = 5)
        {
            string showLuanChuyen = "true";
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new ContactStoreDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            ContactStore itContactStore = new ContactStore();
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];

            itContactStore.StatusId = CommonConstants.LIENHEGIANHANG_DANGXULY;
            var model = dao.ListAllPaging(itContactStore, searchString, ref totalRecord, page, pageSize);
            if (session.GroupID != CommonConstants.PHEDUYET_GROUP)
            {
                showLuanChuyen = "false";
            }
            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "CONTACTSTORE";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListContactStore = model;
            ViewBag.showLuanChuyen = showLuanChuyen;
            return View(itContactStore);
        }
        [HttpPost]
        [HasCredential(RoleID = "DANGXULY_CONTACTSTORE")]
        public ActionResult ListDangXuLy(ContactStore itContactStore)
        {
            string showLuanChuyen = "true";
            string searchString = "";
            int page = 1;
            int pageSize = 5;
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new ContactStoreDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            itContactStore.StatusId = CommonConstants.LIENHEGIANHANG_DANGXULY;
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session.GroupID != CommonConstants.PHEDUYET_GROUP)
            {
                showLuanChuyen = "false";
            }

            var model = dao.ListAllPaging(itContactStore, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "CONTACTSTORE";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListContactStore = model;
            ViewBag.showLuanChuyen = showLuanChuyen;
            return View(itContactStore);
        }
        //End Gian hàng chờ duyệt

        //Gian hàng không được duyệt
        [HasCredential(RoleID = "DUOCDUYET_CONTACTSTORE")]
        public ActionResult ListDuocDuyet(string searchString, int page = 1, int pageSize = 5)
        {
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new ContactStoreDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            ContactStore itContactStore = new ContactStore();
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];

            itContactStore.StatusId = CommonConstants.LIENHEGIANHANG_DUOCDUYET;
            var model = dao.ListAllPaging(itContactStore, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "CONTACTSTORE";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListContactStore = model;
            return View(itContactStore);
        }
        [HttpPost]
        [HasCredential(RoleID = "DUOCDUYET_CONTACTSTORE")]
        public ActionResult ListDuocDuyet(ContactStore itContactStore)
        {
            string searchString = "";
            int page = 1;
            int pageSize = 5;
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new ContactStoreDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            itContactStore.StatusId = CommonConstants.LIENHEGIANHANG_DUOCDUYET;
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];

            var model = dao.ListAllPaging(itContactStore, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "CONTACTSTORE";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListContactStore = model;
            return View(itContactStore);
        }
        //End Gian hàng không được duyệt

        //Gian hàng không được duyệt
        [HasCredential(RoleID = "KHONGDUYET_CONTACTSTORE")]
        public ActionResult ListKhongDuyet(string searchString, int page = 1, int pageSize = 5)
        {
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new ContactStoreDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            ContactStore itContactStore = new ContactStore();
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];

            itContactStore.StatusId = CommonConstants.LIENHEGIANHANG_KHONGDUYET;
            var model = dao.ListAllPaging(itContactStore, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "CONTACTSTORE";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListContactStore = model;
            return View(itContactStore);
        }
        [HttpPost]
        [HasCredential(RoleID = "KHONGDUYET_CONTACTSTORE")]
        public ActionResult ListKhongDuyet(ContactStore itContactStore)
        {
            string searchString = "";
            int page = 1;
            int pageSize = 5;
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new ContactStoreDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            itContactStore.StatusId = CommonConstants.LIENHEGIANHANG_KHONGDUYET;
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];

            var model = dao.ListAllPaging(itContactStore, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "CONTACTSTORE";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListContactStore = model;
            return View(itContactStore);
        }
        //End Gian hàng không được duyệt

        [HttpGet]
        [HasCredential(RoleID = "ADD_CONTACTSTORE")]
        public ActionResult Create()
        {
            ContactStore item = new ContactStore();
            //item.hidIndexFile = "0";
            SetViewBag();
            return View(item);
        }

        [HttpGet]
        [HasCredential(RoleID = "EDIT_CONTACTSTORE")]
        public ActionResult Edit(long id)
        {
            var dao = new ContactStoreDao();
            var ContactStore = dao.GetByID(id);

            //if (Contact.ContactImage != "" && Contact.ContactImage != "0" && Contact.ContactImage != null)
            //{
            //    dynamic jsonFile = JsonConvert.DeserializeObject(Contact.ContactImage);
            //    int i = 0;
            //    foreach (var itjon in jsonFile)
            //    {
            //        string Comment = itjon.filename;
            //        string Url = itjon.urldownload;
            //        Contact.hidListFile += "<tr id='" + i + "'><td><p><input type='hidden' id='link" + i + "' value='" + Url + "'><img src='" + Url + "' width='100' height='100'></p></td><td><textarea id='TextArea1' cols='40' rows='4'>" + Comment + "</textarea></td><td><a onclick='RemoveRow(" + i + ");'>Xóa</a></td></tr>";
            //        i += 1;
            //    }
            //    Contact.hidIndexFile = i + "";
            //}
            //SetViewBag(Contact.CategoryID);

            return View(ContactStore);
        }

        [HttpPost]
        [ValidateInput(false)]
        [HasCredential(RoleID = "EDIT_CONTACTSTORE")]
        public ActionResult Edit(ContactStore model)
        {
            string rec;
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (ModelState.IsValid)
            {
                try
                {

                    model.ModifiedBy = session.UserName;
                    model.ModifiedDate = DateTime.Now;

                    string content = System.IO.File.ReadAllText(Server.MapPath("~/assets/client/template/RepContact.html"));
                    content = content.Replace("{{Name}}", model.Name);
                    content = content.Replace("{{Phone}}", model.Phone);
                    content = content.Replace("{{Email}}", model.Email);
                    content = content.Replace("{{Title}}", model.Title);
                    content = content.Replace("{{Content}}", model.Content);
                    content = content.Replace("{{ContactAnswer}}", model.AnswerContent);
                    var toEmail = model.Email.ToString();

                    new ClsCommon.MailHelper().SendMail(toEmail, "Phản hồi liên hệ", content);
                    //if (model.hidFileLinkDown != "0" && model.hidFileLinkDown != "" && model.hidFileLinkDown != null)
                    //{
                    //    string[] arrFileLink = model.hidFileLinkDown.Split(new string[] { "|" }, StringSplitOptions.None);
                    //    string[] arrFileNote = model.hidFileNote.Split(new string[] { "|" }, StringSplitOptions.None);
                    //    string jsonTapTinDinhKem = "";
                    //    for (int i = 0; i <= arrFileLink.Length - 2; i++)
                    //    {
                    //        jsonTapTinDinhKem += "{'filename':'" + arrFileNote[i] + "','urldownload':'" + arrFileLink[i] + "'}" + ",";
                    //    }
                    //    model.ContactImage = "[" + jsonTapTinDinhKem.Remove(jsonTapTinDinhKem.Length - 1) + "]";// bỏ dấu , cuối cùng
                    //}

                    new ContactStoreDao().Update(model);
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

            if (session.GroupID == CommonConstants.ADMIN_GROUP)
            {
                rec = "Index";
            }
            else
            {
                if (session.GroupID == CommonConstants.QLGIANHANG_GROUP)
                {
                    rec = "ListChoTiepNhan";
                }
                else
                {
                    rec = "Index";
                }
            }
            return RedirectToAction(rec);
        }

        [HttpPost]
        [ValidateInput(false)]
        [HasCredential(RoleID = "ADD_CONTACTSTORE")]
        public ActionResult Create(ContactStore model)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                model.CreatedBy = session.UserName;

                //if (model.hidFileLinkDown != "0" && model.hidFileLinkDown != "" && model.hidFileLinkDown != null)
                //{
                //    string[] arrFileLink = model.hidFileLinkDown.Split(new string[] { "|" }, StringSplitOptions.None);
                //    string[] arrFileNote = model.hidFileNote.Split(new string[] { "|" }, StringSplitOptions.None);
                //    string jsonTapTinDinhKem = "";
                //    for (int i = 0; i <= arrFileLink.Length - 2; i++)
                //    {
                //        jsonTapTinDinhKem += "{'filename':'" + arrFileNote[i] + "','urldownload':'" + arrFileLink[i] + "'}" + ",";
                //    }
                //    model.ContactImage = "[" + jsonTapTinDinhKem.Remove(jsonTapTinDinhKem.Length - 1) + "]";// bỏ dấu , cuối cùng
                //}
                new ContactStoreDao().Create(model);
                string rec;
                if (session.GroupID == CommonConstants.ADMIN_GROUP)
                {
                    rec = "Index";
                }
                else
                {
                    if (session.GroupID == CommonConstants.QLGIANHANG_GROUP)
                    {
                        rec = "ListChoTiepNhan";
                    }
                    else
                    {
                        rec = "Index";
                    }
                }
                return RedirectToAction(rec);
            }
            SetViewBag();
            return View();
        }

        [HttpPost]
        [HasCredential(RoleID = "DELETEMULTI_CONTACTSTORE")]
        public JsonResult DeleteMulti(string[] ids)
        {
            try
            {
                var result = false;
                foreach (var i in ids)
                {
                    int id = int.Parse(i.ToString());
                    result = new ContactStoreDao().Delete(id);
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

        [HttpGet]
        public ActionResult Action(long id)
        {
            var dao = new ContactStoreDao();
            var daoLogAction = new LogActionDao();
            var daoSysAction = new SysActionDao();
            LogAction itLogAction = new LogAction();
            SysAction itSysAction = new SysAction();
            var ContactStore = dao.GetByID(id);
            itSysAction.CurrentStatus = ContactStore.StatusId;
            itSysAction.IdSysProcedure = CommonConstants.QUYTRINH_LIENHEGIANHANG;
            List<SelectListItem> listThaoTac = new List<SelectListItem>();
            List<SysAction> lstSysAction = daoSysAction.ListAll(itSysAction);
            foreach (SysAction item in lstSysAction)
            {
                listThaoTac.Add(new SelectListItem { Value = item.Id + "", Text = item.Title });
            }

            itLogAction.ObjId = Convert.ToInt32(ContactStore.ID);
            itLogAction.SysProcedureId = CommonConstants.QUYTRINH_LIENHEGIANHANG;
            List<LogAction> lstLogAction = daoLogAction.ListAll(itLogAction);

            ViewBag.SysActionId = new SelectList(listThaoTac, "Value", "Text", null);
            ViewBag.itemContactStore = ContactStore;
            ViewBag.lstLogAction = lstLogAction;
            // SetViewBag(Contact.CategoryID);
            return View(ContactStore);
        }

        [HttpPost]
        public ActionResult Action(ContactStore model)
        {
            int? curStatusId = 0;
            if (ModelState.IsValid)
            {
                try
                {
                    var dao = new ContactStoreDao();
                    var daoSysAction = new SysActionDao();
                    var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                    model.ModifiedBy = session.UserName;
                    model.ModifiedDate = DateTime.Now;
                    ContactStore item = dao.GetByID(model.ID);
                    curStatusId = item.StatusId;
                    model.Title = item.Title;
                    model.Content = item.Content;
                    model.Status = item.Status;
                    model.Email = item.Email;
                    model.Name = item.Name;
                    model.Phone = item.Phone;
                    model.AnswerContent = item.AnswerContent;
                    model.FileActtach = item.FileActtach;
                    model.Phone = item.Phone;
                    //Lấy trạng thái tiếp theo
                    var itSysAction = daoSysAction.GetById(model.SysActionId);
                    model.StatusId = itSysAction.NextStatus;
                    //End lấy trạng thái theo

                    new ContactStoreDao().Update(model);

                    //Save logAction
                    var daoLogAction = new LogActionDao();
                    LogAction itLogAction = new LogAction();
                    itLogAction.Created = DateTime.Now;
                    itLogAction.ActionId = model.SysActionId;
                    itLogAction.SysProcedureId = CommonConstants.QUYTRINH_LIENHEGIANHANG;
                    itLogAction.CurrentStatusId = curStatusId;
                    itLogAction.ObjId = Convert.ToInt32(model.ID);
                    itLogAction.UserIdCreator = Convert.ToInt32(session.UserID);
                    itLogAction.CreatorUserName = session.UserName;
                    itLogAction.CreatorName = session.Name;
                    itLogAction.Note = model.LogActionNote;

                    daoLogAction.Insert(itLogAction);
                    //End Save logAction
                    Session[CommonConstants.MSG_SUCCESS] = "Chuyển thành công";
                }
                catch (Exception ex)
                {
                    Session[CommonConstants.MSG_ERROR] = "Chuyển thất bại";
                    throw ex;
                }
            }
            else
            {
                Session[CommonConstants.MSG_ERROR] = "Chuyển thất bại";
            }
            string rec;
            rec = "CloseModel";
            return RedirectToAction(rec);
        }
        [HttpGet]
        public ActionResult CloseModel()
        {
            return View();
        }

        public string getTitle(int ID)
        {
            var dao = new ContactStoreDao();
            var itemSearch = dao.GetByID(ID);
            string title = "";
            if (itemSearch != null)
            {
                title = itemSearch.Title;
            }
            return title;
        }
        public void SetViewBag(long? selectedId = null)
        {
            //var dao = new CategoryDao();
            //ViewBag.CategoryID = new SelectList(dao.ListAll(), "ID", "Name", selectedId);
        }

    }
}