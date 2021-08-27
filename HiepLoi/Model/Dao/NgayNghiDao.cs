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
    public class NgayNghiDao
    {
        OnlineShopDbContext db = null;
        public static string USER_SESSION = "USER_SESSION";
        public NgayNghiDao()
        {
            db = new OnlineShopDbContext();
        }

        public long Insert(NgayNghi entity)
        {
            db.NgayNghis.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }

        public IEnumerable<NgayNghi> ListAllPaging(string searchString, ref int totalRecord, int page, int pageSize)
        {
            IQueryable<NgayNghi> model = db.NgayNghis;
            //if (!string.IsNullOrEmpty(searchString))
            //{
            //    model = model.Where(x => x.Name.Contains(searchString) || x.Code.Contains(searchString));
            //}
            totalRecord = model.Count();
            return model.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
        }

        public NgayNghi GetById(int id)
        {
            return db.NgayNghis.Find(id);
        }
        public NgayNghi ViewDetail(int id)
        {
            return db.NgayNghis.Find(id);
        }
        public bool Delete(int id)
        {
            try
            {
                var NgayNghi = db.NgayNghis.Find(id);
                db.NgayNghis.Remove(NgayNghi);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
         public bool Update(NgayNghi entity)
        {
            try
            {
                var NgayNghi = db.NgayNghis.Find(entity.Id);
                NgayNghi.Ngay = entity.Ngay;
                NgayNghi.MoTa = entity.MoTa;
                //NgayNghi.Code = entity.Code;
                //NgayNghi.Name = entity.Name;
                //NgayNghi.Modified = entity.Modified;
                //NgayNghi.Modifier = entity.Modifier;
                //NgayNghi.Desription = entity.Desription;
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
