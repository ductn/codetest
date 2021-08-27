using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using PagedList;
using System.Configuration;
using ClsCommon;

namespace Model.Dao
{
    public class ProvinceDao
    {
        OnlineShopDbContext db = null;
        public ProvinceDao()
        {
            db = new OnlineShopDbContext();
        }

        public long Insert(Province entity)
        {
            db.Provinces.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }
        //public long InsertForFacebook(Province entity)
        //{
        //    var Province = db.Provinces.SingleOrDefault(x => x.ProvinceName == entity.ProvinceName);
        //    if (Province == null)
        //    {
        //        db.Provinces.Add(entity);
        //        db.SaveChanges();
        //        return entity.ID;
        //    }
        //    else
        //    {
        //        return Province.ID;
        //    }

        //}
        //public bool Update(Province entity)
        //{
        //    try
        //    {
        //        var Province = db.Provinces.Find(entity.ID);
        //        Province.Name = entity.Name;
        //        if (!string.IsNullOrEmpty(entity.Password))
        //        {
        //            Province.Password = entity.Password;
        //        }
        //        Province.Address = entity.Address;
        //        Province.Email = entity.Email;
        //        Province.ModifiedBy = entity.ModifiedBy;
        //        Province.ModifiedDate = DateTime.Now;
        //        db.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        //logging
        //        return false;
        //    }

        //}
        public List<Province> ListByParentId(int ParentId)
        {
            int _ParentId = 0;
            if (ParentId != 0)
            {
                Province item = db.Provinces.Where(x => x.Id == ParentId).SingleOrDefault();
                if (item != null)
                {
                    _ParentId = (int)item.ParentId;
                }
            }
            return db.Provinces.Where(x => x.ParentId == _ParentId).ToList();
        }
        public IEnumerable<Province> ListAllPaging(int parentid, ref int totalRecord, string searchString, int page, int pageSize)
        {
            IQueryable<Province> model = db.Provinces;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Title.Contains(searchString) || x.Title.Contains(searchString));
            }
            if (parentid>=0)
            {
                model = model.Where(x => x.ParentId == parentid);
            }
            totalRecord = model.Count();
            return model.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
        }
        public List<Province> ListAll(Province entity)
        {
            IQueryable<Province> model = db.Provinces;
            if (entity != null)
            {
                if (entity.ParentId != null)
                {
                    model = model.Where(x => x.ParentId == entity.ParentId);
                }
                if (entity.Title != null)
                {
                    model = model.Where(x => x.Title.ToUpper() == entity.Title.ToUpper());
                }
            }
            return model.OrderByDescending(x => x.Id).ToList();
        }
        public Province GetById(int id)
        {
            return db.Provinces.Find(id);
        }
        public Province ViewDetail(int id)
        {
            return db.Provinces.Find(id);
        }
        //public List<string> GetListCredential(string ProvinceName)
        //{
        //    var Province = db.Provinces.Single(x => x.ProvinceName == ProvinceName);
        //    var data = (from a in db.Credentials
        //                join b in db.ProvinceGroups on a.ProvinceGroupID equals b.ID
        //                join c in db.Roles on a.RoleID equals c.ID
        //                where b.ID == Province.GroupID
        //                select new
        //                {
        //                    RoleID = a.RoleID,
        //                    ProvinceGroupID = a.ProvinceGroupID
        //                }).AsEnumerable().Select(x => new Credential()
        //                {
        //                    RoleID = x.RoleID,
        //                    ProvinceGroupID = x.ProvinceGroupID
        //                });
        //    return data.Select(x => x.RoleID).ToList();

        //}


        //public bool ChangeStatus(long id)
        //{
        //    var Province = db.Provinces.Find(id);
        //    Province.Status = !Province.Status;
        //    db.SaveChanges();
        //    return Province.Status;
        //}
        public bool Delete(int id)
        {
            try
            {
                var Province = db.Provinces.Find(id);
                db.Provinces.Remove(Province);
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
