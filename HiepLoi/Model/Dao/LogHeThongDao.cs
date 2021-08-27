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
    public class LogHeThongDao
    {
        OnlineShopDbContext db = null;
        public static string USER_SESSION = "USER_SESSION";
        public LogHeThongDao()
        {
            db = new OnlineShopDbContext();
        }

        public long Insert(LogHeThong entity)
        {
            db.LogHeThongs.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }

        public IEnumerable<LogHeThong> ListAllPaging(string searchString, ref int totalRecord, int page, int pageSize)
        {
            IQueryable<LogHeThong> model = db.LogHeThongs;
            //if (!string.IsNullOrEmpty(searchString))
            //{
            //    model = model.Where(x => x.Name.Contains(searchString) || x.Code.Contains(searchString));
            //}
            totalRecord = model.Count();
            return model.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
        }

        public LogHeThong GetById(int id)
        {
            return db.LogHeThongs.Find(id);
        }
        public LogHeThong ViewDetail(int id)
        {
            return db.LogHeThongs.Find(id);
        }
        public bool Delete(int id)
        {
            try
            {
                var LogHeThong = db.LogHeThongs.Find(id);
                db.LogHeThongs.Remove(LogHeThong);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
         public bool Update(LogHeThong entity)
        {
            try
            {
                var LogHeThong = db.LogHeThongs.Find(entity.Id);
                LogHeThong.NoiDung = entity.NoiDung;
                LogHeThong.ChucNang = entity.ChucNang;
                LogHeThong.SuKien = entity.SuKien;
                LogHeThong.ThoiDiem = entity.ThoiDiem;
                LogHeThong.Ip = entity.Ip;
                LogHeThong.NguoiDung = entity.NguoiDung;
                //LogHeThong.Code = entity.Code;
                //LogHeThong.Name = entity.Name;
                //LogHeThong.Modified = entity.Modified;
                //LogHeThong.Modifier = entity.Modifier;
                //LogHeThong.Desription = entity.Desription;
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
