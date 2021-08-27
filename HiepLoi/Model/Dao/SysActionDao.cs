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
    public class SysActionDao
    {
        OnlineShopDbContext db = null;
        public static string USER_SESSION = "USER_SESSION";
        public SysActionDao()
        {
            db = new OnlineShopDbContext();
        }

        public long Insert(SysAction entity)
        {
            db.SysActions.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }

        public IEnumerable<SysAction> ListAllPaging(SysAction entity, string searchString, ref int totalRecord, int page, int pageSize)
        {
            IQueryable<SysAction> model = db.SysActions;
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
        public List<SysAction> ListAll(SysAction entity)
        {
            IQueryable<SysAction> model = db.SysActions;
            if (entity != null)
            {
                if (entity.IdSysProcedure != 0)
                {
                    model = model.Where(x => x.IdSysProcedure == entity.IdSysProcedure);
                }
                if (entity.CurrentStatus != null)
                {
                    model = model.Where(x => x.CurrentStatus == entity.CurrentStatus);
                }
                if(entity.UserGroups != null)
                {
                    model = model.Where(x => x.UserGroups.Contains(entity.UserGroups));
                }
            }
            return model.OrderBy(x => x.DisplayOrder).ToList();
        }

        public SysAction GetById(int id)
        {
            return db.SysActions.Find(id);
        }
        public SysAction ViewDetail(int id)
        {
            return db.SysActions.Find(id);
        }
        public bool Delete(int id)
        {
            try
            {
                var SysAction = db.SysActions.Find(id);
                db.SysActions.Remove(SysAction);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
         public bool Update(SysAction entity)
        {
            try
            {
                var SysAction = db.SysActions.Find(entity.Id);
                SysAction.Title = entity.Title;
                SysAction.CurrentStatus = entity.CurrentStatus;
                SysAction.NextStatus = entity.NextStatus;
                SysAction.ButtonName = entity.ButtonName;
                SysAction.Description = entity.Description;
                SysAction.IdSysProcedure = entity.IdSysProcedure;
                SysAction.UserGroups = entity.UserGroups;
                SysAction.DisplayOrder = entity.DisplayOrder;
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
