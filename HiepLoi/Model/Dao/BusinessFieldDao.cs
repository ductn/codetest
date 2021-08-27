using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class BusinessFieldDao
    {
        OnlineShopDbContext db = null;
        public BusinessFieldDao()
        {
            db = new OnlineShopDbContext();
        }

        public IEnumerable<BusinessField> ListAllPaging(BusinessField searchString, int pageIndex, int pageSize)
        {
            IQueryable<BusinessField> model = db.BusinessFields;
            if (!string.IsNullOrEmpty(searchString.Name))
            {
                model = model.Where(x => x.Name.Contains(searchString.Name));
            }
            if (!string.IsNullOrEmpty(searchString.Status.ToString()))
            {
                var statuBool = searchString.Status.ToString();
                model = model.Where(x => x.Status == statuBool);
            }
            if (pageIndex == 0 && pageSize == 0)
            {
                return model.OrderBy(x => x.Id);
            }
            else
            {
                return model.OrderBy(x => x.Id).ToPagedList(pageIndex, pageSize);
            }
        }
        public List<BusinessField> ListAll(BusinessField entity)
        {
            IQueryable<BusinessField> model = db.BusinessFields;
            if (entity != null)
            {
                if (entity.Status != null && entity.Status != "")
                {
                    model = model.Where(x => x.Status == entity.Status);
                }
            }
            return model.OrderByDescending(x => x.Id).ToList();
        }
        public BusinessField GetById(int functionid)
        {
            return db.BusinessFields.SingleOrDefault(x => x.Id == functionid);
        }
        public BusinessField ViewDetail(int id)
        {
            return db.BusinessFields.Find(id);
        }
        public long Insert(BusinessField entity)
        {
            db.BusinessFields.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }
        public bool Update(BusinessField entity)
        {
            try
            {
                var BusinessField = db.BusinessFields.Find(entity.Id);
                BusinessField.Code = entity.Code;
                BusinessField.Name = entity.Name;
                BusinessField.ParentId = entity.ParentId;
                BusinessField.Status = entity.Status;
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
                var BusinessField = db.BusinessFields.Find(id);
                db.BusinessFields.Remove(BusinessField);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public String ChangeStatus(long id)
        {
            var BusinessFields = db.BusinessFields.Find(id);
            //BusinessFields.Status = !BusinessFields.Status;
            String status = BusinessFields.Status;
            if(status=="1")
            {
                status = "0";
            }
            BusinessFields.Status = status;
            db.SaveChanges();
            return BusinessFields.Status;
        }
    }
}
