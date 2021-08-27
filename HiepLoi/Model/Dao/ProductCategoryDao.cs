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
    public class ProductCategoryDao
    {
        OnlineShopDbContext db = null;
        public ProductCategoryDao()
        {
            db = new OnlineShopDbContext();
        }

        public IEnumerable<ProductCategory> ListAllPaging(ProductCategory searchString, ref int ParentId, ref int totalRecord, int pageIndex, int pageSize)
        {
            IQueryable<ProductCategory> model = db.ProductCategorys;
            if (!string.IsNullOrEmpty(searchString.Name))
            {
                model = model.Where(x => x.Name.ToUpper().Contains(searchString.Name.ToUpper()) || x.Name.Contains(searchString.Name));
            }
            if (!string.IsNullOrEmpty(searchString.Status.ToString()))
            {
                var statuBool = bool.Parse(searchString.Status.ToString());
                model = model.Where(x => x.Status == statuBool);
            }
            if (searchString.ParentID >= 0)
            {
                model = model.Where(x => x.ParentID == searchString.ParentID);
                try
                {
                    if (searchString.ParentID != 0)
                    {
                        ParentId = (int)db.ProductCategorys.SingleOrDefault(x => x.Id == searchString.ParentID).ParentID;
                    }
                }
                catch (Exception)
                {

                }
            }
            totalRecord = model.Count();
            return model.OrderBy(x => x.DisplayOrder).ToPagedList(pageIndex, pageSize);
        }
        public List<ProductCategory> ListAll()
        {
            return db.ProductCategorys.Where(x => x.Status == true).ToList();
        }
        public List<ProductCategory> ListShowOnHome()
        {
            return db.ProductCategorys.Where(x => x.ShowOnHome == true).ToList();
        }
        public ProductCategory GetById(long Id)
        {
            return db.ProductCategorys.SingleOrDefault(x => x.Id == Id);
        }
        public IEnumerable<ProductCategory> GetByParentId(int ParentId)
        {
            return db.ProductCategorys.Where(x => x.ParentID == ParentId).OrderBy(x => x.DisplayOrder);
        }
        public List<ProductCategory> ListByParentID(int ParentID)
        {
            return db.ProductCategorys.Where(x => x.Status == true && x.ParentID == ParentID).ToList();
        }
        public ProductCategory ViewDetailById(long Id,int ParentID = 0)
        {
            return db.ProductCategorys.Where(x => x.Status == true && x.Id == Id && x.ParentID == ParentID).FirstOrDefault();
        }
        public ProductCategory ViewDetail(long Id)
        {
            return db.ProductCategorys.Find(Id);
        }
        public long Insert(ProductCategory productCategory)
        {
            db.ProductCategorys.Add(productCategory);
            db.SaveChanges();
            return productCategory.Id;
        }

        public bool Update(ProductCategory entity)
        {
            try
            {
                var productCategory = db.ProductCategorys.Find(entity.Id);
                productCategory.Icons = entity.Icons;
                productCategory.Name = entity.Name;
                productCategory.MetaTitle = entity.MetaTitle;
                productCategory.ParentID = entity.ParentID;
                productCategory.DisplayOrder = entity.DisplayOrder;
                productCategory.SeoTitle = entity.SeoTitle;
                productCategory.ModifiedDate = entity.ModifiedDate;
                productCategory.ModifiedBy = entity.ModifiedBy;
                productCategory.MetaKeywords = entity.MetaKeywords;
                productCategory.MetaDescriptions = entity.MetaDescriptions;
                productCategory.Status = entity.Status;
                productCategory.ShowOnHome = entity.ShowOnHome;
                productCategory.Language = entity.Language;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public bool Delete(int id)
        {
            try
            {
                var productCategory = db.ProductCategorys.Find(id);
                db.ProductCategorys.Remove(productCategory);
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
            var productCategory = db.ProductCategorys.Find(id);
            productCategory.Status = !productCategory.Status;
            db.SaveChanges();
            return productCategory.Status;
        }

        public bool ChangeShowOnHome(long id)
        {
            var productCategory = db.ProductCategorys.Find(id);
            productCategory.ShowOnHome = !productCategory.ShowOnHome;
            db.SaveChanges();
            return productCategory.ShowOnHome;
        }
    }
}
