using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using BotDetect;
using Model.Dao;
using Model.EF;
using OnlineShop.Areas.Admin.Models;
using OnlineShop.Common;

namespace OnlineShop.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        [HttpGet]
        public ActionResult Index()
        {
            string userName = Request.QueryString["userName"];
            if (userName != null)
            {
                string passWord = Request.QueryString["passWord"];
                var result = new UserDao().Login(userName, Common.Encryptor.MD5Hash(passWord), true);
                if (result == 1)
                {
                    try
                    {
                        string strAutoRememberLogin = Common.Encryptor.MD5Hash(userName + passWord);
                        User user = new UserDao().GetByUserName(userName);
                        user.AutoRememberLogin = strAutoRememberLogin;
                        new UserDao().Update(user);
                        if (Request.Cookies["_ATRL"] == null)
                        {
                            HttpCookie cookie = new HttpCookie("_ATRL");
                            cookie.Value = strAutoRememberLogin;
                            cookie.Expires = DateTime.Now.AddDays(365);
                            Response.Cookies.Add(cookie);
                        }
                        else
                        {
                            HttpCookie cookie = HttpContext.Request.Cookies.Get("_ATRL");
                            cookie.Value = strAutoRememberLogin;
                            cookie.Expires = DateTime.Now.AddDays(365);
                            Response.Cookies.Set(cookie);
                        }
                    }
                    catch (Exception)
                    {

                    }
                    string url = FunctionLogin(userName);
                    if (Request.Cookies["Module"] != null)
                    {
                        HttpCookie cookieModule = HttpContext.Request.Cookies.Get("Module");
                        string module = cookieModule.Value;
                        if (module != "")
                        {
                            url = "~/Admin/Home/Dashboard" + module;
                        }
                    }
                    else
                    {
                        string rec = "~/Admin/Home/LanhDaoSo";
                        if (url == rec)
                        {
                            url = "~/Admin/Home/TimKiemLanhDao";
                        }
                    }
                    return Redirect(url);
                }
            }
            else
            {
                try
                {
                    if (Request.Cookies["_ATRL"] != null)
                    {
                        HttpCookie cookie = HttpContext.Request.Cookies.Get("_ATRL");
                        if (cookie.Value != "")
                        {
                            var dao = new UserDao();
                            var entity = dao.GetByAutoRememberLogin(cookie.Value);
                            if (entity != null)
                            {
                                if (entity.UserName != "" && entity.Password != "")
                                {
                                    var result = dao.Login(entity.UserName, entity.Password, true);
                                    if (result == 1)
                                    {
                                        string url = FunctionLogin(entity.UserName);
                                        if (Request.Cookies["Module"] != null)
                                        {
                                            HttpCookie cookieModule = HttpContext.Request.Cookies.Get("Module");
                                            string module = cookieModule.Value;
                                            if (module != "")
                                            {
                                                url = "~/Admin/Home/Dashboard" + module;
                                            }
                                        }
                                        //GhiLog
                                        LogHeThong log = new LogHeThong();
                                        log.NguoiDung = entity.UserName;
                                        log.NoiDung = "Đăng nhập hệ thống";
                                        log.SuKien = "Đăng nhập";
                                        log.ThoiDiem = DateTime.Now;
                                        log.ChucNang = "Login";
                                        log.Ip = ClsCommon.CommonConstants.DomainName;
                                        new LogHeThongDao().Insert(log);
                                        return Redirect(url);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception)
                {

                }
            }
            return View();
        }
        public ActionResult UpdateCookies()
        {
            try
            {
                UserLogin userSession = (UserLogin)Session[CommonConstants.USER_SESSION];
                var dao = new UserDao();
                var user = dao.GetById(int.Parse(userSession.UserID.ToString()));
                ConfigTheme configTheme = new ConfigTheme();
                configTheme.SidebarColor = ClsCommon.CommonStringUnit.GetCookies("SidebarColor");
                configTheme.LogoColor = ClsCommon.CommonStringUnit.GetCookies("LogoColor");
                configTheme.HeaderColor = ClsCommon.CommonStringUnit.GetCookies("HeaderColor");
                configTheme.SidebarPostion = ClsCommon.CommonStringUnit.GetCookies("SidebarPostion");
                configTheme.Header = ClsCommon.CommonStringUnit.GetCookies("Header");
                configTheme.SidebarMenu = ClsCommon.CommonStringUnit.GetCookies("SidebarMenu");
                configTheme.Footer = ClsCommon.CommonStringUnit.GetCookies("Footer");
                configTheme.SidebarClosed = ClsCommon.CommonStringUnit.GetCookies("SidebarClosed");
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                user.ConfigThemes = serializer.Serialize(configTheme);
                dao.Update(user);
            }
            catch (Exception)
            {

            }
            return View();
        }
        public string FunctionLogin(string UserName)
        {
            var dao = new UserDao();
            var user = dao.GetByUserName(UserName);
            var userSession = new UserLogin();
            userSession.UserName = user.UserName;
            userSession.Name = user.Name;
            userSession.Avatar = user.Avatar;
            userSession.UserID = user.ID;
            userSession.GroupID = user.GroupID;
            userSession.UnitCode = user.UnitCode;
            var listCredentials = dao.GetListCredential(UserName);
            var listSysFunction = dao.GetListCredentialSysFunction(UserName);

            Session.Add(CommonConstants.SESSION_CREDENTIALS, listCredentials);
            Session.Add(CommonConstants.USER_SESSION, userSession);
            Session.Add(CommonConstants.SESSION_SYSFUNCTION, listSysFunction);
            //return RedirectToAction("Index", "Home");
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            if (user.ConfigThemes != null && user.ConfigThemes != "")
            {
                ConfigTheme configTheme = serializer.Deserialize<ConfigTheme>(user.ConfigThemes);

                if (configTheme != null)
                {
                    ClsCommon.CommonStringUnit.SetCookies("SidebarColor", configTheme.SidebarColor);
                    ClsCommon.CommonStringUnit.SetCookies("LogoColor", configTheme.LogoColor);
                    ClsCommon.CommonStringUnit.SetCookies("HeaderColor", configTheme.HeaderColor);
                    ClsCommon.CommonStringUnit.SetCookies("SidebarPostion", configTheme.SidebarPostion);
                    ClsCommon.CommonStringUnit.SetCookies("Header", configTheme.Header);
                    ClsCommon.CommonStringUnit.SetCookies("SidebarMenu", configTheme.SidebarMenu);
                    ClsCommon.CommonStringUnit.SetCookies("Footer", configTheme.Footer);
                    ClsCommon.CommonStringUnit.SetCookies("SidebarClosed", configTheme.SidebarClosed);
                }
            }
            else
            {
                try
                {
                    ConfigTheme configTheme = new ConfigTheme();
                    configTheme.SidebarColor = "blue";
                    configTheme.LogoColor = "logo-blue";
                    configTheme.HeaderColor = "header-blue";
                    configTheme.SidebarPostion = "left";
                    configTheme.Header = "default";
                    configTheme.SidebarMenu = "accordion";
                    configTheme.Footer = "default";
                    configTheme.SidebarClosed = "0";
                    user.ConfigThemes = serializer.Serialize(configTheme);
                    dao.Update(user);
                }
                catch (Exception)
                {

                }
            }
            string rec;
            if (user.GroupID.Equals("ADMIN"))
            {
                rec = "~/Admin/Home";
            }else if (user.GroupID.Equals(CommonConstants.LANHDAOSO_GROUP))
            {
                rec = "~/Admin/Home/LanhDaoSo";
                //rec = "~/Admin/Home/Index";
            }
            else
            {
                rec = "~/Admin/Home";
            }
            return rec;
        }
        [HttpPost]
        public JsonResult confirmLogin(string json)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            User user = serializer.Deserialize<User>(json);
            var dao = new UserDao();
            var entity = dao.GetById(int.Parse(user.ID.ToString()));
            bool status = false;
            string url = "";
            if (entity != null)
            {
                if (entity.UserName != "" && entity.Password != "")
                {
                    var result = dao.Login(entity.UserName, entity.Password, true);
                    if (result == 1)
                    {
                        url = FunctionLogin(entity.UserName);
                        status = true;
                    }
                }
            }
            return Json(new
            {
                data = url,
                status = status
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Index(LoginModel model)
        {
            string checkDiDong = Request.Params["checkDiDong"];
            if (checkDiDong != null)
            {
                var result = new UserDao().Login(model.UserName, Common.Encryptor.MD5Hash(model.Password), true);
                if (result == 1)
                {
                    try
                    {
                        string strAutoRememberLogin = Common.Encryptor.MD5Hash(model.UserName + model.Password);
                        User user = new UserDao().GetByUserName(model.UserName);
                        user.AutoRememberLogin = strAutoRememberLogin;
                        new UserDao().Update(user);
                        if (Request.Cookies["_ATRL"] == null)
                        {
                            HttpCookie cookie = new HttpCookie("_ATRL");
                            cookie.Value = strAutoRememberLogin;
                            cookie.Expires = DateTime.Now.AddDays(365);
                            Response.Cookies.Add(cookie);
                        }
                        else
                        {
                            HttpCookie cookie = HttpContext.Request.Cookies.Get("_ATRL");
                            cookie.Value = strAutoRememberLogin;
                            cookie.Expires = DateTime.Now.AddDays(365);
                            Response.Cookies.Set(cookie);
                        }
                    }
                    catch (Exception)
                    {

                    }
                    string url = FunctionLogin(model.UserName);
                    if (Request.Cookies["Module"] != null)
                    {
                        HttpCookie cookieModule = HttpContext.Request.Cookies.Get("Module");
                        string module = cookieModule.Value;
                        if (module != "")
                        {
                            url = "~/Admin/Home/Dashboard" + module;
                        }
                    }
                    else
                    {
                        string rec = "~/Admin/Home/LanhDaoSo";
                        if (url == rec)
                        {
                            url = "~/Admin/Home/TimKiemLanhDao";
                        }
                    }
                    return Redirect(url);
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var dao = new UserDao();
                    var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.Password), true);
                    if (result == 1)
                    {
                        if (model.RememberMe)
                        {
                            try
                            {
                                string strAutoRememberLogin = Encryptor.MD5Hash(model.UserName + model.Password);
                                User user = dao.GetByUserName(model.UserName);
                                user.AutoRememberLogin = strAutoRememberLogin;
                                dao.Update(user);
                                if (Request.Cookies["_ATRL"] == null)
                                {
                                    HttpCookie cookie = new HttpCookie("_ATRL");
                                    cookie.Value = strAutoRememberLogin;
                                    cookie.Expires = DateTime.Now.AddDays(365);
                                    Response.Cookies.Add(cookie);
                                }
                                else
                                {
                                    HttpCookie cookie = HttpContext.Request.Cookies.Get("_ATRL");
                                    cookie.Value = strAutoRememberLogin;
                                    cookie.Expires = DateTime.Now.AddDays(365);
                                    Response.Cookies.Set(cookie);
                                }
                            }
                            catch (Exception)
                            {

                            }
                        }
                        string url = FunctionLogin(model.UserName);
                        if (Request.Cookies["Module"] != null)
                        {
                            try
                            {
                                HttpCookie cookieModule = HttpContext.Request.Cookies.Get("Module");
                                string module = cookieModule.Value;
                                if (module != "")
                                {
                                    url = "~/Admin/Home/Dashboard" + module;
                                }
                            }
                            catch (Exception)
                            {

                            }
                        }
                        LogHeThong log = new LogHeThong();
                        log.NguoiDung = model.UserName;
                        log.NoiDung = "Đăng nhập hệ thống";
                        log.SuKien = "Đăng nhập";
                        log.ThoiDiem = DateTime.Now;
                        log.ChucNang = "Login";
                        log.Ip = ClsCommon.CommonConstants.DomainName;
                        new LogHeThongDao().Insert(log);
                        return Redirect(url);
                    }
                    else if (result == 0)
                    {
                        ModelState.AddModelError("", "Tài khoản không tồn tại.");
                    }
                    else if (result == -1)
                    {
                        ModelState.AddModelError("", "Tài khoản đang bị khoá.");
                    }
                    else if (result == -2)
                    {
                        ModelState.AddModelError("", "Mật khẩu không đúng.");
                    }
                    else if (result == -3)
                    {
                        ModelState.AddModelError("", "Tài khoản của bạn không có quyền đăng nhập.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "đăng nhập không đúng.");
                    }
                }
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
            }
            return Redirect("~/Login");
        }
        public ActionResult Logout()
        {
            UserLogin userSession = (UserLogin)Session[CommonConstants.USER_SESSION];
            LogHeThong log = new LogHeThong();
            log.NguoiDung = userSession.UserName;
            log.NoiDung = "Đăng xuất hệ thống";
            log.SuKien = "Đăng xuất";
            log.ThoiDiem = DateTime.Now;
            log.ChucNang = "Login";
            log.Ip = ClsCommon.CommonConstants.DomainName;
            new LogHeThongDao().Insert(log);
            Session[CommonConstants.USER_SESSION] = null;
            if (Request.Cookies["_ATRL"] != null)
            {
                HttpContext.Response.Cookies.Set(new HttpCookie("_ATRL") { Value = string.Empty });
            }
            return Redirect("~/Login");
        }

        [HttpGet]
        public ActionResult GetUserJson(string userName , string passWord)
        {
            bool status = true;
            User user = new User();
            user.UserName = userName;
            user.Password = Common.Encryptor.MD5Hash(passWord);
            var data = new UserDao().ListAll(user).FirstOrDefault();
            return Json(new
            {
                Name = data.Name,
                Avatar = data.Avatar,
                GroupID = data.GroupID,
                status = status,
                UserID = data.ID
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CheckLogin(string userName, string passWord)
        {
            bool status = false;
            User user = new UserDao().CheckLogin(userName, Common.Encryptor.MD5Hash(passWord));
            Store store = new StoreDao().GetByUserId(int.Parse(user.ID.ToString()));
            if (user != null)
            {
                UserCheckLoginApp userCheckLoginApp = new UserCheckLoginApp();
                userCheckLoginApp.UserName = user.UserName;
                userCheckLoginApp.Password = user.Password;
                userCheckLoginApp.Name = user.Name;
                userCheckLoginApp.Avatar = user.Avatar;
                userCheckLoginApp.GroupID = user.GroupID;
                userCheckLoginApp.UserID = user.ID.ToString();
                if (store != null)
                {
                    userCheckLoginApp.StoreId = store.StoreId.ToString();
                }
                else
                {
                    userCheckLoginApp.StoreId = "0";
                }
                
                status = true;
                return Json(new
                {
                    status = status,
                    data = userCheckLoginApp,
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    status = status,
                    data = "{}"
                }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult ChangerPassword(string json)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            UserChangerPaswword obj = serializer.Deserialize<UserChangerPaswword>(json);
            bool status = true;
            string message = "";
            var dao = new UserDao();
            if (dao.CheckUserName(obj.UserId, Common.Encryptor.MD5Hash(obj.CurrentPassword)))
            {
                var entity = dao.GetById(int.Parse(obj.UserId.ToString()));
                if (!string.IsNullOrEmpty(obj.Password))
                {
                    var encryptedMd5Pas = Common.Encryptor.MD5Hash(obj.Password);
                    entity.Password = encryptedMd5Pas;
                }
                var result = dao.Update(entity);
                if (result)
                {
                    message = "success";
                }
                else
                {
                    message = "unsuccessful";
                }
            }
            else
            {
                message = "Current password does not match";
            }
            
            return Json(new
            {
                status = status,
                message = message,
            }, JsonRequestBehavior.AllowGet);
        }
    }
}