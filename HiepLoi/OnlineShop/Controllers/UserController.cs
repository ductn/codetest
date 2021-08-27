using BotDetect.Web.UI.Mvc;
using Facebook;
using Model.Dao;
using Model.EF;
using ClsCommon;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using System.Globalization;

namespace OnlineShop.Controllers
{

    public class UserController : Controller
    {
        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }

        // GET: User
        [HttpGet]
        public ActionResult Register()
        {
            var user = new Model.EF.User();
            //DropdownList Huyện, xã
            Province itemProvince = new Province();
            var daoProvince = new ProvinceDao();
            itemProvince.ParentId = Common.CommonConstants.TINH_ID;
            List<SelectListItem> listHuyen = new List<SelectListItem>();
            List<Province> lstHuyen = daoProvince.ListAll(itemProvince);

            itemProvince.ParentId = 0;
            List<SelectListItem> listTinh = new List<SelectListItem>();
            List<Province> lstTinh = daoProvince.ListAll(itemProvince);

            itemProvince.ParentId = lstHuyen.FirstOrDefault().Id;
            List<SelectListItem> listXa = new List<SelectListItem>();
            List<Province> lstXa = daoProvince.ListAll(itemProvince);

            foreach (Province item in lstTinh)
            {
                listTinh.Add(new SelectListItem { Value = item.Id + "", Text = item.Title });
            }

            foreach (Province item in lstHuyen)
            {
                listHuyen.Add(new SelectListItem { Value = item.Id + "", Text = item.Title });
            }
            foreach (Province item in lstXa)
            {
                listXa.Add(new SelectListItem { Value = item.Id + "", Text = item.Title });
            }
            ViewBag.ProvinceID = new SelectList(listTinh, "Value", "Text", Common.CommonConstants.TINH_ID);
            ViewBag.HuyenID = new SelectList(listHuyen, "Value", "Text", null);
            ViewBag.XaID = new SelectList(listXa, "Value", "Text", null);
            //End DropdownList Huyện, xã
            return View(user);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Register(Model.EF.User user)
        {
            //int ckErrorz = 0;
            //string CustomErrorz = "Tài khoản đăng ký thành công. Vui lòng kiểm tra email để xác thực việc đăng ký.";
            //DropdownList Huyện, xã
            Province itemProvince = new Province();
            var daoProvince = new ProvinceDao();
            itemProvince.ParentId = Common.CommonConstants.TINH_ID;
            List<SelectListItem> listHuyen = new List<SelectListItem>();
            List<Province> lstHuyen = daoProvince.ListAll(itemProvince);

            itemProvince.ParentId = 0;
            List<SelectListItem> listTinh = new List<SelectListItem>();
            List<Province> lstTinh = daoProvince.ListAll(itemProvince);

            itemProvince.ParentId = lstHuyen.FirstOrDefault().Id;
            List<SelectListItem> listXa = new List<SelectListItem>();
            List<Province> lstXa = daoProvince.ListAll(itemProvince);

            foreach (Province item in lstTinh)
            {
                listTinh.Add(new SelectListItem { Value = item.Id + "", Text = item.Title });
            }

            foreach (Province item in lstHuyen)
            {
                listHuyen.Add(new SelectListItem { Value = item.Id + "", Text = item.Title });
            }
            foreach (Province item in lstXa)
            {
                listXa.Add(new SelectListItem { Value = item.Id + "", Text = item.Title });
            }
            ViewBag.ProvinceID = new SelectList(listTinh, "Value", "Text", Common.CommonConstants.TINH_ID);
            ViewBag.HuyenID = new SelectList(listHuyen, "Value", "Text", null);
            ViewBag.XaID = new SelectList(listXa, "Value", "Text", null);
            //End DropdownList Huyện, xã
            if (ModelState.IsValid)
            {
                var dao = new Model.Dao.UserDao();
                var encryptedMd5Pas = Common.Encryptor.MD5Hash(user.Password);
                user.Password = encryptedMd5Pas;
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                if (session != null)
                {
                    user.CreatedBy = session.UserName;
                }
                user.GroupID = CommonConstants.NGUOIDUNGCONGKHAI_GROUP;
                user.Status = false;
                long id = 0;
                if (user.loaiTK == "2")
                {
                    //Model.QLDAEF.User userQLDA = new Model.QLDAEF.User();
                    //userQLDA.UserName = user.UserName;
                    //userQLDA.Password = user.Password;
                    //userQLDA.GroupID = "CAPNHATTIENDODUAN";
                    //userQLDA.Name = user.Name;
                    //userQLDA.Address = user.Address;
                    //userQLDA.Email = user.Email;
                    //userQLDA.Phone = user.Phone;
                    //userQLDA.ProvinceID = user.ProvinceID;
                    //userQLDA.DistrictID = user.DistrictID;
                    //userQLDA.ChucVu = user.ChucVu;
                    //userQLDA.HuyenID = user.HuyenID;
                    //userQLDA.XaID = user.XaID;
                    //userQLDA.Status = false;

                    //id = new Model.QLDADao.UserDao().Insert(userQLDA);
                }
                else
                {
                    id = dao.Insert(user);
                }
                if (id > 0)
                {
                    //SetAlert("Thêm user thành công", "success");
                    //string Link = CommonConstants.DomainName + "/xac-nhan-dang-ky/" + user.UserName + ".html";
                    //if (user.loaiTK == "2")
                    //{
                    //Link = CommonConstants.DomainName + "/xac-nhan-dang-ky-tai-khoan-nha-dau-tu/" + user.UserName + ".html";
                    //}
                    //else
                    //{
                    //Link = CommonConstants.DomainName + "/xac-nhan-dang-ky/" + user.UserName + ".html";
                    //}
                    //string content = System.IO.File.ReadAllText(Server.MapPath("~/assets/client/template/RegisterAccount.html"));
                    //content = content.Replace("{{Domain}}", CommonConstants.DomainName);
                    //content = content.Replace("{{FullName}}", user.Name);
                    //content = content.Replace("{{UserName}}", user.UserName);
                    //content = content.Replace("{{Link}}", Link);
                    //var toEmail = user.Email;
                    //int rs = new ClsCommon.MailHelper().SendMail2(toEmail, "Phản hồi đăng ký tài khoản", content);
                    //ViewBag.ckError = ckError;
                    //ViewBag.CustomError = CustomError;
                    return RedirectToAction("KiemTraTaiKhoanView");
                    //return RedirectToAction("Login", "User");
                    //return View();
                }
                else
                {
                    //ckErrorz = 1;
                    ModelState.AddModelError("", "Thêm user không thành công");
                }
            }
            return View("Register");
        }

        // GET: User
        [HttpGet]
        public ActionResult DangKyApp()
        {
            var user = new Model.EF.User();
            //DropdownList Huyện, xã
            Province itemProvince = new Province();
            var daoProvince = new ProvinceDao();
            itemProvince.ParentId = Common.CommonConstants.TINH_ID;
            List<SelectListItem> listHuyen = new List<SelectListItem>();
            List<Province> lstHuyen = daoProvince.ListAll(itemProvince);

            itemProvince.ParentId = 0;
            List<SelectListItem> listTinh = new List<SelectListItem>();
            List<Province> lstTinh = daoProvince.ListAll(itemProvince);

            itemProvince.ParentId = lstHuyen.FirstOrDefault().Id;
            List<SelectListItem> listXa = new List<SelectListItem>();
            List<Province> lstXa = daoProvince.ListAll(itemProvince);

            foreach (Province item in lstTinh)
            {
                listTinh.Add(new SelectListItem { Value = item.Id + "", Text = item.Title });
            }

            foreach (Province item in lstHuyen)
            {
                listHuyen.Add(new SelectListItem { Value = item.Id + "", Text = item.Title });
            }
            foreach (Province item in lstXa)
            {
                listXa.Add(new SelectListItem { Value = item.Id + "", Text = item.Title });
            }
            ViewBag.ProvinceID = new SelectList(listTinh, "Value", "Text", Common.CommonConstants.TINH_ID);
            ViewBag.HuyenID = new SelectList(listHuyen, "Value", "Text", null);
            ViewBag.XaID = new SelectList(listXa, "Value", "Text", null);
            //End DropdownList Huyện, xã
            return View(user);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult DangKyApp(Model.EF.User user)
        {
            int ckError = 0;
            string CustomError = "Tài khoản đăng ký thành công. Vui lòng kiểm tra email để xác thực việc đăng ký.";
            //DropdownList Huyện, xã
            Province itemProvince = new Province();
            var daoProvince = new ProvinceDao();
            itemProvince.ParentId = Common.CommonConstants.TINH_ID;
            List<SelectListItem> listHuyen = new List<SelectListItem>();
            List<Province> lstHuyen = daoProvince.ListAll(itemProvince);

            itemProvince.ParentId = 0;
            List<SelectListItem> listTinh = new List<SelectListItem>();
            List<Province> lstTinh = daoProvince.ListAll(itemProvince);

            itemProvince.ParentId = lstHuyen.FirstOrDefault().Id;
            List<SelectListItem> listXa = new List<SelectListItem>();
            List<Province> lstXa = daoProvince.ListAll(itemProvince);

            foreach (Province item in lstTinh)
            {
                listTinh.Add(new SelectListItem { Value = item.Id + "", Text = item.Title });
            }

            foreach (Province item in lstHuyen)
            {
                listHuyen.Add(new SelectListItem { Value = item.Id + "", Text = item.Title });
            }
            foreach (Province item in lstXa)
            {
                listXa.Add(new SelectListItem { Value = item.Id + "", Text = item.Title });
            }
            ViewBag.ProvinceID = new SelectList(listTinh, "Value", "Text", Common.CommonConstants.TINH_ID);
            ViewBag.HuyenID = new SelectList(listHuyen, "Value", "Text", null);
            ViewBag.XaID = new SelectList(listXa, "Value", "Text", null);
            //End DropdownList Huyện, xã
            if (ModelState.IsValid)
            {
                var dao = new Model.Dao.UserDao();
                var encryptedMd5Pas = Common.Encryptor.MD5Hash(user.Password);
                user.Password = encryptedMd5Pas;
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                if (session != null)
                {
                    user.CreatedBy = session.UserName;
                }
                user.GroupID = CommonConstants.NGUOIDUNGCONGKHAI_GROUP;
                user.Status = false;
                long id = dao.Insert(user);

                if (id > 0)
                {
                    //SetAlert("Thêm user thành công", "success");
                    string Link = CommonConstants.DomainName + "/xac-nhan-dang-ky/" + user.UserName + ".html";
                    string content = System.IO.File.ReadAllText(Server.MapPath("~/assets/client/template/RegisterAccount.html"));
                    content = content.Replace("{{Domain}}", CommonConstants.DomainName);
                    content = content.Replace("{{FullName}}", user.Name);
                    content = content.Replace("{{UserName}}", user.UserName);
                    content = content.Replace("{{Link}}", Link);
                    var toEmail = user.Email;
                    int rs = new ClsCommon.MailHelper().SendMail2(toEmail, "Phản hồi đăng ký tài khoản", content);
                    ViewBag.ckError = ckError;
                    ViewBag.CustomError = CustomError;
                    //return RedirectToAction("Login", "User");
                    return View();
                }
                else
                {
                    ckError = 1;
                    ModelState.AddModelError("", "Thêm user không thành công");
                }
            }
            return View("DangKyApp");
        }

        public ActionResult Login(string userId)
        {
            String username = "";
            var checkApp = Request.QueryString["checkApp"];
            if (checkApp != null)
            {
                if (userId != null && userId != "")
                {
                    var checkCongKhai = new Model.Dao.UserDao().GetById(int.Parse(userId));
                    if (checkCongKhai != null)
                    {
                        username = checkCongKhai.UserName;
                        if (checkCongKhai.GroupID == CommonConstants.NGUOIDUNGCONGKHAI_GROUP || checkCongKhai.GroupID == CommonConstants.NHADAUTU_GROUP)//Kiểm tra tài khoản có phải là người dùng công khai
                        {
                            var result = new Model.Dao.UserDao().Login(checkCongKhai.UserName, checkCongKhai.Password);
                            if (result == 1)
                            {
                                var user = new Model.Dao.UserDao().GetByUserName(checkCongKhai.UserName);
                                var userSession = new UserLogin();
                                userSession.UserName = user.UserName;
                                userSession.UserID = user.ID;
                                userSession.Name = user.Name;
                                userSession.GroupID = user.GroupID;
                                Session.Add(CommonConstants.USER_SESSION, userSession);
                                var checkDM = Request.QueryString["checkDM"];
                                if (checkDM != null)
                                {
                                    return Redirect("~/doi-mat-khau-app.html");
                                }
                                var checkLoai = Request.QueryString["checkLoai"];
                                if(checkLoai == "1")
                                {
                                    return Redirect("~/thong-tin-ca-nhan-app.html");
                                }
                                if (checkLoai == "2")
                                {
                                    return Redirect("~/doi-mat-khau-app.html");
                                }
                                if (checkLoai == "3")
                                {
                                    return Redirect("~/lien-he-gian-hang-app.html");
                                }
                                if (checkLoai == "4")
                                {
                                    return Redirect("~/tao-moi-gian-hang-app.html");
                                }
                                if (checkLoai == "5")
                                {
                                    return Redirect("~/cap-nhat-gian-hang-app.html");
                                }
                                if (checkLoai == "6")
                                {
                                    return Redirect("~/gian-hang-moi-app.html");
                                }
                                if (checkLoai == "7")
                                {
                                    return Redirect("~/gian-hang-dang-cap-nhat-app.html");
                                }
                                if (checkLoai == "8")
                                {
                                    return Redirect("~/gian-hang-cho-bo-sung-app.html");
                                }
                                if (checkLoai == "9")
                                {
                                    return Redirect("~/gian-hang-cho-phe-duyet-app.html");
                                }
                                if (checkLoai == "10")
                                {
                                    return Redirect("~/gian-hang-duoc-duyet-app.html");
                                }
                                if (checkLoai == "11")
                                {
                                    return Redirect("~/gian-hang-khong-duyet-app.html");
                                }
                                if (checkLoai == "12")
                                {
                                    return Redirect("~/san-pham-moi-app.html");
                                }
                                if (checkLoai == "13")
                                {
                                    return Redirect("~/tao-moi-san-pham-app.html");
                                }
                                if (checkLoai == "14")
                                {
                                    var idSanPham = Request.QueryString["idSanPham"];
                                    return Redirect("~/cap-nhat-san-pham-app.html?Id="+ idSanPham);
                                }
                                if (checkLoai == "15")
                                {
                                    return Redirect("~/san-pham-dang-cap-nhat-app.html");
                                }
                                if (checkLoai == "16")
                                {
                                    return Redirect("~/san-pham-cho-bo-sung-app.html");
                                }
                                if (checkLoai == "17")
                                {
                                    return Redirect("~/san-pham-cho-phe-duyet-app.html");
                                }
                                if (checkLoai == "18")
                                {
                                    return Redirect("~/san-pham-duoc-duyet-app.html");
                                }
                                if (checkLoai == "19")
                                {
                                    return Redirect("~/san-pham-khong-duyet-app.html");
                                }
                                if (checkLoai == "20")
                                {
                                    return Redirect("~/don-hang-cho-xu-ly-app.html");
                                }
                                if (checkLoai == "21")
                                {
                                    return Redirect("~/don-hang-da-xu-ly-app.html");
                                }
                                //if (checkLoai == "22")
                                //{
                                //    return Redirect("~/thong-tin-don-hang-app.html?orderID=" + idSanPham");
                                //}
                            }

                        }
                    }
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
                            var dao = new Model.Dao.UserDao();
                            var entity = dao.GetByAutoRememberLogin(cookie.Value);
                            if (entity != null)
                            {
                                if (entity.UserName != "" && entity.Password != "")
                                {
                                    var result = dao.Login(entity.UserName, entity.Password, false);
                                    if (result == 1)
                                    {
                                        var user = dao.GetByUserName(entity.UserName);
                                        var userSession = new UserLogin();
                                        userSession.UserName = user.UserName;
                                        userSession.UserID = user.ID;
                                        userSession.Name = user.Name;
                                        Session.Add(CommonConstants.USER_SESSION, userSession);
                                        return Redirect("~/thong-tin-ca-nhan.html");
                                    }
                                }
                            }
                        }
                    }
                    else
                    {

                    }
                }
                catch (Exception)
                {

                }
            }

            LoginModel model = new LoginModel();
            model.UserName = username;
            return View(model);
        }
        [HttpGet]
        public ActionResult ForgotPassword()
        {

            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string email)
        {
            int ckError = 0;
            string CustomError = "Gửi email thành công. Vui lòng kiểm tra email.";
            if (ModelState.IsValid)
            {
                var dao = new Model.Dao.UserDao();
                var model = dao.GetByEmail(email);
                if (model != null)
                {
                    if (model.ckQuenMK == "1")
                    {
                        ckError = 1;
                        CustomError = "Yêu cầu đổi mật khẩu đã được gửi trước đó.";
                        ViewBag.ckError = ckError;
                        ViewBag.CustomError = CustomError;
                        return View(model);
                    }
                    string tokenQuenMK = Common.Encryptor.MD5Hash(DateTime.Now.ToString());
                    model.tokenQuenMK = tokenQuenMK;
                    model.ckQuenMK = "1";
                    string Link = CommonConstants.DomainName + "/cap-nhat-mat-khau/" + model.ID + "/" + model.tokenQuenMK + ".html";
                    string content = System.IO.File.ReadAllText(Server.MapPath("~/assets/client/template/ForgotPass.html"));
                    content = content.Replace("{{Domain}}", CommonConstants.DomainName);
                    content = content.Replace("{{FullName}}", model.Name);
                    content = content.Replace("{{Link}}", Link);
                    var toEmail = email;
                    int rs = new ClsCommon.MailHelper().SendMail2(toEmail, "Phản hồi đổi mật khẩu", content);
                    if (rs == 1)
                    {
                        dao.Update(model);
                    }
                    else
                    {
                        ckError = 1;
                        CustomError = "Gửi email thất bại.";
                        ViewBag.ckError = ckError;
                        ViewBag.CustomError = CustomError;
                        return View(model);
                    }
                }
                else
                {
                    ckError = 1;
                    CustomError = "Email không tồn tại trên hệ thống.";
                    ViewBag.ckError = ckError;
                    ViewBag.CustomError = CustomError;
                    return View(model);
                }

            }
            ViewBag.ckError = ckError;
            ViewBag.CustomError = CustomError;
            return View();
        }
        [HttpGet]
        public ActionResult QuenMatKhauApp()
        {

            return View();
        }

