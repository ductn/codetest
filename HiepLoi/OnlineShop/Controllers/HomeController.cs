using Model.Dao;
using OnlineShop.Common;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Model.EF;
using Model.ViewModel;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.IO;

namespace OnlineShop.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //Lấy gian hàng 
            //Store itStore = new Store();
            //itStore.StatusId = CommonConstants.GIANHANG_DUOCDUYET;
            //ViewBag.ListGianHang = new StoreDao().ListAll(itStore);
            return View();
        }

        [HttpGet]
        public JsonResult SliderBanner(int IsType = 1, int Take = 10)
        {
            var model = new SliderDao().ListByIsType(IsType,Take);
            return Json(new
            {
                data = model,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult BusinessPartner(string StatusId = "3", int Take = 10)
        {
            var model = new BusinessPartnerDao().ListByStatusId(StatusId,Take);
            return Json(new
            {
                data = model,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult SanPhamChinh(int Take = 4)
        {
            Product entity = new Product();
            entity.StatusId = 3;
            entity.IsMain = true;
            var NhomSanPhams = new ProductDao().GroupByNhomSanPham(entity);
            List<Product> objs = new List<Product>();

            foreach (var item in NhomSanPhams)
            {
                entity.ProductCategoryParentId = (int)item.ProductCategoryId;
                var model = new ProductDao().ListByEntity(entity).Skip(0).Take(Take);
                foreach (var e in model)
                {
                    objs.Add(e);
                }
            }
            return Json(new
            {
                data = objs,
                NhomSanPhams = NhomSanPhams,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DangGiamGia(int Take = 4)
        {
            Product entity = new Product();
            entity.StatusId = 3;
            entity.IsPromotion = true;
            var NhomSanPhams = new ProductDao().GroupByNhomSanPham(entity);
            List<Product> objs = new List<Product>();

            foreach (var item in NhomSanPhams)
            {
                entity.ProductCategoryParentId = (int)item.ProductCategoryId;
                var model = new ProductDao().ListByEntity(entity).Skip(0).Take(Take);
                foreach (var e in model)
                {
                    objs.Add(e);
                }
            }
            return Json(new
            {
                data = objs,
                NhomSanPhams = NhomSanPhams,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult TopNganhHang(int Take = 4)
        {
            Product entity = new Product();
            entity.StatusId = 3;
            entity.IsHot = true;
            var NhomSanPhams = new ProductDao().GroupByNhomSanPham(entity);
            List<Product> objs = new List<Product>();

            foreach (var item in NhomSanPhams)
            {
                entity.ProductCategoryParentId = (int)item.ProductCategoryId;
                var model = new ProductDao().ListByEntity(entity).Skip(0).Take(Take);
                foreach (var e in model)
                {
                    objs.Add(e);
                }
            }
            return Json(new
            {
                data = objs,
                NhomSanPhams = NhomSanPhams,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        [ChildActionOnly]
        //[OutputCache(Duration = 3600 * 24)]
        public ActionResult MainMenu()
        {
            if (Session["UserKhachMua"] == null)
            {
                Session["UserKhachMua"] = "UserKhachMua" + DateTime.Now.ToFileTimeUtc();
            }
            var model = new MenuDao().ListByGroupId(1);
            ViewBag.Categorys = new CategoryDao().ListAll();
            return PartialView(model);
        }

        [ChildActionOnly]
        public PartialViewResult Slider()
        {
            var model = new SliderDao().ListByIsType(1,20);
            return PartialView(model);
        }
        public List<CategoryUnit> getCategoryUnit(int Id)
        {
            List<CategoryUnit> CategoryUnits = null;
            if (Id != 0)
            {
                CategoryUnits = new CategoryUnitDao().ListByCategoryId(Id);
            }
            return CategoryUnits;
        }

        public Unit getUnitById(int Id)
        {
            Unit unit = new Unit();
            try
            {
                if (Id != 0)
                {
                    var dao = new UnitDao();
                    Unit itemSearch = new Unit();
                    itemSearch.Id = Id;
                    List<Unit> lstUnit = dao.ListAll(itemSearch);
                    if (lstUnit.Count > 0)
                    {
                        unit = lstUnit.First();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return unit;
        }

        [ChildActionOnly]
        [OutputCache(Duration = 3600 * 24)]
        public ActionResult TopMenu()
        {
            if (Session["UserKhachMua"] == null)
            {
                Session["UserKhachMua"] = "UserKhachMua" + DateTime.Now.ToFileTimeUtc();
            }

            var model = new MenuDao().ListByGroupId(2);
            return PartialView(model);
        }

        [ChildActionOnly]
        public PartialViewResult Header()
        {
            var cart = Session[CommonConstants.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }

            return PartialView(list);
        }

        [ChildActionOnly]
        public PartialViewResult FeedbackPartialView()
        {
            Contact entity = new Contact();
            entity.StatusId = 3;
            var model = new ContactDao().ListAll(entity);
            return PartialView(model);
        }
        [ChildActionOnly]
        //[OutputCache(Duration = 3600 * 24)]
        public ActionResult Footer()
        {
            var hitCounter = new HitCounter();
            DateTime now = DateTime.Now;
            hitCounter.Date = now;

            //Kiểm tra xem trong ngày có bảng ghi đếm truy cập chưa
            var checkHitCounter = new HitCounterDao().GetById(hitCounter.Date);
            if (checkHitCounter != null)
            {
                checkHitCounter.Counter = checkHitCounter.Counter + 1;
                new HitCounterDao().Update(checkHitCounter);
                ViewBag.CountHitToday = checkHitCounter.Counter;
            }
            else
            {
                long? count = 1;
                hitCounter.Counter = count;
                new HitCounterDao().Insert(hitCounter);
                ViewBag.CountHitToday = 1;
            }
            //Đếm tất cả truy cập
            var lstAllHitCounter = new HitCounterDao().ListAll(null);
            if (lstAllHitCounter.ToList() != null)
            {
                long? SumCounter = 0;
                foreach (var itmHit in lstAllHitCounter.ToList())
                {
                    SumCounter = SumCounter + itmHit.Counter;
                }
                ViewBag.SumCounter = SumCounter.GetValueOrDefault().ToString("#,##0.###").Replace(",", ".");
            }
            //Đếm truy cập trong tuần
            int DayOfWeek = (int)now.DayOfWeek;
            DateTime Monday = now.AddDays(-DayOfWeek);
            var lstCounterInWeek = lstAllHitCounter.Where(x => x.Date >= Monday && x.Date <= now);
            if (lstCounterInWeek.ToList() != null)
            {
                long? sumCounterInWeek = 0;
                foreach (var itmHit in lstCounterInWeek.ToList())
                {
                    sumCounterInWeek = sumCounterInWeek + itmHit.Counter;
                }
                ViewBag.sumCounterInWeek = sumCounterInWeek.GetValueOrDefault().ToString("#,##0.###").Replace(",", ".");
            }
            //var model = new FooterDao().GetFooter();
            return PartialView();
        }

        public ActionResult ERR404()
        {
            return View();
        }

        public ActionResult ParentView()
        {
            return View();
        }

    }
}