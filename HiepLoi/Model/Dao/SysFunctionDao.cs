using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class SysFunctionDao
    {
        OnlineShopDbContext db = null;
        public SysFunctionDao()
        {
            db = new OnlineShopDbContext();
        }

        public IEnumerable<SysFunction> ListAllPaging(SysFunction searchString, ref int ParentId, ref int totalRecord, int pageIndex, int pageSize)
        {
            IQueryable<SysFunction> model = db.SysFunctions;
            if (!string.IsNullOrEmpty(searchString.FunctionName))
            {
                model = model.Where(x => x.FunctionName.ToUpper().Contains(searchString.FunctionName.ToUpper()) || x.Link.Contains(searchString.FunctionName));
            }
            if (!string.IsNullOrEmpty(searchString.Status.ToString()))
            {
                var statuBool = bool.Parse(searchString.Status.ToString());
                model = model.Where(x => x.Status == statuBool);
            }
            if (searchString.ParentId >= 0)
            {
                model = model.Where(x => x.ParentId == searchString.ParentId);
                try
                {
                    if (searchString.ParentId != 0)
                    {
                        ParentId = (int)db.SysFunctions.SingleOrDefault(x => x.FunctionID == searchString.ParentId).ParentId;
                    }
                }
                catch (Exception)
                {

                }
            }
            totalRecord = model.Count();
            return model.OrderBy(x=>x.isModule).ThenBy(x => x.DisplayOrder).ToPagedList(pageIndex, pageSize);
        }
        public List<SysFunction> ListAll(SysFunction entity)
        {
            IQueryable<SysFunction> model = db.SysFunctions;
            if (entity != null)
            {
                //if (!string.IsNullOrEmpty(entity.Code))
                //{
                //    model = model.Where(x => x.Code.Contains(entity.Code) || x.Code.Contains(entity.Code));
                //}
            }
            return model.OrderByDescending(x => x.FunctionID).ToList();
        }
        public SysFunction GetById(int functionid)
        {
            return db.SysFunctions.SingleOrDefault(x => x.FunctionID == functionid);
        }
        public IEnumerable<SysFunction> GetByParentId(int ParentId)
        {
            return db.SysFunctions.Where(x => x.ParentId == ParentId).OrderBy(x=>x.DisplayOrder);
        }
        public SysFunction ViewDetail(int id)
        {
            return db.SysFunctions.Find(id);
        }
        public long Insert(SysFunction entity)
        {
            db.SysFunctions.Add(entity);
            db.SaveChanges();
            return entity.FunctionID;
        }
        public bool Update(SysFunction entity)
        {
            try
            {
                var sysFunction = db.SysFunctions.Find(entity.FunctionID);
                sysFunction.FunctionName = entity.FunctionName;
                sysFunction.Link = entity.Link;
                sysFunction.Target = entity.Target;
                sysFunction.ParentId = entity.ParentId;
                sysFunction.DisplayOrder = entity.DisplayOrder;
                sysFunction.Icons = entity.Icons;
                sysFunction.TypeID = entity.TypeID;
                sysFunction.isController = entity.isController;
                sysFunction.isModule = entity.isModule;
                sysFunction.RoleID = entity.RoleID;
                sysFunction.Status = entity.Status;
                sysFunction.ColorModule = entity.ColorModule;
                sysFunction.NameModule = entity.NameModule;
                sysFunction.IconsModule = entity.IconsModule;
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
                var sysFunction = db.SysFunctions.Find(id);
                db.SysFunctions.Remove(sysFunction);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool ChangeStatus(long id)
        {
            var sysFunctions = db.SysFunctions.Find(id);
            sysFunctions.Status = !sysFunctions.Status;
            db.SaveChanges();
            return sysFunctions.Status;
        }
    }
}
