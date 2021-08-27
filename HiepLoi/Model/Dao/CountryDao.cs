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
    public class CountryDao
    {
        OnlineShopDbContext db = null;
        public static string USER_SESSION = "USER_SESSION";
        public CountryDao()
        {
            db = new OnlineShopDbContext();
        }

        public long Insert(Country entity)
        {
            db.Countries.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }

        public IEnumerable<Country> ListAllPaging(Country entity,string searchString, ref int totalRecord, int page, int pageSize)
        {
            IQueryable<Country> model = db.Countries;
            if(entity != null)
            {
                if(entity.Code != null && entity.Code != "")
                {
                    model = model.Where(x => x.Code.Contains(entity.Code));
                }
                if (entity.Name != null && entity.Name != "")
                {
                    model = model.Where(x => x.Name.Contains(entity.Name));
                }
            }
            totalRecord = model.Count();
            return model.OrderByDescending(x => x.Created).ToPagedList(page, pageSize);
        }
        public List<Country> ListAll(Country entity)
        {
            IQueryable<Country> model = db.Countries;
            if (entity != null)
            {
                if (entity.Code != null && entity.Code != "")
                {
                    model = model.Where(x => x.Code.Contains(entity.Code));
                }
                if (entity.Name != null && entity.Name != "")
                {
                    model = model.Where(x => x.Name.Contains(entity.Name));
                }
            }
            return model.OrderByDescending(x => x.Created).ToList();
        }
        public Country GetById(int id)
        {
            return db.Countries.Find(id);
        }
        public Country ViewDetail(int id)
        {
            return db.Countries.Find(id);
        }
        public bool Delete(int id)
        {
            try
            {
                var Country = db.Countries.Find(id);
                db.Countries.Remove(Country);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
         public bool Update(Country entity)
        {
            try
            {
                var Country = db.Countries.Find(entity.Id);
                Country.Code = entity.Code;
                Country.Name = entity.Name;
                Country.Modified = entity.Modified;
                Country.Modifier = entity.Modifier;
                Country.Desription = entity.Desription;
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