        [HttpPost]
        public ActionResult QuenMatKhauApp(string email)
        {
            int ckError = 0;
            string CustomError = "Gửi email thành công. Vui lòng kiểm tra email.";
            if (ModelState.IsValid)
            {
                var dao = new Model.Dao.UserDao();
                var model = dao.GetByEmail(email);
                if (model != null)
                {
                    if (model.ckQuenMK == "1")
                    {
                        ckError = 1;
                        CustomError = "Yêu cầu đổi mật khẩu đã được gửi trước đó.";
                        ViewBag.ckError = ckError;
                        ViewBag.CustomError = CustomError;
                        return View(model);
                    }
                    string tokenQuenMK = Common.Encryptor.MD5Hash(DateTime.Now.ToString());
                    model.tokenQuenMK = tokenQuenMK;
                    model.ckQuenMK = "1";
                    string Link = CommonConstants.DomainName + "/cap-nhat-mat-khau/" + model.ID + "/" + model.tokenQuenMK + ".html";
                    string content = System.IO.File.ReadAllText(Server.MapPath("~/assets/client/template/ForgotPass.html"));
                    content = content.Replace("{{Domain}}", CommonConstants.DomainName);
                    content = content.Replace("{{FullName}}", model.Name);
                    content = content.Replace("{{Link}}", Link);
                    var toEmail = email;
                    int rs = new ClsCommon.MailHelper().SendMail2(toEmail, "Phản hồi đổi mật khẩu", content);
                    if (rs == 1)
                    {
                        dao.Update(model);
                    }
                    else
                    {
                        ckError = 1;
                        CustomError = "Gửi email thất bại.";
                        ViewBag.ckError = ckError;
                        ViewBag.CustomError = CustomError;
                        return View(model);
                    }
                }
                else
                {
                    ckError = 1;
                    CustomError = "Email không tồn tại trên hệ thống.";
                    ViewBag.ckError = ckError;
                    ViewBag.CustomError = CustomError;
                    return View(model);
                }

            }
            ViewBag.ckError = ckError;
            ViewBag.CustomError = CustomError;
            return View();
        }
        [HttpGet]
        public ActionResult UpdatePassword(int userid, string token)
        {
            var dao = new Model.Dao.UserDao();
            var model = dao.GetByToKen(userid, token);
            if (model != null)
            {
                ViewBag.userId = userid;
                return View();
            }
            else
            {
                //string nview = "~/Views/Home/ERR404.cshtml"; 
                //return View(nview);
                return RedirectToRoute("404");
            }
        }

