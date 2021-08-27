using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using PagedList;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ContactStoreDao
    {
        OnlineShopDbContext db = null;
        public ContactStoreDao()
        {
            db = new OnlineShopDbContext();
        }
        public IEnumerable<ContactStore> ListAllPaging(ContactStore entity, string searchString, ref int totalRecord, int page, int pageSize)
        {
            IQueryable<ContactStore> model = db.ContactStores;
            if (entity != null)
            {
                if (entity.StatusId != null)
                {
                    model = model.Where(x => x.StatusId == entity.StatusId);
                }
                if (!string.IsNullOrEmpty(entity.Title) && entity.Title != "")
                {
                    model = model.Where(x => x.Title.Contains(entity.Title));
                }
                //if (entity.DonViID != null)
                //{
                //    model = model.Where(x => x.DonViID == entity.DonViID);
                //}
            }
            totalRecord = model.Count();
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        /// <summary>
        /// List all ContactStore for client
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<ContactStore> ListAllPaging(int page, int pageSize)
        {
            IQueryable<ContactStore> model = db.ContactStores;
            return model.OrderByDescending(x => x.ID).ToPagedList(page, pageSize);
        }
        public List<ContactStore> ListAll(ContactStore entity)
        {
            IQueryable<ContactStore> model = db.ContactStores;
            if (entity != null)
            {
                if (entity.StatusId != null)
                {
                    model = model.Where(x => x.StatusId == entity.StatusId);
                }
                //if (entity.DonViID != null)
                //{
                //    model = model.Where(x => x.DonViID == entity.DonViID);
                //}
                //if (entity.ListStatusId != null && entity.ListStatusId.Count>0)
                //{
                //    model = model.Where(x=> entity.ListStatusId.Any(y => y == x.StatusId));
                //}
            }
            return model.OrderByDescending(x => x.ID).ToList();
        }

        public ContactStore GetByID(long id)
        {
            return db.ContactStores.Find(id);
        }

        public Tag GetTag(string id)
        {
            return db.Tags.Find(id);
        }
        public long Create(ContactStore ContactStore)
        {
            ContactStore.CreatedDate = DateTime.Now;
            ContactStore.StatusId = 1;
            ContactStore.IsAnswer = false;
            ContactStore.Status = true;
            db.ContactStores.Add(ContactStore);
            db.SaveChanges();
            return ContactStore.ID;
        }
        public bool Update(ContactStore entity)
        {
            try
            {
                var ContactStore = db.ContactStores.Find(entity.ID);
                ContactStore.ModifiedDate = DateTime.Now;
                ContactStore.Title = entity.Title;
                ContactStore.Content = entity.Content;
                ContactStore.Status = entity.Status;
                ContactStore.Email = entity.Email;
                ContactStore.Name = entity.Name;
                ContactStore.Phone = entity.Phone;
                ContactStore.AnswerContent = entity.AnswerContent;
                ContactStore.FileActtach = entity.FileActtach;
                ContactStore.Phone = entity.Phone;
                ContactStore.StatusId = entity.StatusId;
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
                var ContactStore = db.ContactStores.Find(id);
                db.ContactStores.Remove(ContactStore);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public ContactStore GetActiveContactStore()
        {
            return db.ContactStores.Single(x => x.Status == true);
        }

        public int InsertFeedBack(Feedback fb)
        {
            db.Feedbacks.Add(fb);
            db.SaveChanges();
            return fb.ID;
        }
    }
}
