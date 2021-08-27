using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class RoleDao
    {
        OnlineShopDbContext db = null;
        public RoleDao()
        {
            db = new OnlineShopDbContext();
        }

        public IEnumerable<Role> ListAllPaging(Role searchString, ref int totalRecord, int pageIndex, int pageSize)
        {
            IQueryable<Role> model = db.Roles;
            if (!string.IsNullOrEmpty(searchString.ID))
            {
                model = model.Where(x => x.ID.Contains(searchString.ID) || x.Name.Contains(searchString.ID));
            }
            totalRecord = model.Count();
            return model.OrderBy(x=>x.ID).ToPagedList(pageIndex, pageSize);
        }
        public List<Role> ListAll(Role entity)
        {
            IQueryable<Role> model = db.Roles;
            if (entity != null)
            {
                if (entity.SysFunctionID >0)
                {
                    model = model.Where(x => x.SysFunctionID == entity.SysFunctionID);
                }
            }
            return model.OrderByDescending(x => x.ID).ToList();
        }
        public Role GetById(string id)
        {
            return db.Roles.SingleOrDefault(x => x.ID == id);
        }
        public IEnumerable<Role> GetBySysFunction(int id)
        {
            return db.Roles.Where(x => x.SysFunctionID == id).OrderBy(x=>x.ID);
        }
        public Role ViewDetail(string id)
        {
            return db.Roles.Find(id);
        }
        public string Insert(Role entity)
        {
            db.Roles.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public bool Update(Role entity)
        {
            try
            {
                var Role = db.Roles.Find(entity.ID);
                Role.Name = entity.Name;
                Role.SysFunctionID = entity.SysFunctionID;
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
                var Role = db.Roles.Find(id);
                db.Roles.Remove(Role);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
