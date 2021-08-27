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
using System.Xml.Linq;
using Model.ViewModel;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        [HasCredential(RoleID = "INDEX_PRODUCT")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new ProductDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            Product itProduct = new Product();
            var model = dao.ListAllPaging(itProduct, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "PRODUCT";
            setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListProduct = model;
            SetViewBag();
            return View(itProduct);
        }

        [HttpPost]
        [HasCredential(RoleID = "INDEX_PRODUCT")]
        public ActionResult Index(Product itProduct)
        {
            string searchString = "";
            int page = 1;
            int pageSize = 5;
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new ProductDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            var model = dao.ListAllPaging(itProduct, searchString, ref totalRecord, page, pageSize);


            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "PRODUCT";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListProduct = model;
            SetViewBag();
            return View(itProduct);
        }

        //Sản phẩm đang soạn
        [HasCredential(RoleID = "CHODUYET_PRODUCT")]
        public ActionResult ListChoDuyet(string searchString, int page = 1, int pageSize = 5)
        {
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new ProductDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            Product itProduct = new Product();
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];

            itProduct.StatusId = CommonConstants.SANPHAM_CHODUYET;
            var model = dao.ListAllPaging(itProduct, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "PRODUCT";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListProduct = model;
            SetViewBag();
            return View(itProduct);
        }

        [HttpPost]
        [HasCredential(RoleID = "CHODUYET_PRODUCT")]
        public ActionResult ListChoDuyet(Product itProduct)
        {
            string searchString = "";
            int page = 1;
            int pageSize = 5;
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new ProductDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            itProduct.StatusId = CommonConstants.SANPHAM_CHODUYET;
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];

            var model = dao.ListAllPaging(itProduct, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "PRODUCT";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListProduct = model;
            SetViewBag();
            return View(itProduct);
        }
        //End Sản phẩm đang soạn

        //Sản phẩm chờ duyệt
        [HasCredential(RoleID = "DUOCDUYET_PRODUCT")]
        public ActionResult ListDuocDuyet(string searchString, int StoreId = 0, int page = 1, int pageSize = 5)
        {
            string showLuanChuyen = "true";
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new ProductDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            Product itProduct = new Product();
            if (StoreId != 0)
            {
                itProduct.StoreId = StoreId;
            }
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];

            itProduct.StatusId = CommonConstants.SANPHAM_DUOCDUYET;
            var model = dao.ListAllPaging(itProduct, searchString, ref totalRecord, page, pageSize);
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
            string controller = "PRODUCT";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListProduct = model;
            ViewBag.showLuanChuyen = showLuanChuyen;
            SetViewBag();
            return View(itProduct);
        }
        [HttpPost]
        [HasCredential(RoleID = "DUOCDUYET_PRODUCT")]
        public ActionResult ListDuocDuyet(Product itProduct)
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
            var dao = new ProductDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            itProduct.StatusId = CommonConstants.SANPHAM_DUOCDUYET;
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session.GroupID != CommonConstants.PHEDUYET_GROUP)
            {
                showLuanChuyen = "false";
            }

            var model = dao.ListAllPaging(itProduct, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "PRODUCT";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListProduct = model;
            ViewBag.showLuanChuyen = showLuanChuyen;
            SetViewBag();
            return View(itProduct);
        }
        //End Sản phẩm chờ duyệt

        //Sản phẩm không được duyệt
        [HasCredential(RoleID = "KHONGDUYET_PRODUCT")]
        public ActionResult ListKhongDuyet(string searchString, int page = 1, int pageSize = 5)
        {
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new ProductDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            Product itProduct = new Product();
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];

            itProduct.StatusId = CommonConstants.SANPHAM_KHONGDUYET;
            var model = dao.ListAllPaging(itProduct, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "PRODUCT";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListProduct = model;
            SetViewBag();
            return View(itProduct);
        }
        [HttpPost]
        [HasCredential(RoleID = "KHONGDUYET_PRODUCT")]
        public ActionResult ListKhongDuyet(Product itProduct)
        {
            string searchString = "";
            int page = 1;
            int pageSize = 5;
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new ProductDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            itProduct.StatusId = CommonConstants.SANPHAM_KHONGDUYET;
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];

            var model = dao.ListAllPaging(itProduct, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "PRODUCT";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListProduct = model;
            SetViewBag();
            return View(itProduct);
        }
        //End Sản phẩm không được duyệt

        //Sản phẩm đã phê duyệt
        [HasCredential(RoleID = "CAPNHAT_PRODUCT")]
        public ActionResult ListCapNhat(string searchString, int page = 1, int pageSize = 5)
        {
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new ProductDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            Product itProduct = new Product();
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];

            itProduct.StatusId = CommonConstants.SANPHAM_CHODUYETCAPNHAT;
            var model = dao.ListAllPaging(itProduct, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "PRODUCT";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListProduct = model;
            SetViewBag();
            return View(itProduct);
        }
        [HttpPost]
        [HasCredential(RoleID = "CAPNHAT_PRODUCT")]
        public ActionResult ListCapNhat(Product itProduct)
        {
            string searchString = "";
            int page = 1;
            int pageSize = 5;
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new ProductDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            itProduct.StatusId = CommonConstants.SANPHAM_CHODUYETCAPNHAT;
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];

            var model = dao.ListAllPaging(itProduct, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "PRODUCT";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListProduct = model;
            SetViewBag();
            return View(itProduct);
        }
        //End Sản phẩm đã phê duyệt

        //Sản phẩm đã công khai
        [HasCredential(RoleID = "CHOBOSUNG_PRODUCT")]
        public ActionResult ListChoBoSung(string searchString, int page = 1, int pageSize = 5)
        {
            string showLuanChuyen = "true";
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new ProductDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            Product itProduct = new Product();
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session.GroupID != CommonConstants.QLCONGKHAI_GROUP) // tài khoản công khai thì không lấy đơn vị
            {
                showLuanChuyen = "false";

            }
            itProduct.StatusId = CommonConstants.SANPHAM_CHOBOSUNG7;
            var model = dao.ListAllPaging(itProduct, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }

            string controller = "PRODUCT";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListProduct = model;
            ViewBag.showLuanChuyen = showLuanChuyen;
            SetViewBag();
            return View(itProduct);
        }
        [HttpPost]
        [HasCredential(RoleID = "CHOBOSUNG_PRODUCT")]
        public ActionResult ListChoBoSung(Product itProduct)
        {
            string searchString = "";
            int page = 1;
            int pageSize = 5;
            HttpCookie cookiePageSize = Request.Cookies["pageSize"];
            if (cookiePageSize != null)
            {
                pageSize = int.Parse(cookiePageSize.Value.ToString());
            }
            var dao = new ProductDao();
            int totalRecord = 0;
            int fromRecord = 0;
            int toRecord = 0;
            itProduct.StatusId = CommonConstants.SANPHAM_CHOBOSUNG7;
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];

            if (session.GroupID != CommonConstants.QLCONGKHAI_GROUP) // tài khoản công khai thì không lấy đơn vị
            {

            }
            var model = dao.ListAllPaging(itProduct, searchString, ref totalRecord, page, pageSize);

            fromRecord = ((page - 1) * pageSize) + 1;
            toRecord = ((page - 1) * pageSize) + pageSize;
            if (toRecord > totalRecord)
            {
                toRecord = totalRecord;
            }
            string controller = "PRODUCT";
            //setPermission(controller);//Hàm này trong BaseController
            ViewBag.SearchString = searchString;
            ViewBag.Total = totalRecord;
            ViewBag.fromRecord = fromRecord;
            ViewBag.toRecord = toRecord;
            ViewBag.controler = controller;
            ViewBag.ListProduct = model;
            SetViewBag();
            return View(itProduct);
        }
        //End Sản phẩm đã công khai   

        [HttpGet]
        [HasCredential(RoleID = "ADD_PRODUCT")]
        public ActionResult Create()
        {
            Product item = new Product();
            item.hidIndexFile = "0";
            SetViewBag();
            return View(item);
        }

        [HttpGet]
        [HasCredential(RoleID = "EDIT_PRODUCT")]
        public ActionResult Edit(long id)
        {
            var dao = new ProductDao();
            var Product = dao.GetByID(id);

            if (Product.MoreImages != "" && Product.MoreImages != "0" && Product.MoreImages != null)
            {
                dynamic jsonFile = JsonConvert.DeserializeObject(Product.MoreImages);
                int i = 0;
                foreach (var itjon in jsonFile)
                {
                    string Comment = itjon.filename;
                    string Url = itjon.urldownload;
                    Product.hidListFile += "<tr id='" + i + "'><td><p style='margin: 0px !important;'><input type='hidden' id='link" + i + "' value='" + Url + "'><img src='" + CommonConstants.DomainName + Url + "' width='70' height='70'></p></td><td><textarea id='TextArea1' cols='40' rows='3'>" + Comment + "</textarea></td><td><a onclick='RemoveRow(" + i + ");' class='btn btn-danger btn-xs'><i class='fa fa-trash-o'></i></a></td></tr>";
                    i += 1;
                }
                Product.hidIndexFile = i + "";
            }
            SetViewBag(Product);

            return View(Product);
        }

        [HttpPost]
        [ValidateInput(false)]
        [HasCredential(RoleID = "EDIT_PRODUCT")]
        public ActionResult Edit(Product model)
        {
            string rec;
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (ModelState.IsValid)
            {
                try
                {

                    model.ModifiedBy = session.UserName;
                    model.ModifiedDate = DateTime.Now;
                    model.Image = model.Image.Replace(CommonConstants.ApplicationName, "");

                    if (model.hidFileLinkDown != "0" && model.hidFileLinkDown != "" && model.hidFileLinkDown != null)
                    {
                        string[] arrFileLink = model.hidFileLinkDown.Split(new string[] { "|" }, StringSplitOptions.None);
                        string[] arrFileNote = model.hidFileNote.Split(new string[] { "|" }, StringSplitOptions.None);
                        string jsonTapTinDinhKem = "";
                        for (int i = 0; i <= arrFileLink.Length - 2; i++)
                        {
                            jsonTapTinDinhKem += "{'filename':'" + arrFileNote[i] + "','urldownload':'" + arrFileLink[i].Replace(CommonConstants.ApplicationName, "") + "'}" + ",";
                        }
                        model.MoreImages = "[" + jsonTapTinDinhKem.Remove(jsonTapTinDinhKem.Length - 1) + "]";// bỏ dấu , cuối cùng
                    }

                    new ProductDao().Update(model);
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
                    if (model.StatusId == CommonConstants.SANPHAM_DUOCDUYET)
                    {
                        rec = "ListDuocDuyet";
                    }
                    else if (model.StatusId == CommonConstants.SANPHAM_CHOBOSUNG7)
                    {
                        rec = "ListChoBoSung";
                    }
                    else if (model.StatusId == CommonConstants.SANPHAM_CHODUYET)
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
        [HasCredential(RoleID = "ADD_PRODUCT")]
        public ActionResult Create(Product model)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                model.CreatedBy = session.UserName;
                model.Image = model.Image.Replace(CommonConstants.ApplicationName, "");

                if (model.hidFileLinkDown != "0" && model.hidFileLinkDown != "" && model.hidFileLinkDown != null)
                {
                    string[] arrFileLink = model.hidFileLinkDown.Split(new string[] { "|" }, StringSplitOptions.None);
                    string[] arrFileNote = model.hidFileNote.Split(new string[] { "|" }, StringSplitOptions.None);
                    string jsonTapTinDinhKem = "";
                    for (int i = 0; i <= arrFileLink.Length - 2; i++)
                    {
                        jsonTapTinDinhKem += "{'filename':'" + arrFileNote[i] + "','urldownload':'" + arrFileLink[i].Replace(CommonConstants.ApplicationName, "") + "'}" + ",";
                    }
                    model.MoreImages = "[" + jsonTapTinDinhKem.Remove(jsonTapTinDinhKem.Length - 1) + "]";// bỏ dấu , cuối cùng
                }
                new ProductDao().Create(model);
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
        [HasCredential(RoleID = "DELETEMULTI_PRODUCT")]
        public JsonResult DeleteMulti(string[] ids)
        {
            try
            {
                var result = false;
                foreach (var i in ids)
                {
                    int id = int.Parse(i.ToString());
                    result = new ProductDao().Delete(id);
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
        public JsonResult ChangeStatus(long id)
        {
            var result = new ProductDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }

        [HttpPost]
        public JsonResult ChangeIsHot(long id)
        {
            var result = new ProductDao().ChangeIsHot(id);
            return Json(new
            {
                status = result
            });
        }

        [HttpPost]
        public JsonResult ChangeIsPromotion(long id)
        {
            var result = new ProductDao().ChangeIsPromotion(id);
            return Json(new
            {
                status = result
            });
        }

        [HttpPost]
        public JsonResult ChangeIsMain(long id)
        {
            var result = new ProductDao().ChangeIsMain(id);
            return Json(new
            {
                status = result
            });
        }

        [HttpPost]
        public JsonResult ChangeGoiYMuaSam(long id)
        {
            var result = new ProductDao().ChangeGoiYMuaSam(id);
            return Json(new
            {
                status = result
            });
        }

        [HttpPost]
        public JsonResult ChangeIsDiscount(long id)
        {
            var result = new ProductDao().ChangeIsDiscount(id);
            return Json(new
            {
                status = result
            });
        }

        [HttpPost]
        public JsonResult ChangeIsTrending(long id)
        {
            var result = new ProductDao().ChangeIsTrending(id);
            return Json(new
            {
                status = result
            });
        }
        [HttpGet]
        public ActionResult Action(long id)
        {
            var dao = new ProductDao();
            var daoLogAction = new LogActionDao();
            var daoSysAction = new SysActionDao();
            LogAction itLogAction = new LogAction();
            SysAction itSysAction = new SysAction();
            var Product = dao.GetByID(id);
            itSysAction.CurrentStatus = Product.StatusId;
            itSysAction.IdSysProcedure = CommonConstants.QUYTRINH_SANPHAM;
            List<SelectListItem> listThaoTac = new List<SelectListItem>();
            List<SysAction> lstSysAction = daoSysAction.ListAll(itSysAction);
            foreach (SysAction item in lstSysAction)
            {
                listThaoTac.Add(new SelectListItem { Value = item.Id + "", Text = item.Title });
            }

            itLogAction.ObjId = Convert.ToInt32(Product.ID);
            itLogAction.SysProcedureId = CommonConstants.QUYTRINH_SANPHAM;
            List<LogAction> lstLogAction = daoLogAction.ListAll(itLogAction);

            ViewBag.SysActionId = new SelectList(listThaoTac, "Value", "Text", null);
            ViewBag.itemProduct = Product;
            ViewBag.lstLogAction = lstLogAction;
            // SetViewBag(Product.CategoryID);
            return View(Product);
        }

        [HttpPost]
        public ActionResult Action(Product model)
        {
            int? curStatusId = 0;
            if (ModelState.IsValid)
            {
                try
                {
                    var dao = new ProductDao();
                    var daoSysAction = new SysActionDao();
                    var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                    model.ModifiedBy = session.UserName;
                    model.ModifiedDate = DateTime.Now;
                    Product item = dao.GetByID(model.ID);
                    curStatusId = item.StatusId;
                    model.Name = item.Name;
                    model.Code = item.Code;
                    model.Description = item.Description;
                    model.Image = item.Image;
                    model.MoreImages = item.MoreImages;
                    model.Price = item.Price;
                    model.PromotionPrice = item.PromotionPrice;
                    model.Quantity = item.Quantity;
                    model.CategoryID = item.CategoryID;
                    model.UnitId = item.UnitId;
                    model.ProductCategoryParentId = item.ProductCategoryParentId;
                    model.ProductCategoryId = item.ProductCategoryId;
                    model.Detail = item.Detail;
                    model.Status = item.Status;
                    model.StoreId = item.StoreId;
                    model.IsHot = item.IsHot;
                    model.IsPromotion = item.IsPromotion;
                    model.IsDiscount = item.IsDiscount;
                    model.IsTrending = item.IsTrending;
                    model.IsMain = item.IsMain;
                    model.GoiYMuaSam = item.GoiYMuaSam;
                    model.TinhTrangHang = item.TinhTrangHang;
                    model.StatusId = item.StatusId;
                    //Lấy trạng thái tiếp theo
                    var itSysAction = daoSysAction.GetById(model.SysActionId);
                    model.StatusId = itSysAction.NextStatus;
                    //End lấy trạng thái theo

                    new ProductDao().Update(model);

                    //Save logAction
                    var daoLogAction = new LogActionDao();
                    LogAction itLogAction = new LogAction();
                    itLogAction.Created = DateTime.Now;
                    itLogAction.ActionId = model.SysActionId;
                    itLogAction.SysProcedureId = CommonConstants.QUYTRINH_SANPHAM;
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
        public JsonResult LoadImages(long id)
        {
            ProductDao dao = new ProductDao();
            var product = dao.ViewDetail(id);
            var images = product.MoreImages;
            XElement xImages = XElement.Parse(images);
            List<string> listImagesReturn = new List<string>();

            foreach (XElement element in xImages.Elements())
            {
                listImagesReturn.Add(element.Value);
            }
            return Json(new
            {
                data = listImagesReturn
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveImages(long id, string images)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var listImages = serializer.Deserialize<List<string>>(images);

            XElement xElement = new XElement("Images");

            foreach (var item in listImages)
            {
                var subStringItem = item.Substring(21);
                xElement.Add(new XElement("Image", subStringItem));
            }
            ProductDao dao = new ProductDao();
            try
            {
                dao.UpdateImages(id, xElement.ToString());
                return Json(new
                {
                    status = true
                });
            }
            catch (Exception)
            {
                return Json(new
                {
                    status = false
                });
            }

        }
        public void SetViewBag(Product entity = null)
        {
            var dao = new CategoryDao();
            var daoStore = new StoreDao();
            Store itStore = new Store();
            List<SelectListItem> listStore = new List<SelectListItem>();
            List<Store> lstStore = daoStore.ListAll(itStore);
            listStore.Add(new SelectListItem { Value = "0", Text = "--Chọn--" });
            foreach (Store item in lstStore)
            {
                listStore.Add(new SelectListItem { Value = item.StoreId + "", Text = item.Title });
            }
            ViewBag.StoreId = new SelectList(listStore, "Value", "Text", 0);

            List<SelectListItem> listCategoryID = new List<SelectListItem>();
            listCategoryID.Add(new SelectListItem { Value = "0", Text = "--Chọn--" });
            foreach (var item in dao.ListAll())
            {
                listCategoryID.Add(new SelectListItem { Value = item.ID.ToString() + "", Text = item.Name });
            }
            string selectCategoryID = "0";
            if (entity != null)
            {
                selectCategoryID = entity.CategoryID.ToString();
            }
            ViewBag.CategoryID = new SelectList(listCategoryID, "Value", "Text", selectCategoryID);

            List<CategoryUnitViewModel> CategoryUnitViewModels = new List<CategoryUnitViewModel>();
            List<SelectListItem> listUnitId = new List<SelectListItem>();
            listUnitId.Add(new SelectListItem { Value = "0", Text = "--Chọn--" });
            string selectUnitId = "0";
            if (entity != null)
            {
                selectUnitId = entity.UnitId.ToString();
                CategoryUnitViewModels = new CategoryUnitDao().ListByCategoryIdViewModel((long)entity.CategoryID);
                foreach (var item in CategoryUnitViewModels)
                {
                    listUnitId.Add(new SelectListItem { Value = item.UnitId + "", Text = item.UnitTitle });
                }
            }
            ViewBag.UnitId = new SelectList(listUnitId, "Value", "Text", selectUnitId);

            List<ProductCategoryUnitViewModel> ProductCategoryUnitViewModels = new List<ProductCategoryUnitViewModel>();
            List<SelectListItem> listProductCategoryParentId = new List<SelectListItem>();
            listProductCategoryParentId.Add(new SelectListItem { Value = "0", Text = "--Chọn--" });
            string selectProductCategoryParentId = "0";
            if (entity != null)
            {
                selectProductCategoryParentId = entity.ProductCategoryParentId.ToString();
                ProductCategoryUnitViewModels = new ProductCategoryUnitDao().ListByUnitIdCategoryIdViewModel(entity.UnitId,(long)entity.CategoryID);
                foreach (var item in ProductCategoryUnitViewModels)
                {
                    listProductCategoryParentId.Add(new SelectListItem { Value = item.ProductCategoryId + "", Text = item.ProductCategoryName });
                }
            }
            ViewBag.ProductCategoryParentId = new SelectList(listProductCategoryParentId, "Value", "Text", selectProductCategoryParentId);

            List<ProductCategory> ProductCategorys = new List<ProductCategory>();
            List<SelectListItem> listProductCategoryId = new List<SelectListItem>();
            listProductCategoryId.Add(new SelectListItem { Value = "0", Text = "--Chọn--" });
            string selectProductCategoryId = "0";
            if (entity != null)
            {
                selectProductCategoryId = entity.ProductCategoryId.ToString();
                ProductCategorys = new ProductCategoryDao().ListByParentID(entity.ProductCategoryParentId);
                foreach (var item in ProductCategorys)
                {
                    listProductCategoryId.Add(new SelectListItem { Value = item.Id + "", Text = item.Name });
                }
            }
            ViewBag.ProductCategoryId = new SelectList(listProductCategoryId, "Value", "Text", selectProductCategoryId);

        }
    }
}