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
    public class SysParameterDao
    {
        OnlineShopDbContext db = null;
        public static string USER_SESSION = "USER_SESSION";
        public SysParameterDao()
        {
            db = new OnlineShopDbContext();
        }

        public long Insert(SysParameter entity)
        {
            db.SysParameters.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }

        public IEnumerable<SysParameter> ListAllPaging(string searchString, ref int totalRecord, int page, int pageSize)
        {
            IQueryable<SysParameter> model = db.SysParameters;
            //if (!string.IsNullOrEmpty(searchString))
            //{
            //    model = model.Where(x => x.Name.Contains(searchString) || x.Code.Contains(searchString));
            //}
            totalRecord = model.Count();
            return model.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
        }

        public SysParameter GetById(int id)
        {
            return db.SysParameters.Find(id);
        }
        public SysParameter ViewDetail(int id)
        {
            return db.SysParameters.Find(id);
        }
        public bool Delete(int id)
        {
            try
            {
                var SysParameter = db.SysParameters.Find(id);
                db.SysParameters.Remove(SysParameter);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
         public bool Update(SysParameter entity)
        {
            try
            {
                var SysParameter = db.SysParameters.Find(entity.Id);
                SysParameter.TenThamSo = entity.TenThamSo;
                SysParameter.KieuDuLieu = entity.KieuDuLieu;
                SysParameter.GiaTri = entity.GiaTri;
                SysParameter.MoTa = entity.MoTa;
                SysParameter.KichHoat = entity.KichHoat;
                //SysParameter.Code = entity.Code;
                //SysParameter.Name = entity.Name;
                //SysParameter.Modified = entity.Modified;
                //SysParameter.Modifier = entity.Modifier;
                //SysParameter.Desription = entity.Desription;
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
