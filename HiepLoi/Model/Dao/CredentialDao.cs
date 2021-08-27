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
    public class CredentialDao
    {
        OnlineShopDbContext db = null;
        public static string USER_SESSION = "USER_SESSION";
        public CredentialDao()
        {
            db = new OnlineShopDbContext();
        }

        public String Insert(Credential entity)
        {
            db.Credentials.Add(entity);
            db.SaveChanges();
            return entity.UserGroupID;
        }

        public IEnumerable<Credential> ListAllPaging(string searchString, ref int totalRecord, int page, int pageSize)
        {
            IQueryable<Credential> model = db.Credentials;
            if (!string.IsNullOrEmpty(searchString))
            {
                // model = model.Where(x => x.Name.Contains(searchString));
            }
            totalRecord = model.Count();
            return model.OrderByDescending(x => x.UserGroupID).ToPagedList(page, pageSize);
        }
        public List<Credential> ListAll(Credential entity)
        {
            IQueryable<Credential> model = db.Credentials;
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(entity.RoleID))
                {
                    model = model.Where(x => x.RoleID == entity.RoleID);
                }
                if (!string.IsNullOrEmpty(entity.UserGroupID))
                {
                    model = model.Where(x => x.UserGroupID == entity.UserGroupID);
                }
            }
            return model.OrderByDescending(x => x.UserGroupID).ToList();
        }
        public Credential GetById(String ID)
        {
            return db.Credentials.Find(ID);
        }
        public Credential ViewDetail(String ID)
        {
            return db.Credentials.Find(ID);
        }
        public bool Delete(String ID)
        {
            try
            {
                var Credential = db.Credentials.Find(ID);
                db.Credentials.Remove(Credential);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool DeleteEntity(Credential entity)
        {
            IQueryable<Credential> model = db.Credentials;
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(entity.RoleID))
                {
                    model = model.Where(x => x.RoleID == entity.RoleID);
                }
                if (!string.IsNullOrEmpty(entity.UserGroupID))
                {
                    model = model.Where(x => x.UserGroupID == entity.UserGroupID);
                }
            }
            var Credential = model.FirstOrDefault();
            try
            {
                db.Credentials.Remove(Credential);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool Update(Credential entity)
        {
            try
            {
                var Credential = db.Credentials.Find(entity.UserGroupID);
                Credential.UserGroupID = entity.UserGroupID;
                Credential.RoleID = entity.RoleID;
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
