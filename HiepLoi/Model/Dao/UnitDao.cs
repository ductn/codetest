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
    public class UnitDao
    {
        OnlineShopDbContext db = null;
        public UnitDao()
        {
            db = new OnlineShopDbContext();
        }

        public long Insert(Unit entity)
        {
            db.Units.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }
        //public long InsertForFacebook(Unit entity)
        //{
        //    var Unit = db.Units.SingleOrDefault(x => x.UnitName == entity.UnitName);
        //    if (Unit == null)
        //    {
        //        db.Units.Add(entity);
        //        db.SaveChanges();
        //        return entity.ID;
        //    }
        //    else
        //    {
        //        return Unit.ID;
        //    }

        //}
        //public bool Update(Unit entity)
        //{
        //    try
        //    {
        //        var Unit = db.Units.Find(entity.ID);
        //        Unit.Name = entity.Name;
        //        if (!string.IsNullOrEmpty(entity.Password))
        //        {
        //            Unit.Password = entity.Password;
        //        }
        //        Unit.Address = entity.Address;
        //        Unit.Email = entity.Email;
        //        Unit.ModifiedBy = entity.ModifiedBy;
        //        Unit.ModifiedDate = DateTime.Now;
        //        db.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        //logging
        //        return false;
        //    }

        //}
        public List<Unit> ListByParentId(int ParentId)
        {
            int _ParentId = 0;
            if (ParentId != 0)
            {
                Unit item = db.Units.Where(x => x.Id == ParentId).SingleOrDefault();
                if (item != null)
                {
                    _ParentId = (int)item.ParentId;
                }
            }
            return db.Units.Where(x => x.ParentId == _ParentId).ToList();
        }
        public List<Unit> ListAll(Unit entity)
        {
            IQueryable<Unit> model = db.Units;
            if (entity !=null)
            {
                if (entity.CodeId != null)
                {
                    model = model.Where(x => x.CodeId == entity.CodeId);
                }
                if (!string.IsNullOrEmpty(entity.Code))
                {
                    model = model.Where(x => x.Code.Contains(entity.Code) || x.Code.Contains(entity.Code));
                }
                if (entity.Id != 0)
                {
                    model = model.Where(x => x.Id == entity.Id);
                }
            }
            return model.OrderByDescending(x => x.Id).ToList();
        }
        public List<Unit> ListShowOnHome()
        {
            return db.Units.Where(x => x.ShowOnHome == true).ToList();
        }
        public IEnumerable<Unit> ListAllPaging(Unit entity,int? parentid, ref int totalRecord, string searchString, int page, int pageSize)
        {
            IQueryable<Unit> model = db.Units;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Title.Contains(searchString) || x.Title.Contains(searchString));
            }
            if (parentid>=0)
            {
                model = model.Where(x => x.ParentId == parentid);
            }
            if (entity !=null)
            {
                if (entity.Title != null)
                {
                    model = model.Where(x => x.Title == entity.Title);
                }
            }
            totalRecord = model.Count();
            return model.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
        }
        public IEnumerable<Unit> ListAllPagingPhongBan(string searchString, ref int totalRecord, int page, int pageSize)
        {
            IQueryable<Unit> model = db.Units;
            //if (!string.IsNullOrEmpty(searchString))
            //{
            //    model = model.Where(x => x.Name.Contains(searchString) || x.Code.Contains(searchString));
            //}
            model = model.Where(x => x.ParentId != 0);
            totalRecord = model.Count();
            return model.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
        }
        public IEnumerable<Unit> ListAllPhongBanPaging(Unit entity, int? parentid, ref int totalRecord, string searchString, int page, int pageSize)
        {
            IQueryable<Unit> model = db.Units;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Title.Contains(searchString) || x.Title.Contains(searchString));
            }
            model = model.Where(x => x.ParentId != 0);
            if (entity != null)
            {
                if (entity.Title != null)
                {
                    model = model.Where(x => x.Title == entity.Title);
                }
            }
            totalRecord = model.Count();
            return model.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
        }

        public Unit GetById(int? id)
        {
            return db.Units.Find(id);
        }
        public Unit ViewDetail(int id)
        {
            return db.Units.Find(id);
        }

        public List<Unit> ListByIsDiaPhuong(int IsDiaPhuong)
        {
            List<Unit> rs;
            if (IsDiaPhuong != 0)
            {
                rs = db.Units.Where(x => x.IsDiaPhuong == IsDiaPhuong).OrderBy(x => x.Sort).ToList();
            }
            else
            {
                rs = null;
            }
            return rs;
        }
        //public List<string> GetListCredential(string UnitName)
        //{
        //    var Unit = db.Units.Single(x => x.UnitName == UnitName);
        //    var data = (from a in db.Credentials
        //                join b in db.UnitGroups on a.UnitGroupID equals b.ID
        //                join c in db.Roles on a.RoleID equals c.ID
        //                where b.ID == Unit.GroupID
        //                select new
        //                {
        //                    RoleID = a.RoleID,
        //                    UnitGroupID = a.UnitGroupID
        //                }).AsEnumerable().Select(x => new Credential()
        //                {
        //                    RoleID = x.RoleID,
        //                    UnitGroupID = x.UnitGroupID
        //                });
        //    return data.Select(x => x.RoleID).ToList();

        //}


        //public bool ChangeStatus(long id)
        //{
        //    var Unit = db.Units.Find(id);
        //    Unit.Status = !Unit.Status;
        //    db.SaveChanges();
        //    return Unit.Status;
        //}
        public bool Delete(int id)
        {
            try
            {
                var Unit = db.Units.Find(id);
                db.Units.Remove(Unit);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool Update(Unit entity)
        {
            try
            {
                var Unit = db.Units.Find(entity.Id);
                Unit.Icons = entity.Icons;
                Unit.Code = entity.Code;
                Unit.Title = entity.Title;
                Unit.ParentId = entity.ParentId;
                Unit.Sort = entity.Sort;
                Unit.DiaChi = entity.DiaChi;
                Unit.DienThoai = entity.DienThoai;
                Unit.Fax = entity.Fax;
                Unit.Email = entity.Email;
                Unit.Website = entity.Website;
                Unit.UnitLevel = entity.UnitLevel;
                Unit.ShowOnHome = entity.ShowOnHome;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //logging
                return false;
            }

        }
        public bool ChangeShowOnHome(int id)
        {
            var units = db.Units.Find(id);
            units.ShowOnHome = !units.ShowOnHome;
            db.SaveChanges();
            return units.ShowOnHome;
        }

    }
}
