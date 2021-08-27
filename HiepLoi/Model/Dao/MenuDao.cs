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
    public class MenuDao
    {
        OnlineShopDbContext db = null;
        public MenuDao()
        {
            db = new OnlineShopDbContext();
        }
        public List<Menu> ListByParentId(int ParentId)
        {
            int _ParentId = 0;
            if (ParentId != 0)
            {
                Menu item = db.Menus.Where(x => x.ID == ParentId).SingleOrDefault();
                if (item != null)
                {
                    _ParentId = (int)item.ParentId;
                }
            }
            return db.Menus.Where(x => x.ParentId == _ParentId).ToList();
        }
        public List<Menu> ListAll(Menu entity)
        {
            IQueryable<Menu> model = db.Menus;
            model = model.Where(x => x.Status == true);
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(entity.Link))
                {
                    model = model.Where(x => x.Link == entity.Link);
                }
                if (entity.ParentId != 0)
                {
                    model = model.Where(x => x.ParentId == entity.ParentId);
                }
                if (entity.TypeID != 0)
                {
                    model = model.Where(x => x.TypeID == entity.TypeID);
                }
            }
            return model.ToList();
        }
        public IEnumerable<Menu> ListAllPaging(Menu entity, int? parentid, ref int totalRecord, string searchString, int page, int pageSize)
        {
            IQueryable<Menu> model = db.Menus;
            model = model.Where(x => x.Status == true);
            if (parentid >= 0)
            {
                model = model.Where(x => x.ParentId == parentid);
            }
            totalRecord = model.Count();
            return model.OrderByDescending(x => x.ID).ToPagedList(page, pageSize);
        }

        public Menu GetById(long? id)
        {
            return db.Menus.Find(id);
        }

        public Tag GetTag(string id)
        {
            return db.Tags.Find(id);
        }
        public long Insert(Menu entity)
        {
            db.Menus.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public long Create(Menu Menu)
        {
            db.Menus.Add(Menu);
            db.SaveChanges();
            return Menu.ID;
        }
        public bool Update(Menu entity)
        {
            try
            {
                var Menu = db.Menus.Find(entity.ID);
                Menu.Text = entity.Text;
                Menu.Link = entity.Link;
                Menu.DisplayOrder = entity.DisplayOrder;
                Menu.Target = entity.Target;
                Menu.Status = entity.Status;
                Menu.IsProduct = entity.IsProduct;
                Menu.TypeID = entity.TypeID;
                Menu.Detail = entity.Detail;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //logging
                return false;
            }

        }
        public Menu ViewDetail(int id)
        {
            return db.Menus.Find(id);
        }
        public bool Delete(int id)
        {
            try
            {
                var Menu = db.Menus.Find(id);
                db.Menus.Remove(Menu);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public List<Menu> ListByGroupId(int groupId)
        {
            return db.Menus.Where(x => x.TypeID == groupId && x.Status==true).OrderBy(x=>x.DisplayOrder).ToList();
        }

        public List<MenuType> ListMenuType()
        {
            IQueryable<MenuType> model = db.MenuTypes;
            return model.ToList();
        }
        public MenuType ViewDetailMenuType(int id)
        {
            return db.MenuTypes.Find(id);
        }
    }
}
