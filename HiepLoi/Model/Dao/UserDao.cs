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
    public class UserDao
    {
        OnlineShopDbContext db = null;
        public UserDao()
        {
            db = new OnlineShopDbContext();
        }

        public long Insert(User entity)
        {
            entity.CreatedDate = DateTime.Now;
            db.Users.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public long InsertForFacebook(User entity)
        {
            var user = db.Users.SingleOrDefault(x => x.UserName == entity.UserName);
            if (user == null)
            {
                entity.CreatedDate = DateTime.Now;
                db.Users.Add(entity);
                db.SaveChanges();
                return entity.ID;
            }
            else
            {
                return user.ID;
            }

        }
        public bool Update(User entity)
        {
            try
            {
                var user = db.Users.Find(entity.ID);
                
                if (!string.IsNullOrEmpty(entity.Password))
                {
                    user.Password = entity.Password;
                }
                user.Name = entity.Name;
                user.Phone = entity.Phone;
                user.Address = entity.Address;
                user.Email = entity.Email;
                user.Avatar = entity.Avatar;
                user.urlFileDinhKem = entity.urlFileDinhKem;
                user.tenFileDinhKem = entity.tenFileDinhKem;
                user.checkXacMinh = entity.checkXacMinh;
                user.ModifiedBy = entity.ModifiedBy;
                user.ModifiedDate = DateTime.Now;
                user.UnitCode = entity.UnitCode;
                user.ChucVu = entity.ChucVu;
                user.ProvinceID = entity.ProvinceID;
                user.HuyenID = entity.HuyenID;
                user.XaID = entity.XaID;
                user.Status = entity.Status;
                user.IsLock = entity.IsLock;
                user.ckHo = entity.ckHo;
                user.ckDN = entity.ckDN;
                user.ckHTX = entity.ckHTX;
                db.SaveChanges();
                return true;
        }
            catch (Exception ex)
            {
                //logging
                return false;
            }

}

        public IEnumerable<User> ListAllPaging(User entity, string searchString, ref int totalRecord, int page, int pageSize)
        {
            IQueryable<User> model = db.Users;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.UserName.Contains(searchString) || x.Name.Contains(searchString));
            }
            if (entity != null)
            {
                if (entity.UserName!=null)
                {
                    model = model.Where(x => x.UserName.Contains(entity.UserName) || x.Name.Contains(entity.UserName));
                }
                if (entity.UnitCode != null && entity.UnitCode !="0")
                {
                    model = model.Where(x => x.UnitCode == entity.UnitCode);
                }
                if (entity.loaiTK != null && entity.loaiTK !="all")
                {
                    if (entity.loaiTK == "cb")//Tài khoản sở ban ngành
                    {
                        model = model.Where(x => x.GroupID != CommonConstants.ADMIN_GROUP && x.GroupID != CommonConstants.NGUOIDUNGCONGKHAI_GROUP && x.GroupID != CommonConstants.MEMBER_GROUP);
                    }
                    if (entity.loaiTK == "ck")//Tài khoản doanh nghiệp
                    {
                        model = model.Where(x => x.GroupID == CommonConstants.NGUOIDUNGCONGKHAI_GROUP);
                    }
                }
                if (entity.IsLock == true)
                {
                    model = model.Where(x => x.IsLock == true);
                }
            }

            totalRecord = model.Count();
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        public IEnumerable<User> ListAllXacMinhPaging(User entity, string searchString, ref int totalRecord, int page, int pageSize)
        {
            IQueryable<User> model = db.Users;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.UserName.Contains(searchString) || x.Name.Contains(searchString));
            }
            if (entity != null)
            {
                if (entity.UserName != null)
                {
                    model = model.Where(x => x.UserName.Contains(entity.UserName) || x.Name.Contains(entity.UserName));
                }
                if (entity.UnitCode != null && entity.UnitCode != "0")
                {
                    model = model.Where(x => x.UnitCode == entity.UnitCode);
                }
                if (entity.loaiTK != null && entity.loaiTK != "all")
                {
                    if (entity.loaiTK == "cb")//Tài khoản sở ban ngành
                    {
                        model = model.Where(x => x.GroupID != CommonConstants.ADMIN_GROUP && x.GroupID != CommonConstants.NGUOIDUNGCONGKHAI_GROUP && x.GroupID != CommonConstants.MEMBER_GROUP);
                    }
                    if (entity.loaiTK == "ck")//Tài khoản doanh nghiệp
                    {
                        model = model.Where(x => x.GroupID == CommonConstants.NGUOIDUNGCONGKHAI_GROUP);
                    }
                }
                if (entity.IsLock == true)
                {
                    model = model.Where(x => x.IsLock == true);
                }
                model = model.Where(x => x.Status == entity.Status);
                model = model.Where(x => x.checkXacMinh == entity.checkXacMinh);
            }

            totalRecord = model.Count();
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        public IEnumerable<User> ListAllXacMinh(User entity)
        {
            IQueryable<User> model = db.Users;
            if (entity != null)
            {
                if (entity.UserName != null)
                {
                    model = model.Where(x => x.UserName == entity.UserName);
                }
                if (entity.UnitCode != null && entity.UnitCode != "0")
                {
                    model = model.Where(x => x.UnitCode == entity.UnitCode);
                }
                if (entity.loaiTK != null && entity.loaiTK != "all")
                {
                    if (entity.loaiTK == "cb")//Tài khoản sở ban ngành
                    {
                        model = model.Where(x => x.GroupID != CommonConstants.ADMIN_GROUP && x.GroupID != CommonConstants.NGUOIDUNGCONGKHAI_GROUP && x.GroupID != CommonConstants.MEMBER_GROUP);
                    }
                    if (entity.loaiTK == "ck")//Tài khoản doanh nghiệp
                    {
                        model = model.Where(x => x.GroupID == CommonConstants.NGUOIDUNGCONGKHAI_GROUP);
                    }
                }
                if (entity.IsLock == true)
                {
                    model = model.Where(x => x.IsLock == true);
                }
                model = model.Where(x => x.Status == entity.Status);
                model = model.Where(x => x.checkXacMinh == entity.checkXacMinh);
            }
            return model.OrderByDescending(x => x.ID);
        }
        public IEnumerable<User> ListAll(User entity)
        {
            IQueryable<User> model = db.Users;
            if (entity != null)
            {
                if (entity.UserName != null)
                {
                    model = model.Where(x => x.UserName == entity.UserName);
                }
                if (entity.UnitCode != null && entity.UnitCode != "0")
                {
                    model = model.Where(x => x.UnitCode == entity.UnitCode);
                }
                if (entity.loaiTK != null && entity.loaiTK != "all")
                {
                    if (entity.loaiTK == "cb")//Tài khoản sở ban ngành
                    {
                        model = model.Where(x => x.GroupID != CommonConstants.ADMIN_GROUP && x.GroupID != CommonConstants.NGUOIDUNGCONGKHAI_GROUP && x.GroupID != CommonConstants.MEMBER_GROUP);
                    }
                    if (entity.loaiTK == "ck")//Tài khoản doanh nghiệp
                    {
                        model = model.Where(x => x.GroupID == CommonConstants.NGUOIDUNGCONGKHAI_GROUP);
                    }
                }
                if (entity.IsLock == true)
                {
                    model = model.Where(x => x.IsLock == true);
                }
            }
            return model.OrderByDescending(x => x.ID);
        }

        public User GetById(int ID)
        {
            return db.Users.SingleOrDefault(x => x.ID == ID);
        }
        public User GetByUserName(string userName)
        {
            return db.Users.SingleOrDefault(x => x.UserName == userName);
        }
        public User GetByEmail(string email)
        {
            return db.Users.SingleOrDefault(x => x.Email == email);
        }
        public User GetByToKen(int userid , string token)
        {
            return db.Users.SingleOrDefault(x => x.ID == userid && x.tokenQuenMK == token);
        }
        public User GetByAutoRememberLogin(string AutoRememberLogin)
        {
            return db.Users.SingleOrDefault(x => x.AutoRememberLogin == AutoRememberLogin);
        }
        public User ViewDetail(int id)
        {
            return db.Users.Find(id);
        }
        public List<string> GetListCredential(string userName)
        {
            var user = db.Users.Single(x => x.UserName == userName);
            var data = (from a in db.Credentials
                        join b in db.UserGroups on a.UserGroupID equals b.ID
                        join c in db.Roles on a.RoleID equals c.ID
                        where b.ID == user.GroupID
                        select new
                        {
                            RoleID = a.RoleID,
                            UserGroupID = a.UserGroupID
                        }).AsEnumerable().Select(x => new Credential()
                        {
                            RoleID = x.RoleID,
                            UserGroupID = x.UserGroupID
                        });
            return data.Select(x => x.RoleID).ToList();

        }

        public IEnumerable<SysFunction> GetListCredentialSysFunction(string userName)
        {
            var user = db.Users.Single(x => x.UserName == userName);
            var data = (from a in db.Credentials
                        join b in db.SysFunctions on a.RoleID equals b.RoleID
                        where a.UserGroupID == user.GroupID
                        select new
                        {
                            FunctionID = b.FunctionID,
                            FunctionName = b.FunctionName,
                            Link = b.Link,
                            Target = b.Target,
                            ParentId = b.ParentId,
                            DisplayOrder = b.DisplayOrder,
                            Icons = b.Icons,
                            TypeID = b.TypeID,
                            isController = b.isController,
                            isModule = b.isModule,
                            RoleID = b.RoleID,
                            Status = b.Status,
                            ColorModule = b.ColorModule,
                            IconsModule = b.IconsModule,
                            NameModule = b.NameModule
                        }).AsEnumerable().Select(x => new SysFunction()
                        {
                            FunctionID = x.FunctionID,
                            FunctionName = x.FunctionName,
                            Link = x.Link,
                            Target = x.Target,
                            ParentId = x.ParentId,
                            DisplayOrder = x.DisplayOrder,
                            Icons = x.Icons,
                            TypeID = x.TypeID,
                            isController = x.isController,
                            isModule = x.isModule,
                            RoleID = x.RoleID,
                            Status = x.Status,
                            ColorModule = x.ColorModule,
                            IconsModule = x.IconsModule,
                            NameModule = x.NameModule
                        });
            return data.Where(x=>x.Status == true).OrderBy(x=>x.isModule).OrderBy(x=>x.DisplayOrder).ToList();

        }
        public int Login(string userName, string passWord, bool isLoginAdmin = false)
        {
            var result = db.Users.SingleOrDefault(x => x.UserName == userName);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (isLoginAdmin == true)
                {
                    if (result.GroupID == CommonConstants.ADMIN_GROUP 
                        || result.GroupID == CommonConstants.MOD_GROUP 
                        || result.GroupID == CommonConstants.BIENTAP_GROUP
                        || result.GroupID == CommonConstants.PHEDUYET_GROUP
                        || result.GroupID == CommonConstants.QLGIANHANG_GROUP
                        || result.GroupID == CommonConstants.QLLIENHE_GROUP
                        || result.GroupID == CommonConstants.QLPHANQUYEN_GROUP
                        || result.GroupID == CommonConstants.QLCONGKHAI_GROUP
                        || result.GroupID == CommonConstants.LANHDAOSO_GROUP)
                    {
                        if (result.Status == false)
                        {
                            return -1;
                        }
                        else
                        {
                            if (result.Password == passWord)
                                return 1;
                            else
                                return -2;
                        }
                    }
                    else
                    {
                        return -3;
                    }
                }
                else
                {
                    if (result.Status == false)
                    {
                        return -1;
                    }
                    else
                    {
                        if (result.Password == passWord)
                            return 1;
                        else
                            return -2;
                    }
                }
            }
        }
        
        public bool ChangeStatus(long id)
        {
            var user = db.Users.Find(id);
            user.Status = !user.Status;
            db.SaveChanges();
            return user.Status;
        }
        public bool Delete(int id)
        {
            try
            {
                var user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool CheckUserName(string userName)
        {
            return db.Users.Count(x => x.UserName == userName) > 0;
        }
        public bool CheckUserName(int UserId , string Password)
        {
            return db.Users.Count(x => x.ID == UserId && x.Password == Password) > 0;
        }
        public User CheckLogin(string userName, string passWord)
        {
            return db.Users.SingleOrDefault(x => x.UserName == userName && x.Password == passWord);
        }
        public bool CheckEmail(string email)
        {
            return db.Users.Count(x => x.Email == email) > 0;
        }
    }
}
