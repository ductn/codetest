using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Model.Dao;
using Model.EF;
using Model.ViewModel;

namespace OnlineShop.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        [ChildActionOnly]
        public PartialViewResult ProductCategory()
        {
            var model = new CategoryDao().ListAll();
            return PartialView(model);
        }
        public PartialViewResult SanPhamChinhPartialView()
        {
            Product entity = new Product();
            entity.StatusId = 3;
            entity.IsMain = true;
            var NhomSanPhams = new ProductDao().GroupByNhomSanPham(entity);
            List<Product> objs = new List<Product>();

            foreach (var item in NhomSanPhams)
            {
                entity.ProductCategoryParentId = (int)item.ProductCategoryId;
                var model = new ProductDao().ListByEntity(entity).Skip(0).Take(5);
                foreach (var e in model)
                {
                    objs.Add(e);
                }
            }
            ViewBag.NhomSanPhams = NhomSanPhams;
            return PartialView(objs);
        }
        public PartialViewResult DangGiamGiaPartialView()
        {
            Product entity = new Product();
            entity.StatusId = 3;
            entity.IsDiscount = true;
            var NhomSanPhams = new ProductDao().GroupByNhomSanPham(entity);
            List<Product> objs = new List<Product>();

            foreach (var item in NhomSanPhams)
            {
                entity.ProductCategoryParentId = (int)item.ProductCategoryId;
                var model = new ProductDao().ListByEntity(entity).Skip(0).Take(5);
                foreach (var e in model)
                {
                    objs.Add(e);
                }
            }
            entity.ProductCategoryParentId = 0;
            var ViewCount = new ProductDao().ListByEntity(entity).OrderByDescending(x => x.ViewCount).Skip(0).Take(6);
            ViewBag.ViewCount = ViewCount;
            ViewBag.NhomSanPhams = NhomSanPhams;
            return PartialView(objs);
        }
        public PartialViewResult TopNganhHangPartialView()
        {
            Product entity = new Product();
            entity.StatusId = 3;
            entity.IsHot = true;
            var NhomSanPhams = new ProductDao().GroupByNhomSanPham(entity);
            List<Product> objs = new List<Product>();

            foreach (var item in NhomSanPhams)
            {
                entity.ProductCategoryParentId = (int)item.ProductCategoryId;
                var model = new ProductDao().ListByEntity(entity).Skip(0).Take(5);
                foreach (var e in model)
                {
                    objs.Add(e);
                }
            }

            entity.ProductCategoryParentId = 0;
            var ViewCount = new ProductDao().ListByEntity(entity).OrderByDescending(x => x.ViewCount).Skip(0).Take(3);
            ViewBag.ViewCount = ViewCount;

            var TopNhanHang = new SliderDao().ListByIsType(5, 20);
            ViewBag.TopNhanHang = TopNhanHang;
            ViewBag.NhomSanPhams = NhomSanPhams;
            return PartialView(objs);
        }
        public PartialViewResult GoiYNganhHangPartialView()
        {
            Product entity = new Product();
            entity.StatusId = 3;
            entity.IsHot = true;
            var NhomSanPhams = new ProductDao().GroupByNhomSanPham(entity);
            List<Product> objs = new List<Product>();

            foreach (var item in NhomSanPhams)
            {
                entity.ProductCategoryParentId = (int)item.ProductCategoryId;
                var model = new ProductDao().ListByEntity(entity).Skip(0).Take(6);
                foreach (var e in model)
                {
                    objs.Add(e);
                }
            }
            ViewBag.NhomSanPhams = NhomSanPhams;
            return PartialView(objs);
        }
        public PartialViewResult ViewCountPartialView()
        {
            Category entity = new Category();
            entity.Status = true;

            var model = new CategoryDao().ListAll(entity).OrderByDescending(x => x.ViewCount).Skip(0).Take(100);

            return PartialView(model);
        }
        public PartialViewResult DanhMucPartialView()
        {
            //var model = new ProductCategoryDao().ListShowOnHome();
            var model = new UnitDao().ListShowOnHome();
            return PartialView(model);
        }
        public PartialViewResult SanPhamKhuyenMaiPartialView()
        {
            Product entity = new Product();
            entity.StatusId = 3;
            entity.IsPromotion = true;
            var NhomSanPhams = new ProductDao().GroupByNhomSanPhamViewCount(entity);

            var model = new ProductDao().ListByEntity(entity).Skip(0).Take(8);

            ViewBag.NhomSanPhams = NhomSanPhams;
            return PartialView(model);
        }
        public PartialViewResult XuHuongPartialView()
        {
            Product entity = new Product();
            entity.StatusId = 3;
            entity.IsTrending = true;
            var NhomSanPhams = new ProductDao().GroupByNhomSanPhamViewCount(entity);

            var model = new ProductDao().ListByEntity(entity).Skip(0).Take(16);

            ViewBag.NhomSanPhams = NhomSanPhams;
            return PartialView(model);
        }
        public JsonResult ListName(string q)
        {
            var data = new ProductDao().ListName(q);
            return Json(new
            {
                data = data,
                status = true
            },JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult getSanPham(string NganhHang)
        {
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            //NganhHang nganhHang = serializer.Deserialize<NganhHang>(NganhHang);

            //List<Breadcrumbs> objs = new List<Breadcrumbs>();
            //var weburl = nganhHang.DomainName;
            //if (nganhHang.CategoryId != 0)
            //{
            //    weburl = weburl + "/" + new CategoryDao().ViewDetail(nganhHang.CategoryId).MetaTitle + "-" + new CategoryDao().ViewDetail(nganhHang.CategoryId).ID;
            //    Breadcrumbs obj = new Breadcrumbs();
            //    obj.href = weburl;
            //    obj.name = new CategoryDao().ViewDetail(nganhHang.CategoryId).Name;
            //    objs.Add(obj);
            //}

            //if (nganhHang.UnitId != 0)
            //{

            //    weburl = weburl + "/" + new UnitDao().ViewDetail(nganhHang.UnitId).MetaTitleUnit + "-" + new UnitDao().ViewDetail(nganhHang.UnitId).Id;
            //    Breadcrumbs obj = new Breadcrumbs();
            //    obj.href = weburl;
            //    obj.name = new UnitDao().ViewDetail(nganhHang.UnitId).Title;
            //    objs.Add(obj);
            //}

            //if (nganhHang.ProductCategoryParentId != 0)
            //{

            //    weburl = weburl + "/" + new ProductCategoryDao().ViewDetail(nganhHang.ProductCategoryParentId).MetaTitle + "-" + new ProductCategoryDao().ViewDetail(nganhHang.ProductCategoryParentId).Id;
            //    Breadcrumbs obj = new Breadcrumbs();
            //    obj.href = weburl;
            //    obj.name = new ProductCategoryDao().ViewDetail(nganhHang.ProductCategoryParentId).Name;
            //    objs.Add(obj);
            //}

            //if (nganhHang.ProductCategoryId != 0)
            //{
            //    weburl = weburl + "/" + new ProductCategoryDao().ViewDetail(nganhHang.ProductCategoryId).MetaTitle + "-" + new ProductCategoryDao().ViewDetail(nganhHang.ProductCategoryId).Id;
            //    Breadcrumbs obj = new Breadcrumbs();
            //    obj.href = weburl;
            //    obj.name = new ProductCategoryDao().ViewDetail(nganhHang.ProductCategoryId).Name;
            //    objs.Add(obj);
            //}
            return Json(new
            {
                data = "",
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SanPham(long cateId = 0, int UnitId = 0, int ProductCategoryParentId = 0, int ProductCategoryId = 0, int page = 1, int pageSize = 1)
        {
            var category = new CategoryDao().ViewDetail(cateId);
            ViewBag.Category = category;
            var unit = new UnitDao().ViewDetail(UnitId);
            ViewBag.Unit = unit;
            ViewBag.TitleCategory = "";
            if (cateId != 0)
            {
                try
                {
                    ViewBag.TitleCategory = new CategoryDao().ViewDetail(cateId).Name;
                }
                catch (Exception)
                {

                }
            }
            ViewBag.TitleUnit = "";
            if (UnitId != 0)
            {
                try
                {
                    ViewBag.TitleUnit = new UnitDao().ViewDetail(UnitId).Title;
                }
                catch (Exception)
                {

                }
            }
            ViewBag.TitleProductCategoryParent = "";
            if (ProductCategoryParentId != 0)
            {
                try
                {
                    ViewBag.TitleProductCategoryParent = new ProductCategoryDao().ViewDetail(ProductCategoryParentId).Name;
                }
                catch (Exception)
                {

                }
            }
            ViewBag.TitleProductCategory = "";
            if (ProductCategoryId != 0)
            {
                try
                {
                    ViewBag.TitleProductCategory = new ProductCategoryDao().ViewDetail(ProductCategoryId).Name;
                }
                catch (Exception)
                {

                }
            }

            ViewBag._cateId = cateId;
            ViewBag._UnitId = UnitId;
            ViewBag._ProductCategoryParentId = ProductCategoryParentId;
            ViewBag._ProductCategoryId = ProductCategoryId;

            int totalRecord = 0;
            var model = new ProductDao().ListByCategoryId(cateId, UnitId, ProductCategoryParentId, ProductCategoryId, ref totalRecord, page, pageSize);

            ViewBag.Total = totalRecord;
            ViewBag.Page = page;

            int maxPage = 5;
            int totalPage = 0;

            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;

            return View(model);
        }
        [HttpPost]
        public ActionResult APISanPhamCategory(string NganhHang)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            NganhHang nganhHang = serializer.Deserialize<NganhHang>(NganhHang);

            List<Category> categorys = new CategoryDao().ListAll();
            List<Breadcrumbs> objs = new List<Breadcrumbs>();
            var weburl = nganhHang.DomainName;
            if (nganhHang.CategoryId != 0)
            {
                weburl = weburl + "/" + new CategoryDao().ViewDetail(nganhHang.CategoryId).MetaTitle + "-" + new CategoryDao().ViewDetail(nganhHang.CategoryId).ID;
                Breadcrumbs obj = new Breadcrumbs();
                obj.href = weburl;
                obj.name = new CategoryDao().ViewDetail(nganhHang.CategoryId).Name;
                objs.Add(obj);
                categorys = new CategoryDao().ListById(nganhHang.CategoryId);
            }

            if (nganhHang.UnitId != 0)
            {

                weburl = weburl + "/" + new UnitDao().ViewDetail(nganhHang.UnitId).MetaTitleUnit + "-" + new UnitDao().ViewDetail(nganhHang.UnitId).Id;
                Breadcrumbs obj = new Breadcrumbs();
                obj.href = weburl;
                obj.name = new UnitDao().ViewDetail(nganhHang.UnitId).Title;
                objs.Add(obj);
            }

            if (nganhHang.ProductCategoryParentId != 0)
            {

                weburl = weburl + "/" + new ProductCategoryDao().ViewDetail(nganhHang.ProductCategoryParentId).MetaTitle + "-" + new ProductCategoryDao().ViewDetail(nganhHang.ProductCategoryParentId).Id;
                Breadcrumbs obj = new Breadcrumbs();
                obj.href = weburl;
                obj.name = new ProductCategoryDao().ViewDetail(nganhHang.ProductCategoryParentId).Name;
                objs.Add(obj);
            }

            if (nganhHang.ProductCategoryId != 0)
            {
                weburl = weburl + "/" + new ProductCategoryDao().ViewDetail(nganhHang.ProductCategoryId).MetaTitle + "-" + new ProductCategoryDao().ViewDetail(nganhHang.ProductCategoryId).Id;
                Breadcrumbs obj = new Breadcrumbs();
                obj.href = weburl;
                obj.name = new ProductCategoryDao().ViewDetail(nganhHang.ProductCategoryId).Name;
                objs.Add(obj);
            }
            return Json(new
            {
                categorys = categorys,
                Breadcrumbs = objs,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult APICategoryUnit(string NganhHang)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            NganhHang nganhHang = serializer.Deserialize<NganhHang>(NganhHang);

            List<CategoryUnitViewModel> CategoryUnitViewModels = new List<CategoryUnitViewModel>();
            List<Breadcrumbs> objs = new List<Breadcrumbs>();
            var weburl = nganhHang.DomainName;
            if (nganhHang.CategoryId != 0)
            {
                try
                {
                    weburl = weburl + "/" + new CategoryDao().ViewDetail(nganhHang.CategoryId).MetaTitle + "-" + new CategoryDao().ViewDetail(nganhHang.CategoryId).ID;
                    Breadcrumbs obj = new Breadcrumbs();
                    obj.href = weburl;
                    obj.name = new CategoryDao().ViewDetail(nganhHang.CategoryId).Name;
                    objs.Add(obj);
                    CategoryUnitViewModels = new CategoryUnitDao().ListByCategoryIdViewModel(nganhHang.CategoryId);
                }
                catch (Exception)
                {

                }
            }

            if (nganhHang.UnitId != 0)
            {
                try
                {
                    weburl = weburl + "/" + new UnitDao().ViewDetail(nganhHang.UnitId).MetaTitleUnit + "-" + new UnitDao().ViewDetail(nganhHang.UnitId).Id;
                    Breadcrumbs obj = new Breadcrumbs();
                    obj.href = weburl;
                    obj.name = new UnitDao().ViewDetail(nganhHang.UnitId).Title;
                    objs.Add(obj);
                }
                catch (Exception)
                {

                }
            }

            if (nganhHang.ProductCategoryParentId != 0)
            {
                try
                {
                    weburl = weburl + "/" + new ProductCategoryDao().ViewDetail(nganhHang.ProductCategoryParentId).MetaTitle + "-" + new ProductCategoryDao().ViewDetail(nganhHang.ProductCategoryParentId).Id;
                    Breadcrumbs obj = new Breadcrumbs();
                    obj.href = weburl;
                    obj.name = new ProductCategoryDao().ViewDetail(nganhHang.ProductCategoryParentId).Name;
                    objs.Add(obj);
                }
                catch (Exception)
                {

                }
            }

            if (nganhHang.ProductCategoryId != 0)
            {
                try
                {
                    weburl = weburl + "/" + new ProductCategoryDao().ViewDetail(nganhHang.ProductCategoryId).MetaTitle + "-" + new ProductCategoryDao().ViewDetail(nganhHang.ProductCategoryId).Id;
                    Breadcrumbs obj = new Breadcrumbs();
                    obj.href = weburl;
                    obj.name = new ProductCategoryDao().ViewDetail(nganhHang.ProductCategoryId).Name;
                    objs.Add(obj);
                }
                catch (Exception)
                {

                }

            }
            return Json(new
            {
                CategoryUnitViewModels = CategoryUnitViewModels,
                Breadcrumbs = objs,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult APIProductCategoryUnit(string NganhHang)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            NganhHang nganhHang = serializer.Deserialize<NganhHang>(NganhHang);

            List<ProductCategoryUnitViewModel> ProductCategoryUnitViewModels = new List<ProductCategoryUnitViewModel>();
            List<Breadcrumbs> objs = new List<Breadcrumbs>();
            var weburl = nganhHang.DomainName;
            if (nganhHang.CategoryId != 0)
            {
                weburl = weburl + "/" + new CategoryDao().ViewDetail(nganhHang.CategoryId).MetaTitle + "-" + new CategoryDao().ViewDetail(nganhHang.CategoryId).ID;
                Breadcrumbs obj = new Breadcrumbs();
                obj.href = weburl;
                obj.name = new CategoryDao().ViewDetail(nganhHang.CategoryId).Name;
                objs.Add(obj);
                ProductCategoryUnitViewModels = new ProductCategoryUnitDao().ListByUnitIdCategoryIdViewModel(nganhHang.UnitId, nganhHang.CategoryId);
            }

            if (nganhHang.UnitId != 0)
            {

                weburl = weburl + "/" + new UnitDao().ViewDetail(nganhHang.UnitId).MetaTitleUnit + "-" + new UnitDao().ViewDetail(nganhHang.UnitId).Id;
                Breadcrumbs obj = new Breadcrumbs();
                obj.href = weburl;
                obj.name = new UnitDao().ViewDetail(nganhHang.UnitId).Title;
                objs.Add(obj);
            }

            if (nganhHang.ProductCategoryParentId != 0)
            {

                weburl = weburl + "/" + new ProductCategoryDao().ViewDetail(nganhHang.ProductCategoryParentId).MetaTitle + "-" + new ProductCategoryDao().ViewDetail(nganhHang.ProductCategoryParentId).Id;
                Breadcrumbs obj = new Breadcrumbs();
                obj.href = weburl;
                obj.name = new ProductCategoryDao().ViewDetail(nganhHang.ProductCategoryParentId).Name;
                objs.Add(obj);
            }

            if (nganhHang.ProductCategoryId != 0)
            {
                weburl = weburl + "/" + new ProductCategoryDao().ViewDetail(nganhHang.ProductCategoryId).MetaTitle + "-" + new ProductCategoryDao().ViewDetail(nganhHang.ProductCategoryId).Id;
                Breadcrumbs obj = new Breadcrumbs();
                obj.href = weburl;
                obj.name = new ProductCategoryDao().ViewDetail(nganhHang.ProductCategoryId).Name;
                objs.Add(obj);
            }
            return Json(new
            {
                ProductCategoryUnitViewModels = ProductCategoryUnitViewModels,
                Breadcrumbs = objs,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult APIProductCategoryParent(string NganhHang)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            NganhHang nganhHang = serializer.Deserialize<NganhHang>(NganhHang);

            List<ProductCategory> ProductCategorys = new List<ProductCategory>();
            List<Breadcrumbs> objs = new List<Breadcrumbs>();
            var weburl = nganhHang.DomainName;
            if (nganhHang.CategoryId != 0)
            {
                weburl = weburl + "/" + new CategoryDao().ViewDetail(nganhHang.CategoryId).MetaTitle + "-" + new CategoryDao().ViewDetail(nganhHang.CategoryId).ID;
                Breadcrumbs obj = new Breadcrumbs();
                obj.href = weburl;
                obj.name = new CategoryDao().ViewDetail(nganhHang.CategoryId).Name;
                objs.Add(obj);
            }

            if (nganhHang.UnitId != 0)
            {

                weburl = weburl + "/" + new UnitDao().ViewDetail(nganhHang.UnitId).MetaTitleUnit + "-" + new UnitDao().ViewDetail(nganhHang.UnitId).Id;
                Breadcrumbs obj = new Breadcrumbs();
                obj.href = weburl;
                obj.name = new UnitDao().ViewDetail(nganhHang.UnitId).Title;
                objs.Add(obj);
            }

            if (nganhHang.ProductCategoryParentId != 0)
            {

                weburl = weburl + "/" + new ProductCategoryDao().ViewDetail(nganhHang.ProductCategoryParentId).MetaTitle + "-" + new ProductCategoryDao().ViewDetail(nganhHang.ProductCategoryParentId).Id;
                Breadcrumbs obj = new Breadcrumbs();
                obj.href = weburl;
                obj.name = new ProductCategoryDao().ViewDetail(nganhHang.ProductCategoryParentId).Name;
                objs.Add(obj);
                ProductCategorys = new ProductCategoryDao().ListByParentID(nganhHang.ProductCategoryParentId);
            }

            if (nganhHang.ProductCategoryId != 0)
            {
                weburl = weburl + "/" + new ProductCategoryDao().ViewDetail(nganhHang.ProductCategoryId).MetaTitle + "-" + new ProductCategoryDao().ViewDetail(nganhHang.ProductCategoryId).Id;
                Breadcrumbs obj = new Breadcrumbs();
                obj.href = weburl;
                obj.name = new ProductCategoryDao().ViewDetail(nganhHang.ProductCategoryId).Name;
                objs.Add(obj);
            }
            return Json(new
            {
                ProductCategorys = ProductCategorys,
                Breadcrumbs = objs,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult APIProduct(string NganhHang)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            NganhHang nganhHang = serializer.Deserialize<NganhHang>(NganhHang);
            var ProductViews = new ProductDao().ListByCategoryIdNoTotal(nganhHang.CategoryId, nganhHang.UnitId, nganhHang.ProductCategoryParentId, nganhHang.ProductCategoryId).Skip(0).Take(8);
            return Json(new
            {
                ProductViews = ProductViews,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DanhSachDangGiamGia(int page = 1, int pageSize = 20)
        {
            Product entity = new Product();
            entity.StatusId = 3;
            entity.IsDiscount = true;
            int totalRecord = new ProductDao().ListByEntity(entity).Count;
            var model = new ProductDao().ListByEntity(entity).Skip((page - 1) * pageSize).Take(pageSize);

            ViewBag.Total = totalRecord;
            ViewBag.Page = page;

            int maxPage = 5;
            int totalPage = 0;

            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;

            return View(model);
        }
        public ActionResult DanhSachTopNganhHang(int page = 1, int pageSize = 20)
        {
            Product entity = new Product();
            entity.StatusId = 3;
            entity.IsHot = true;
            int totalRecord = new ProductDao().ListByEntity(entity).Count;
            var model = new ProductDao().ListByEntity(entity).Skip((page - 1) * pageSize).Take(pageSize);

            ViewBag.Total = totalRecord;
            ViewBag.Page = page;

            int maxPage = 5;
            int totalPage = 0;

            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;

            return View(model);
        }
        public ActionResult DanhSachSanPhamKhuyenMai(int page = 1, int pageSize = 20)
        {
            Product entity = new Product();
            entity.StatusId = 3;
            entity.IsPromotion = true;
            int totalRecord = new ProductDao().ListByEntity(entity).Count;
            var model = new ProductDao().ListByEntity(entity).Skip((page - 1) * pageSize).Take(pageSize);

            ViewBag.Total = totalRecord;
            ViewBag.Page = page;

            int maxPage = 5;
            int totalPage = 0;

            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;

            return View(model);
        }
        public ActionResult NhanhSanPham(long cateId = 0, int UnitId = 0, int ProductCategoryParentId = 0, int ProductCategoryId = 0, int page = 1, int pageSize = 20)
        {
            var category = new CategoryDao().ViewDetail(cateId);
            ViewBag.Category = category;
            var unit = new UnitDao().ViewDetail(UnitId);
            ViewBag.Unit = unit;
            ViewBag.TitleCategory = "";
            if (cateId != 0)
            {
                try
                {
                    ViewBag.TitleCategory = new CategoryDao().ViewDetail(cateId).Name;
                }
                catch (Exception)
                {

                }
            }
            ViewBag.TitleUnit = "";
            if (UnitId != 0)
            {
                try
                {
                    ViewBag.TitleUnit = new UnitDao().ViewDetail(UnitId).Title;
                }
                catch (Exception)
                {

                }
            }
            ViewBag.TitleProductCategoryParent = "";
            if (ProductCategoryParentId != 0)
            {
                try
                {
                    ViewBag.TitleProductCategoryParent = new ProductCategoryDao().ViewDetail(ProductCategoryParentId).Name;
                }
                catch (Exception)
                {

                }
            }
            ViewBag.TitleProductCategory = "";
            if (ProductCategoryId != 0)
            {
                try
                {
                    ViewBag.TitleProductCategory = new ProductCategoryDao().ViewDetail(ProductCategoryId).Name;
                }
                catch (Exception)
                {

                }
            }

            ViewBag._cateId = cateId;
            ViewBag._UnitId = UnitId;
            ViewBag._ProductCategoryParentId = ProductCategoryParentId;
            ViewBag._ProductCategoryId = ProductCategoryId;

            int totalRecord = 0;
            var model = new ProductDao().ListByCategoryId(cateId, UnitId, ProductCategoryParentId, ProductCategoryId, ref totalRecord, page, pageSize);

            ViewBag.Total = totalRecord;
            ViewBag.Page = page;

            int maxPage = 5;
            int totalPage = 0;

            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;

            return View(model);
        }
        public ActionResult NhanhNhanHang(long cateId = 0, int UnitId = 0, int ProductCategoryParentId = 0, int ProductCategoryId = 0, int page = 1, int pageSize = 20)
        {
            var category = new CategoryDao().ViewDetail(cateId);
            ViewBag.Category = category;
            var unit = new UnitDao().ViewDetail(UnitId);
            ViewBag.Unit = unit;
            ViewBag.TitleCategory = "";
            if (cateId != 0)
            {
                try
                {
                    ViewBag.TitleCategory = new CategoryDao().ViewDetail(cateId).Name;
                }
                catch (Exception)
                {

                }
            }
            ViewBag.TitleUnit = "";
            if (UnitId != 0)
            {
                try
                {
                    ViewBag.TitleUnit = new UnitDao().ViewDetail(UnitId).Title;
                }
                catch (Exception)
                {

                }
            }
            ViewBag.TitleProductCategoryParent = "";
            if (ProductCategoryParentId != 0)
            {
                try
                {
                    ViewBag.TitleProductCategoryParent = new ProductCategoryDao().ViewDetail(ProductCategoryParentId).Name;
                }
                catch (Exception)
                {

                }
            }
            ViewBag.TitleProductCategory = "";
            if (ProductCategoryId != 0)
            {
                try
                {
                    ViewBag.TitleProductCategory = new ProductCategoryDao().ViewDetail(ProductCategoryId).Name;
                }
                catch (Exception)
                {

                }
            }

            ViewBag._cateId = cateId;
            ViewBag._UnitId = UnitId;
            ViewBag._ProductCategoryParentId = ProductCategoryParentId;
            ViewBag._ProductCategoryId = ProductCategoryId;

            int totalRecord = 0;
            var model = new ProductDao().ListByCategoryId(cateId, UnitId, ProductCategoryParentId, ProductCategoryId, ref totalRecord, page, pageSize);

            ViewBag.Total = totalRecord;
            ViewBag.Page = page;

            int maxPage = 5;
            int totalPage = 0;

            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;

            return View(model);
        }
        public ActionResult NganhHang(long cateId, int UnitId = 0, int ProductCategoryParentId = 0, int ProductCategoryId = 0, int page = 1, int pageSize = 20)
        {
            var category = new CategoryDao().ViewDetail(cateId);
            ViewBag.Category = category;
            var unit = new UnitDao().ViewDetail(UnitId);
            ViewBag.Unit = unit;
            ViewBag.TitleCategory = "";
            if (cateId != 0)
            {
                try
                {
                    ViewBag.TitleCategory = "";
                    var obj = new CategoryDao().ViewDetail(cateId);
                    if (obj != null)
                    {
                        ViewBag.TitleCategory = obj.Name;
                    }
                }
                catch (Exception)
                {

                }
            }
            ViewBag.TitleUnit = "";
            if (UnitId != 0)
            {
                try
                {
                    ViewBag.TitleUnit = new UnitDao().ViewDetail(UnitId).Title;
                }
                catch (Exception)
                {

                }
            }
            ViewBag.TitleProductCategoryParent = "";
            if (ProductCategoryParentId != 0)
            {
                try
                {
                    ViewBag.TitleProductCategoryParent = new ProductCategoryDao().ViewDetail(ProductCategoryParentId).Name;
                }
                catch (Exception)
                {

                }
            }
            ViewBag.TitleProductCategory = "";
            if (ProductCategoryId != 0)
            {
                try
                {
                    ViewBag.TitleProductCategory = new ProductCategoryDao().ViewDetail(ProductCategoryId).Name;
                }
                catch (Exception)
                {

                }
            }

            ViewBag._cateId = cateId;
            ViewBag._UnitId = UnitId;
            ViewBag._ProductCategoryParentId = ProductCategoryParentId;
            ViewBag._ProductCategoryId = ProductCategoryId;

            int totalRecord = 0;
            var model = new ProductDao().ListByCategoryId(cateId, UnitId, ProductCategoryParentId, ProductCategoryId, ref totalRecord, page, pageSize);

            ViewBag.Total = totalRecord;
            ViewBag.Page = page;

            int maxPage = 5;
            int totalPage = 0;

            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;

            return View(model);
        }
        public ActionResult Category(long cateId,int UnitId = 0,int ProductCategoryParentId = 0, int ProductCategoryId = 0, int page = 1, int pageSize = 20)
        {
            var category = new CategoryDao().ViewDetail(cateId);
            ViewBag.Category = category;
            var unit = new UnitDao().ViewDetail(UnitId);
            ViewBag.Unit = unit;
            ViewBag.TitleCategory = "";
            if (cateId != 0)
            {
                try
                {
                    ViewBag.TitleCategory = new CategoryDao().ViewDetail(cateId).Name;
                }
                catch (Exception)
                {

                }
            }
            ViewBag.TitleUnit = "";
            if (UnitId != 0)
            {
                try
                {
                    ViewBag.TitleUnit = new UnitDao().ViewDetail(UnitId).Title;
                }
                catch (Exception)
                {

                }
            }
            ViewBag.TitleProductCategoryParent = "";
            if (ProductCategoryParentId != 0)
            {
                try
                {
                    ViewBag.TitleProductCategoryParent = new ProductCategoryDao().ViewDetail(ProductCategoryParentId).Name;
                }
                catch (Exception)
                {

                }
            }
            ViewBag.TitleProductCategory = "";
            if (ProductCategoryId != 0)
            {
                try
                {
                    ViewBag.TitleProductCategory = new ProductCategoryDao().ViewDetail(ProductCategoryId).Name;
                }
                catch (Exception)
                {

                }
            }

            ViewBag._cateId = cateId;
            ViewBag._UnitId = UnitId;
            ViewBag._ProductCategoryParentId = ProductCategoryParentId;
            ViewBag._ProductCategoryId = ProductCategoryId;

            int totalRecord = 0;
            var model = new ProductDao().ListByCategoryId(cateId, UnitId, ProductCategoryParentId, ProductCategoryId, ref totalRecord, page, pageSize);

            ViewBag.Total = totalRecord;
            ViewBag.Page = page;

            int maxPage = 5;
            int totalPage = 0;

            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;

            return View(model);
        }
        public ActionResult Search(string keyword, int page = 1, int pageSize = 20)
        {
            int totalRecord = 0;
            var model = new ProductDao().Search(keyword, ref totalRecord, page, pageSize);

            ViewBag.Total = totalRecord;
            ViewBag.Page = page;
            ViewBag.Keyword = keyword;
            int maxPage = 5;
            int totalPage = 0;

            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;

            return View(model);
        }
        [HttpPost]
        public JsonResult ChangeViewCount(long id)
        {
            var result = new ProductDao().ChangeViewCount(id);
            return Json(new
            {
                ViewCount = result,
                status = true
            });
        }
        [OutputCache(CacheProfile = "Cache1DayForProduct")]
        public ActionResult Detail(long id)
        {
            var product = new ProductDao().ViewDetail(id);
            ViewBag.Category = new CategoryDao().ViewDetail(product.CategoryID.Value);
            ViewBag.RelatedProducts = new ProductDao().ListRelatedProducts(id);
            return View(product);
        }
        [HttpPost]
        public ActionResult getUnit(string json)
        {
            Unit unit = new Unit();
            var Units = new UnitDao().ListAll(unit);
            return Json(new
            {
                data = Units,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult getCountry(string json)
        {
            Country country = new Country();
            var Countrys = new CountryDao().ListAll(country);
            return Json(new
            {
                data = Countrys,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult getMaterial(string json)
        {
            Material entity = new Material();
            var datas = new MaterialDao().ListAll(entity);
            return Json(new
            {
                data = datas,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult getUtility(string json)
        {
            Utility entity = new Utility();
            var datas = new UtilityDao().ListAll(entity);
            return Json(new
            {
                data = datas,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
    }
}