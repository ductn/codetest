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
    public class StoreDao
    {
        OnlineShopDbContext db = null;
        public static string USER_SESSION = "USER_SESSION";
        public StoreDao()
        {
            db = new OnlineShopDbContext();
        }

        public IEnumerable<Store> ListAllPaging(Store entity, string searchString, ref int totalRecord, int page, int pageSize)
        {
            IQueryable<Store> model = db.Stores;
            if (entity != null)
            {
                if (entity.StatusId != null)
                {
                    model = model.Where(x => x.StatusId == entity.StatusId);
                }
                if (entity.Title != null)
                {
                    model = model.Where(x => x.Title.Contains(entity.Title));
                }
                //if (entity.DonViID != null)
                //{
                //    model = model.Where(x => x.DonViID == entity.DonViID);
                //}
            }
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Title.Contains(searchString));
            }
            totalRecord = model.Count();
            return model.OrderByDescending(x => x.StoreId).ToPagedList(page, pageSize);
        }
        /// <summary>
        /// List all Store for client
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<Store> ListAllPaging(int page, int pageSize)
        {
            IQueryable<Store> model = db.Stores;
            return model.OrderByDescending(x => x.StoreId).ToPagedList(page, pageSize);
        }
        public List<Store> ListAll(Store entity)
        {
            IQueryable<Store> model = db.Stores;
            if (entity != null)
            {
                if (entity.StatusId != null)
                {
                    model = model.Where(x => x.StatusId == entity.StatusId);
                }
                if (entity.NganhNgheId != null)
                {
                    model = model.Where(x => x.NganhNgheId == entity.NganhNgheId);
                }
                if (entity.LinhVucKinhDoanhId != null)
                {
                    model = model.Where(x => x.LinhVucKinhDoanhId == entity.LinhVucKinhDoanhId);
                }
                if (entity.UserId != null)
                {
                    model = model.Where(x => x.UserId == entity.UserId);
                }
                if (entity.ListStatusId != null && entity.ListStatusId.Count > 0)
                {
                    model = model.Where(x => entity.ListStatusId.Any(y => y == x.StatusId));
                }
            }
            return model.OrderByDescending(x => x.StoreId).ToList();
        }

        public Store GetByID(long? id)
        {
            return db.Stores.Find(id);
        }
        public Store GetByUserId(int userId)
        {
            return db.Stores.SingleOrDefault(x => x.UserId == userId);
        }
        public Tag GetTag(string id)
        {
            return db.Tags.Find(id);
        }
        public long Create(Store Store)
        {
            if (!string.IsNullOrEmpty(Store.QueryString))
            {
                Store.QueryString = StringHelper.ToUnsignString(Store.Title);
            }
            Store.CreatedDate = DateTime.Now;
            db.Stores.Add(Store);
            db.SaveChanges();
            return Store.StoreId;
        }
        public bool Update(Store entity)
        {
            try
            {
                var Store = db.Stores.Find(entity.StoreId);
                if (!string.IsNullOrEmpty(entity.Title))
                {
                    entity.QueryString = StringHelper.ToUnsignString(entity.Title);
                }
                Store.ModifiedDate = DateTime.Now;
                Store.Title = entity.Title;
                Store.QueryString = entity.QueryString;
                Store.Description = entity.Description;
                Store.DiaChi = entity.DiaChi;
                Store.Slogan = entity.Slogan;
                Store.URLWEB = entity.URLWEB;
                Store.Email = entity.Email;
                Store.ImgLogo = entity.ImgLogo;
                Store.Zalo = entity.Zalo;
                Store.SkypeId = entity.SkypeId;
                Store.HoTen = entity.HoTen;
                Store.Phone = entity.Phone;
                Store.PhoneOther = entity.PhoneOther;
                Store.HeaderImage = entity.HeaderImage;
                Store.Map = entity.Map;
                //Store.NganhNgheId = entity.NganhNgheId;
                Store.NganhNgheName = entity.NganhNgheName;
                Store.QuyMoVon = entity.QuyMoVon;
                Store.NhanLuc = entity.NhanLuc;
                Store.DoanhThu = entity.DoanhThu;
                Store.LoiNhuan = entity.LoiNhuan;
                Store.StoreImage = entity.StoreImage;
                Store.StatusId = entity.StatusId;
                Store.LinhVucKinhDoanhId = entity.LinhVucKinhDoanhId;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //logging
                return false;
            }

        }
        public bool UpdateCountView(Store entity)
        {
            try
            {
                var Store = db.Stores.Find(entity.StoreId);
                Store.CountView = entity.CountView;
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
                var Store = db.Stores.Find(id);
                db.Stores.Remove(Store);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool ChangeStatusQT(int idStore, int nextStatus)
        {
            try
            {
                var Store = db.Stores.Find(idStore);
                Store.StatusId = nextStatus;
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
