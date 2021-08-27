using Model.Dao;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Model.EF;
using ClsCommon;
using System.Configuration;
using System.IO;

namespace OnlineShop.Controllers
{
    public class CartController : Controller
    {
        private const string CartSession = "CartSession";
        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        public JsonResult DeleteAll()
        {
            Session[CartSession] = null;
            return Json(new
            {
                status = true
            });
        }

        public JsonResult Delete(int id)
        {
            //var sessionCart = (List<CartItem>)Session[CartSession];
            //sessionCart.RemoveAll(x => x.Product.ID == id);
            //Session[CartSession] = sessionCart;
            var result = false;
            result = new GioHangDao().Delete(id);
            return Json(new
            {
                status = true
            });
        }
        public JsonResult UpdateQuantity(int idItemGioHang, int soluong)
        {
            var result = false;
            GioHang item = new GioHangDao().GetById(idItemGioHang);
            item.SoLuong = soluong;
            result = new GioHangDao().Update(item);
            return Json(new
            {
                status = result
            });
        }
        public JsonResult CreatOrder(string fullname, string address, string phone, string note, int? gioiTinh, string userKhach, string email)
        {
            var result = false;
            GioHang itSearch = new GioHang();
            itSearch.UserKhach = userKhach;
            itSearch.Status = 0;
            List<GioHang> lstGioHang = new GioHangDao().ListAll(itSearch);
            List<Int32> tmpLstStoreId = new List<Int32>();
            foreach (var item in lstGioHang)
            {
                tmpLstStoreId.Add(item.StorId.GetValueOrDefault());
            }
            List<Int32> LstStoreId = new List<Int32>();
            LstStoreId = tmpLstStoreId.Distinct().ToList();
            foreach (var item in LstStoreId)
            {
                //Tạo đơn hàng theo gian hàng
                Order newOrder = new Order();
                newOrder.StoreId = item;
                newOrder.UserKhach = userKhach;
                newOrder.CreatedDate = DateTime.Now;
                newOrder.Status = 0;
                newOrder.ShipName = fullname;
                newOrder.ShipMobile = phone;
                newOrder.ShipAddress = address;
                newOrder.SexOrder = gioiTinh;
                newOrder.ShipEmail = email;
                long OrderId = new OrderDao().Insert(newOrder);
                //End tạo đơn hàng theo gian hàng
                //Tạo chi tiết đơn hàng
                itSearch.StorId = item;
                List<GioHang> GioHang = new GioHangDao().ListAll(itSearch);
                foreach (var gh in GioHang)
                {
                    Product product = new ProductDao().GetByID(gh.IdProduct.GetValueOrDefault());
                    OrderDetail orderDetail = new OrderDetail();
                    decimal? gia = 0;
                    if (product.IsPromotion == true)
                    {
                        gia = product.PromotionPrice;
                    }
                    else
                    {
                        gia = product.Price;
                    }
                    orderDetail.ProductID = product.ID;
                    orderDetail.OrderID = OrderId;
                    orderDetail.Price = gia;
                    orderDetail.Status = 0;
                    orderDetail.Quantity = gh.SoLuong;
                    orderDetail.StoreId = item;
                    result = new OrderDetailDao().Insert(orderDetail);
                }
                //End Tạo chi tiết đơn hàng
            }
            foreach (var item in lstGioHang)
            {
                item.Status = 1;
                new GioHangDao().Update(item);
            }
            return Json(new
            {
                status = result
            });
        }
        //public JsonResult Update(string cartModel)
        //{
        //    var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
        //    var sessionCart = (List<CartItem>)Session[CartSession];

