using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClsCommon;
using CommonConstants = ClsCommon.CommonConstants;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ContentController : BaseController
    {
        // GET: Admin/Content
        [HasCredential(RoleID = "INDEX_CONTENT")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new ContentDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            Content itContent = new Content();
            var model = dao.ListAllPaging(itContent, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "CONTENT";
            setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.ListContent = model;
            return View(itContent);
        }

        [HttpPost]
        [HasCredential(RoleID = "INDEX_CONTENT")]
        public ActionResult Index(Content itContent)
        {
            string searchString = "";
            int page = 1;
            int pageSize = 5;
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new ContentDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            var model = dao.ListAllPaging(itContent, searchString, ref totalRecord, page, pageSize);

            
            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "CONTENT";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListContent = model;
            return View(itContent);
        }

        //Tin tức đang soạn
        [HasCredential(RoleID = "SOANTHAO_CONTENT")]
        public ActionResult ListSoanThao(string searchString, int page = 1, int pageSize = 5)
        {
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new ContentDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            Content itContent = new Content();
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            itContent.DonViID = session.UnitCode;
            itContent.StatusId = CommonConstants.TINTTUC_DANGSOAN;
            var model = dao.ListAllPaging(itContent, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "CONTENT";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListContent = model;
            return View(itContent);
        }

        [HttpPost]
        [HasCredential(RoleID = "SOANTHAO_CONTENT")]
        public ActionResult ListSoanThao(Content itContent)
        {
            string searchString = "";
            int page = 1;
            int pageSize = 5;
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new ContentDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            itContent.StatusId = CommonConstants.TINTTUC_DANGSOAN;
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            itContent.DonViID = session.UnitCode;
            var model = dao.ListAllPaging(itContent, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "CONTENT";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListContent = model;
            return View(itContent);
        }
        //End Tin tức đang soạn

        //Tin tức giới thiệu
        [HasCredential(RoleID = "INDEX_CONTENT_GIOITHIEU")]
        public ActionResult ListGioiThieu(string searchString, int page = 1, int pageSize = 5)
        {
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new ContentDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            Content itContent = new Content();
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            itContent.DonViID = session.UnitCode;
            itContent.StatusId = 77;
            itContent.CategoryID = 5;
            var model = dao.ListAllPaging(itContent, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "CONTENT";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListContent = model;
            return View(itContent);
        }

        [HttpPost]
        [HasCredential(RoleID = "INDEX_CONTENT_GIOITHIEU")]
        public ActionResult ListGioiThieu(Content itContent)
        {
            string searchString = "";
            int page = 1;
            int pageSize = 5;
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new ContentDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            itContent.StatusId = 77;
            itContent.CategoryID = 5;
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            itContent.DonViID = session.UnitCode;
            var model = dao.ListAllPaging(itContent, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "CONTENT";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListContent = model;
            return View(itContent);
        }
        //End Tin tức giới thiệu
        [HttpGet]
        [HasCredential(RoleID = "ADD_CONTENT_GIOITHIEU")]
        public ActionResult CreateGioiThieu()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [HasCredential(RoleID = "ADD_CONTENT_GIOITHIEU")]
        public ActionResult CreateGioiThieu(Content model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                    model.CreatedBy = session.UserName;
                    model.DonViID = session.UnitCode;
                    model.Status = true;
                    model.Author = session.UserName;
                    model.CreatedDate = DateTime.Now;
                    model.CategoryID = 5;
                    model.StatusId = 77;
                    new ContentDao().CreateGioiThieu(model);
                    Session[CommonConstants.MSG_SUCCESS] = "Lưu thành công";
                }
                catch (Exception ex)
                {
                    Session[CommonConstants.MSG_ERROR] = "Lưu thất bại";
                    throw ex;
                }
                return RedirectToAction("ListGioiThieu");
            }
            else
            {
                Session[CommonConstants.MSG_ERROR] = "Lưu thất bại";
            }
            return View();
        }

        [HttpGet]
        [HasCredential(RoleID = "EDIT_CONTENT_GIOITHIEU")]
        public ActionResult EditGioiThieu(int id)
        {
            var dao = new ContentDao();
            var Content = dao.GetByID(id);
            return View(Content);
        }

        [HttpPost]
        [HasCredential(RoleID = "EDIT_CONTENT_GIOITHIEU")]
        [ValidateInput(false)]
        public ActionResult EditGioiThieu(Content model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                    model.CreatedBy = session.UserName;
                    model.DonViID = session.UnitCode;
                    model.Status = true;
                    model.ModifiedBy = session.UserName;
                    model.ModifiedDate = DateTime.Now;
                    model.CategoryID = 5;
                    model.StatusId = 77;
                    new ContentDao().Update(model);
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
            return RedirectToAction("ListGioiThieu");
        }

        [HasCredential(RoleID = "VIEW_CONTENT_GIOITHIEU")]
        public ActionResult ViewGioiThieu(int id)
        {
            var Content = new ContentDao().ViewDetail(id);
            ViewBag.Content = Content;
            return View(Content);
        }

        [HttpDelete]
        [HasCredential(RoleID = "DELETE_CONTENT_GIOITHIEU")]
        public ActionResult DeleteGioiThieu(int id)
        {
            var dao = new ContentDao();
            var Content = dao.Delete(id);
            return View(Content);
        }

        [HttpPost]
        [HasCredential(RoleID = "DELETEMULTI_CONTENT_GIOITHIEU")]
        public JsonResult DeleteMultiGioiThieu(string[] ids)
        {
            try
            {
                var result = false;
                foreach (var i in ids)
                {
                    int id = int.Parse(i.ToString());
                    result = new ContentDao().Delete(id);
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

        //Tin tức chờ duyệt
        [HasCredential(RoleID = "CHODUYET_CONTENT")]
        public ActionResult ListChoDuyet(string searchString, int page = 1, int pageSize = 5)
        {
            string showLuanChuyen = "true";
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new ContentDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            Content itContent = new Content();
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            itContent.DonViID = session.UnitCode;
            itContent.StatusId = CommonConstants.TINTTUC_TRINHDUYET;
            var model = dao.ListAllPaging(itContent, searchString, ref totalRecord, page, pageSize);
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
            string controller = "CONTENT";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListContent = model;
            ViewBag.showLuanChuyen = showLuanChuyen;
            return View(itContent);
        }
        [HttpPost]
        [HasCredential(RoleID = "CHODUYET_CONTENT")]
        public ActionResult ListChoDuyet(Content itContent)
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
            var dao = new ContentDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            itContent.StatusId = CommonConstants.TINTTUC_TRINHDUYET;
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session.GroupID != CommonConstants.PHEDUYET_GROUP)
            {
                showLuanChuyen = "false";
            }
            itContent.DonViID = session.UnitCode;
            var model = dao.ListAllPaging(itContent, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "CONTENT";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListContent = model;
            ViewBag.showLuanChuyen = showLuanChuyen;
            return View(itContent);
        }
        //End Tin tức chờ duyệt

        //Tin tức không được duyệt
        [HasCredential(RoleID = "KHONGDUYET_CONTENT")]
        public ActionResult ListKhongDuyet(string searchString, int page = 1, int pageSize = 5)
        {
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new ContentDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            Content itContent = new Content();
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            itContent.DonViID = session.UnitCode;
            itContent.StatusId = CommonConstants.TINTTUC_KHONGDUYET;
            var model = dao.ListAllPaging(itContent, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "CONTENT";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListContent = model;
            return View(itContent);
        }
        [HttpPost]
        [HasCredential(RoleID = "KHONGDUYET_CONTENT")]
        public ActionResult ListKhongDuyet(Content itContent)
        {
            string searchString = "";
            int page = 1;
            int pageSize = 5;
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new ContentDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            itContent.StatusId = CommonConstants.TINTTUC_KHONGDUYET;
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            itContent.DonViID = session.UnitCode;
            var model = dao.ListAllPaging(itContent, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "CONTENT";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListContent = model;
            return View(itContent);
        }
        //End Tin tức không được duyệt

        //Tin tức đã phê duyệt
        [HasCredential(RoleID = "DAPHEDUYET_CONTENT")]
        public ActionResult ListDaPheDuyet(string searchString, int page = 1, int pageSize = 5)
        {
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new ContentDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            Content itContent = new Content();
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            itContent.DonViID = session.UnitCode;
            itContent.StatusId = CommonConstants.TINTTUC_XUATBAN;
            var model = dao.ListAllPaging(itContent, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "CONTENT";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListContent = model;
            return View(itContent);
        }
        [HttpPost]
        [HasCredential(RoleID = "DAPHEDUYET_CONTENT")]
        public ActionResult ListDaPheDuyet(Content itContent)
        {
            string searchString = "";
            int page = 1;
            int pageSize = 5;
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new ContentDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            itContent.StatusId = CommonConstants.TINTTUC_XUATBAN;
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            itContent.DonViID = session.UnitCode;
            var model = dao.ListAllPaging(itContent, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "CONTENT";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListContent = model;
            return View(itContent);
        }
        //End Tin tức đã phê duyệt

        //Tin tức đã công khai
        [HasCredential(RoleID = "CONGKHAI_CONTENT")]
        public ActionResult ListCongKhai(string searchString, int page = 1, int pageSize = 5)
        {
            string showLuanChuyen = "true";
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new ContentDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            Content itContent = new Content();
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session.GroupID != CommonConstants.QLCONGKHAI_GROUP) // tài khoản công khai thì không lấy đơn vị
            {
                showLuanChuyen = "false";
                itContent.DonViID = session.UnitCode;
            }
            itContent.StatusId = CommonConstants.TINTTUC_CONGKHAI;
            var model = dao.ListAllPaging(itContent, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }

            string controller = "CONTENT";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListContent = model;
            ViewBag.showLuanChuyen = showLuanChuyen;
            return View(itContent);
        }
        [HttpPost]
        [HasCredential(RoleID = "CONGKHAI_CONTENT")]
        public ActionResult ListCongKhai(Content itContent)
        {
            string searchString = "";
            int page = 1;
            int pageSize = 5;
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new ContentDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            itContent.StatusId = CommonConstants.TINTTUC_CONGKHAI;
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];

            if (session.GroupID != CommonConstants.QLCONGKHAI_GROUP) // tài khoản công khai thì không lấy đơn vị
            {
                itContent.DonViID = session.UnitCode;
            }
            var model = dao.ListAllPaging(itContent, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "CONTENT";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListContent = model;
            return View(itContent);
        }
        //End Tin tức đã công khai   

        //Tin tức bổ sung
        [HasCredential(RoleID = "BOSUNG_CONTENT")]
        public ActionResult ListBoSung(string searchString, int page = 1, int pageSize = 5)
        {
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new ContentDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            Content itContent = new Content();
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            itContent.DonViID = session.UnitCode;
            itContent.StatusId = CommonConstants.TINTTUC_TRAVE;
            var model = dao.ListAllPaging(itContent, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "CONTENT";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListContent = model;
            return View(itContent);
        }
        [HttpPost]
        [HasCredential(RoleID = "BOSUNG_CONTENT")]
        public ActionResult ListBoSung(Content itContent)
        {
            string searchString = "";
            int page = 1;
            int pageSize = 5;
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new ContentDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            itContent.StatusId = CommonConstants.TINTTUC_TRAVE;
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            itContent.DonViID = session.UnitCode;
            var model = dao.ListAllPaging(itContent, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "CONTENT";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListContent = model;
            return View(itContent);
        }
        //End Tin tức bổ sung

        //Tin tức thu hồi
        [HasCredential(RoleID = "THUHOI_CONTENT")]
        public ActionResult ListThuHoi(string searchString, int page = 1, int pageSize = 5)
        {
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new ContentDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            Content itContent = new Content();
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            itContent.StatusId = CommonConstants.TINTTUC_THUHOI;
            var model = dao.ListAllPaging(itContent, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "CONTENT";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListContent = model;
            return View(itContent);
        }
        [HttpPost]
        [HasCredential(RoleID = "THUHOI_CONTENT")]
        public ActionResult ListThuHoi(Content itContent)
        {
            string searchString = "";
            int page = 1;
            int pageSize = 5;
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new ContentDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            itContent.StatusId = CommonConstants.TINTTUC_THUHOI;
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            var model = dao.ListAllPaging(itContent, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "CONTENT";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListContent = model;
            return View(itContent);
        }
        //End Tin tức thu hồi

        //Tin chờ công khai
        [HasCredential(RoleID = "CHOCONGKHAI_CONTENT")]
        public ActionResult ListChoCongKhai(Content itContent ,string searchString, int page = 1, int pageSize = 5)
        {
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new ContentDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            itContent.StatusId = CommonConstants.TINTTUC_XUATBAN;
            var model = dao.ListAllPaging(itContent, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "CONTENT";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListContent = model;
            return View(itContent);
        }
        [HttpPost]
        [HasCredential(RoleID = "CHOCONGKHAI_CONTENT")]
        public ActionResult ListChoCongKhai(Content itContent)
        {
            string searchString = "";
            int page = 1; 
            int pageSize = 5;
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new ContentDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            itContent.StatusId = CommonConstants.TINTTUC_XUATBAN;
            var model = dao.ListAllPaging(itContent, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "CONTENT";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListContent = model;
            return View(itContent);
        }
        //End Tin chờ công khai
        [HttpGet]
        [HasCredential(RoleID = "ADD_CONTENT")]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        [HttpGet]
        [HasCredential(RoleID = "EDIT_CONTENT")]
        public ActionResult Edit(long id)
        {
            var dao = new ContentDao();
            var content = dao.GetByID(id);

            SetViewBag(content.CategoryID);

            return View(content);
        }

        [HttpPost]
        [ValidateInput(false)]
        [HasCredential(RoleID = "EDIT_CONTENT")]
        public ActionResult Edit(Content model)
        {
            string rec;
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (ModelState.IsValid)
            {
                try
                {
                    //model.Modifier = session.UserName;
                    //model.Modified = DateTime.Now;
                    LogHeThong log = new LogHeThong();
                    log.NguoiDung = session.UserName;
                    log.NoiDung = "Edit tin tức";
                    log.SuKien = "Edit";
                    log.ThoiDiem = DateTime.Now;
                    log.ChucNang = "Content";
                    log.Ip = ClsCommon.CommonConstants.DomainName;
                    new ContentDao().Update(model);
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
            
            if (session.GroupID==CommonConstants.ADMIN_GROUP)
            {
                rec = "Index";
            }
            else
            {
                if (session.GroupID == CommonConstants.BIENTAP_GROUP)
                {
                    rec = "ListSoanThao";
                }
                else if (session.GroupID == CommonConstants.PHEDUYET_GROUP)
                {
                    rec = "ListChoDuyet";
                }
                else if (session.GroupID == CommonConstants.QLCONGKHAI_GROUP)
                {
                    rec = "ListChoCongKhai";
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
        [HasCredential(RoleID = "ADD_CONTENT")]
        public ActionResult Create(Content model)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                model.CreatedBy = session.UserName;
                model.DonViID = session.UnitCode;
                model.Status = true;
                var culture = Session[CommonConstants.CurrentCulture];
                model.Language = culture.ToString();
                LogHeThong log = new LogHeThong();
                log.NguoiDung = session.UserName;
                log.NoiDung = "Create tin tức";
                log.SuKien = "Create";
                log.ThoiDiem = DateTime.Now;
                log.ChucNang = "Content";
                log.Ip = ClsCommon.CommonConstants.DomainName;
                new ContentDao().Create(model);
                string rec;
                if (session.GroupID == CommonConstants.ADMIN_GROUP)
                {
                    rec = "Index";
                }
                else
                {
                    if (session.GroupID == CommonConstants.BIENTAP_GROUP)
                    {
                        rec = "ListSoanThao";
                    }
                    else if (session.GroupID == CommonConstants.PHEDUYET_GROUP)
                    {
                        rec = "ListChoDuyet";
                    }
                    else if (session.GroupID == CommonConstants.QLCONGKHAI_GROUP)
                    {
                        rec = "ListChoCongKhai";
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
        [HasCredential(RoleID = "DELETEMULTI_CONTENT")]
        public JsonResult DeleteMulti(string[] ids)
        {
            try
            {
                var result = false;
                foreach (var i in ids)
                {
                    int id = int.Parse(i.ToString());
                    result = new ContentDao().Delete(id);
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
            var user = (UserLogin)Session[CommonConstants.USER_SESSION];
            var dao = new ContentDao();
            var daoLogAction = new LogActionDao();
            var daoSysAction = new SysActionDao();
            LogAction itLogAction = new LogAction();
            SysAction itSysAction = new SysAction();
            var content = dao.GetByID(id);
            itSysAction.CurrentStatus = content.StatusId;
            itSysAction.IdSysProcedure = CommonConstants.QUYTRINH_TINTUC;
            itSysAction.UserGroups = user.GroupID;
            List<SelectListItem> listThaoTac = new List<SelectListItem>();
            List<SysAction> lstSysAction = daoSysAction.ListAll(itSysAction);
            foreach (SysAction item in lstSysAction)
            {
                listThaoTac.Add(new SelectListItem { Value = item.Id + "", Text = item.Title });
            }

            itLogAction.ObjId = Convert.ToInt32(content.ID);
            itLogAction.SysProcedureId = CommonConstants.QUYTRINH_TINTUC;
            List<LogAction> lstLogAction = daoLogAction.ListAll(itLogAction);

            ViewBag.SysActionId = new SelectList(listThaoTac, "Value", "Text", null);
            ViewBag.itemContent = content;
            ViewBag.lstLogAction = lstLogAction;
            SetViewBag(content.CategoryID);
            return View(content);
        }

        [HttpPost]
        public ActionResult Action(Content model)
        {
            int? curStatusId = 0;
            if (ModelState.IsValid)
            {
                try
                {
                    var dao = new ContentDao();
                    var daoSysAction = new SysActionDao();
                    var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                    //model.Modifier = session.UserName;
                    //model.Modified = DateTime.Now;
                    Content item = dao.GetByID(model.ID);
                    curStatusId = item.StatusId;
                    model.Name = item.Name;
                    model.Description = item.Description;
                    model.Status = item.Status;
                    model.Image = item.Image;
                    model.CategoryID = item.CategoryID;
                    model.Detail = item.Detail;
                    model.Author = item.Author;
                    //Lấy trạng thái tiếp theo
                    var itSysAction = daoSysAction.GetById(model.SysActionId);
                    model.StatusId = itSysAction.NextStatus;
                    if(model.StatusId == CommonConstants.TINTTUC_CONGKHAI)//trạng thái công khai
                    {
                        model.PublishedDate = DateTime.Now;
                    }
                    //End lấy trạng thái theo

                    new ContentDao().Update(model);

                    //Save logAction
                    var daoLogAction = new LogActionDao();
                    LogAction itLogAction = new LogAction();
                    itLogAction.Created = DateTime.Now;
                    itLogAction.ActionId = model.SysActionId;
                    itLogAction.SysProcedureId = CommonConstants.QUYTRINH_TINTUC;
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
        public void SetViewBag(long? selectedId = null)
        {
            var dao = new CategoryDao();
            ViewBag.CategoryID = new SelectList(dao.ListAll(), "ID", "Name", selectedId);
        }

        public string test(string id, string id2)
        {
            return id + " " + id2;
        }
    }
}