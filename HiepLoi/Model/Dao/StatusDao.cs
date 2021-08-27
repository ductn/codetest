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
    public class StatusDao
    {
        OnlineShopDbContext db = null;
        public static string USER_SESSION = "USER_SESSION";
        public StatusDao()
        {
            db = new OnlineShopDbContext();
        }

        public long Insert(Status entity)
        {
            db.Status.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }

        public IEnumerable<Status> ListAllPaging(Status entity, string searchString, ref int totalRecord, int page, int pageSize)
        {
            IQueryable<Status> model = db.Status;
            if (entity != null)
            {
                if(entity.IdSysProcedure != 0)
                {
                    model = model.Where(x => x.IdSysProcedure == entity.IdSysProcedure);
                }
            }
            totalRecord = model.Count();
            return model.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
        }
        public List<Status> ListAll(Status entity)
        {
            IQueryable<Status> model = db.Status;
            if (entity != null)
            {
                if (entity.IdSysProcedure != 0)
                {
                    model = model.Where(x => x.IdSysProcedure == entity.IdSysProcedure);
                }
                if (entity.StatusId != 0 && entity.StatusId != null)
                {
                    model = model.Where(x => x.StatusId == entity.StatusId);
                }
            }
            return model.OrderByDescending(x => x.Id).ToList();
        }
        public Status GetByStatusIdIdSysProcedure(int StatusId, int IdSysProcedure)
        {
            return db.Status.Where(x => x.StatusId == StatusId && x.IdSysProcedure == IdSysProcedure).FirstOrDefault();
        }
        public Status GetById(int id)
        {
            return db.Status.Find(id);
        }
        public Status ViewDetail(int id)
        {
            return db.Status.Find(id);
        }
        public bool Delete(int id)
        {
            try
            {
                var Status = db.Status.Find(id);
                db.Status.Remove(Status);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
         public bool Update(Status entity)
        {
            try
            {
                var Status = db.Status.Find(entity.Id);
                Status.StatusId = entity.StatusId;
                Status.Title = entity.Title;
                Status.IdSysProcedure = entity.IdSysProcedure;
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
