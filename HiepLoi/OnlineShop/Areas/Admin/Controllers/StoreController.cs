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
    public class StoreController : BaseController
    {
        // GET: Admin/Store
        [HasCredential(RoleID = "INDEX_STORE")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new StoreDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            Store itStore = new Store();
            var model = dao.ListAllPaging(itStore, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "STORE";
            setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.ListStore = model;
            return View(itStore);
        }

        [HttpPost]
        [HasCredential(RoleID = "INDEX_STORE")]
        public ActionResult Index(Store itStore)
        {
            string searchString = "";
            int page = 1;
            int pageSize = 5;
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new StoreDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            var model = dao.ListAllPaging(itStore, searchString, ref totalRecord, page, pageSize);


            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "STORE";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListStore = model;
            return View(itStore);
        }

        //Gian hàng đang soạn
        [HasCredential(RoleID = "CHODUYET_STORE")]
        public ActionResult ListChoDuyet(string searchString, int page = 1, int pageSize = 5)
        {
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new StoreDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            Store itStore = new Store();
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            
            itStore.StatusId = CommonConstants.GIANHANG_CHODUYET;
            var model = dao.ListAllPaging(itStore, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "STORE";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListStore = model;
            return View(itStore);
        }

        [HttpPost]
        [HasCredential(RoleID = "CHODUYET_STORE")]
        public ActionResult ListChoDuyet(Store itStore)
        {
            string searchString = "";
            int page = 1;
            int pageSize = 5;
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new StoreDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            itStore.StatusId = CommonConstants.GIANHANG_CHODUYET;
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            
            var model = dao.ListAllPaging(itStore, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "STORE";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListStore = model;
            return View(itStore);
        }
        //End Gian hàng đang soạn

        //Gian hàng chờ duyệt
        [HasCredential(RoleID = "DUOCDUYET_STORE")]
        public ActionResult ListDuocDuyet(string searchString, int page = 1, int pageSize = 5)
        {
            string showLuanChuyen = "true";
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new StoreDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            Store itStore = new Store();
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            
            itStore.StatusId = CommonConstants.GIANHANG_DUOCDUYET;
            var model = dao.ListAllPaging(itStore, searchString, ref totalRecord, page, pageSize);
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
            string controller = "STORE";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListStore = model;
            ViewBag.showLuanChuyen = showLuanChuyen;
            return View(itStore);
        }
        [HttpPost]
        [HasCredential(RoleID = "DUOCDUYET_STORE")]
        public ActionResult ListDuocDuyet(Store itStore)
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
            var dao = new StoreDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            itStore.StatusId = CommonConstants.GIANHANG_DUOCDUYET;
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session.GroupID != CommonConstants.PHEDUYET_GROUP)
            {
                showLuanChuyen = "false";
            }
            
            var model = dao.ListAllPaging(itStore, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "STORE";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListStore = model;
            ViewBag.showLuanChuyen = showLuanChuyen;
            return View(itStore);
        }
        //End Gian hàng chờ duyệt

        //Gian hàng không được duyệt
        [HasCredential(RoleID = "KHONGDUYET_STORE")]
        public ActionResult ListKhongDuyet(string searchString, int page = 1, int pageSize = 5)
        {
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new StoreDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            Store itStore = new Store();
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            
            itStore.StatusId = CommonConstants.GIANHANG_KHONGDUYET;
            var model = dao.ListAllPaging(itStore, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "STORE";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListStore = model;
            return View(itStore);
        }
        [HttpPost]
        [HasCredential(RoleID = "KHONGDUYET_STORE")]
        public ActionResult ListKhongDuyet(Store itStore)
        {
            string searchString = "";
            int page = 1;
            int pageSize = 5;
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new StoreDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            itStore.StatusId = CommonConstants.GIANHANG_KHONGDUYET;
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            
            var model = dao.ListAllPaging(itStore, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "STORE";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListStore = model;
            return View(itStore);
        }
        //End Gian hàng không được duyệt

        //Gian hàng đã phê duyệt
        [HasCredential(RoleID = "CAPNHAT_STORE")]
        public ActionResult ListCapNhat(string searchString, int page = 1, int pageSize = 5)
        {
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new StoreDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            Store itStore = new Store();
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            
            itStore.StatusId = CommonConstants.GIANHANG_CHODUYETCAPNHAT;
            var model = dao.ListAllPaging(itStore, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "STORE";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListStore = model;
            return View(itStore);
        }
        [HttpPost]
        [HasCredential(RoleID = "CAPNHAT_STORE")]
        public ActionResult ListCapNhat(Store itStore)
        {
            string searchString = "";
            int page = 1;
            int pageSize = 5;
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new StoreDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            itStore.StatusId = CommonConstants.GIANHANG_CHODUYETCAPNHAT;
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            
            var model = dao.ListAllPaging(itStore, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "STORE";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListStore = model;
            return View(itStore);
        }
        //End Gian hàng đã phê duyệt

        //Gian hàng đã công khai
        [HasCredential(RoleID = "CHOBOSUNG_STORE")]
        public ActionResult ListChoBoSung(string searchString, int page = 1, int pageSize = 5)
        {
            string showLuanChuyen = "true";
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new StoreDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            Store itStore = new Store();
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session.GroupID != CommonConstants.QLCONGKHAI_GROUP) // tài khoản công khai thì không lấy đơn vị
            {
                showLuanChuyen = "false";
                
            }
            itStore.StatusId = CommonConstants.GIANHANG_CHOBOSUNG7;
            var model = dao.ListAllPaging(itStore, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }

            string controller = "STORE";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListStore = model;
            ViewBag.showLuanChuyen = showLuanChuyen;
            return View(itStore);
        }
        [HttpPost]
        [HasCredential(RoleID = "CHOBOSUNG_STORE")]
        public ActionResult ListChoBoSung(Store itStore)
        {
            string searchString = "";
            int page = 1;
            int pageSize = 5;
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new StoreDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            itStore.StatusId = CommonConstants.GIANHANG_CHOBOSUNG7;
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];

            if (session.GroupID != CommonConstants.QLCONGKHAI_GROUP) // tài khoản công khai thì không lấy đơn vị
            {
                
            }
            var model = dao.ListAllPaging(itStore, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "STORE";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListStore = model;
            return View(itStore);
        }
        //End Gian hàng đã công khai   

        [HttpGet]
        [HasCredential(RoleID = "ADD_STORE")]
        public ActionResult Create()
        {
            Store item = new Store();
            item.hidIndexFile = "0";
            SetViewBag();
            return View(item);
        }

        [HttpGet]
        [HasCredential(RoleID = "EDIT_STORE")]
        [ValidateInput(false)]
        public ActionResult Edit(long id)
        {
            var dao = new StoreDao();
            var Store = dao.GetByID(id);

            if (Store.StoreImage != "" && Store.StoreImage != "0" && Store.StoreImage != null)
            {
                dynamic jsonFile = JsonConvert.DeserializeObject(Store.StoreImage);
                int i = 0;
                foreach (var itjon in jsonFile)
                {
                    string Comment = itjon.filename;
                    string Url = itjon.urldownload;
                    Store.hidListFile += "<tr id='" + i + "'><td><p><input type='hidden' id='link" + i + "' value='" + Url + "'><img src='" + CommonConstants.DomainName + Url + "' width='100' height='100'></p></td><td><textarea id='TextArea1' cols='40' rows='4'>" + Comment + "</textarea></td><td><a onclick='RemoveRow(" + i + ");'>Xóa</a></td></tr>";
                    i += 1;
                }
                Store.hidIndexFile = i + "";
            }
            SetViewBag();
            return View(Store);
        }

        [HttpPost]
        [ValidateInput(false)]
        [HasCredential(RoleID = "EDIT_STORE")]
        public ActionResult Edit(Store model)
        {
            string rec;
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (ModelState.IsValid)
            {
                try
                {

                    model.ModifiedBy = session.UserName;
                    model.ModifiedDate = DateTime.Now;
                    if (model.ImgLogo != null)
                    {
                        model.ImgLogo = model.ImgLogo.Replace(CommonConstants.ApplicationName, "");
                    }
                    if (model.HeaderImage != null)
                    {
                        model.HeaderImage = model.HeaderImage.Replace(CommonConstants.ApplicationName, "");
                    }                    
                    if (model.hidFileLinkDown != "0" && model.hidFileLinkDown != "" && model.hidFileLinkDown != null)
                    {
                        string[] arrFileLink = model.hidFileLinkDown.Split(new string[] { "|" }, StringSplitOptions.None);
                        string[] arrFileNote = model.hidFileNote.Split(new string[] { "|" }, StringSplitOptions.None);
                        string jsonTapTinDinhKem = "";
                        for (int i = 0; i <= arrFileLink.Length - 2; i++)
                        {
                            jsonTapTinDinhKem += "{'filename':'" + arrFileNote[i] + "','urldownload':'" + arrFileLink[i].Replace(CommonConstants.ApplicationName, "") + "'}" + ",";
                        }
                        model.StoreImage = "[" + jsonTapTinDinhKem.Remove(jsonTapTinDinhKem.Length - 1) + "]";// bỏ dấu , cuối cùng
                    }
                    else
                    {
                        model.StoreImage = null;
                    }

                    new StoreDao().Update(model);
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
                    rec = "ListChoDuyet";
                    if (model.StatusId == CommonConstants.GIANHANG_DUOCDUYET)
                    {
                        rec = "ListDuocDuyet";
                    }
                    else if (model.StatusId == CommonConstants.GIANHANG_CHOBOSUNG7)
                    {
                        rec = "ListChoBoSung";
                    }
                    else if (model.StatusId == CommonConstants.GIANHANG_CHODUYET)
                    {
                        rec = "ListChoDuyet";
                    }
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
        [HasCredential(RoleID = "ADD_STORE")]
        public ActionResult Create(Store model)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                model.CreatedBy = session.UserName;
                model.StatusId = CommonConstants.GIANHANG_CHODUYET;
                model.ImgLogo = model.ImgLogo.Replace(CommonConstants.ApplicationName, "");
                model.HeaderImage = model.HeaderImage.Replace(CommonConstants.ApplicationName, "");
                if (model.hidFileLinkDown != "0" && model.hidFileLinkDown !="" && model.hidFileLinkDown !=null)
                {
                    string[] arrFileLink = model.hidFileLinkDown.Split(new string[] { "|" }, StringSplitOptions.None);
                    string[] arrFileNote = model.hidFileNote.Split(new string[] { "|" }, StringSplitOptions.None);
                    string jsonTapTinDinhKem = "";
                    for (int i = 0; i <= arrFileLink.Length - 2; i++)
                    {
                        jsonTapTinDinhKem += "{'filename':'" + arrFileNote[i] + "','urldownload':'" + arrFileLink[i].Replace(CommonConstants.ApplicationName, "") + "'}" + ",";
                    }
                    model.StoreImage = "[" + jsonTapTinDinhKem.Remove(jsonTapTinDinhKem.Length - 1) + "]";// bỏ dấu , cuối cùng
                }
                new StoreDao().Create(model);
                string rec;
                if (session.GroupID == CommonConstants.ADMIN_GROUP)
                {
                    rec = "Index";
                }
                else
                {
                    if (session.GroupID == CommonConstants.QLGIANHANG_GROUP)
                    {
                        rec = "ListChoDuyet";
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
        [HasCredential(RoleID = "DELETEMULTI_STORE")]
        public JsonResult DeleteMulti(string[] ids)
        {
            try
            {
                var result = false;
                foreach (var i in ids)
                {
                    int id = int.Parse(i.ToString());
                    result = new StoreDao().Delete(id);
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
            var dao = new StoreDao();
            var daoLogAction = new LogActionDao();
            var daoSysAction = new SysActionDao();
            LogAction itLogAction = new LogAction();
            SysAction itSysAction = new SysAction();
            var Store = dao.GetByID(id);
            itSysAction.CurrentStatus = Store.StatusId;
            itSysAction.IdSysProcedure = CommonConstants.QUYTRINH_GIANHANG;
            List<SelectListItem> listThaoTac = new List<SelectListItem>();
            List<SysAction> lstSysAction = daoSysAction.ListAll(itSysAction);
            foreach (SysAction item in lstSysAction)
            {
                listThaoTac.Add(new SelectListItem { Value = item.Id + "", Text = item.Title });
            }

            itLogAction.ObjId = Convert.ToInt32(Store.StoreId);
            itLogAction.SysProcedureId = CommonConstants.QUYTRINH_GIANHANG;
            List<LogAction> lstLogAction = daoLogAction.ListAll(itLogAction);

            ViewBag.SysActionId = new SelectList(listThaoTac, "Value", "Text", null);
            ViewBag.itemStore = Store;
            ViewBag.lstLogAction = lstLogAction;
            // SetViewBag(Store.CategoryID);
            return View(Store);
        }

        [HttpPost]
        public ActionResult Action(Store model)
        {
            int? curStatusId = 0;
            if (ModelState.IsValid)
            {
                try
                {
                    var dao = new StoreDao();
                    var daoSysAction = new SysActionDao();
                    var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                    model.ModifiedBy = session.UserName;
                    model.ModifiedDate = DateTime.Now;
                    Store item = dao.GetByID(model.StoreId);
                    curStatusId = item.StatusId;
                    model.Title = item.Title;
                    model.Slogan = item.Slogan;
                    model.URLWEB = item.URLWEB;
                    model.Email = item.Email;
                    model.ImgLogo = item.ImgLogo;
                    model.Zalo = item.Zalo;
                    model.SkypeId = item.SkypeId;
                    model.HoTen = item.HoTen;
                    model.Phone = item.Phone;
                    model.PhoneOther = item.PhoneOther;
                    model.HeaderImage = item.HeaderImage;
                    model.Map = item.Map;
                    model.NganhNgheId = item.NganhNgheId;
                    model.NganhNgheName = item.NganhNgheName;
                    model.QuyMoVon = item.QuyMoVon;
                    model.NhanLuc = item.NhanLuc;
                    model.DoanhThu = item.DoanhThu;
                    model.LoiNhuan = item.LoiNhuan;
                    model.StoreImage = item.StoreImage;
                    model.DiaChi = item.DiaChi;
                    //Lấy trạng thái tiếp theo
                    var itSysAction = daoSysAction.GetById(model.SysActionId);
                    model.StatusId = itSysAction.NextStatus;
                    //End lấy trạng thái theo

                    new StoreDao().Update(model);

                    //Save logAction
                    var daoLogAction = new LogActionDao();
                    LogAction itLogAction = new LogAction();
                    itLogAction.Created = DateTime.Now;
                    itLogAction.ActionId = model.SysActionId;
                    itLogAction.SysProcedureId = CommonConstants.QUYTRINH_GIANHANG;
                    itLogAction.CurrentStatusId = curStatusId;
                    itLogAction.ObjId = Convert.ToInt32(model.StoreId);
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

        public string getTitle(int StoreId)
        {
            var dao = new StoreDao();
            var itemSearch = dao.GetByID(StoreId);
            string title = "";
            if (itemSearch != null)
            {
                title = itemSearch.Title;
            }
            return title;
        }
        public void SetViewBag()
        {
            //Start DropdownList lĩnh vực
            BusinessField itBusinessField = new BusinessField();
            var daoBusinessField = new BusinessFieldDao();
            var lstLV = daoBusinessField.ListAll(itBusinessField).Where(x => x.MapId == null);
            List<SelectListItem> listLinhVuc = new List<SelectListItem>();
            listLinhVuc.Add(new SelectListItem { Value = "0", Text = "--Chọn--" });
            foreach (BusinessField item in lstLV)
            {
                listLinhVuc.Add(new SelectListItem { Value = item.Id + "", Text = item.Name });
            }
            ViewBag.LinhVucKinhDoanhId = new SelectList(listLinhVuc, "Value", "Text", null);
            //End DropdownList Lĩnh vực
        }

    }
}