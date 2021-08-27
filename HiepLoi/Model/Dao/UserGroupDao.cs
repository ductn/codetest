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
    public class UserGroupDao
    {
        OnlineShopDbContext db = null;
        public static string USER_SESSION = "USER_SESSION";
        public UserGroupDao()
        {
            db = new OnlineShopDbContext();
        }

        public String Insert(UserGroup entity)
        {
            db.UserGroups.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public IEnumerable<UserGroup> ListAllPaging(string searchString, ref int totalRecord, int page, int pageSize)
        {
            IQueryable<UserGroup> model = db.UserGroups;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            totalRecord = model.Count();
            return model.OrderByDescending(x => x.ID).ToPagedList(page, pageSize);
        }
        public List<UserGroup> ListAll(UserGroup entity)
        {
            IQueryable<UserGroup> model = db.UserGroups;
            if (entity != null)
            {
                //if (!string.IsNullOrEmpty(entity.Code))
                //{
                //    model = model.Where(x => x.Code.Contains(entity.Code) || x.Code.Contains(entity.Code));
                //}
            }
            return model.OrderByDescending(x => x.ID).ToList();
        }
        public UserGroup GetById(String ID)
        {
            return db.UserGroups.Find(ID);
        }
        public UserGroup ViewDetail(String ID)
        {
            return db.UserGroups.Find(ID);
        }
        public bool Delete(String ID)
        {
            try
            {
                var UserGroup = db.UserGroups.Find(ID);
                db.UserGroups.Remove(UserGroup);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
         public bool Update(UserGroup entity)
        {
            try
            {
                var UserGroup = db.UserGroups.Find(entity.ID);
                UserGroup.ID = entity.ID;
                UserGroup.Name = entity.Name;
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
