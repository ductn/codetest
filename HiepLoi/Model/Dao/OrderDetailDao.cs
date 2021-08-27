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
    public class OrderDetailDao
    {
        OnlineShopDbContext db = null;
        public OrderDetailDao()
        {
            db = new OnlineShopDbContext();
        }
        public bool Insert(OrderDetail detail)
        {
            try
            {
                db.OrderDetails.Add(detail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;

            }
        }
        public IEnumerable<OrderDetail> ListAllPaging(string searchString, ref int totalRecord, int page, int pageSize)
        {
            IQueryable<OrderDetail> model = db.OrderDetails;
            //if (!string.IsNullOrEmpty(searchString))
            //{
            //    model = model.Where(x => x.Name.Contains(searchString));
            //}
            totalRecord = model.Count();
            return model.OrderByDescending(x => x.OrderID).ToPagedList(page, pageSize);
        }

        public List<OrderDetail> ListAll(OrderDetail entity)
        {
            IQueryable<OrderDetail> model = db.OrderDetails;
            if (entity != null)
            {
                if (entity.Status != null)
                {
                    model = model.Where(x => x.Status == entity.Status);
                }
                if (entity.OrderID >0)
                {
                    model = model.Where(x => x.OrderID == entity.OrderID);
                }
            }
            return model.OrderByDescending(x => x.OrderID).ToList();
        }

        public OrderDetail GetById(int id)
        {
            return db.OrderDetails.Find(id);
        }
        public OrderDetail ViewDetail(int id)
        {
            return db.OrderDetails.Find(id);
        }
        public bool Delete(int id)
        {
            try
            {
                var OrderDetail = db.OrderDetails.Find(id);
                db.OrderDetails.Remove(OrderDetail);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool Update(OrderDetail entity)
        {
            try
            {
                var OrderDetail = db.OrderDetails.Find(entity.OrderID);
                OrderDetail.Code = entity.Code;
                OrderDetail.Quantity = entity.Quantity;
                OrderDetail.Price = entity.Price;
                OrderDetail.StoreId = entity.StoreId;
                OrderDetail.Amount = entity.Amount;
                OrderDetail.Code = entity.Code;
                OrderDetail.Status = entity.Status;
                OrderDetail.DeliveryDate = entity.DeliveryDate;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //logging
                return false;
            }

        }
        public bool ChangeStatusQT(OrderDetail entity, int nextStatus)
        {
            try
            {
                IQueryable<OrderDetail> model = db.OrderDetails;
                if (entity != null)
                {
                    if (entity.ProductID > 0)
                    {
                        model = model.Where(x => x.ProductID == entity.ProductID);
                    }
                    if (entity.OrderID>0)
                    {
                        model = model.Where(x => x.OrderID == entity.OrderID);
                    }
                }
                var orderDetail = model.FirstOrDefault();
                orderDetail.Status = nextStatus;
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