        [HttpGet]
        public ActionResult AcceptRegister(string UserName)
        {
            var dao = new Model.Dao.UserDao();
            var model = dao.GetByUserName(UserName);
            if (model != null)
            {
                model.Status = true;
                dao.Update(model);
                ViewBag.UserName = UserName;
                return View();
            }
            else
            {
                //string nview = "~/Views/Home/ERR404.cshtml"; 
                //return View(nview);
                return RedirectToRoute("404");
            }
        }

        //[HttpGet]
        //public ActionResult AcceptAccountNhaDauTuRegister(string UserName)
        //{
        //    var dao = new Model.QLDADao.UserDao();
        //    var model = dao.GetByUserName(UserName);
        //    if (model != null)
        //    {
        //        model.Status = true;
        //        dao.Update(model);
        //        ViewBag.UserName = UserName;
        //        return View();
        //    }
        //    else
        //    {
        //        //string nview = "~/Views/Home/ERR404.cshtml"; 
        //        //return View(nview);
        //        return RedirectToRoute("404");
        //    }
        //}

        [HttpPost]
        public ActionResult UpdatePassword(int userId, string newPass, string xacNhanPass)
        {
            int ckError = 0;
            string CustomError = "Đổi mật khẩu thành công";
            if (ModelState.IsValid)
            {
                var dao = new Model.Dao.UserDao();
                var model = dao.ViewDetail(userId);
                if (model != null)
                {
                    if (newPass == xacNhanPass)
                    {
                        var encryptedMd5Pas = Common.Encryptor.MD5Hash(newPass);
                        model.Password = encryptedMd5Pas;
                        model.ckQuenMK = "0";
                        model.tokenQuenMK = "";
                        dao.Update(model);
                    }
                    else
                    {
                        ckError = 1;
                        CustomError = "Vui lòng nhập đúng xác nhận mật khởi mới";
                    }
                }
            }
            ViewBag.ckError = ckError;
            ViewBag.userId = userId;
            ViewBag.CustomError = CustomError;
            return View();
        }

