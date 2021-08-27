using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using PagedList;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ContactDao
    {
        OnlineShopDbContext db = null;
        public ContactDao()
        {
            db = new OnlineShopDbContext();
        }
        public IEnumerable<Contact> ListAllPaging(Contact entity, string searchString, ref int totalRecord, int page, int pageSize)
        {
            IQueryable<Contact> model = db.Contacts;
            if (entity != null)
            {
                if (entity.StatusId != null)
                {
                    model = model.Where(x => x.StatusId == entity.StatusId);
                }
                if (!string.IsNullOrEmpty(entity.Title))
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
        /// List all Contact for client
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<Contact> ListAllPaging(int page, int pageSize)
        {
            IQueryable<Contact> model = db.Contacts;
            return model.OrderByDescending(x => x.ID).ToPagedList(page, pageSize);
        }
        public List<Contact> ListAll(Contact entity)
        {
            IQueryable<Contact> model = db.Contacts;
            if (entity != null)
            {
                if (entity.StatusId != null)
                {
                    model = model.Where(x => x.StatusId == entity.StatusId);
                }
                if (!string.IsNullOrEmpty(entity.Title))
                {
                    model = model.Where(x => x.Title.Contains(entity.Title));
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

        public Contact GetByID(long id)
        {
            return db.Contacts.Find(id);
        }

        public Tag GetTag(string id)
        {
            return db.Tags.Find(id);
        }
        public long Create(Contact Contact)
        {
            Contact.CreatedDate = DateTime.Now;
            Contact.StatusId = 1;
            Contact.Status = true;
            db.Contacts.Add(Contact);
            db.SaveChanges();
            return Contact.ID;
        }
        public bool Update(Contact entity)
        {
            try
            {
                var Contact = db.Contacts.Find(entity.ID);
                Contact.ModifiedDate = DateTime.Now;
                Contact.Title = entity.Title;
                Contact.Content = entity.Content;
                Contact.Status = entity.Status;
                Contact.Email = entity.Email;
                Contact.Name = entity.Name;
                Contact.Phone = entity.Phone;
                Contact.AnswerContent = entity.AnswerContent;
                Contact.FileActtach = entity.FileActtach;
                Contact.Phone = entity.Phone;
                Contact.StatusId = entity.StatusId;
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
                var Contact = db.Contacts.Find(id);
                db.Contacts.Remove(Contact);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public Contact GetActiveContact()
        {
            return db.Contacts.Single(x => x.Status == true);
        }

        public int InsertFeedBack(Feedback fb)
        {
            db.Feedbacks.Add(fb);
            db.SaveChanges();
            return fb.ID;
        }
    }
}
