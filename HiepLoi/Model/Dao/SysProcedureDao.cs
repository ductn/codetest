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
    public class SysProcedureDao
    {
        OnlineShopDbContext db = null;
        public static string USER_SESSION = "USER_SESSION";
        public SysProcedureDao()
        {
            db = new OnlineShopDbContext();
        }

        public long Insert(SysProcedure entity)
        {
            db.SysProcedures.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }

        public IEnumerable<SysProcedure> ListAllPaging(string searchString, ref int totalRecord, int page, int pageSize)
        {
            IQueryable<SysProcedure> model = db.SysProcedures;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Title.Contains(searchString));
            }
            totalRecord = model.Count();
            return model.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
        }
        public List<SysProcedure> ListAll(SysProcedure entity)
        {
            IQueryable<SysProcedure> model = db.SysProcedures;
            if (entity != null)
            {
                //if (!string.IsNullOrEmpty(entity.Code))
                //{
                //    model = model.Where(x => x.Code.Contains(entity.Code) || x.Code.Contains(entity.Code));
                //}
            }
            return model.OrderByDescending(x => x.Id).ToList();
        }
        public SysProcedure GetById(int id)
        {
            return db.SysProcedures.Find(id);
        }
        public SysProcedure ViewDetail(int id)
        {
            return db.SysProcedures.Find(id);
        }
        public bool Delete(int id)
        {
            try
            {
                var SysProcedure = db.SysProcedures.Find(id);
                db.SysProcedures.Remove(SysProcedure);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
         public bool Update(SysProcedure entity)
        {
            try
            {
                var SysProcedure = db.SysProcedures.Find(entity.Id);
                SysProcedure.Title = entity.Title;
                SysProcedure.Code = entity.Code;
                SysProcedure.Sort = entity.Sort;
                SysProcedure.Description = entity.Description;
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
