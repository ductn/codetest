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
    public class TienTeDao
    {
        OnlineShopDbContext db = null;
        public static string USER_SESSION = "USER_SESSION";
        public TienTeDao()
        {
            db = new OnlineShopDbContext();
        }

        public long Insert(TienTe entity)
        {
            db.TienTes.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }

        public IEnumerable<TienTe> ListAllPaging(string searchString, ref int totalRecord, int page, int pageSize)
        {
            IQueryable<TienTe> model = db.TienTes;
            //if (!string.IsNullOrEmpty(searchString))
            //{
            //    model = model.Where(x => x.Name.Contains(searchString) || x.Code.Contains(searchString));
            //}
            totalRecord = model.Count();
            return model.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
        }

        public TienTe GetById(int id)
        {
            return db.TienTes.Find(id);
        }
        public TienTe ViewDetail(int id)
        {
            return db.TienTes.Find(id);
        }
        public bool Delete(int id)
        {
            try
            {
                var TienTe = db.TienTes.Find(id);
                db.TienTes.Remove(TienTe);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
         public bool Update(TienTe entity)
        {
            try
            {
                var TienTe = db.TienTes.Find(entity.Id);
                TienTe.TenTien = entity.TenTien;
                TienTe.MoTa = entity.MoTa;
                //TienTe.Code = entity.Code;
                //TienTe.Name = entity.Name;
                //TienTe.Modified = entity.Modified;
                //TienTe.Modifier = entity.Modifier;
                //TienTe.Desription = entity.Desription;
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
