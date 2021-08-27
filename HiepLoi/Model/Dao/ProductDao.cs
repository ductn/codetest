using ClsCommon;
using Model.EF;
using Model.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ProductDao
    {
        OnlineShopDbContext db = null;
        public ProductDao()
        {
            db = new OnlineShopDbContext();
        }
        public IEnumerable<Product> ListAllPaging(Product entity, string searchString, ref int totalRecord, int page, int pageSize)
        {
            IQueryable<Product> model = db.Products;
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(entity.Name) && entity.Name != "")
                {
                    model = model.Where(x => x.Name.Contains(entity.Name));
                }
                if (entity.StatusId != null)
                {
                    model = model.Where(x => x.StatusId == entity.StatusId);
                }
                if (entity.StoreId != null && entity.StoreId != 0)
                {
                    model = model.Where(x => x.StoreId == entity.StoreId);
                }
                if (entity.ListStatusId != null && entity.ListStatusId.Count > 0)
                {
                    model = model.Where(x => entity.ListStatusId.Any(y => y == x.StatusId));
                }
            }
            if (searchString!=null && searchString != "")
            {
                    model = model.Where(x => x.Name.Contains(searchString));
            }
            totalRecord = model.Count();
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        /// <summary>
        /// List all Product for client
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<Product> ListAllPaging(int page, int pageSize)
        {
            IQueryable<Product> model = db.Products;
            return model.OrderByDescending(x => x.ID).ToPagedList(page, pageSize);
        }
        public List<Product> ListAll(Product entity)
        {
            IQueryable<Product> model = db.Products;
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(entity.Name) && entity.Name != "")
                {
                    model = model.Where(x => x.Name.Contains(entity.Name));
                }
                if (entity.StatusId != null)
                {
                    model = model.Where(x => x.StatusId == entity.StatusId);
                }
                if (entity.StoreId != null && entity.StoreId != 0)
                {
                    model = model.Where(x => x.StoreId == entity.StoreId);
                }
                if (entity.IsHot != false)
                {
                    model = model.Where(x => x.IsHot == true);
                }
                if (entity.ListStatusId != null && entity.ListStatusId.Count > 0)
                {
                    model = model.Where(x => entity.ListStatusId.Any(y => y == x.StatusId));
                }
            }
            model = model.Where(x => x.Status == true);
            return model.OrderByDescending(x => x.ID).ToList();
        }

        public List<Product> ListByEntity(Product entity)
        {
            IQueryable<Product> model = db.Products;
            if (entity != null)
            {
                if (entity.StatusId != null)
                {
                    model = model.Where(x => x.StatusId == entity.StatusId);
                }
                if (entity.IsHot)
                {
                    model = model.Where(x => x.IsHot == true);
                }
                if (entity.IsDiscount)
                {
                    model = model.Where(x => x.IsDiscount == true);
                }
                if (entity.IsTrending)
                {
                    model = model.Where(x => x.IsTrending == true);
                }
                if (entity.IsPromotion)
                {
                    model = model.Where(x => x.IsPromotion == true);
                }
                if (entity.IsMain)
                {
                    model = model.Where(x => x.IsMain == true);
                }
                if (entity.GoiYMuaSam)
                {
                    model = model.Where(x => x.GoiYMuaSam == true);
                }
                if (entity.ProductCategoryParentId != 0)
                {
                    model = model.Where(x => x.ProductCategoryParentId == entity.ProductCategoryParentId);
                }
                if (entity.ViewCount > 0)
                {
                    model = model.Where(x => x.ViewCount > entity.ViewCount);
                }
            }
            model = model.Where(x => x.Status == true);
            return model.OrderByDescending(x => x.ID).ToList();
        }
        public List<ProductCategoryUnitViewModel> GroupByNhomSanPhamViewCount(Product entity)
        {
            var model = (from a in db.ProductCategorys
                         join d in db.Products on a.Id equals d.ProductCategoryParentId
                         join b in db.Categories on d.CategoryID equals b.ID
                         join c in db.Units on d.UnitId equals c.Id
                         where d.Status == true && d.StatusId == entity.StatusId && d.ViewCount > entity.ViewCount
                         orderby a.DisplayOrder descending
                         select new ProductCategoryUnitViewModel()
                         {
                             Id = (int)d.ID,
                             UnitId = c.Id,
                             UnitTitle = c.Title,
                             MetaTitleUnit = c.MetaTitleUnit,
                             CategoryID = b.ID,
                             CategoryName = b.Name,
                             CategoryMetaTitle = b.MetaTitle,
                             ProductCategoryId = a.Id,
                             ProductCategoryName = a.Name,
                             ProductCategoryMetaTitle = a.MetaTitle
                         });
            List<ProductCategoryUnitViewModel> objs = new List<ProductCategoryUnitViewModel>();
            foreach (var item in model)
            {
                if (objs.Where(x => x.ProductCategoryId == item.ProductCategoryId).Count() == 0)
                {
                    objs.Add(item);
                }
            }
            return objs.ToList();
        }
        public List<ProductCategoryUnitViewModel> GroupByNhomSanPham(Product entity)
        {
            var model = (from a in db.ProductCategorys
                         join d in db.Products on a.Id equals d.ProductCategoryParentId
                         join b in db.Categories on d.CategoryID equals b.ID
                         join c in db.Units on d.UnitId equals c.Id
                         where d.Status == true && d.StatusId == entity.StatusId
                         orderby a.DisplayOrder descending
                         select  new ProductCategoryUnitViewModel()
                         {
                             Id = (int)d.ID,
                             UnitId = c.Id,
                             UnitTitle = c.Title,
                             MetaTitleUnit = c.MetaTitleUnit,
                             CategoryID = b.ID,
                             CategoryName = b.Name,
                             CategoryMetaTitle = b.MetaTitle,
                             ProductCategoryId = a.Id,
                             ProductCategoryName = a.Name,
                             ProductCategoryMetaTitle = a.MetaTitle
                         });
            if (entity.GoiYMuaSam)
            {
                model = (from a in db.ProductCategorys
                         join d in db.Products on a.Id equals d.ProductCategoryParentId
                         join b in db.Categories on d.CategoryID equals b.ID
                         join c in db.Units on d.UnitId equals c.Id
                         where d.Status == true && d.StatusId == entity.StatusId && d.GoiYMuaSam == true && d.CategoryID == entity.CategoryID
                         orderby a.DisplayOrder descending
                         select new ProductCategoryUnitViewModel()
                         {
                             Id = (int)d.ID,
                             UnitId = c.Id,
                             UnitTitle = c.Title,
                             MetaTitleUnit = c.MetaTitleUnit,
                             CategoryID = b.ID,
                             CategoryName = b.Name,
                             CategoryMetaTitle = b.MetaTitle,
                             ProductCategoryId = a.Id,
                             ProductCategoryName = a.Name,
                             ProductCategoryMetaTitle = a.MetaTitle
                         });
            }
            if (entity.IsHot)
            {
                model = (from a in db.ProductCategorys
                         join d in db.Products on a.Id equals d.ProductCategoryParentId
                         join b in db.Categories on d.CategoryID equals b.ID
                         join c in db.Units on d.UnitId equals c.Id
                         where d.Status == true && d.StatusId == entity.StatusId && d.IsHot == true
                         orderby a.DisplayOrder descending
                         select new ProductCategoryUnitViewModel()
                         {
                             Id = (int)d.ID,
                             UnitId = c.Id,
                             UnitTitle = c.Title,
                             MetaTitleUnit = c.MetaTitleUnit,
                             CategoryID = b.ID,
                             CategoryName = b.Name,
                             CategoryMetaTitle = b.MetaTitle,
                             ProductCategoryId = a.Id,
                             ProductCategoryName = a.Name,
                             ProductCategoryMetaTitle = a.MetaTitle
                         });
            }
            if (entity.IsPromotion)
            {
                model = (from a in db.ProductCategorys
                         join d in db.Products on a.Id equals d.ProductCategoryParentId
                         join b in db.Categories on d.CategoryID equals b.ID
                         join c in db.Units on d.UnitId equals c.Id
                         where d.Status == true && d.StatusId == entity.StatusId && d.IsPromotion == true
                         orderby a.DisplayOrder descending
                         select new ProductCategoryUnitViewModel()
                         {
                             Id = (int)d.ID,
                             UnitId = c.Id,
                             UnitTitle = c.Title,
                             MetaTitleUnit = c.MetaTitleUnit,
                             CategoryID = b.ID,
                             CategoryName = b.Name,
                             CategoryMetaTitle = b.MetaTitle,
                             ProductCategoryId = a.Id,
                             ProductCategoryName = a.Name,
                             ProductCategoryMetaTitle = a.MetaTitle
                         });
            }
            if (entity.IsMain)
            {
                model = (from a in db.ProductCategorys
                         join d in db.Products on a.Id equals d.ProductCategoryParentId
                         join b in db.Categories on d.CategoryID equals b.ID
                         join c in db.Units on d.UnitId equals c.Id
                         where d.Status == true && d.StatusId == entity.StatusId && d.IsMain == true
                         orderby a.DisplayOrder descending
                         select new ProductCategoryUnitViewModel()
                         {
                             Id = (int)d.ID,
                             UnitId = c.Id,
                             UnitTitle = c.Title,
                             MetaTitleUnit = c.MetaTitleUnit,
                             CategoryID = b.ID,
                             CategoryName = b.Name,
                             CategoryMetaTitle = b.MetaTitle,
                             ProductCategoryId = a.Id,
                             ProductCategoryName = a.Name,
                             ProductCategoryMetaTitle = a.MetaTitle
                         });
            }
            if (entity.IsDiscount)
            {
                model = (from a in db.ProductCategorys
                         join d in db.Products on a.Id equals d.ProductCategoryParentId
                         join b in db.Categories on d.CategoryID equals b.ID
                         join c in db.Units on d.UnitId equals c.Id
                         where d.Status == true && d.StatusId == entity.StatusId && d.IsDiscount == true
                         orderby a.DisplayOrder descending
                         select new ProductCategoryUnitViewModel()
                         {
                             Id = (int)d.ID,
                             UnitId = c.Id,
                             UnitTitle = c.Title,
                             MetaTitleUnit = c.MetaTitleUnit,
                             CategoryID = b.ID,
                             CategoryName = b.Name,
                             CategoryMetaTitle = b.MetaTitle,
                             ProductCategoryId = a.Id,
                             ProductCategoryName = a.Name,
                             ProductCategoryMetaTitle = a.MetaTitle
                         });
            }
            if (entity.IsTrending)
            {
                model = (from a in db.ProductCategorys
                         join d in db.Products on a.Id equals d.ProductCategoryParentId
                         join b in db.Categories on d.CategoryID equals b.ID
                         join c in db.Units on d.UnitId equals c.Id
                         where d.Status == true && d.StatusId == entity.StatusId && d.IsTrending == true
                         orderby a.DisplayOrder descending
                         select new ProductCategoryUnitViewModel()
                         {
                             Id = (int)d.ID,
                             UnitId = c.Id,
                             UnitTitle = c.Title,
                             MetaTitleUnit = c.MetaTitleUnit,
                             CategoryID = b.ID,
                             CategoryName = b.Name,
                             CategoryMetaTitle = b.MetaTitle,
                             ProductCategoryId = a.Id,
                             ProductCategoryName = a.Name,
                             ProductCategoryMetaTitle = a.MetaTitle
                         });
            }
            List<ProductCategoryUnitViewModel> objs = new List<ProductCategoryUnitViewModel>();
            foreach (var item in model)
            {
                if (objs.Where(x => x.ProductCategoryId == item.ProductCategoryId).Count() == 0)
                {
                    objs.Add(item);
                }
            }
            return objs.ToList();
        }

        public Product GetByID(long id)
        {
            return db.Products.Find(id);
        }

        public Tag GetTag(string id)
        {
            return db.Tags.Find(id);
        }
        public long Create(Product Product)
        {
            if (!string.IsNullOrEmpty(Product.Name))
            {
                Product.MetaTitle = StringHelper.ToUnsignString(Product.Name);
            }
            Product.CreatedDate = DateTime.Now;
            Product.StatusId = 2;
            Product.Status = true;
            db.Products.Add(Product);
            db.SaveChanges();
            return Product.ID;
        }
        public bool Update(Product entity)
        {
            try
            {
                var Product = db.Products.Find(entity.ID);
                if (!string.IsNullOrEmpty(entity.Name))
                {
                    entity.MetaTitle = StringHelper.ToUnsignString(entity.Name);
                }
                Product.ModifiedDate = DateTime.Now;
                Product.Name = entity.Name;
                Product.MetaTitle = entity.MetaTitle;
                Product.Code = entity.Code;
                Product.Description = entity.Description;
                Product.Image = entity.Image;
                Product.MoreImages = entity.MoreImages;
                Product.Price = entity.Price;
                Product.PromotionPrice = entity.PromotionPrice;
                Product.Quantity = entity.Quantity;
                Product.CategoryID = entity.CategoryID;
                Product.UnitId = entity.UnitId;
                Product.ProductCategoryParentId = entity.ProductCategoryParentId;
                Product.ProductCategoryId = entity.ProductCategoryId;
                Product.Detail = entity.Detail;
                Product.Status = entity.Status;
                Product.ViewCount = entity.ViewCount;
                Product.StoreId = entity.StoreId;
                Product.IsHot = entity.IsHot;
                Product.IsDiscount = entity.IsDiscount;
                Product.IsTrending = entity.IsTrending;
                Product.IsPromotion = entity.IsPromotion;
                Product.IsMain = entity.IsMain;
                Product.GoiYMuaSam = entity.GoiYMuaSam;
                Product.TinhTrangHang = entity.TinhTrangHang;
                Product.Quantity = entity.Quantity;
                Product.StatusId = entity.StatusId;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //logging
                return false;
            }

        }
        public bool Delete(int id)
        {
            try
            {
                var Product = db.Products.Find(id);
                db.Products.Remove(Product);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool ChangeStatus(long id)
        {
            var obj = db.Products.Find(id);
            obj.Status = !obj.Status;
            db.SaveChanges();
            return obj.Status;
        }
        public bool ChangeIsHot(long id)
        {
            var obj = db.Products.Find(id);
            obj.IsHot = !obj.IsHot;
            db.SaveChanges();
            return obj.IsHot;
        }

        public bool ChangeIsPromotion(long id)
        {
            var obj = db.Products.Find(id);
            obj.IsPromotion = !obj.IsPromotion;
            db.SaveChanges();
            return obj.IsPromotion;
        }

        public bool ChangeIsMain(long id)
        {
            var obj = db.Products.Find(id);
            obj.IsMain = !obj.IsMain;
            db.SaveChanges();
            return obj.IsMain;
        }

        public bool ChangeGoiYMuaSam(long id)
        {
            var obj = db.Products.Find(id);
            obj.GoiYMuaSam = !obj.GoiYMuaSam;
            db.SaveChanges();
            return obj.GoiYMuaSam;
        }

        public bool ChangeIsDiscount(long id)
        {
            var obj = db.Products.Find(id);
            obj.IsDiscount = !obj.IsDiscount;
            db.SaveChanges();
            return obj.IsDiscount;
        }

        public bool ChangeIsTrending(long id)
        {
            var obj = db.Products.Find(id);
            obj.IsTrending = !obj.IsTrending;
            db.SaveChanges();
            return obj.IsTrending;
        }

        public int ChangeViewCount(long id)
        {
            var obj = db.Products.Find(id);
            obj.ViewCount = (obj.ViewCount + 1);
            db.SaveChanges();
            var obj2 = db.Categories.Find(obj.CategoryID);
            obj2.ViewCount = db.Products.Where(x => x.CategoryID == obj.CategoryID).Sum(x => x.ViewCount);
            db.SaveChanges();
            return obj.ViewCount;
        }

        public List<Product> ListNewProduct(int top)
        {
            return db.Products.OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }
        public List<string> ListName(string keyword)
        {
            return db.Products.Where(x => x.Name.Contains(keyword)).Select(x => x.Name).ToList();
        }
        public IEnumerable<Product> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Product> model = db.Products;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Name.Contains(searchString));
            }

            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        /// <summary>
        /// Get list product by category
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public List<ProductViewModel> ListByCategoryId(long categoryID, int UnitId, int ProductCategoryParentId, int ProductCategoryId, ref int totalRecord, int pageIndex = 1, int pageSize = 2)
        {
            if (categoryID != 0 && UnitId == 0 && ProductCategoryParentId == 0 && ProductCategoryId == 0)
            {
                totalRecord = db.Products.Where(x => x.CategoryID == categoryID).Count();
                var model = (from a in db.Products
                             join b in db.Categories
                             on a.CategoryID equals b.ID
                             where a.CategoryID == categoryID
                             select new ProductViewModel()
                             {
                                 CateMetaTitle = b.MetaTitle,
                                 CateName = b.Name,
                                 CreatedDate = a.CreatedDate,
                                 ID = a.ID,
                                 CategoryID = (long)a.CategoryID,
                                 UnitId = a.UnitId,
                                 ProductCategoryParentId = a.ProductCategoryParentId,
                                 ProductCategoryId = a.ProductCategoryId,
                                 Images = a.Image,
                                 Name = a.Name,
                                 MetaTitle = a.MetaTitle,
                                 Price = a.Price
                             });
                model.OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                return model.ToList();
            }
            else if (categoryID != 0 && UnitId != 0 && ProductCategoryParentId == 0 && ProductCategoryId == 0) {
                totalRecord = db.Products.Where(x => x.CategoryID == categoryID && x.UnitId == UnitId).Count();
                var model = (from a in db.Products
                             join b in db.Categories
                             on a.CategoryID equals b.ID
                             where a.CategoryID == categoryID && a.UnitId == UnitId
                             select new ProductViewModel()
                             {
                                 CateMetaTitle = b.MetaTitle,
                                 CateName = b.Name,
                                 CreatedDate = a.CreatedDate,
                                 ID = a.ID,
                                 CategoryID = (long)a.CategoryID,
                                 UnitId = a.UnitId,
                                 ProductCategoryParentId = a.ProductCategoryParentId,
                                 ProductCategoryId = a.ProductCategoryId,
                                 Images = a.Image,
                                 Name = a.Name,
                                 MetaTitle = a.MetaTitle,
                                 Price = a.Price
                             });
                model.OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                return model.ToList();
            }
            else if (categoryID != 0 && UnitId != 0 && ProductCategoryParentId != 0 && ProductCategoryId == 0)
            {
                totalRecord = db.Products.Where(x => x.CategoryID == categoryID && x.UnitId == UnitId && x.ProductCategoryParentId == ProductCategoryParentId).Count();
                var model = (from a in db.Products
                             join b in db.Categories
                             on a.CategoryID equals b.ID
                             where a.CategoryID == categoryID && a.UnitId == UnitId && a.ProductCategoryParentId == ProductCategoryParentId
                             select new ProductViewModel()
                             {
                                 CateMetaTitle = b.MetaTitle,
                                 CateName = b.Name,
                                 CreatedDate = a.CreatedDate,
                                 ID = a.ID,
                                 CategoryID = (long)a.CategoryID,
                                 UnitId = a.UnitId,
                                 ProductCategoryParentId = a.ProductCategoryParentId,
                                 ProductCategoryId = a.ProductCategoryId,
                                 Images = a.Image,
                                 Name = a.Name,
                                 MetaTitle = a.MetaTitle,
                                 Price = a.Price
                             });
                model.OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                return model.ToList();
            }
            else if (categoryID != 0 && UnitId != 0 && ProductCategoryParentId != 0 && ProductCategoryId != 0)
            {
                totalRecord = db.Products.Where(x => x.CategoryID == categoryID && x.UnitId == UnitId && x.ProductCategoryParentId == ProductCategoryParentId && x.ProductCategoryId == ProductCategoryId).Count();
                var model = (from a in db.Products
                             join b in db.Categories
                             on a.CategoryID equals b.ID
                             where a.CategoryID == categoryID && a.UnitId == UnitId && a.ProductCategoryParentId == ProductCategoryParentId && a.ProductCategoryId == ProductCategoryId
                             select new ProductViewModel()
                             {
                                 CateMetaTitle = b.MetaTitle,
                                 CateName = b.Name,
                                 CreatedDate = a.CreatedDate,
                                 ID = a.ID,
                                 CategoryID = (long)a.CategoryID,
                                 UnitId = a.UnitId,
                                 ProductCategoryParentId = a.ProductCategoryParentId,
                                 ProductCategoryId = a.ProductCategoryId,
                                 Images = a.Image,
                                 Name = a.Name,
                                 MetaTitle = a.MetaTitle,
                                 Price = a.Price
                             });
                model.OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                return model.ToList();
            }
            else if (categoryID == 0 && UnitId != 0 && ProductCategoryParentId == 0 && ProductCategoryId == 0)
            {
                totalRecord = db.Products.Where(x => x.UnitId == UnitId).Count();
                var model = (from a in db.Products
                             join b in db.Categories
                             on a.CategoryID equals b.ID
                             where a.UnitId == UnitId
                             select new ProductViewModel()
                             {
                                 CateMetaTitle = b.MetaTitle,
                                 CateName = b.Name,
                                 CreatedDate = a.CreatedDate,
                                 ID = a.ID,
                                 CategoryID = (long)a.CategoryID,
                                 UnitId = a.UnitId,
                                 ProductCategoryParentId = a.ProductCategoryParentId,
                                 ProductCategoryId = a.ProductCategoryId,
                                 Images = a.Image,
                                 Name = a.Name,
                                 MetaTitle = a.MetaTitle,
                                 Price = a.Price
                             });
                model.OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                return model.ToList();
            }
            else { 
                totalRecord = db.Products.Where(x => x.CategoryID == categoryID && x.UnitId == UnitId).Count();
                var model = (from a in db.Products
                             join b in db.Categories
                             on a.CategoryID equals b.ID
                             select new ProductViewModel()
                             {
                                 CateMetaTitle = b.MetaTitle,
                                 CateName = b.Name,
                                 CreatedDate = a.CreatedDate,
                                 ID = a.ID,
                                 CategoryID = (long)a.CategoryID,
                                 UnitId = a.UnitId,
                                 ProductCategoryParentId = a.ProductCategoryParentId,
                                 ProductCategoryId = a.ProductCategoryId,
                                 Images = a.Image,
                                 Name = a.Name,
                                 MetaTitle = a.MetaTitle,
                                 Price = a.Price
                             });
                model.OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                return model.ToList();
            }
        }

        public List<ProductViewModel> ListByCategoryIdNoTotal(long categoryID, int UnitId, int ProductCategoryParentId, int ProductCategoryId)
        {
            if (categoryID != 0 && UnitId == 0 && ProductCategoryParentId == 0 && ProductCategoryId == 0)
            {
                var model = (from a in db.Products
                             join b in db.Categories
                             on a.CategoryID equals b.ID
                             where a.CategoryID == categoryID
                             select new ProductViewModel()
                             {
                                 CateMetaTitle = b.MetaTitle,
                                 CateName = b.Name,
                                 CreatedDate = a.CreatedDate,
                                 ID = a.ID,
                                 CategoryID = (long)a.CategoryID,
                                 UnitId = a.UnitId,
                                 ProductCategoryParentId = a.ProductCategoryParentId,
                                 ProductCategoryId = a.ProductCategoryId,
                                 Images = a.Image,
                                 Name = a.Name,
                                 MetaTitle = a.MetaTitle,
                                 Price = a.Price
                             });
                model.OrderByDescending(x => x.CreatedDate);
                return model.ToList();
            }
            else if (categoryID != 0 && UnitId != 0 && ProductCategoryParentId == 0 && ProductCategoryId == 0)
            {
                var model = (from a in db.Products
                             join b in db.Categories
                             on a.CategoryID equals b.ID
                             where a.CategoryID == categoryID && a.UnitId == UnitId
                             select new ProductViewModel()
                             {
                                 CateMetaTitle = b.MetaTitle,
                                 CateName = b.Name,
                                 CreatedDate = a.CreatedDate,
                                 ID = a.ID,
                                 CategoryID = (long)a.CategoryID,
                                 UnitId = a.UnitId,
                                 ProductCategoryParentId = a.ProductCategoryParentId,
                                 ProductCategoryId = a.ProductCategoryId,
                                 Images = a.Image,
                                 Name = a.Name,
                                 MetaTitle = a.MetaTitle,
                                 Price = a.Price
                             });
                model.OrderByDescending(x => x.CreatedDate);
                return model.ToList();
            }
            else if (categoryID != 0 && UnitId != 0 && ProductCategoryParentId != 0 && ProductCategoryId == 0)
            {
                var model = (from a in db.Products
                             join b in db.Categories
                             on a.CategoryID equals b.ID
                             where a.CategoryID == categoryID && a.UnitId == UnitId && a.ProductCategoryParentId == ProductCategoryParentId
                             select new ProductViewModel()
                             {
                                 CateMetaTitle = b.MetaTitle,
                                 CateName = b.Name,
                                 CreatedDate = a.CreatedDate,
                                 ID = a.ID,
                                 CategoryID = (long)a.CategoryID,
                                 UnitId = a.UnitId,
                                 ProductCategoryParentId = a.ProductCategoryParentId,
                                 ProductCategoryId = a.ProductCategoryId,
                                 Images = a.Image,
                                 Name = a.Name,
                                 MetaTitle = a.MetaTitle,
                                 Price = a.Price
                             });
                model.OrderByDescending(x => x.CreatedDate);
                return model.ToList();
            }
            else if (categoryID != 0 && UnitId != 0 && ProductCategoryParentId != 0 && ProductCategoryId != 0)
            {
                var model = (from a in db.Products
                             join b in db.Categories
                             on a.CategoryID equals b.ID
                             where a.CategoryID == categoryID && a.UnitId == UnitId && a.ProductCategoryParentId == ProductCategoryParentId && a.ProductCategoryId == ProductCategoryId
                             select new ProductViewModel()
                             {
                                 CateMetaTitle = b.MetaTitle,
                                 CateName = b.Name,
                                 CreatedDate = a.CreatedDate,
                                 ID = a.ID,
                                 CategoryID = (long)a.CategoryID,
                                 UnitId = a.UnitId,
                                 ProductCategoryParentId = a.ProductCategoryParentId,
                                 ProductCategoryId = a.ProductCategoryId,
                                 Images = a.Image,
                                 Name = a.Name,
                                 MetaTitle = a.MetaTitle,
                                 Price = a.Price
                             });
                model.OrderByDescending(x => x.CreatedDate);
                return model.ToList();
            }
            else
            {
                var model = (from a in db.Products
                             join b in db.Categories
                             on a.CategoryID equals b.ID
                             select new ProductViewModel()
                             {
                                 CateMetaTitle = b.MetaTitle,
                                 CateName = b.Name,
                                 CreatedDate = a.CreatedDate,
                                 ID = a.ID,
                                 CategoryID = (long)a.CategoryID,
                                 UnitId = a.UnitId,
                                 ProductCategoryParentId = a.ProductCategoryParentId,
                                 ProductCategoryId = a.ProductCategoryId,
                                 Images = a.Image,
                                 Name = a.Name,
                                 MetaTitle = a.MetaTitle,
                                 Price = a.Price
                             });
                model.OrderByDescending(x => x.CreatedDate);
                return model.ToList();
            }
        }
        public List<ProductViewModel> Search(string keyword, ref int totalRecord, int pageIndex = 1, int pageSize = 2)
        {
            totalRecord = db.Products.Where(x => x.Name == keyword).Count();
            var model = (from a in db.Products
                         join b in db.Categories
                         on a.CategoryID equals b.ID
                         where a.Name.Contains(keyword)
                         select new
                         {
                             CateMetaTitle = b.MetaTitle,
                             CateName = b.Name,
                             CreatedDate = a.CreatedDate,
                             ID = a.ID,
                             CategoryID = (long)a.CategoryID,
                             UnitId = a.UnitId,
                             ProductCategoryParentId = a.ProductCategoryParentId,
                             ProductCategoryId = a.ProductCategoryId,
                             Images = a.Image,
                             Name = a.Name,
                             MetaTitle = a.MetaTitle,
                             Price = a.Price
                         }).AsEnumerable().Select(x => new ProductViewModel()
                         {
                             CateMetaTitle = x.MetaTitle,
                             CateName = x.Name,
                             CreatedDate = x.CreatedDate,
                             ID = x.ID,
                             CategoryID = x.CategoryID,
                             UnitId = x.UnitId,
                             ProductCategoryParentId = x.ProductCategoryParentId,
                             ProductCategoryId = x.ProductCategoryId,
                             Images = x.Images,
                             Name = x.Name,
                             MetaTitle = x.MetaTitle,
                             Price = x.Price
                         });
            model.OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return model.ToList();
        }
        /// <summary>
        /// List feature product
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<Product> ListFeatureProduct(int top)
        {
            return db.Products.Where(x => x.TopHot != null && x.TopHot > DateTime.Now).OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }
        public List<Product> ListRelatedProducts(long ID)
        {
            var product = db.Products.Find(ID);
            return db.Products.Where(x => x.ID != ID && x.CategoryID == product.CategoryID).ToList();
        }

        public void UpdateImages(long ID, string images)
        {
            var product = db.Products.Find(ID);
            product.MoreImages = images;
            db.SaveChanges();
        }
        public Product ViewDetail(long id)
        {
            return db.Products.Find(id);
        }

        public bool ChangeStatusQT(int idProduct, int nextStatus)
        {
            try
            {
                var Product = db.Products.Find(idProduct);
                Product.StatusId = nextStatus;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
