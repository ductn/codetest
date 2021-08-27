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
    public class BusinessPartnerDao
    {
        OnlineShopDbContext db = null;
        public static string USER_SESSION = "USER_SESSION";
        public BusinessPartnerDao()
        {
            db = new OnlineShopDbContext();
        }

        public IEnumerable<BusinessPartner> ListAllPaging(BusinessPartner entity, string searchString, ref int totalRecord, int page, int pageSize)
        {
            IQueryable<BusinessPartner> model = db.BusinessPartners;
            if (entity != null)
            {
                if (entity.StatusId != null && entity.StatusId != "")
                {
                    model = model.Where(x => x.StatusId == entity.StatusId);
                }
                if (entity.TieuDe != null && entity.TieuDe != "")
                {
                    model = model.Where(x => x.TieuDe == entity.TieuDe);
                }
                
            }
            totalRecord = model.Count();
            return model.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
        }
       
        public IEnumerable<BusinessPartner> ListAllPaging(int page, int pageSize)
        {
            IQueryable<BusinessPartner> model = db.BusinessPartners;
            return model.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
        }
        public IEnumerable<BusinessPartner> ListAll(BusinessPartner entity)
        {
            IQueryable<BusinessPartner> model = db.BusinessPartners;
            if (entity != null)
            {
                if (entity.StatusId != null && entity.StatusId != "")
                {
                    model = model.Where(x => x.StatusId == entity.StatusId);
                }
                if (entity.TieuDe != null && entity.TieuDe != "")
                {
                    model = model.Where(x => x.TieuDe == entity.TieuDe);
                }

            }
            return model.OrderByDescending(x => x.Id);
        }

        public IEnumerable<BusinessPartner> ListByStatusId(String _StatusId, int _Take)
        {
            IQueryable<BusinessPartner> model = db.BusinessPartners;
            return model.Where(x => x.StatusId == _StatusId).OrderBy(x => x.Id).Skip(0).Take(_Take);
        }

        public BusinessPartner GetByID(long id)
        {
            return db.BusinessPartners.Find(id);
        }

        public BusinessPartner ViewDetail(int id)
        {
            return db.BusinessPartners.Find(id);
        }
        public Tag GetTag(string id)
        {
            return db.Tags.Find(id);
        }
        public long Insert(BusinessPartner entity)
        {
            entity.Image = entity.Image.Replace(CommonConstants.ApplicationName, "");
            db.BusinessPartners.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }
        public long Create(BusinessPartner BusinessPartner)
        {
            BusinessPartner.Image = BusinessPartner.Image.Replace(CommonConstants.ApplicationName, "");
            BusinessPartner.StatusId = "3";
            db.BusinessPartners.Add(BusinessPartner);
            db.SaveChanges();
            return BusinessPartner.Id;
        }
        public bool Update(BusinessPartner entity)
        {
            try
            {
                var BusinessPartner = db.BusinessPartners.Find(entity.Id);
                BusinessPartner.TieuDe = entity.TieuDe;
                BusinessPartner.Url = entity.Url;
                BusinessPartner.Slogan = entity.Slogan;
                BusinessPartner.DiaChi = entity.DiaChi;
                BusinessPartner.SDT = entity.SDT;
                BusinessPartner.Email = entity.Email;
                BusinessPartner.Image = entity.Image.Replace(CommonConstants.ApplicationName, "");
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                //logging
                return false;
            }

        }
        public bool Delete(int id)
        {
            try
            {
                var BusinessPartner = db.BusinessPartners.Find(id);
                db.BusinessPartners.Remove(BusinessPartner);
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
