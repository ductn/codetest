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
    public class CategoryUnitDao
    {
        OnlineShopDbContext db = null;
        public CategoryUnitDao()
        {
            db = new OnlineShopDbContext();
        }

        public List<CategoryUnit> ListAll()
        {
            return db.CategoryUnits.ToList();
        }

        public List<CategoryUnit> ListByCategoryId(long CategoryId)
        {
            return db.CategoryUnits.Where(x => x.CategoryId == CategoryId).ToList();
        }

        public CategoryUnit ListByCategoryUnit(long CategoryId,int Unit)
        {
            return db.CategoryUnits.Where(x => x.CategoryId == CategoryId && x.UnitId == Unit).FirstOrDefault();
        }

        public List<CategoryUnitViewModel> ListAllViewModel()
        {
            var model = (from a in db.CategoryUnits
                         join b in db.Categories on a.CategoryId equals b.ID
                         join c in db.Units on a.UnitId equals c.Id
                         select new CategoryUnitViewModel()
                         {
                             Id = a.Id,
                             UnitId = c.Id,
                             UnitTitle = c.Title,
                             MetaTitleUnit = c.MetaTitleUnit,
                             CategoryID = b.ID,
                             CategoryName = b.Name,
                             CategoryMetaTitle = b.MetaTitle
                         });
            model.OrderByDescending(x => x.CategoryID);
            return model.ToList();
        }

        public List<CategoryUnitViewModel> ListByCategoryIdViewModel(long CategoryId)
        {
            var model = (from a in db.CategoryUnits
                         join b in db.Categories on a.CategoryId equals b.ID
                         join c in db.Units on a.UnitId equals c.Id
                         where a.CategoryId == CategoryId
                         select new CategoryUnitViewModel()
                         {
                             Id = a.Id,
                             UnitId = c.Id,
                             UnitTitle = c.Title,
                             MetaTitleUnit = c.MetaTitleUnit,
                             CategoryID = b.ID,
                             CategoryName = b.Name,
                             CategoryMetaTitle = b.MetaTitle
                         });
            model.OrderByDescending(x => x.UnitId);
            return model.ToList();
        }

        public List<CategoryUnitViewModel> ListByUnitIdViewModel(int UnitId)
        {
            var model = (from a in db.CategoryUnits
                         join b in db.Categories on a.CategoryId equals b.ID
                         join c in db.Units on a.UnitId equals c.Id
                         where a.UnitId == UnitId
                         select new CategoryUnitViewModel()
                         {
                             Id = a.Id,
                             UnitId = c.Id,
                             UnitTitle = c.Title,
                             MetaTitleUnit = c.MetaTitleUnit,
                             CategoryID = b.ID,
                             CategoryName = b.Name,
                             CategoryMetaTitle = b.MetaTitle
                         });
            model.OrderByDescending(x => x.UnitId);
            return model.ToList();
        }

        public long Insert(CategoryUnit categoryUnit)
        {
            db.CategoryUnits.Add(categoryUnit);
            db.SaveChanges();
            return categoryUnit.Id;
        }
        public bool Update(CategoryUnit entity)
        {
            try
            {
                var categoryUnit = db.CategoryUnits.Find(entity.Id);
                categoryUnit.CategoryId = entity.CategoryId;
                categoryUnit.UnitId = entity.UnitId;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //logging
                return false;
            }

        }

        public CategoryUnit ViewDetail(int id)
        {
            return db.CategoryUnits.Find(id);
        }

        public bool Delete(long id)
        {
            try
            {
                var entity = db.CategoryUnits.Find(id);
                db.CategoryUnits.Remove(entity);
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