        //    foreach (var item in sessionCart)
        //    {
        //        var jsonItem = jsonCart.SingleOrDefault(x => x.Product.ID == item.Product.ID);
        //        if (jsonItem != null)
        //        {
        //            item.Quantity = jsonItem.Quantity;
        //        }
        //    }
        //    Session[CartSession] = sessionCart;
        //    return Json(new
        //    {
        //        status = true
        //    });
        //}
        public ActionResult AddItem(long productId, int quantity)
        {
            var product = new ProductDao().ViewDetail(productId);
            var cart = Session[CartSession];
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.Product.ID == productId))
                {

                    foreach (var item in list)
                    {
                        if (item.Product.ID == productId)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    //tạo mới đối tượng cart item
                    var item = new CartItem();
                    item.Product = product;
                    item.Quantity = quantity;
                    list.Add(item);
                }
                //Gán vào session
                Session[CartSession] = list;
            }
            else
            {
                //tạo mới đối tượng cart item
                var item = new CartItem();
                item.Product = product;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);
                //Gán vào session
                Session[CartSession] = list;
            }
            return RedirectToAction("Index");
        }
        //public JsonResult AddItem(int idProduct, int idStore, int quantity)
        //{
        //    string Khach = (string)Session["UserKhachMua"];
        //    string message = "";
        //    if (Khach!=null)
        //    {
        //        var itemProduct = new ProductDao().GetByID(idProduct);
        //        var itGioHang = new GioHang();
        //        itGioHang.Status = 0;
        //        itGioHang.IdProduct = idProduct;
        //        itGioHang.UserKhach = Khach;
        //        var lstGioHang = new GioHangDao().ListAll(itGioHang);
        //        if (lstGioHang.Count()>0)
        //        {
        //            foreach (var itemGH in lstGioHang) 
        //            {
        //                if(itemGH.IdProduct == idProduct)
        //                {
        //                    itemGH.SoLuong += quantity; 
        //                    new GioHangDao().Update(itemGH);
        //                }
        //            }
        //            message = "Thêm vào giỏ hàng thành công!";
        //        }
        //        else
        //        {
        //            var newGioHang = new GioHang();
        //            newGioHang.UserKhach = Khach;
        //            newGioHang.IdProduct = idProduct;
        //            newGioHang.StorId = idStore;
        //            newGioHang.SoLuong = quantity;
        //            newGioHang.Status = 0;
        //            new GioHangDao().Insert(newGioHang);
        //            message = "Thêm vào giỏ hàng thành công!";
        //        }
        //    }
        //    return Json(new
        //    {
        //        data = "",
        //        message= message,
        //        status = true
        //    }, JsonRequestBehavior.AllowGet); ;
        //}
        [HttpGet]
        public ActionResult Payment()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        [HttpPost]
        public ActionResult Payment(string shipName,string mobile,string address,string email)
        {
            var order = new Order();
            order.CreatedDate = DateTime.Now;
            order.ShipAddress = address;
            order.ShipMobile = mobile;
            order.ShipName = shipName;
            order.ShipEmail = email;

            try
            {
                var id = new OrderDao().Insert(order);
                var cart = (List<CartItem>)Session[CartSession];
                var detailDao = new Model.Dao.OrderDetailDao();
                decimal total = 0;
                foreach (var item in cart)
                {
                    var orderDetail = new OrderDetail();
                    orderDetail.ProductID = item.Product.ID;
                    orderDetail.OrderID = id;
                    orderDetail.Price = item.Product.Price;
                    orderDetail.Quantity = item.Quantity;
                    detailDao.Insert(orderDetail);

                    total += (item.Product.Price.GetValueOrDefault(0) * item.Quantity);
                }
                string content = System.IO.File.ReadAllText(Server.MapPath("~/assets/client/template/neworder.html"));

                content = content.Replace("{{CustomerName}}", shipName);
                content = content.Replace("{{Phone}}", mobile);
                content = content.Replace("{{Email}}", email);
                content = content.Replace("{{Address}}", address);
                content = content.Replace("{{Total}}", total.ToString("N0"));
                var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                new MailHelper().SendMail(email, "Đơn hàng mới từ OnlineShop", content);
                new MailHelper().SendMail(toEmail, "Đơn hàng mới từ OnlineShop", content);
            }
            catch (Exception)
            {
                //ghi log
                return Redirect("/loi-thanh-toan");
            }
            return Redirect("/hoan-thanh");
        }

        public ActionResult Success()
        {
            return View();
        }
    }
}