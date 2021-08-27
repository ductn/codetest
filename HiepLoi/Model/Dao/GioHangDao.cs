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
    public class GioHangDao
    {
        OnlineShopDbContext db = null;
        public static string USER_SESSION = "USER_SESSION";
        public GioHangDao()
        {
            db = new OnlineShopDbContext();
        }

        public long Insert(GioHang entity)
        {
            db.GioHangs.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }

        public IEnumerable<GioHang> ListAllPaging(string searchString, ref int totalRecord, int page, int pageSize)
        {
            IQueryable<GioHang> model = db.GioHangs;
            //if (!string.IsNullOrEmpty(searchString))
            //{
            //    model = model.Where(x => x.Name.Contains(searchString));
            //}
            totalRecord = model.Count();
            return model.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
        }
        public IEnumerable<GioHang> ListAllPaging(GioHang entity, string searchString, ref int totalRecord, int page, int pageSize)
        {
            IQueryable<GioHang> model = db.GioHangs;
            if (entity != null)
            {
                if (entity.UserKhach != null)
                {
                    model = model.Where(x => x.UserKhach == entity.UserKhach);
                }
                if (entity.IdProduct != null)
                {
                    model = model.Where(x => x.IdProduct == entity.IdProduct);
                }
                if (entity.StorId != null)
                {
                    model = model.Where(x => x.StorId == entity.StorId);
                }
                if (entity.Status != null)
                {
                    model = model.Where(x => x.Status == entity.Status);
                }
            }
            totalRecord = model.Count();
            return model.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
        }

        public List<GioHang> ListAll(GioHang entity)
        {
            IQueryable<GioHang> model = db.GioHangs;
            if (entity != null)
            {
                if (entity.UserKhach != null)
                {
                    model = model.Where(x => x.UserKhach == entity.UserKhach);
                }
                if (entity.IdProduct != null)
                {
                    model = model.Where(x => x.IdProduct == entity.IdProduct);
                }
                if (entity.StorId != null)
                {
                    model = model.Where(x => x.StorId == entity.StorId);
                }
                if (entity.Status != null)
                {
                    model = model.Where(x => x.Status == entity.Status);
                }
            }
            return model.OrderByDescending(x => x.Id).ToList();
        }

        public GioHang GetById(int id)
        {
            return db.GioHangs.Find(id);
        }
        public GioHang ViewDetail(int id)
        {
            return db.GioHangs.Find(id);
        }
        public bool Delete(int id)
        {
            try
            {
                var GioHang = db.GioHangs.Find(id);
                db.GioHangs.Remove(GioHang);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
         public bool Update(GioHang entity)
        {
            try
            {
                var GioHang = db.GioHangs.Find(entity.Id);
                GioHang.Code = entity.Code;
                GioHang.UserKhach = entity.UserKhach;
                GioHang.TypeKhach = entity.TypeKhach;
                GioHang.IdProduct = entity.IdProduct;
                GioHang.SoLuong = entity.SoLuong;
                GioHang.SoDienThoai = entity.SoDienThoai;
                GioHang.Status = entity.Status;
                GioHang.StorId = entity.StorId;
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
