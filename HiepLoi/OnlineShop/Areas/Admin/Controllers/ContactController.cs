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
    public class ContactController : BaseController
    {
        // GET: Admin/Contact
        [HasCredential(RoleID = "INDEX_CONTACT")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new ContactDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            Contact itContact = new Contact();
            var model = dao.ListAllPaging(itContact, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "CONTACT";
            setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListContact = model;
            return View(itContact);
        }

        [HttpPost]
        [HasCredential(RoleID = "INDEX_CONTACT")]
        public ActionResult Index(Contact itContact)
        {
            string searchString = "";
            int page = 1;
            int pageSize = 5;
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new ContactDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            var model = dao.ListAllPaging(itContact, searchString, ref totalRecord, page, pageSize);


            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "CONTACT";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListContact = model;
            return View(itContact);
        }

        //Gian hàng đang soạn
        [HasCredential(RoleID = "CHOTIEPNHAN_CONTACT")]
        public ActionResult ListChoTiepNhan(string searchString, int page = 1, int pageSize = 5)
        {
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new ContactDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            Contact itContact = new Contact();
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];

            itContact.StatusId = CommonConstants.LIENHENGUOIDUNG_CHOTIEPNHAN;
            var model = dao.ListAllPaging(itContact, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "CONTACT";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListContact = model;
            return View(itContact);
        }

        [HttpPost]
        [HasCredential(RoleID = "CHOTIEPNHAN_CONTACT")]
        public ActionResult ListChoTiepNhan(Contact itContact)
        {
            string searchString = "";
            int page = 1;
            int pageSize = 5;
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new ContactDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            itContact.StatusId = CommonConstants.LIENHENGUOIDUNG_CHOTIEPNHAN;
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];

            var model = dao.ListAllPaging(itContact, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "CONTACT";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListContact = model;
            return View(itContact);
        }
        //End Gian hàng đang soạn

        //Gian hàng chờ duyệt
        [HasCredential(RoleID = "DANGXULY_CONTACT")]
        public ActionResult ListDangXuLy(string searchString, int page = 1, int pageSize = 5)
        {
            string showLuanChuyen = "true";
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new ContactDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            Contact itContact = new Contact();
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];

            itContact.StatusId = CommonConstants.LIENHENGUOIDUNG_DANGXULY;
            var model = dao.ListAllPaging(itContact, searchString, ref totalRecord, page, pageSize);
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
            string controller = "CONTACT";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListContact = model;
            ViewBag.showLuanChuyen = showLuanChuyen;
            return View(itContact);
        }
        [HttpPost]
        [HasCredential(RoleID = "DANGXULY_CONTACT")]
        public ActionResult ListDangXuLy(Contact itContact)
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
            var dao = new ContactDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            itContact.StatusId = CommonConstants.LIENHENGUOIDUNG_DANGXULY;
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session.GroupID != CommonConstants.PHEDUYET_GROUP)
            {
                showLuanChuyen = "false";
            }

            var model = dao.ListAllPaging(itContact, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "CONTACT";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListContact = model;
            ViewBag.showLuanChuyen = showLuanChuyen;
            return View(itContact);
        }
        //End Gian hàng chờ duyệt

        //Gian hàng không được duyệt
        [HasCredential(RoleID = "DUOCDUYET_CONTACT")]
        public ActionResult ListDuocDuyet(string searchString, int page = 1, int pageSize = 5)
        {
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new ContactDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            Contact itContact = new Contact();
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];

            itContact.StatusId = CommonConstants.LIENHENGUOIDUNG_DUOCDUYET;
            var model = dao.ListAllPaging(itContact, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "CONTACT";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListContact = model;
            return View(itContact);
        }
        [HttpPost]
        [HasCredential(RoleID = "DUOCDUYET_CONTACT")]
        public ActionResult ListDuocDuyet(Contact itContact)
        {
            string searchString = "";
            int page = 1;
            int pageSize = 5;
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new ContactDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            itContact.StatusId = CommonConstants.LIENHENGUOIDUNG_DUOCDUYET;
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];

            var model = dao.ListAllPaging(itContact, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "CONTACT";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListContact = model;
            return View(itContact);
        }
        //End Gian hàng không được duyệt

        //Gian hàng không được duyệt
        [HasCredential(RoleID = "KHONGDUYET_CONTACT")]
        public ActionResult ListKhongDuyet(string searchString, int page = 1, int pageSize = 5)
        {
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new ContactDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            Contact itContact = new Contact();
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];

            itContact.StatusId = CommonConstants.LIENHENGUOIDUNG_KHONGDUYET;
            var model = dao.ListAllPaging(itContact, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "CONTACT";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListContact = model;
            return View(itContact);
        }
        [HttpPost]
        [HasCredential(RoleID = "KHONGDUYET_CONTACT")]
        public ActionResult ListKhongDuyet(Contact itContact)
        {
            string searchString = "";
            int page = 1;
            int pageSize = 5;
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new ContactDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            itContact.StatusId = CommonConstants.LIENHENGUOIDUNG_KHONGDUYET;
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];

            var model = dao.ListAllPaging(itContact, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "CONTACT";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListContact = model;
            return View(itContact);
        }
        //End Gian hàng không được duyệt

        [HttpGet]
        [HasCredential(RoleID = "ADD_CONTACT")]
        public ActionResult Create()
        {
            Contact item = new Contact();
            //item.hidIndexFile = "0";
            SetViewBag();
            return View(item);
        }

        [HttpGet]
        [HasCredential(RoleID = "EDIT_CONTACT")]
        public ActionResult Edit(long id)
        {
            var dao = new ContactDao();
            var Contact = dao.GetByID(id);

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

            return View(Contact);
        }

        [HttpPost]
        [ValidateInput(false)]
        [HasCredential(RoleID = "EDIT_CONTACT")]
        public ActionResult Edit(Contact model)
        {
            string rec;
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (ModelState.IsValid)
            {
                try
                {

                    model.ModifiedBy = session.UserName;
                    model.ModifiedDate = DateTime.Now;

                    //string content = System.IO.File.ReadAllText(Server.MapPath("~/assets/client/template/RepContact.html"));
                    //content = content.Replace("{{Name}}", model.Name);
                    //content = content.Replace("{{Phone}}", model.Phone);
                    //content = content.Replace("{{Email}}", model.Email);
                    //content = content.Replace("{{Title}}", model.Title);
                    //content = content.Replace("{{Content}}", model.Content);
                    //content = content.Replace("{{ContactAnswer}}", model.AnswerContent);
                    //var toEmail = model.Email.ToString();

                    //new MailHelper().SendMail(toEmail, "Phản hồi liên hệ", content);
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

                    new ContactDao().Update(model);
                    Session[CommonConstants.MSG_SUCCESS] = "Sửa thành công";
                }
                catch (Exception)
                {
                    Session[CommonConstants.MSG_ERROR] = "Sửa thất bại";
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
                if (session.GroupID == CommonConstants.QLLIENHE_GROUP)
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
        [HasCredential(RoleID = "ADD_CONTACT")]
        public ActionResult Create(Contact model)
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
                new ContactDao().Create(model);
                string rec;
                if (session.GroupID == CommonConstants.ADMIN_GROUP)
                {
                    rec = "Index";
                }
                else
                {
                    if (session.GroupID == CommonConstants.QLLIENHE_GROUP)
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
        [HasCredential(RoleID = "DELETEMULTI_CONTACT")]
        public JsonResult DeleteMulti(string[] ids)
        {
            try
            {
                var result = false;
                foreach (var i in ids)
                {
                    int id = int.Parse(i.ToString());
                    result = new ContactDao().Delete(id);
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
            var dao = new ContactDao();
            var daoLogAction = new LogActionDao();
            var daoSysAction = new SysActionDao();
            LogAction itLogAction = new LogAction();
            SysAction itSysAction = new SysAction();
            var Contact = dao.GetByID(id);
            itSysAction.CurrentStatus = Contact.StatusId;
            itSysAction.IdSysProcedure = CommonConstants.QUYTRINH_LIENHENGUOIDUNG;
            List<SelectListItem> listThaoTac = new List<SelectListItem>();
            List<SysAction> lstSysAction = daoSysAction.ListAll(itSysAction);
            foreach (SysAction item in lstSysAction)
            {
                listThaoTac.Add(new SelectListItem { Value = item.Id + "", Text = item.Title });
            }

            itLogAction.ObjId = Convert.ToInt32(Contact.ID);
            itLogAction.SysProcedureId = CommonConstants.QUYTRINH_LIENHENGUOIDUNG;
            List<LogAction> lstLogAction = daoLogAction.ListAll(itLogAction);

            ViewBag.SysActionId = new SelectList(listThaoTac, "Value", "Text", null);
            ViewBag.itemContact = Contact;
            ViewBag.lstLogAction = lstLogAction;
            // SetViewBag(Contact.CategoryID);
            return View(Contact);
        }

        [HttpPost]
        public ActionResult Action(Contact model)
        {
            int? curStatusId = 0;
            if (ModelState.IsValid)
            {
                try
                {
                    var dao = new ContactDao();
                    var daoSysAction = new SysActionDao();
                    var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                    model.ModifiedBy = session.UserName;
                    model.ModifiedDate = DateTime.Now;
                    Contact item = dao.GetByID(model.ID);
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

                    new ContactDao().Update(model);

                    //Save logAction
                    var daoLogAction = new LogActionDao();
                    LogAction itLogAction = new LogAction();
                    itLogAction.Created = DateTime.Now;
                    itLogAction.ActionId = model.SysActionId;
                    itLogAction.SysProcedureId = CommonConstants.QUYTRINH_LIENHENGUOIDUNG;
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
            var dao = new ContactDao();
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