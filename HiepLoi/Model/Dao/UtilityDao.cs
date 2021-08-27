using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClsCommon;
using PagedList;
using System.Web;

namespace Model.Dao
{
    public class UtilityDao
    {
        OnlineShopDbContext db = null;
        public UtilityDao()
        {
            db = new OnlineShopDbContext();
        }

        public IEnumerable<Utility> ListAllPaging(Utility searchString, ref int totalRecord, int pageIndex, int pageSize)
        {
            IQueryable<Utility> model = db.Utilitys;
            if (!string.IsNullOrEmpty(searchString.Name))
            {
                model = model.Where(x => x.Name.ToUpper().Contains(searchString.Name.ToUpper()));
            }
            if (!string.IsNullOrEmpty(searchString.Status.ToString()))
            {
                var statuBool = bool.Parse(searchString.Status.ToString());
                model = model.Where(x => x.Status == statuBool);
            }

            totalRecord = model.Count();
            return model.OrderByDescending(x => x.Id).ToPagedList(pageIndex, pageSize);
        }
        public List<Utility> ListAll(Utility entity)
        {
            IQueryable<Utility> model = db.Utilitys;
            if (entity != null)
            {
                //if (!string.IsNullOrEmpty(entity.Code))
                //{
                //    model = model.Where(x => x.Code.Contains(entity.Code) || x.Code.Contains(entity.Code));
                //}
            }
            return model.OrderByDescending(x => x.Id).ToList();
        }
        public Utility GetById(int id)
        {
            return db.Utilitys.SingleOrDefault(x => x.Id == id);
        }
        public Utility ViewDetail(int id)
        {
            return db.Utilitys.Find(id);
        }
        public long Insert(Utility entity)
        {
            db.Utilitys.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }
        public bool Update(Utility entity)
        {
            try
            {
                var Utility = db.Utilitys.Find(entity.Id);
                Utility.Code = entity.Code;
                Utility.Name = entity.Name;
                Utility.Desription = entity.Desription;
                Utility.Sort = entity.Sort;
                Utility.ModifiedBy = entity.ModifiedBy;
                Utility.ModifiedDate = DateTime.Now;
                Utility.Status = entity.Status;
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
                var Utility = db.Utilitys.Find(id);
                db.Utilitys.Remove(Utility);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool ChangeStatus(int id)
        {
            var Utilitys = db.Utilitys.Find(id);
            Utilitys.Status = !Utilitys.Status;
            db.SaveChanges();
            return Utilitys.Status;
        }
    }
}
