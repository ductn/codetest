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
    public class MaterialDao
    {
        OnlineShopDbContext db = null;
        public MaterialDao()
        {
            db = new OnlineShopDbContext();
        }
        public IEnumerable<Material> ListAllPaging(Material searchString,ref int totalRecord, int pageIndex, int pageSize)
        {
            IQueryable<Material> model = db.Materials;
            if (!string.IsNullOrEmpty(searchString.Name))
            {
                model = model.Where(x => x.Name.ToUpper().Contains(searchString.Name.ToUpper()) );
            }
            if (!string.IsNullOrEmpty(searchString.Status.ToString()))
            {
                var statuBool = bool.Parse(searchString.Status.ToString());
                model = model.Where(x => x.Status == statuBool);
            }
            
            totalRecord = model.Count();
            return model.OrderByDescending(x => x.Id).ToPagedList(pageIndex, pageSize);
        }
        public List<Material> ListAll(Material entity)
        {
            IQueryable<Material> model = db.Materials;
            if (entity != null)
            {
                //if (!string.IsNullOrEmpty(entity.Code))
                //{
                //    model = model.Where(x => x.Code.Contains(entity.Code) || x.Code.Contains(entity.Code));
                //}
            }
            return model.OrderByDescending(x => x.Id).ToList();
        }
        public Material GetById(int id)
        {
            return db.Materials.SingleOrDefault(x => x.Id == id);
        }
        public Material ViewDetail(int id)
        {
            return db.Materials.Find(id);
        }
        public long Insert(Material entity)
        {
            db.Materials.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }
        public bool Update(Material entity)
        {
            try
            {
                var Material = db.Materials.Find(entity.Id);
                Material.Code = entity.Code;
                Material.Name = entity.Name;
                Material.Desription = entity.Desription;
                Material.Sort = entity.Sort;
                Material.ModifiedBy = entity.ModifiedBy;
                Material.ModifiedDate = DateTime.Now;
                Material.Status = entity.Status;
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
                var Material = db.Materials.Find(id);
                db.Materials.Remove(Material);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool ChangeStatus(int id)
        {
            var Materials = db.Materials.Find(id);
            Materials.Status = !Materials.Status;
            db.SaveChanges();
            return Materials.Status;
        }
    }
}
