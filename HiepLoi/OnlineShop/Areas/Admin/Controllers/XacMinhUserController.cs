using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;
using Model.Dao;
using ClsCommon;
using PagedList;
using System.Web.Script.Serialization;

namespace OnlineShop.Areas.Admin.Controllers
{

    public class XacMinhUserController : BaseController
    {
        // GET: Admin/XacMinhUser
        [HasCredential(RoleID = "INDEX_XACMINHUSER")]
        public ActionResult Index(int page = 1, int pageSize = 5)
        {
            
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var itUser = new User();
            var dao = new UserDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            itUser.loaiTK = "ck";
            var listUser = dao.ListAllXacMinhPaging(itUser, "", ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "XACMINHUSER";
            setPermission(controller);//Hàm này trong BaseController
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.listUser = listUser;
            return View(itUser);
        }

        [HttpPost]
        [HasCredential(RoleID = "INDEX_XACMINHUSER")]
        public ActionResult Index(User itUser, int page = 1, int pageSize = 5)
        {
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new UserDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            itUser.loaiTK = "ck";
            var listUser = dao.ListAllXacMinhPaging(itUser, "", ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "XACMINHUSER";
            setPermission(controller);//Hàm này trong BaseController
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.listUser = listUser;
            return View(itUser);
        }

        // GET: Admin/XacMinhUser
        [HasCredential(RoleID = "DUYET_XACMINHUSER")]
        public ActionResult Duyet(int page = 1, int pageSize = 5)
        {

            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var itUser = new User();
            var dao = new UserDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            itUser.loaiTK = "ck";
            itUser.Status = true;
            var listUser = dao.ListAllXacMinhPaging(itUser, "", ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "XACMINHUSER";
            setPermission(controller);//Hàm này trong BaseController
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.listUser = listUser;
            return View(itUser);
        }

        [HttpPost]
        [HasCredential(RoleID = "DUYET_XACMINHUSER")]
        public ActionResult Duyet(User itUser, int page = 1, int pageSize = 5)
        {
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new UserDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            itUser.loaiTK = "ck";
            itUser.Status = true;
            var listUser = dao.ListAllXacMinhPaging(itUser, "", ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "XACMINHUSER";
            setPermission(controller);//Hàm này trong BaseController
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.listUser = listUser;
            return View(itUser);
        }

        // GET: Admin/XacMinhUser
        [HasCredential(RoleID = "KHONGDUYET_XACMINHUSER")]
        public ActionResult KhongDuyet(int page = 1, int pageSize = 5)
        {

            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var itUser = new User();
            var dao = new UserDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            itUser.loaiTK = "ck";
            itUser.Status = false;
            itUser.checkXacMinh = true;
            var listUser = dao.ListAllXacMinhPaging(itUser, "", ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "XACMINHUSER";
            setPermission(controller);//Hàm này trong BaseController
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.listUser = listUser;
            return View(itUser);
        }

        [HttpPost]
        [HasCredential(RoleID = "KHONGDUYET_XACMINHUSER")]
        public ActionResult KhongDuyet(User itUser, int page = 1, int pageSize = 5)
        {
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new UserDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            itUser.loaiTK = "ck";
            itUser.Status = false;
            itUser.checkXacMinh = true;
            var listUser = dao.ListAllXacMinhPaging(itUser, "", ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "XACMINHUSER";
            setPermission(controller);//Hàm này trong BaseController
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.listUser = listUser;
            return View(itUser);
        }

        [HttpPost]
        [HasCredential(RoleID = "INDEX_XACMINHUSER")]
        public ActionResult DuyetTK(string ghichu , int id)
        {
            User user = new UserDao().GetById(id);
            user.checkXacMinh = true;
            user.Status = true;
            user.checkXacMinh = false;
            new UserDao().Update(user);
            string Link = CommonConstants.DomainName + "/xac-nhan-dang-ky/" + user.UserName + ".html";
            if (user.loaiTK == "2")
            {
                Link = CommonConstants.DomainName + "/xac-nhan-dang-ky-tai-khoan-nha-dau-tu/" + user.UserName + ".html";
            }
            else
            {
                Link = CommonConstants.DomainName + "/xac-nhan-dang-ky/" + user.UserName + ".html";
            }
            string content = System.IO.File.ReadAllText(Server.MapPath("~/assets/client/template/RegisterAccount.html"));
            content = content.Replace("{{Domain}}", CommonConstants.DomainName);
            content = content.Replace("{{FullName}}", user.Name);
            content = content.Replace("{{UserName}}", user.UserName);
            content = content.Replace("{{Link}}", Link);
            content = content.Replace("{{Ghichu}}", ghichu);
            var toEmail = user.Email;
            int rs = new ClsCommon.MailHelper().SendMail2(toEmail, "Phản hồi đăng ký tài khoản", content);
            return Json(new
            {
                rs = rs,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [HasCredential(RoleID = "INDEX_XACMINHUSER")]
        public ActionResult KhongDuyetTK(string ghichu, int id)
        {
            User user = new UserDao().GetById(id);
            user.checkXacMinh = true;
            new UserDao().Update(user);
            string Link = CommonConstants.DomainName + "/xac-nhan-dang-ky/" + user.UserName + ".html";
            if (user.loaiTK == "2")
            {
                Link = CommonConstants.DomainName + "/xac-nhan-dang-ky-tai-khoan-nha-dau-tu/" + user.UserName + ".html";
            }
            else
            {
                Link = CommonConstants.DomainName + "/xac-nhan-dang-ky/" + user.UserName + ".html";
            }
            string content = System.IO.File.ReadAllText(Server.MapPath("~/assets/client/template/RepFailAccount.html"));
            content = content.Replace("{{Domain}}", CommonConstants.DomainName);
            content = content.Replace("{{FullName}}", user.Name);
            content = content.Replace("{{Ghichu}}", ghichu);
            var toEmail = user.Email;
            int rs = new ClsCommon.MailHelper().SendMail2(toEmail, "Phản hồi đăng ký tài khoản", content);
            return Json(new
            {
                rs = rs,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
    }
}