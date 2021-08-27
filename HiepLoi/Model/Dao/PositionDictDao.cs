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
    public class PositionDictDao
    {
        OnlineShopDbContext db = null;
        public static string USER_SESSION = "USER_SESSION";
        public PositionDictDao()
        {
            db = new OnlineShopDbContext();
        }

        public long Insert(PositionDict entity)
        {
            db.PositionDicts.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }

        public IEnumerable<PositionDict> ListAllPaging(string searchString, ref int totalRecord, int page, int pageSize)
        {
            IQueryable<PositionDict> model = db.PositionDicts;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Code.Contains(searchString));
            }
            totalRecord = model.Count();
            return model.OrderByDescending(x => x.Created).ToPagedList(page, pageSize);
        }
        public List<PositionDict> ListAll()
        {
            IQueryable<PositionDict> model = db.PositionDicts;
            return model.OrderByDescending(x => x.Created).ToList();
        }
        public PositionDict GetById(int id)
        {
            return db.PositionDicts.Find(id);
        }
        public PositionDict ViewDetail(int id)
        {
            return db.PositionDicts.Find(id);
        }
        public bool Delete(int id)
        {
            try
            {
                var PositionDict = db.PositionDicts.Find(id);
                db.PositionDicts.Remove(PositionDict);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
         public bool Update(PositionDict entity)
        {
            try
            {
                var PositionDict = db.PositionDicts.Find(entity.Id);
                PositionDict.Code = entity.Code;
                PositionDict.Name = entity.Name;
                PositionDict.Modified = entity.Modified;
                PositionDict.Modifier = entity.Modifier;
                PositionDict.Desription = entity.Desription;
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
