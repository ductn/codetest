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

    public class UserController : BaseController
    {
        // GET: Admin/User
        [HasCredential(RoleID = "INDEX_USER")]
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
            itUser.loaiTK = "all";
            var listUser = dao.ListAllPaging(itUser, "", ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "USER";
            setPermission(controller);//Hàm này trong BaseController
            SetViewBag();
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.listUser = listUser;
            return View(itUser);
        }

        [HttpPost]
        [HasCredential(RoleID = "INDEX_USER")]
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
            itUser.loaiTK = "all";
            var listUser = dao.ListAllPaging(itUser, "", ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "USER";
            setPermission(controller);//Hàm này trong BaseController
            SetViewBag();
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.listUser = listUser;
            return View(itUser);
        }

        [HasCredential(RoleID = "INDEX_CANBO_USER")]
        public ActionResult TaiKhoanCanBo(int page = 1, int pageSize = 5)
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
            itUser.loaiTK = "cb";
            var listUser = dao.ListAllPaging(itUser, "", ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "USER";
            setPermission(controller);//Hàm này trong BaseController
            SetViewBag();
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.listUser = listUser;
            return View(itUser);
        }

        [HttpPost]
        [HasCredential(RoleID = "INDEX_CANBO_USER")]
        public ActionResult TaiKhoanCanBo(User itUser, int page = 1, int pageSize = 5)
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
            itUser.loaiTK = "cb";
            var listUser = dao.ListAllPaging(itUser, "", ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "USER";
            setPermission(controller);//Hàm này trong BaseController
            SetViewBag();
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.listUser = listUser;
            return View(itUser);
        }
        [HasCredential(RoleID = "INDEX_CONGKHAI_USER")]
        public ActionResult TaiKhoanCongKhai(int page = 1, int pageSize = 5)
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
            var listUser = dao.ListAllPaging(itUser, "", ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "USER";
            setPermission(controller);//Hàm này trong BaseController
            SetViewBag();
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.listUser = listUser;
            return View(itUser);
        }

        [HttpPost]
        [HasCredential(RoleID = "INDEX_CONGKHAI_USER")]
        public ActionResult TaiKhoanCongKhai(User itUser, int page = 1, int pageSize = 5)
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
            var listUser = dao.ListAllPaging(itUser, "", ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "USER";
            setPermission(controller);//Hàm này trong BaseController
            SetViewBag();
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.listUser = listUser;
            return View(itUser);
        }
        [HasCredential(RoleID = "INDEX_CONGKHAI_ISLOCK_USER")]
        public ActionResult TaiKhoanCongKhaiBiKhoa(int page = 1, int pageSize = 5)
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
            itUser.IsLock = true;
            var listUser = dao.ListAllPaging(itUser, "", ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "USER";
            setPermission(controller);//Hàm này trong BaseController
            SetViewBag();
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.listUser = listUser;
            return View(itUser);
        }

        [HttpPost]
        [HasCredential(RoleID = "INDEX_CONGKHAI_ISLOCK_USER")]
        public ActionResult TaiKhoanCongKhaiBiKhoa(User itUser, int page = 1, int pageSize = 5)
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
            itUser.IsLock = true;
            var listUser = dao.ListAllPaging(itUser, "", ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "USER";
            setPermission(controller);//Hàm này trong BaseController
            SetViewBag();
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.listUser = listUser;
            return View(itUser);
        }

        [HttpGet]
        [HasCredential(RoleID = "ADD_USER")]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        [HasCredential(RoleID = "EDIT_USER")]
        public ActionResult Edit(int id, string loaiTK="all")
        {
            var user = new UserDao().ViewDetail(id);
            user.loaiTK = loaiTK;
            SetViewBag();
            return View(user);
        }

        [HasCredential(RoleID = "VIEW_USER")]
        public ActionResult View(int id)
        {
            var user = new UserDao().ViewDetail(id);
            SetViewBag();
            return View(user);
        }

        [HttpPost]
        [HasCredential(RoleID = "ADD_USER")]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();

                var encryptedMd5Pas = Common.Encryptor.MD5Hash(user.Password);
                user.Password = encryptedMd5Pas;
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                if (session != null)
                {
                    user.CreatedBy = session.UserName;
                }
                long id = dao.Insert(user);
                if (id > 0)
                {
                    SetAlert("Thêm user thành công", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm user không thành công");
                }
            }
            SetViewBag();
            return View("Index");
        }
        [HttpPost]
        [HasCredential(RoleID = "EDIT_USER")]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if (!string.IsNullOrEmpty(user.Password))
                {
                    var encryptedMd5Pas = Common.Encryptor.MD5Hash(user.Password);
                    user.Password = encryptedMd5Pas;
                }
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                if (session != null)
                {
                    user.ModifiedBy = session.UserName;
                }
                var result = dao.Update(user);
                if (result)
                {
                    SetAlert("Sửa user thành công", "success");
                    Session[CommonConstants.MSG_SUCCESS] = "Sửa user thành công";
                    string rt = "Index";
                    if (user.loaiTK == "cb")
                    {
                        rt = "TaiKhoanCanBo";
                    }
                    else if (user.loaiTK == "ck")
                    {
                        rt = "TaiKhoanCongKhai";
                    }
                    else if (user.loaiTK == "ckbk")
                    {
                        rt = "TaiKhoanCongKhaiBiKhoa";
                    }
                    return RedirectToAction(rt, "User");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật user không thành công");
                }
            }
            SetViewBag();
            return View("Index");
        }
        [HttpDelete]
        [HasCredential(RoleID = "DELETE_USER")]
        public ActionResult Delete(int id)
        {
            new UserDao().Delete(id);

            return RedirectToAction("Index");
        }
        [HttpPost]
        [HasCredential(RoleID = "DELETEMULTI_USER")]
        public JsonResult DeleteMulti(string[] ids)
        {
            try
            {
                var result = false;
                foreach (var i in ids)
                {
                    int id = int.Parse(i.ToString());
                    result = new UserDao().Delete(id);
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
        [HttpPost]
        [HasCredential(RoleID = "EDIT_USER")]
        public JsonResult ChangeStatus(long id)
        {
            var result = new UserDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }

        public void SetViewBag(long? selectedId = null)
        {
            //var customerTypes = new[]
            //{
            //    new SelectListItem(){Value = "all", Text= "All"},
            //    new SelectListItem(){Value = "business", Text= "Business"},
            //    new SelectListItem(){Value = "private", Text= "Private"},
            //};
            //ViewBag.GroupID = customerTypes;
            var dao = new UnitDao();
            var daoUserGroup = new UserGroupDao();


            List<SelectListItem> listUserGroup = new List<SelectListItem>();
            List<SelectListItem> list = new List<SelectListItem>();
            List<Unit> lstUnit = dao.ListAll(null);
            List<UserGroup> lstVaiTro = daoUserGroup.ListAll(null);
            list.Add(new SelectListItem { Value = "0", Text = "--Chọn--" });
            foreach (Unit item in lstUnit)
            {
                list.Add(new SelectListItem { Value = item.Code + "", Text = item.Title });
            }
            foreach (UserGroup item in lstVaiTro)
            {
                listUserGroup.Add(new SelectListItem { Value = item.ID + "", Text = item.Name });
            }
            ViewBag.UnitCode = new SelectList(list, "Value", "Text", selectedId);
            ViewBag.GroupID = new SelectList(listUserGroup, "Value", "Text", selectedId);

            var daoPositionDict = new PositionDictDao();

            List<SelectListItem> lstPositionDict = new List<SelectListItem>();
            List<PositionDict> lstAllPositionDict = daoPositionDict.ListAll();
            foreach (PositionDict item in lstAllPositionDict)
            {
                lstPositionDict.Add(new SelectListItem { Value = item.Id + "", Text = item.Name });
            }
            ViewBag.ChucVu = new SelectList(lstPositionDict, "Value", "Text", selectedId);
        }

        [HttpGet]
        public JsonResult GetDetail(int id)
        {
            var dao = new UserDao();
            var entity = dao.ViewDetail(id);

            return Json(new
            {
                data = entity,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [HasCredential(RoleID = "EDIT_USER")]
        public JsonResult SaveProfile(string json)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            User obj = serializer.Deserialize<User>(json);
            var dao = new UserDao();
            bool status = false;
            string message = string.Empty;
            //update entity if <> 0
            var entity = dao.GetById(int.Parse(obj.ID.ToString()));
            entity.Name = obj.Name;
            entity.Email = obj.Email;
            entity.Phone = obj.Phone;
            entity.Avatar = obj.Avatar;
            try
            {
                dao.Update(entity);
                status = true;
            }
            catch (Exception ex)
            {

                status = false;
                message = ex.Message;
            }
            return Json(new
            {
                data = entity,
                status = status
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [HasCredential(RoleID = "EDIT_USER")]
        public JsonResult ChangePassword(string json)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            User obj = serializer.Deserialize<User>(json);
            var dao = new UserDao();
            bool status = false;
            string message = string.Empty;
            //update entity if <> 0
            var entity = dao.GetById(int.Parse(obj.ID.ToString()));
            if (!string.IsNullOrEmpty(obj.Password))
            {
                var encryptedMd5Pas = Common.Encryptor.MD5Hash(obj.Password);
                entity.Password = encryptedMd5Pas;
            }
            try
            {
                dao.Update(entity);
                status = true;
            }
            catch (Exception ex)
            {

                status = false;
                message = ex.Message;
            }
            return Json(new
            {
                data = entity,
                status = status
            }, JsonRequestBehavior.AllowGet);
        }
    }
}