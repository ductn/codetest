using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Dao;
using Model.EF;

namespace OnlineShop.Controllers
{
    public class AboutController : Controller
    {
        // GET: About
        public ActionResult Index()
        {
            var daoMenu = new MenuDao();
            var itMenu = new Menu();
            itMenu.Link = "/gioi-thieu.html";
            var itemMenu = daoMenu.ListAll(itMenu).FirstOrDefault();
            ViewBag.itemMenu = itemMenu;
            return View();
        }
    }
}