        public ActionResult LoginFacebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email",
            });

            return Redirect(loginUrl.AbsoluteUri);
        }

        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });


            var accessToken = result.access_token;
            if (!string.IsNullOrEmpty(accessToken))
            {
                fb.AccessToken = accessToken;
                // Get the user's information, like email, first name, middle name etc
                dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email");
                string email = me.email;
                string userName = me.email;
                string firstname = me.first_name;
                string middlename = me.middle_name;
                string lastname = me.last_name;

                var user = new Model.EF.User();
                user.Email = email;
                user.UserName = email;
                user.Status = true;
                user.Name = firstname + " " + middlename + " " + lastname;
                user.CreatedDate = DateTime.Now;
                var resultInsert = new Model.Dao.UserDao().InsertForFacebook(user);
                if (resultInsert > 0)
                {
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.UserID = user.ID;
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                }
            }
            return Redirect("/");
        }
        public ActionResult Logout()
        {
            Session[CommonConstants.USER_SESSION] = null;
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            int ckError = 0;
            string CustomError = "";
            if (ModelState.IsValid)
            {
                var dao = new Model.Dao.UserDao();
                var itUser = new Model.EF.User();
                itUser.UserName = model.UserName;
                var checkCongKhai = dao.ListAll(itUser).FirstOrDefault();
                if (checkCongKhai != null)
                {
                    if (checkCongKhai.GroupID == CommonConstants.NGUOIDUNGCONGKHAI_GROUP || checkCongKhai.GroupID == CommonConstants.NHADAUTU_GROUP)//Kiểm tra tài khoản có phải là người dùng công khai
                    {
                        var result = dao.Login(model.UserName, Common.Encryptor.MD5Hash(model.Password));
                        if (result == 1)
                        {
                            if (model.RememberMe)
                            {
                                try
                                {
                                    string strAutoRememberLogin = Common.Encryptor.MD5Hash(model.UserName + model.Password);
                                    Model.EF.User user1 = dao.GetByUserName(model.UserName);
                                    user1.AutoRememberLogin = strAutoRememberLogin;
                                    dao.Update(user1);
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

                            var user = dao.GetByUserName(model.UserName);
                            var userSession = new UserLogin();
                            userSession.UserName = user.UserName;
                            userSession.UserID = user.ID;
                            userSession.Name = user.Name;
                            userSession.GroupID = user.GroupID;
                            Session.Add(CommonConstants.USER_SESSION, userSession);
                            return Redirect("~/thong-tin-ca-nhan.html");
                        }
                        else if (result == 0)
                        {
                            ckError = 1;
                            CustomError = "Tài khoản không tồn tại.";
                            ModelState.AddModelError("", "Tài khoản không tồn tại.");
                        }
                        else if (result == -1)
                        {
                            ckError = 1;
                            CustomError = "Tài khoản chưa được kích hoạt.";
                            ModelState.AddModelError("", "Tài khoản chưa được kích hoạt.");
                        }
                        else if (result == -2)
                        {
                            ckError = 1;
                            CustomError = "Mật khẩu không đúng.";
                            ModelState.AddModelError("", "Mật khẩu không đúng.");
                        }
                        else
                        {
                            ckError = 1;
                            CustomError = "đăng nhập không đúng.";
                            ModelState.AddModelError("", "đăng nhập không đúng.");
                        }
                    }
                }
                else
                {
                    ckError = 1;
                    CustomError = "Tài khoản không tồn tại.";
                    ModelState.AddModelError("", "Tài khoản không tồn tại.");
                }

            }
            ViewBag.ckError = ckError;
            ViewBag.CustomError = CustomError;
            return View(model);
        }
        //[HttpPost]
        //[CaptchaValidation("CaptchaCode", "registerCapcha", "Mã xác nhận không đúng!")]
        //public ActionResult Register(RegisterModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var dao = new UserDao();
        //        if (dao.CheckUserName(model.UserName))
        //        {
        //            ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
        //        }
        //        else if (dao.CheckEmail(model.Email))
        //        {
        //            ModelState.AddModelError("", "Email đã tồn tại");
        //        }
        //        else
        //        {
        //            var user = new User();
        //            user.Name = model.Name;
        //            user.Password = Common.Encryptor.MD5Hash(model.Password);
        //            user.Phone = model.Phone;
        //            user.Email = model.Email;
        //            user.Address = model.Address;
        //            user.CreatedDate = DateTime.Now;
        //            user.Status = true;
        //            if (!string.IsNullOrEmpty(model.ProvinceID))
        //            {
        //                user.ProvinceID = int.Parse(model.ProvinceID);
        //            }
        //            if (!string.IsNullOrEmpty(model.DistrictID))
        //            {
        //                user.DistrictID = int.Parse(model.DistrictID);
        //            }

        //            var result = dao.Insert(user);
        //            if (result > 0)
        //            {
        //                ViewBag.Success = "Đăng ký thành công";
        //                model = new RegisterModel();
        //            }
        //            else
        //            {
        //                ModelState.AddModelError("", "Đăng ký không thành công.");
        //            }
        //        }
        //    }
        //    return View(model);
        //}

        public JsonResult LoadProvince()
        {
            var xmlDoc = XDocument.Load(Server.MapPath(@"~/assets/client/data/Provinces_Data.xml"));

            var xElements = xmlDoc.Element("Root").Elements("Item").Where(x => x.Attribute("type").Value == "province");
            var list = new List<ProvinceModel>();
            ProvinceModel province = null;
            foreach (var item in xElements)
            {
                province = new ProvinceModel();
                province.ID = int.Parse(item.Attribute("id").Value);
                province.Name = item.Attribute("value").Value;
                list.Add(province);

            }
            return Json(new
            {
                data = list,
                status = true
            });
        }
        public JsonResult LoadDistrict(int provinceID)
        {
            var xmlDoc = XDocument.Load(Server.MapPath(@"~/assets/client/data/Provinces_Data.xml"));

            var xElement = xmlDoc.Element("Root").Elements("Item")
                .Single(x => x.Attribute("type").Value == "province" && int.Parse(x.Attribute("id").Value) == provinceID);

            var list = new List<DistrictModel>();
            DistrictModel district = null;
            foreach (var item in xElement.Elements("Item").Where(x => x.Attribute("type").Value == "district"))
            {
                district = new DistrictModel();
                district.ID = int.Parse(item.Attribute("id").Value);
                district.Name = item.Attribute("value").Value;
                district.ProvinceID = int.Parse(xElement.Attribute("id").Value);
                list.Add(district);

            }
            return Json(new
            {
                data = list,
                status = true
            });
        }
        public JsonResult GetUserInfor(string UserName)
        {
            string loaiHinh = "0";
            string sonha = "";
            string[] a = { };
            var dao = new Model.Dao.UserDao();
            var user = new Model.EF.User();
            user.UserName = UserName;
            Model.EF.User dataUser = dao.ListAll(user).FirstOrDefault();
            bool checkUser = false;

            if (dataUser != null)
            {
                checkUser = true;
            }

            //Doanh nghiệp
            //var daoDoanhNghiep = new DanhSachDoanhNghiepDao();
            //var DanhSachDoanhNghiep = new DanhSachDoanhNghiep();
            //DanhSachDoanhNghiep.MaSoDoanhNghiep = UserName;
            //DanhSachDoanhNghiep DoanhNghiep = daoDoanhNghiep.ListAll(DanhSachDoanhNghiep).FirstOrDefault();
            //if (DoanhNghiep != null)
            //{
            //    loaiHinh = "1";
            //    sonha = DoanhNghiep.DiaChiTruSoChinh;
            //    a = sonha.Split(new string[] { "," }, StringSplitOptions.None);
            //    if (a.Length == 9)
            //    {
            //        sonha = a[0] + a[1] + a[2] + a[3] + a[4];
            //    }
            //    if (a.Length == 8)
            //    {
            //        sonha = a[0] + a[1] + a[2] + a[3];
            //    }
            //    if (a.Length == 7)
            //    {
            //        sonha = a[0] + a[1] + a[2];
            //    }
            //    if (a.Length == 6)
            //    {
            //        sonha = a[0] + a[1];
            //    }
            //    if (a.Length == 5)
            //    {
            //        sonha = a[0];
            //    }
            //}

            //End doanh nghiệp
            //Hợp tác xã
            //var daoHopTacXa = new HopTacXaHouseholdDao();
            //var hopTacXaHouseHold = new HopTacXaHousehold();
            //hopTacXaHouseHold.HopTacXaCode = UserName;
            //HopTacXaHousehold HTX = daoHopTacXa.ListAll(hopTacXaHouseHold).FirstOrDefault();
            //if (HTX != null)
            //{
            //    loaiHinh = "2";
            //    sonha = HTX.Biz_HeadOffice;
            //    a = sonha.Split(new string[] { "," }, StringSplitOptions.None);
            //    if (a.Length == 8)
            //    {
            //        sonha = a[0] + a[1] + a[2] + a[3] + a[4];
            //    }
            //    if (a.Length == 7)
            //    {
            //        sonha = a[0] + a[1] + a[2] + a[3];
            //    }
            //    if (a.Length == 6)
            //    {
            //        sonha = a[0] + a[1] + a[2];
            //    }
            //    if (a.Length == 5)
            //    {
            //        sonha = a[0] + a[1];
            //    }
            //    if (a.Length == 4)
            //    {
            //        sonha = a[0];
            //    }
            //}
            //End Hợp tác xã
            //Hộ kinh doanh
            //var daoCertifiedHouseHold = new CertifiedHouseholdDao();
            //var certifiedHoseHold = new CertifiedHousehold();
            //certifiedHoseHold.CertifiedCode = UserName;
            //CertifiedHousehold HKD = daoCertifiedHouseHold.ListAll(certifiedHoseHold).FirstOrDefault();
            //if (HKD != null)
            //{
            //    loaiHinh = "3";
            //    sonha = HKD.Biz_HeadOffice;
            //    a = sonha.Split(new string[] { "," }, StringSplitOptions.None);
            //    if (a.Length == 8)
            //    {
            //        sonha = a[0] + a[1] + a[2] + a[3] + a[4];
            //    }
            //    if (a.Length == 7)
            //    {
            //        sonha = a[0] + a[1] + a[2] + a[3];
            //    }
            //    if (a.Length == 6)
            //    {
            //        sonha = a[0] + a[1] + a[2];
            //    }
            //    if (a.Length == 5)
            //    {
            //        sonha = a[0] + a[1];
            //    }
            //    if (a.Length == 4)
            //    {
            //        sonha = a[0];
            //    }
            //}
            //End hộ kinh doanh
            return Json(new
            {
                checkUser = checkUser,
                loai = loaiHinh,
                //dataHKD = HKD,
                //dataDN = DoanhNghiep,
                //dataHTX = HTX,
                sonha = sonha,
                status = true
            }, JsonRequestBehavior.AllowGet); ;
        }
        public JsonResult CheckPassOld(string UserName, String PasswordOld)
        {
            var result = new Model.Dao.UserDao().Login(UserName, Common.Encryptor.MD5Hash(PasswordOld));
            bool rs = false;
            if (result == 1)
            {
                rs = true;
            }
            return Json(new
            {
                status = rs
            }, JsonRequestBehavior.AllowGet); ;
        }
        [HttpGet]
        public ActionResult KiemTraTaiKhoanView()
        {
            return View();
        }
    }
}