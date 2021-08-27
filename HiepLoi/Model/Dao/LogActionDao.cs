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
    public class LogActionDao
    {
        OnlineShopDbContext db = null;
        public static string USER_SESSION = "USER_SESSION";
        public LogActionDao()
        {
            db = new OnlineShopDbContext();
        }

        public long Insert(LogAction entity)
        {
            db.LogActions.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }

        public IEnumerable<LogAction> ListAllPaging(string searchString, ref int totalRecord, int page, int pageSize)
        {
            IQueryable<LogAction> model = db.LogActions;
            if (!string.IsNullOrEmpty(searchString))
            {
                //model = model.Where(x => x.Title.Contains(searchString));
            }
            totalRecord = model.Count();
            return model.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
        }
        public List<LogAction> ListAll(LogAction entity)
        {
            IQueryable<LogAction> model = db.LogActions;
            if (entity != null)
            {
                if (entity.ObjId != null)
                {
                    model = model.Where(x => x.ObjId == entity.ObjId);
                }
                if (entity.SysProcedureId != null)
                {
                    model = model.Where(x => x.SysProcedureId == entity.SysProcedureId);
                }
            }
            return model.OrderBy(x => x.Id).ToList();
        }
        public LogAction GetById(int id)
        {
            return db.LogActions.Find(id);
        }
        public LogAction ViewDetail(int id)
        {
            return db.LogActions.Find(id);
        }
        public bool Delete(int id)
        {
            try
            {
                var LogAction = db.LogActions.Find(id);
                db.LogActions.Remove(LogAction);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
         public bool Update(LogAction entity)
        {
            try
            {
                var LogAction = db.LogActions.Find(entity.Id);
                LogAction.SysProcedureId = entity.SysProcedureId;
                LogAction.ObjId = entity.ObjId;
                LogAction.CreatorUserName = entity.CreatorUserName;
                LogAction.UserIdCreator = entity.UserIdCreator;
                LogAction.CreatorName = entity.CreatorName;
                LogAction.ReceiverUserName = entity.ReceiverUserName;
                LogAction.UserIdReceiver = entity.UserIdReceiver;
                LogAction.ReceiverName = entity.ReceiverName;
                LogAction.Created = DateTime.Now;
                LogAction.PublishedDate = entity.PublishedDate;
                LogAction.eEffectiveDate = entity.eEffectiveDate;
                LogAction.CurrentStatusId = entity.CurrentStatusId;
                LogAction.ActionId = entity.ActionId;
                LogAction.Note = entity.Note;
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
