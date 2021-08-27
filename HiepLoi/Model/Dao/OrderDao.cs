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
    public class OrderDao
    {
        OnlineShopDbContext db = null;
        public OrderDao()
        {
            db = new OnlineShopDbContext();
        }
        public long Insert(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
            return order.ID;
        }
        public IEnumerable<Order> ListAllPaging(Order entity, string searchString, ref int totalRecord, int page, int pageSize)
        {
            IQueryable<Order> model = db.Orders;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.ShipMobile.Contains(searchString));
            }
            if (entity !=null)
            {
                if (entity.UserKhach != null)
                {
                    model = model.Where(x => x.UserKhach == entity.UserKhach);
                }
                if (entity.ShipMobile != null)
                {
                    model = model.Where(x => x.ShipMobile == entity.ShipMobile);
                }
                if (entity.StoreId != null)
                {
                    model = model.Where(x => x.StoreId == entity.StoreId);
                }
                if (entity.Status !=null)
                {
                    model = model.Where(x => x.Status == entity.Status);
                }
                if (entity.ListStatusId != null && entity.ListStatusId.Count > 0)
                {
                    model = model.Where(x => entity.ListStatusId.Any(y => y == x.Status));
                }
            }
            totalRecord = model.Count();
            return model.OrderByDescending(x => x.ID).ToPagedList(page, pageSize);
        }
        public List<Order> ListAll(Order entity)
        {
            IQueryable<Order> model = db.Orders;
            if (entity != null)
            {
                if (entity.UserKhach != null)
                {
                    model = model.Where(x => x.UserKhach == entity.UserKhach);
                }
                if (entity.Status != null)
                {
                    model = model.Where(x => x.Status == entity.Status);
                }
                if (entity.ListStatusId != null && entity.ListStatusId.Count > 0)
                {
                    model = model.Where(x => entity.ListStatusId.Any(y => y == x.Status));
                }
            }
            return model.OrderByDescending(x => x.ID).ToList();
        }

        public Order GetById(int id)
        {
            return db.Orders.Find(id);
        }
        public Order ViewDetail(int id)
        {
            return db.Orders.Find(id);
        }
        public bool Delete(int id)
        {
            try
            {
                var Order = db.Orders.Find(id);
                db.Orders.Remove(Order);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool Update(Order entity)
        {
            try
            {
                var Order = db.Orders.Find(entity.ID);
                Order.CreatedDate = entity.CreatedDate;
                Order.UserKhach = entity.UserKhach;
                Order.CustomerID = entity.CustomerID;
                Order.ShipName = entity.ShipName;
                Order.ShipMobile = entity.ShipMobile;
                Order.ShipAddress = entity.ShipAddress;
                Order.Status = entity.Status;
                Order.ShipEmail = entity.ShipEmail;
                Order.StoreId = entity.StoreId;
                Order.SexOrder = entity.SexOrder;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //logging
                return false;
            }

        }
        public bool ChangeStatusQT(int idOrder, int nextStatus)
        {
            try
            {
                var Order = db.Orders.Find(idOrder);
                Order.Status = nextStatus;
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
