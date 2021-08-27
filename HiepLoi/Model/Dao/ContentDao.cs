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
    public class ContentDao
    {
        OnlineShopDbContext db = null;
        public static string USER_SESSION = "USER_SESSION";
        public ContentDao()
        {
            db = new OnlineShopDbContext();
        }

        public IEnumerable<Content> ListAllPaging(Content entity, string searchString, ref int totalRecord, int page, int pageSize)
        {
            IQueryable<Content> model = db.Contents;
            if (entity != null)
            {
                if (entity.StatusId != null)
                {
                    model = model.Where(x => x.StatusId == entity.StatusId);
                }
                if (entity.DonViID != null)
                {
                    model = model.Where(x => x.DonViID == entity.DonViID);
                }
                if (entity.CategoryID != 0 && entity.CategoryID != null)
                {
                    model = model.Where(x => x.CategoryID == entity.CategoryID);
                }
                if (!string.IsNullOrEmpty(entity.Name) && entity.Name != "")
                {
                    model = model.Where(x => x.Name.Contains(entity.Name));
                }
            }
            totalRecord = model.Count();
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        /// <summary>
        /// List all content for client
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<Content> ListAllPaging(int page, int pageSize)
        {
            IQueryable<Content> model = db.Contents;
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        public IEnumerable<Content> ListAll(Content entity)
        {
            IQueryable<Content> model = db.Contents;
            if (entity != null)
            {
                if (entity.StatusId != null)
                {
                    model = model.Where(x => x.StatusId == entity.StatusId);
                }
                if (entity.CategoryID != null)
                {
                    model = model.Where(x => x.CategoryID == entity.CategoryID);
                }
                if (entity.DonViID != null)
                {
                    model = model.Where(x => x.DonViID == entity.DonViID);
                }
                if (entity.ListStatusId != null && entity.ListStatusId.Count>0)
                {
                    model = model.Where(x=> entity.ListStatusId.Any(y => y == x.StatusId));
                }
            }
            return model.OrderByDescending(x => x.ID);
        }
        public IEnumerable<Content> ListAllByTag(string tag, int page, int pageSize)
        {
            var model = (from a in db.Contents
                         join b in db.ContentTags
                         on a.ID equals b.ContentID
                         where b.TagID == tag
                         select new
                         {
                             Name = a.Name,
                             MetaTitle = a.MetaTitle,
                             Image = a.Image,
                             Description = a.Description,
                             CreatedDate = a.CreatedDate,
                             CreatedBy = a.CreatedBy,
                             ID = a.ID

                         }).AsEnumerable().Select(x => new Content()
                         {
                             Name = x.Name,
                             MetaTitle = x.MetaTitle,
                             Image = x.Image,
                             Description = x.Description,
                             CreatedDate = x.CreatedDate,
                             CreatedBy = x.CreatedBy,
                             ID = x.ID
                         });
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        public Content GetByID(long id)
        {
            return db.Contents.Find(id);
        }

        public Tag GetTag(string id)
        {
            return db.Tags.Find(id);
        }
        public long Create(Content content)
        {
            //Xử lý alias
            if (string.IsNullOrEmpty(content.MetaTitle))
            {
                content.MetaTitle = StringHelper.ToUnsignString(content.Name);
            }
            content.CreatedDate = DateTime.Now;
            content.ViewCount = 0;
            content.StatusId = 1;
            db.Contents.Add(content);
            db.SaveChanges();

            //Xử lý tag
            if (!string.IsNullOrEmpty(content.Tags))
            {
                string[] tags = content.Tags.Split(',');
                foreach (var tag in tags)
                {
                    var tagId = StringHelper.ToUnsignString(tag);
                    var existedTag = this.CheckTag(tagId);

                    //insert to to tag table
                    if (!existedTag)
                    {
                        this.InsertTag(tagId, tag);
                    }

                    //insert to content tag
                    this.InsertContentTag(content.ID, tagId);

                }
            }

            return content.ID;
        }
        public long CreateGioiThieu(Content content)
        {
            //Xử lý alias
            if (string.IsNullOrEmpty(content.MetaTitle))
            {
                content.MetaTitle = StringHelper.ToUnsignString(content.Name);
            }
            content.CreatedDate = DateTime.Now;
            content.ViewCount = 0;
            db.Contents.Add(content);
            db.SaveChanges();

            //Xử lý tag
            if (!string.IsNullOrEmpty(content.Tags))
            {
                string[] tags = content.Tags.Split(',');
                foreach (var tag in tags)
                {
                    var tagId = StringHelper.ToUnsignString(tag);
                    var existedTag = this.CheckTag(tagId);

                    //insert to to tag table
                    if (!existedTag)
                    {
                        this.InsertTag(tagId, tag);
                    }

                    //insert to content tag
                    this.InsertContentTag(content.ID, tagId);

                }
            }

            return content.ID;
        }
        public bool Update(Content entity)
        {
            try
            {
                var Content = db.Contents.Find(entity.ID);
                Content.ModifiedDate = DateTime.Now;
                Content.Name = entity.Name;
                Content.Description = entity.Description;
                Content.Image = entity.Image;
                Content.CategoryID = entity.CategoryID;
                Content.Detail = entity.Detail;
                //Content.Status = entity.Status;
                Content.Author = entity.Author;
                Content.StatusId = entity.StatusId;
                Content.DonViID = Content.DonViID;
                if (entity.PublishedDate !=null)
                {
                    Content.PublishedDate = entity.PublishedDate;
                }
                if (entity.eEffectiveDate != null)
                {
                    Content.eEffectiveDate = entity.eEffectiveDate;
                }
                if (entity.bEffectiveDate != null)
                {
                    Content.bEffectiveDate = entity.bEffectiveDate;
                }
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //logging
                return false;
            }

        }
        public bool UpdateCountView(Content entity)
        {
            try
            {
                var Content = db.Contents.Find(entity.ID);
                Content.ViewCount = entity.ViewCount;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //logging
                return false;
            }

        }
        public long Edit(Content content)
        {
            //Xử lý alias
            if (string.IsNullOrEmpty(content.MetaTitle))
            {
                content.MetaTitle = StringHelper.ToUnsignString(content.Name);
            }
            content.CreatedDate = DateTime.Now;
            db.SaveChanges();

            //Xử lý tag
            if (!string.IsNullOrEmpty(content.Tags))
            {
                this.RemoveAllContentTag(content.ID);
                string[] tags = content.Tags.Split(',');
                foreach (var tag in tags)
                {
                    var tagId = StringHelper.ToUnsignString(tag);
                    var existedTag = this.CheckTag(tagId);

                    //insert to to tag table
                    if (!existedTag)
                    {
                        this.InsertTag(tagId, tag);
                    }

                    //insert to content tag
                    this.InsertContentTag(content.ID, tagId);

                }
            }

            return content.ID;
        }
        public bool Delete(int id)
        {
            try
            {
                var content = db.Contents.Find(id);
                db.Contents.Remove(content);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public void RemoveAllContentTag(long contentId)
        {
            db.ContentTags.RemoveRange(db.ContentTags.Where(x => x.ContentID == contentId));
            db.SaveChanges();
        }
        public void InsertTag(string id, string name)
        {
            var tag = new Tag();
            tag.ID = id;
            tag.Name = name;
            db.Tags.Add(tag);
            db.SaveChanges();
        }

        public void InsertContentTag(long contentId, string tagId)
        {
            var contentTag = new ContentTag();
            contentTag.ContentID = contentId;
            contentTag.TagID = tagId;
            db.ContentTags.Add(contentTag);
            db.SaveChanges();
        }
        public bool CheckTag(string id)
        {
            return db.Tags.Count(x => x.ID == id) > 0;
        }

        public List<Tag> ListTag(long contentId)
        {
            var model = (from a in db.Tags
                         join b in db.ContentTags
                         on a.ID equals b.TagID
                         where b.ContentID == contentId
                         select new
                         {
                             ID = b.TagID,
                             Name = a.Name
                         }).AsEnumerable().Select(x => new Tag()
                         {
                             ID = x.ID,
                             Name = x.Name
                         });
            return model.ToList();
        }

        public Content ViewDetail(int id)
        {
            return db.Contents.Find(id);
        }
    }
}
