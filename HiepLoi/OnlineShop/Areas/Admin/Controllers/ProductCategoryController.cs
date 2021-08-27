using Model.Dao;
using Model.EF;
using ClsCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ProductCategoryController : BaseController
    {
        // GET: Admin/Category
        [HasCredential(RoleID = "INDEX_PRODUCTCATEGORY")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult LoadData(string json, int page = 1, int pageSize = 10)
        {
            var dao = new ProductCategoryDao();

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            ProductCategory productCategory = serializer.Deserialize<ProductCategory>(json);
            int totalRecord = 0;
            int ParentId = 0;
            var model = dao.ListAllPaging(productCategory, ref ParentId, ref totalRecord, page, pageSize);

            return Json(new
            {
                data = model,
                total = totalRecord,
                parentid = ParentId,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [HasCredential(RoleID = "SAVE_PRODUCTCATEGORY")]
        public JsonResult SaveData(string json)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            ProductCategory productCategory = serializer.Deserialize<ProductCategory>(json);
            var dao = new ProductCategoryDao();
            bool status = false;
            string message = string.Empty;
            if (productCategory.Id == 0)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                productCategory.CreatedBy = session.UserName;
                productCategory.CreatedDate = DateTime.Now;
                if (!string.IsNullOrEmpty(productCategory.Name))
                {
                    productCategory.MetaTitle = StringHelper.ToUnsignString(productCategory.Name);
                }
                productCategory.Language = "VN";
                try
                {
                    dao.Insert(productCategory);
                    status = true;
                }
                catch (Exception ex)
                {

                    status = false;
                    message = ex.Message;
                }
            }
            else
            {
                var entity = dao.GetById(productCategory.Id);
                entity.Icons = productCategory.Icons;
                entity.Name = productCategory.Name;
                entity.ParentID = productCategory.ParentID;
                entity.DisplayOrder = productCategory.DisplayOrder;
                entity.Status = productCategory.Status;
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                entity.ModifiedBy = session.UserName;
                entity.ModifiedDate = DateTime.Now;
                if (!string.IsNullOrEmpty(productCategory.Name))
                {
                    entity.MetaTitle = StringHelper.ToUnsignString(productCategory.Name);
                }
                entity.ShowOnHome = productCategory.ShowOnHome;
                entity.Language = "VN";
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

            }
            return Json(new
            {
                status = status,
                message = message
            });
        }
        [HttpPost]
        public JsonResult loadSelectParentId(int ParentId)
        {
            var dao = new ProductCategoryDao();
            if (ParentId != 0)
            {
                try
                {
                    ParentId = (int)dao.GetById(ParentId).ParentID;
                }
                catch (Exception)
                {

                }
            }
            var entity = dao.GetByParentId(ParentId);

            return Json(new
            {
                data = entity,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult loadSelectCategoryUnit(int Id)
        {
            var emtity = new CategoryUnitDao().ListAllViewModel();

            return Json(new
            {
                data = emtity,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetDetail(int id)
        {
            var dao = new ProductCategoryDao();
            var entity = dao.ViewDetail((long)id);
            return Json(new
            {
                data = entity,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [HasCredential(RoleID = "DELETE_PRODUCTCATEGORY")]
        public JsonResult Delete(int id)
        {
            try
            {
                var result = new ProductCategoryDao().Delete(id);
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
        [HasCredential(RoleID = "DELETEMULTI_PRODUCTCATEGORY")]
        public JsonResult DeleteMulti(string[] ids)
        {
            try
            {
                var result = false;
                foreach (var i in ids)
                {
                    int id = int.Parse(i.ToString());
                    result = new ProductCategoryDao().Delete(id);
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
        [HasCredential(RoleID = "EDIT_PRODUCTCATEGORY")]
        public JsonResult ChangeStatus(int id)
        {
            var result = new ProductCategoryDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
        public JsonResult ChangeShowOnHome(int id)
        {
            var result = new ProductCategoryDao().ChangeShowOnHome(id);
            return Json(new
            {
                status = result
            });
        }
        [HttpPost]
        public JsonResult SaveProductCategoryUnit(string json)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            ProductCategoryUnit productCategoryUnit = serializer.Deserialize<ProductCategoryUnit>(json);
            var dao = new ProductCategoryUnitDao();
            bool status = false;
            string message = string.Empty;

            var model = dao.ListByCategoryUnitProductCategory(productCategoryUnit.CategoryId, productCategoryUnit.UnitId, productCategoryUnit.ProductCategoryId);
            if (model == null)
            {
                try
                {
                    ProductCategoryUnit entity = new ProductCategoryUnit();
                    entity.CategoryId = productCategoryUnit.CategoryId;
                    entity.UnitId = productCategoryUnit.UnitId;
                    entity.ProductCategoryId = productCategoryUnit.ProductCategoryId;
                    dao.Insert(entity);
                    status = true;
                }
                catch (Exception ex)
                {

                    status = false;
                    message = ex.Message;
                }
            }
            else
            {
                try
                {
                    model.CategoryId = productCategoryUnit.CategoryId;
                    model.UnitId = productCategoryUnit.UnitId;
                    model.ProductCategoryId = productCategoryUnit.ProductCategoryId;
                    dao.Update(model);
                    status = true;
                }
                catch (Exception ex)
                {

                    status = false;
                    message = ex.Message;
                }
            }
            return Json(new
            {
                status = status,
                message = message
            });
        }

        [HttpGet]
        public JsonResult GetDetailCategoryUnit(int id)
        {
            var dao = new ProductCategoryUnitDao();
            var entity = dao.ListByProductCategoryIdViewModel(id);
            var TitleProductCategory = new ProductCategoryDao().ViewDetail((long)id).Name;
            return Json(new
            {
                data = entity,
                TitleProductCategory = TitleProductCategory,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult deleteProductCategoryUnit(int ProductCategoryUnitId)
        {
            try
            {
                var result = new ProductCategoryUnitDao().Delete((long)ProductCategoryUnitId);
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
        public JsonResult saveDataMultiRole(string strSysFunction)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            SysFunction sysFunction = serializer.Deserialize<SysFunction>(strSysFunction);
            bool status = false;
            string message = string.Empty;
            //add new entity if id  = 0
            if (sysFunction.FunctionID != 0 && sysFunction.RoleID != null)
            {
                var roleDao = new RoleDao();
                var modelRole = roleDao.GetBySysFunction(sysFunction.FunctionID).Where(x => x.ID == sysFunction.RoleID);

                string[] lstRole = { "INDEX", "ADD", "SAVE", "EDIT", "DELETE", "DELETEMULTI" };
                foreach (var item in lstRole)
                {
                    if (sysFunction.isController != null)
                    {
                        string roleid = item + "_" + sysFunction.isController.ToUpper();
                        int functionid = sysFunction.FunctionID;
                        var checkmodel = roleDao.GetById(roleid);
                        if (checkmodel != null)
                        {
                            if (checkmodel.SysFunctionID != functionid)
                            {
                                try
                                {
                                    Role role = new Role();
                                    role.ID = roleid;
                                    role.Name = "";
                                    role.SysFunctionID = functionid;
                                    roleDao.Insert(role);
                                    status = true;
                                }
                                catch (Exception ex)
                                {

                                    status = false;
                                    message = ex.Message;
                                }
                            }

                        }
                        else
                        {
                            try
                            {
                                Role role = new Role();
                                role.ID = roleid;
                                role.Name = "";
                                role.SysFunctionID = functionid;
                                roleDao.Insert(role);
                                status = true;
                            }
                            catch (Exception ex)
                            {

                                status = false;
                                message = ex.Message;
                            }
                        }
                    }
                }
            }
            return Json(new
            {
                status = status,
                message = message
            });
        }
        public string getCountMenu(string Link, string isController, int ParentId)
        {
            string count = "";
            //Tin Tức
            string linkTinTucSoanThao = "/Admin/Content/ListSoanThao";
            string linkTinTucChoDuyet = "/Admin/Content/ListChoDuyet";
            string linkTinTucChoCongKhai = "/Admin/Content/ListChoCongKhai";
            string linkTinTucKhongDuyet = "/Admin/Content/ListKhongDuyet";
            string linkTinTucDaPheDuyet = "/Admin/Content/ListDaPheDuyet";
            string linkTinTucCongKhai = "/Admin/Content/ListCongKhai";
            string linkTinTucBoSung = "/Admin/Content/ListBoSung";
            string linkTinTucThuHoi = "/Admin/Content/ListThuHoi";

            //Bài giới thiệu
            string linkGioiThieu = "/Admin/Content/ListGioiThieu";

            //Chính sách ưu đãi
            //string linkChinhSachUuDaiSoanThao = "/Admin/ChinhSachUuDai/ListSoanThao";
            //string linkChinhSachUuDaiChoDuyet = "/Admin/ChinhSachUuDai/ListChoDuyet";
            //string linkChinhSachUuDaiChoCongKhai = "/Admin/ChinhSachUuDai/ListChoCongKhai";
            //string linkChinhSachUuDaiKhongDuyet = "/Admin/ChinhSachUuDai/ListKhongDuyet";
            //string linkChinhSachUuDaiDaPheDuyet = "/Admin/ChinhSachUuDai/ListDaPheDuyet";
            //string linkChinhSachUuDaiCongKhai = "/Admin/ChinhSachUuDai/ListCongKhai";
            //string linkChinhSachUuDaiBoSung = "/Admin/ChinhSachUuDai/ListBoSung";
            //string linkChinhSachUuDaiThuHoi = "/Admin/ChinhSachUuDai/ListThuHoi";

            // Gian hàng
            string linkGianHangChoDuyet = "/Admin/Store/ListChoDuyet";
            string linkGianHangDuocDuyet = "/Admin/Store/ListDuocDuyet";
            string linkGianHangKhongDuyet = "/Admin/Store/ListKhongDuyet";
            string linkGianHangCapNhat = "/Admin/Store/ListCapNhat";

            // Xác minh tài khoản
            string linkTaiKhoanXacMinh = "/Admin/XacMinhUser/Index";
            string linkTaiKhoanDuyet = "/Admin/XacMinhUser/Duyet";
            string linkTaiKhoanKhongDuyet = "/Admin/XacMinhUser/KhongDuyet";

            // Sản phẩm
            string linkSanPhamChoDuyet = "/Admin/Product/ListChoDuyet";
            string linkSanPhamDuocDuyet = "/Admin/Product/ListDuocDuyet";
            string linkSanPhamKhongDuyet = "/Admin/Product/ListKhongDuyet";
            string linkSanPhamCapNhat = "/Admin/Product/ListCapNhat";

            // Liên hệ người dùng
            string linkLienHeNguoiDungChoTiepNhan = "/Admin/Contact/ListChoTiepNhan";
            string linkLienHeNguoiDungDangXuLy = "/Admin/Contact/ListDangXuLy";
            string linkLienHeNguoiDungKhongDuyet = "/Admin/Contact/ListKhongDuyet";
            string linkLienHeNguoiDungDuocDuyet = "/Admin/Contact/ListDuocDuyet";

            // Liên hệ gian hàng
            string linkLienHeGianHangChoTiepNhan = "/Admin/ContactStore/ListChoTiepNhan";
            string linkLienHeGianHangDangXuLy = "/Admin/ContactStore/ListDangXuLy";
            string linkLienHeGianHangKhongDuyet = "/Admin/ContactStore/ListKhongDuyet";
            string linkLienHeGianHangDuocDuyet = "/Admin/ContactStore/ListDuocDuyet";

            // Đơn hàng
            string linkDonHangChoXuLy = "/Admin/Order/ListChoXuLy";
            string linkDonHangDaXuLy = "/Admin/Order/ListDaXuLy";

            if (isController == "Content")
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                if (ParentId != 0)// link con
                {
                    var dao = new ContentDao();
                    Model.EF.Content itContent = new Model.EF.Content();
                    if (Link == linkTinTucSoanThao)
                    {
                        itContent.DonViID = session.UnitCode;
                        itContent.StatusId = CommonConstants.TINTTUC_DANGSOAN;
                        count = dao.ListAll(itContent).Count().ToString();
                    }
                    if (Link == linkTinTucChoDuyet)
                    {
                        itContent.DonViID = session.UnitCode;
                        itContent.StatusId = CommonConstants.TINTTUC_TRINHDUYET;
                        count = dao.ListAll(itContent).Count().ToString();
                    }
                    if (Link == linkTinTucChoCongKhai)
                    {
                        itContent.StatusId = CommonConstants.TINTTUC_XUATBAN;
                        count = dao.ListAll(itContent).Count().ToString();
                    }
                    if (Link == linkTinTucKhongDuyet)
                    {
                        itContent.DonViID = session.UnitCode;
                        itContent.StatusId = CommonConstants.TINTTUC_KHONGDUYET;
                        count = dao.ListAll(itContent).Count().ToString();
                    }
                    if (Link == linkTinTucDaPheDuyet)
                    {
                        itContent.DonViID = session.UnitCode;
                        itContent.StatusId = CommonConstants.TINTTUC_XUATBAN;
                        count = dao.ListAll(itContent).Count().ToString();
                    }
                    if (Link == linkTinTucCongKhai)
                    {
                        if (session.GroupID != CommonConstants.QLCONGKHAI_GROUP)
                        {
                            itContent.DonViID = session.UnitCode;
                        }
                        itContent.StatusId = CommonConstants.TINTTUC_CONGKHAI;
                        count = dao.ListAll(itContent).Count().ToString();
                    }
                    if (Link == linkTinTucBoSung)
                    {
                        itContent.DonViID = session.UnitCode;
                        itContent.StatusId = CommonConstants.TINTTUC_TRAVE;
                        count = dao.ListAll(itContent).Count().ToString();
                    }
                    if (Link == linkTinTucThuHoi)
                    {
                        itContent.StatusId = CommonConstants.TINTTUC_THUHOI;
                        count = dao.ListAll(itContent).Count().ToString();
                    }
                }
                else
                { // link cha
                    var dao = new ContentDao();
                    Model.EF.Content itContent = new Model.EF.Content();
                    if (session.GroupID == CommonConstants.BIENTAP_GROUP)
                    {
                        if (Link == linkGioiThieu)
                        {
                            itContent.DonViID = session.UnitCode;
                            itContent.CategoryID = 5;
                            itContent.StatusId = 77;
                            count = dao.ListAll(itContent).Count().ToString();
                        }
                        else
                        {
                            itContent.DonViID = session.UnitCode;
                            itContent.ListStatusId = new List<int>();
                            itContent.ListStatusId.Add(CommonConstants.TINTTUC_DANGSOAN);
                            itContent.ListStatusId.Add(CommonConstants.TINTTUC_TRINHDUYET);
                            itContent.ListStatusId.Add(CommonConstants.TINTTUC_KHONGDUYET);
                            itContent.ListStatusId.Add(CommonConstants.TINTTUC_CONGKHAI);
                            itContent.ListStatusId.Add(CommonConstants.TINTTUC_XUATBAN);
                            itContent.ListStatusId.Add(CommonConstants.TINTTUC_TRAVE);
                            count = dao.ListAll(itContent).Count().ToString();
                        }
                    }
                    if (session.GroupID == CommonConstants.PHEDUYET_GROUP)
                    {
                        itContent.DonViID = session.UnitCode;
                        itContent.ListStatusId = new List<int>();
                        itContent.ListStatusId.Add(CommonConstants.TINTTUC_TRINHDUYET);
                        itContent.ListStatusId.Add(CommonConstants.TINTTUC_KHONGDUYET);
                        itContent.ListStatusId.Add(CommonConstants.TINTTUC_XUATBAN);
                        itContent.ListStatusId.Add(CommonConstants.TINTTUC_CONGKHAI);
                        count = dao.ListAll(itContent).Count().ToString();
                    }
                    if (session.GroupID == CommonConstants.QLCONGKHAI_GROUP)
                    {
                        itContent.ListStatusId = new List<int>();
                        itContent.ListStatusId.Add(CommonConstants.TINTTUC_XUATBAN);
                        itContent.ListStatusId.Add(CommonConstants.TINTTUC_CONGKHAI);
                        itContent.ListStatusId.Add(CommonConstants.TINTTUC_THUHOI);
                        count = dao.ListAll(itContent).Count().ToString();
                    }
                }
            }
            else if (isController == "ChinhSachUuDai") // Chính sách ưu đãi
            {
                //var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                //if (ParentId != 0) // link con
                //{
                //    var dao = new ChinhSachUuDaiDao();
                //    Model.EF.ChinhSachUuDai itChinhSachUuDai = new Model.EF.ChinhSachUuDai();
                //    if (Link == linkChinhSachUuDaiSoanThao)
                //    {
                //        itChinhSachUuDai.DonViID = session.UnitCode;
                //        itChinhSachUuDai.StatusId = CommonConstants.CHINHSACHUUDAI_DANGSOAN;
                //        count = dao.ListAll(itChinhSachUuDai).Count().ToString();
                //    }
                //    if (Link == linkChinhSachUuDaiChoDuyet)
                //    {
                //        itChinhSachUuDai.DonViID = session.UnitCode;
                //        itChinhSachUuDai.StatusId = CommonConstants.CHINHSACHUUDAI_TRINHDUYET;
                //        count = dao.ListAll(itChinhSachUuDai).Count().ToString();
                //    }
                //    if (Link == linkChinhSachUuDaiChoCongKhai)
                //    {
                //        itChinhSachUuDai.StatusId = CommonConstants.CHINHSACHUUDAI_XUATBAN;
                //        count = dao.ListAll(itChinhSachUuDai).Count().ToString();
                //    }
                //    if (Link == linkChinhSachUuDaiKhongDuyet)
                //    {
                //        itChinhSachUuDai.DonViID = session.UnitCode;
                //        itChinhSachUuDai.StatusId = CommonConstants.CHINHSACHUUDAI_KHONGDUYET;
                //        count = dao.ListAll(itChinhSachUuDai).Count().ToString();
                //    }
                //    if (Link == linkChinhSachUuDaiDaPheDuyet)
                //    {
                //        itChinhSachUuDai.DonViID = session.UnitCode;
                //        itChinhSachUuDai.StatusId = CommonConstants.CHINHSACHUUDAI_XUATBAN;
                //        count = dao.ListAll(itChinhSachUuDai).Count().ToString();
                //    }
                //    if (Link == linkChinhSachUuDaiCongKhai)
                //    {
                //        if (session.GroupID != CommonConstants.QLCONGKHAI_GROUP)
                //        {
                //            itChinhSachUuDai.DonViID = session.UnitCode;
                //        }
                //        itChinhSachUuDai.StatusId = CommonConstants.CHINHSACHUUDAI_CONGKHAI;
                //        count = dao.ListAll(itChinhSachUuDai).Count().ToString();
                //    }
                //    if (Link == linkChinhSachUuDaiBoSung)
                //    {
                //        itChinhSachUuDai.DonViID = session.UnitCode;
                //        itChinhSachUuDai.StatusId = CommonConstants.CHINHSACHUUDAI_TRAVE;
                //        count = dao.ListAll(itChinhSachUuDai).Count().ToString();
                //    }
                //    if (Link == linkChinhSachUuDaiThuHoi)
                //    {
                //        itChinhSachUuDai.StatusId = CommonConstants.CHINHSACHUUDAI_THUHOI;
                //        count = dao.ListAll(itChinhSachUuDai).Count().ToString();
                //    }
                //}
                //else // link cha
                //{
                //    var dao = new ChinhSachUuDaiDao();
                //    Model.EF.ChinhSachUuDai itChinhSachUuDai = new Model.EF.ChinhSachUuDai();
                //    if (session.GroupID == CommonConstants.BIENTAP_GROUP)
                //    {
                //        itChinhSachUuDai.DonViID = session.UnitCode;
                //        count = dao.ListAll(itChinhSachUuDai).Count().ToString();
                //    }
                //    if (session.GroupID == CommonConstants.PHEDUYET_GROUP)
                //    {
                //        itChinhSachUuDai.DonViID = session.UnitCode;
                //        itChinhSachUuDai.ListStatusId = new List<int>();
                //        itChinhSachUuDai.ListStatusId.Add(CommonConstants.CHINHSACHUUDAI_TRINHDUYET);
                //        itChinhSachUuDai.ListStatusId.Add(CommonConstants.CHINHSACHUUDAI_KHONGDUYET);
                //        itChinhSachUuDai.ListStatusId.Add(CommonConstants.CHINHSACHUUDAI_XUATBAN);
                //        itChinhSachUuDai.ListStatusId.Add(CommonConstants.CHINHSACHUUDAI_CONGKHAI);
                //        count = dao.ListAll(itChinhSachUuDai).Count().ToString();
                //    }
                //    if (session.GroupID == CommonConstants.QLCONGKHAI_GROUP)
                //    {
                //        itChinhSachUuDai.ListStatusId = new List<int>();
                //        itChinhSachUuDai.ListStatusId.Add(CommonConstants.CHINHSACHUUDAI_XUATBAN);
                //        itChinhSachUuDai.ListStatusId.Add(CommonConstants.CHINHSACHUUDAI_CONGKHAI);
                //        itChinhSachUuDai.ListStatusId.Add(CommonConstants.CHINHSACHUUDAI_THUHOI);
                //        count = dao.ListAll(itChinhSachUuDai).Count().ToString();
                //    }
                //}
            }
            else if (isController == "Store") // Gian hàng
            {
                var dao = new StoreDao();
                Model.EF.Store itGianHang = new Model.EF.Store();
                if (ParentId != 0) // link con
                {
                    if (Link == linkGianHangChoDuyet)
                    {
                        itGianHang.StatusId = CommonConstants.GIANHANG_CHODUYET;
                        count = dao.ListAll(itGianHang).Count().ToString();
                    }
                    if (Link == linkGianHangDuocDuyet)
                    {
                        itGianHang.StatusId = CommonConstants.GIANHANG_DUOCDUYET;
                        count = dao.ListAll(itGianHang).Count().ToString();
                    }
                    if (Link == linkGianHangKhongDuyet)
                    {
                        itGianHang.StatusId = CommonConstants.GIANHANG_KHONGDUYET;
                        count = dao.ListAll(itGianHang).Count().ToString();
                    }
                    if (Link == linkGianHangCapNhat)
                    {
                        itGianHang.StatusId = CommonConstants.GIANHANG_CHODUYETCAPNHAT;
                        count = dao.ListAll(itGianHang).Count().ToString();
                    }
                }
                else // link cha
                {
                    count = dao.ListAll(itGianHang).Count().ToString();
                }

            }
            else if (isController == "Product") // Sản phẩm
            {
                var dao = new ProductDao();
                Model.EF.Product itSanPham = new Model.EF.Product();
                if (ParentId != 0) // link con
                {
                    if (Link == linkSanPhamChoDuyet)
                    {
                        itSanPham.StatusId = CommonConstants.SANPHAM_CHODUYET;
                        count = dao.ListAll(itSanPham).Count().ToString();
                    }
                    if (Link == linkSanPhamDuocDuyet)
                    {
                        itSanPham.StatusId = CommonConstants.SANPHAM_DUOCDUYET;
                        count = dao.ListAll(itSanPham).Count().ToString();
                    }
                    if (Link == linkSanPhamKhongDuyet)
                    {
                        itSanPham.StatusId = CommonConstants.SANPHAM_KHONGDUYET;
                        count = dao.ListAll(itSanPham).Count().ToString();
                    }
                    if (Link == linkSanPhamCapNhat)
                    {
                        itSanPham.StatusId = CommonConstants.SANPHAM_CHODUYETCAPNHAT;
                        count = dao.ListAll(itSanPham).Count().ToString();
                    }
                }
                else // link cha
                {
                    List<int> lstStatus = new List<int>();
                    lstStatus.Add(CommonConstants.SANPHAM_CHODUYET);
                    lstStatus.Add(CommonConstants.SANPHAM_DUOCDUYET);
                    lstStatus.Add(CommonConstants.SANPHAM_KHONGDUYET);
                    lstStatus.Add(CommonConstants.SANPHAM_CHODUYETCAPNHAT);
                    itSanPham.ListStatusId = lstStatus;
                    count = dao.ListAll(itSanPham).Count().ToString();
                }

            }
            else if (isController == "Contact") // Liên hệ người dùng
            {
                var dao = new ContactDao();
                Model.EF.Contact itContact = new Model.EF.Contact();
                if (ParentId != 0) // link con
                {
                    if (Link == linkLienHeNguoiDungChoTiepNhan)
                    {
                        itContact.StatusId = CommonConstants.LIENHENGUOIDUNG_CHOTIEPNHAN;
                        count = dao.ListAll(itContact).Count().ToString();
                    }
                    if (Link == linkLienHeNguoiDungDangXuLy)
                    {
                        itContact.StatusId = CommonConstants.LIENHENGUOIDUNG_DANGXULY;
                        count = dao.ListAll(itContact).Count().ToString();
                    }
                    if (Link == linkLienHeNguoiDungKhongDuyet)
                    {
                        itContact.StatusId = CommonConstants.LIENHENGUOIDUNG_KHONGDUYET;
                        count = dao.ListAll(itContact).Count().ToString();
                    }
                    if (Link == linkLienHeNguoiDungDuocDuyet)
                    {
                        itContact.StatusId = CommonConstants.LIENHENGUOIDUNG_DUOCDUYET;
                        count = dao.ListAll(itContact).Count().ToString();
                    }
                }
                else // link cha
                {
                    count = dao.ListAll(itContact).Count().ToString();
                }
            }
            else if (isController == "ContactStore") // Liên hệ gian hàng
            {
                var dao = new ContactStoreDao();
                Model.EF.ContactStore itContactStore = new Model.EF.ContactStore();
                if (ParentId != 0) // link con
                {
                    if (Link == linkLienHeGianHangChoTiepNhan)
                    {
                        itContactStore.StatusId = CommonConstants.LIENHEGIANHANG_CHOTIEPNHAN;
                        count = dao.ListAll(itContactStore).Count().ToString();
                    }
                    if (Link == linkLienHeGianHangDangXuLy)
                    {
                        itContactStore.StatusId = CommonConstants.LIENHEGIANHANG_DANGXULY;
                        count = dao.ListAll(itContactStore).Count().ToString();
                    }
                    if (Link == linkLienHeGianHangKhongDuyet)
                    {
                        itContactStore.StatusId = CommonConstants.LIENHEGIANHANG_KHONGDUYET;
                        count = dao.ListAll(itContactStore).Count().ToString();
                    }
                    if (Link == linkLienHeGianHangDuocDuyet)
                    {
                        itContactStore.StatusId = CommonConstants.LIENHEGIANHANG_DUOCDUYET;
                        count = dao.ListAll(itContactStore).Count().ToString();
                    }
                }
                else // link cha
                {
                    count = dao.ListAll(itContactStore).Count().ToString();
                }
            }
            else if (isController == "Order") // Đơn hàng
            {
                var dao = new OrderDao();
                Model.EF.Order itOrder = new Model.EF.Order();
                if (ParentId != 0) // link con
                {
                    if (Link == linkDonHangChoXuLy)
                    {
                        itOrder.Status = 0;
                        count = dao.ListAll(itOrder).Count().ToString();
                    }
                    if (Link == linkDonHangDaXuLy)
                    {
                        List<Int32> lstStatus = new List<Int32>();
                        lstStatus.Add(1);
                        lstStatus.Add(2);
                        itOrder.ListStatusId = lstStatus;
                        count = dao.ListAll(itOrder).Count().ToString();
                    }
                }
                else // link cha
                {
                    count = dao.ListAll(itOrder).Count().ToString();
                }
            }
            else if (isController == "XacMinhUser")
            {
                if (ParentId != 0) // link con
                {
                    if (Link == linkTaiKhoanXacMinh)
                    {
                        User itUser = new User();
                        itUser.loaiTK = "ck";
                        count = new UserDao().ListAllXacMinh(itUser).Count().ToString(); ;
                    }
                    if (Link == linkTaiKhoanDuyet)
                    {
                        User itUser = new User();
                        itUser.loaiTK = "ck";
                        itUser.Status = true;
                        count = new UserDao().ListAllXacMinh(itUser).Count().ToString(); ;
                    }
                    if (Link == linkTaiKhoanKhongDuyet)
                    {
                        User itUser = new User();
                        itUser.loaiTK = "ck";
                        itUser.Status = false;
                        itUser.checkXacMinh = true;
                        count = new UserDao().ListAllXacMinh(itUser).Count().ToString(); ;
                    }
                }
                else
                {
                    User itUser = new User();
                    itUser.loaiTK = "ck";
                    count = new UserDao().ListAll(itUser).Count().ToString();
                }
            }
            else
            {
                count = "";
            }

            if (!count.Equals("0") && !count.Equals("") && count != "" && count != "0")
            {
                count = "(" + count + ")";
            }
            else
            {
                count = "";
            }
            return count;
        }
    }
}