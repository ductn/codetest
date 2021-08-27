using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.EF;
using System.Threading.Tasks;
using PagedList;

namespace Model.Dao
{
   public  class SliderDao
    {
        OnlineShopDbContext db = null;
        public SliderDao()
        {
            db = new OnlineShopDbContext();
        }
        public long Insert(Slider entity)
        {
            if (GetSort(entity) != null)
            {
                entity.DisplayOrder = GetSort(entity).DisplayOrder + 1;
            }
            else
            {
                entity.DisplayOrder = 1;
            }

            db.Sliders.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public IEnumerable<Slider> ListAllPaging(string searchString, ref int totalRecord, int page, int pageSize)
        {
            IQueryable<Slider> model = db.Sliders;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            totalRecord = model.Count();
            return model.OrderBy(x => x.IsType).ThenBy(x => x.DisplayOrder).ToPagedList(page, pageSize);
        }
        public IEnumerable<Slider> ListAll(Slider entity)
        {
            IQueryable<Slider> model = db.Sliders;
            if (entity != null)
            {
            }
            return model.OrderByDescending(x => x.ID);
        }
        public IEnumerable<Slider> ListByIsType(int _IsType,int _Take)
        {
            IQueryable<Slider> model = db.Sliders;
            return model.Where(x => x.IsType == _IsType && x.Status == true).OrderBy(x => x.DisplayOrder).Skip(0).Take(_Take);
        }
        public Slider GetSort(Slider entity)
        {
            IQueryable<Slider> model = db.Sliders;
            return model.Where(x => x.IsType == entity.IsType).OrderByDescending(x => x.DisplayOrder).ToPagedList(1, 1).FirstOrDefault();
        }
        public Slider GetById(int id)
        {
            return db.Sliders.Find(id);
        }
        public Slider ViewDetail(int id)
        {
            return db.Sliders.Find(id);
        }
        public bool Delete(int id)
        {
            try
            {
                var slide = db.Sliders.Find(id);
                db.Sliders.Remove(slide);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool Update(Slider entity)
        {
            try
            {
                var slide = db.Sliders.Find(entity.ID);
                slide.IsType = entity.IsType;
                slide.Name = entity.Name;
                slide.Image = entity.Image;
                slide.DisplayOrder = entity.DisplayOrder;
                slide.Link = entity.Link;
                slide.Description = entity.Description;
                slide.ModifiedBy = entity.ModifiedBy;
                slide.ModifiedDate = DateTime.Now;
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
