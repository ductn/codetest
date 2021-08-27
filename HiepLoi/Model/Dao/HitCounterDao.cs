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
    public class HitCounterDao
    {
        OnlineShopDbContext db = null;
        public static string USER_SESSION = "USER_SESSION";
        public HitCounterDao()
        {
            db = new OnlineShopDbContext();
        }

        public bool Insert(HitCounter entity)
        {
            db.HitCounters.Add(entity);
            db.SaveChanges();
            return true;
        }

        public IEnumerable<HitCounter> ListAllPaging(string searchString, ref int totalRecord, int page, int pageSize)
        {
            IQueryable<HitCounter> model = db.HitCounters;
            if (!string.IsNullOrEmpty(searchString))
            {
            }
            totalRecord = model.Count();
            return model.OrderByDescending(x => x.Date).ToPagedList(page, pageSize);
        }
        public IEnumerable<HitCounter> ListAll(HitCounter entity)
        {
            IQueryable<HitCounter> model = db.HitCounters;
            if (entity != null)
            {
                if (entity.Date != null)
                {
                    model = model.Where(x => x.Date == entity.Date);
                }
            }
            return model.OrderByDescending(x => x.Date);
        }

        public HitCounter GetById(DateTime id)
        {
            DateTime date = new DateTime(id.Year, id.Month, id.Day);
            return db.HitCounters.Find(date);
        }
        public HitCounter ViewDetail(int id)
        {
            return db.HitCounters.Find(id);
        }
        public bool Delete(int id)
        {
            try
            {
                var HitCounter = db.HitCounters.Find(id);
                db.HitCounters.Remove(HitCounter);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
         public bool Update(HitCounter entity)
        {
            try
            {
                var HitCounter = db.HitCounters.Find(entity.Date);
                HitCounter.Counter = entity.Counter;
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
