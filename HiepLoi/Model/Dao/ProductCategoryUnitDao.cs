using Model.EF;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ProductCategoryUnitDao
    {
        OnlineShopDbContext db = null;
        public ProductCategoryUnitDao()
        {
            db = new OnlineShopDbContext();
        }
        public long Insert(ProductCategoryUnit productCategoryUnit)
        {
            db.ProductCategoryUnits.Add(productCategoryUnit);
            db.SaveChanges();
            return productCategoryUnit.Id;
        }
        public bool Update(ProductCategoryUnit entity)
        {
            try
            {
                var productCategoryUnit = db.ProductCategoryUnits.Find(entity.Id);
                productCategoryUnit.CategoryId = entity.CategoryId;
                productCategoryUnit.UnitId = entity.UnitId;
                productCategoryUnit.ProductCategoryId = entity.ProductCategoryId;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //logging
                return false;
            }

        }
        public List<ProductCategoryUnit> ListAll()
        {
            return db.ProductCategoryUnits.ToList();
        }

        public List<ProductCategoryUnit> ListByUnitIdCategoryId(int UnitId,long CategoryId)
        {
            if (UnitId != 0 && CategoryId != 0)
            {
                return db.ProductCategoryUnits.Where(x => x.UnitId == UnitId && x.CategoryId == CategoryId).ToList();
            }
            else if(UnitId == 0 && CategoryId != 0)
            {
                return db.ProductCategoryUnits.Where(x => x.CategoryId == CategoryId).ToList();
            }
            else
            {
                return db.ProductCategoryUnits.ToList();
            }
        }

        public ProductCategoryUnit ListByCategoryUnitProductCategory(long CategoryId,int UnitId,int ProductCategoryId )
        {
            return db.ProductCategoryUnits.Where(x => x.CategoryId == CategoryId && x.UnitId == UnitId && x.ProductCategoryId == ProductCategoryId).FirstOrDefault();
           
        }

        public List<ProductCategoryUnitViewModel> ListByUnitIdCategoryIdViewModel(int UnitId, long CategoryId)
        {
            var model = (from a in db.ProductCategoryUnits
                         join b in db.Categories on a.CategoryId equals b.ID
                         join c in db.Units on a.UnitId equals c.Id
                         join d in db.ProductCategorys on a.ProductCategoryId equals d.Id
                         where a.CategoryId == CategoryId && a.UnitId == UnitId
                         select new ProductCategoryUnitViewModel()
                         {
                             Id = a.Id,
                             UnitId = c.Id,
                             UnitTitle = c.Title,
                             MetaTitleUnit = c.MetaTitleUnit,
                             CategoryID = b.ID,
                             CategoryName = b.Name,
                             CategoryMetaTitle = b.MetaTitle,
                             ProductCategoryId = d.Id,
                             ProductCategoryName = d.Name,
                             ProductCategoryMetaTitle = d.MetaTitle
                         });
            model.OrderByDescending(x => x.ProductCategoryId);
            return model.ToList();
        }

        public List<ProductCategoryUnitViewModel> ListByProductCategoryIdViewModel(int ProductCategoryId)
        {
            var model = (from a in db.ProductCategoryUnits
                         join b in db.Categories on a.CategoryId equals b.ID
                         join c in db.Units on a.UnitId equals c.Id
                         join d in db.ProductCategorys on a.ProductCategoryId equals d.Id
                         where a.ProductCategoryId == ProductCategoryId
                         select new ProductCategoryUnitViewModel()
                         {
                             Id = a.Id,
                             UnitId = c.Id,
                             UnitTitle = c.Title,
                             MetaTitleUnit = c.MetaTitleUnit,
                             CategoryID = b.ID,
                             CategoryName = b.Name,
                             CategoryMetaTitle = b.MetaTitle,
                             ProductCategoryId = d.Id,
                             ProductCategoryName = d.Name,
                             ProductCategoryMetaTitle = d.MetaTitle
                         });
            model.OrderByDescending(x => x.CategoryID);
            return model.ToList();
        }

        public ProductCategoryUnit ViewDetail(int id)
        {
            return db.ProductCategoryUnits.Find(id);
        }

        public bool Delete(long id)
        {
            try
            {
                var entity = db.ProductCategoryUnits.Find(id);
                db.ProductCategoryUnits.Remove(entity);
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
