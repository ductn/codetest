using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class CategoryDao
    {
        OnlineShopDbContext db = null;
        public CategoryDao()
        {
            db = new OnlineShopDbContext();
        }
        public long Insert(Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges();
            return category.ID;
        }
        public List<Category> ListAll()
        {
            return db.Categories.Where(x => x.Status == true).ToList();
        }
        public List<Category> ListAll(Category entity)
        {
            IQueryable<Category> model = db.Categories;
            if (entity != null)
            {
                model = model.Where(x => x.Status == entity.Status);
            }
            return model.OrderBy(x => x.DisplayOrder).ToList();
        }

        public List<Category> ListByGoiYMuaSam()
        {
            IQueryable<Category> model = db.Categories;
            model = model.Where(x => x.GoiYMuaSam == true);
            model = model.Where(x => x.Status == true);
            return model.OrderBy(x => x.DisplayOrder).ToList();
        }
        public IEnumerable<Category> ListAllPaging(string searchString,ref int totalRecord, int page, int pageSize)
        {
            IQueryable<Category> model = db.Categories;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Name.Contains(searchString));
            }
            //model = model.Where(x => x.Status == true);
            totalRecord = model.Count();
            return model.OrderBy(x => x.DisplayOrder).ToPagedList(page, pageSize);
        }
        public List<Category> ListById(long id)
        {
            return db.Categories.Where(x => x.Status == true && x.ID == id).ToList();
        }
        public Category GetById(int id)
        {
            return db.Categories.Find(id);
        }
        public Category ViewDetail(long id)
        {
            return db.Categories.Find(id);
        }
        public bool ChangeStatus(long id)
        {
            var obj = db.Categories.Find(id);
            obj.Status = !obj.Status;
            db.SaveChanges();
            return obj.Status;
        }

        public bool ChangeGoiYMuaSam(long id)
        {
            var obj = db.Categories.Find(id);
            obj.GoiYMuaSam = !obj.GoiYMuaSam;
            db.SaveChanges();
            return obj.GoiYMuaSam;
        }

        public bool Delete(int id)
        {
            try
            {
                var entity = db.Categories.Find(id);
                db.Categories.Remove(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool Update(Category entity)
        {
            try
            {
                var category = db.Categories.Find(entity.ID);
                category.Name = entity.Name;
                category.MetaTitle = entity.MetaTitle;
                category.ParentID = entity.ParentID;
                category.DisplayOrder = entity.DisplayOrder;
                category.SeoTitle = entity.SeoTitle;
                category.ModifiedDate = entity.ModifiedDate;
                category.ModifiedBy = entity.ModifiedBy;
                category.MetaKeywords = entity.MetaKeywords;
                category.MetaDescriptions = entity.MetaDescriptions;
                category.Status = entity.Status;
                category.ShowOnHome = entity.ShowOnHome;
                category.GoiYMuaSam = entity.GoiYMuaSam;
                category.Language = entity.Language;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //logging
                return false;
            }

        }
    }
}
