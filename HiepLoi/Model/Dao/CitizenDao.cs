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
    public class CitizenDao
    {
        OnlineShopDbContext db = null;
        public static string USER_SESSION = "USER_SESSION";
        public CitizenDao()
        {
            db = new OnlineShopDbContext();
        }

        public long Insert(Citizen entity)
        {
            db.Citizens.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }

        public IEnumerable<Citizen> ListAllPaging(string searchString, ref int totalRecord, int page, int pageSize)
        {
            IQueryable<Citizen> model = db.Citizens;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Code.Contains(searchString));
            }
            totalRecord = model.Count();
            return model.OrderByDescending(x => x.Created).ToPagedList(page, pageSize);
        }

        public Citizen GetById(int id)
        {
            return db.Citizens.Find(id);
        }
        public Citizen ViewDetail(int id)
        {
            return db.Citizens.Find(id);
        }
        public bool Delete(int id)
        {
            try
            {
                var Citizen = db.Citizens.Find(id);
                db.Citizens.Remove(Citizen);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
         public bool Update(Citizen entity)
        {
            try
            {
                var Citizen = db.Citizens.Find(entity.Id);
                Citizen.Code = entity.Code;
                Citizen.Name = entity.Name;
                Citizen.Modified = entity.Modified;
                Citizen.Modifier = entity.Modifier;
                Citizen.Desription = entity.Desription;
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
