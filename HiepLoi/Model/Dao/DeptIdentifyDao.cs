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
    public class DeptIdentifyDao
    {
        OnlineShopDbContext db = null;
        public static string USER_SESSION = "USER_SESSION";
        public DeptIdentifyDao()
        {
            db = new OnlineShopDbContext();
        }

        public long Insert(DeptIdentify entity)
        {
            if (GetSort() != null)
            {
                entity.Sort = GetSort().Sort + 1;
            }
            else
            {
                entity.Sort = 0;
            }
            db.DeptIdentifries.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }

        public IEnumerable<DeptIdentify> ListAllPaging(string searchString, ref int totalRecord, int page, int pageSize)
        {
            IQueryable<DeptIdentify> model = db.DeptIdentifries;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            totalRecord = model.Count();
            return model.OrderByDescending(x => x.Created).ToPagedList(page, pageSize);
        }

        public DeptIdentify GetSort()
        {
            IQueryable<DeptIdentify> model = db.DeptIdentifries;
            return model.OrderByDescending(x => x.Sort).ToPagedList(1, 1).FirstOrDefault();
        }

        public DeptIdentify GetById(int id)
        {
            return db.DeptIdentifries.Find(id);
        }
        public DeptIdentify ViewDetail(int id)
        {
            return db.DeptIdentifries.Find(id);
        }
        public bool Delete(int id)
        {
            try
            {
                var DeptIdentify = db.DeptIdentifries.Find(id);
                db.DeptIdentifries.Remove(DeptIdentify);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
         public bool Update(DeptIdentify entity)
        {
            try
            {
                var DeptIdentify = db.DeptIdentifries.Find(entity.Id);
                DeptIdentify.Name = entity.Name;
                DeptIdentify.Code = entity.Code;
                DeptIdentify.Desription = entity.Desription;
                DeptIdentify.Sort = entity.Sort;
                DeptIdentify.Modified = entity.Modified;
                DeptIdentify.Modifier = entity.Modifier;
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
