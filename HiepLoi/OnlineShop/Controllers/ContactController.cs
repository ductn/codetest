using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            //var thongTinLienHe = new ThongTinLienHe();
            //thongTinLienHe.Type = 1;
            //var lstSo = new ThongTinLienHeDao().ListAll(thongTinLienHe);
            //thongTinLienHe.Type = 2;
            //var lstPTCKH = new ThongTinLienHeDao().ListAll(thongTinLienHe);
            //ViewBag.lstSo = lstSo;
            //ViewBag.lstPTCKH = lstPTCKH;
            return View();
        }

        [HttpPost]
        public JsonResult Send(string name, string mobile, string title, string email, string content)
        {
            var contact = new Contact();
            contact.Name = name;
            contact.Email = email;
            contact.CreatedDate = DateTime.Now;
            contact.Phone = mobile;
            contact.Content = content;
            contact.Title = title;

            var id = new ContactDao().Create(contact);
            if (id > 0)
            {
                return Json(new
                {
                    status = true
                });
                //send mail
            }

            else
                return Json(new
                {
                    status = false
                });

        }
    }
}