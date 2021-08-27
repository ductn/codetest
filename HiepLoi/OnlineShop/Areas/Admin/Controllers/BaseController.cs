using ClsCommon;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        //initilizing culture on controller initialization
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            if (Session[CommonConstants.CurrentCulture] != null)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(Session[CommonConstants.CurrentCulture].ToString());
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(Session[CommonConstants.CurrentCulture].ToString());
            }
            else
            {
                Session[CommonConstants.CurrentCulture] = "vi";
                Thread.CurrentThread.CurrentCulture = new CultureInfo("vi");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("vi");
            }
        }

        // changing culture
        public ActionResult ChangeCulture(string ddlCulture, string returnUrl)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(ddlCulture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(ddlCulture);

            Session[CommonConstants.CurrentCulture] = ddlCulture;
            return Redirect(returnUrl);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                //filterContext.Result = new RedirectToRouteResult(new
                //    RouteValueDictionary(new { controller = "Login", action = "Index", Area = "Admin" }));
                filterContext.Result = new RedirectResult(CommonConstants.DomainName + "/Login/Index");
            }
            base.OnActionExecuting(filterContext);
        }

        protected void SetAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";
            }
            else if (type == "warning")
            {
                TempData["AlertType"] = "alert-warning";
            }
            else if (type == "error")
            {
                TempData["AlertType"] = "alert-danger";
            }
        }
        public void setPermission(string controler)
        {
            //list
            string classView = "false";
            string classCreate = "false";
            string classSave = "false";
            string classEdit = "false";
            string classDelete = "false";
            string classDeleteMulti = "false";
            //string classLogin = "false";
            var daoCredential = new CredentialDao();
            Credential itemCredential = new Credential();

            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            string[] lstAction = {"ADD", "SAVE", "EDIT", "DELETE", "DELETEMULTI" };
            List<string> lstRole = new List<string>();
            itemCredential.UserGroupID = session.GroupID;
            foreach (string item in lstAction)
            {
                itemCredential.RoleID = item + "_" + controler;
                Credential itemDeleteCredential = daoCredential.ListAll(itemCredential).FirstOrDefault();
                if (itemDeleteCredential != null)
                {
                    if (item.Equals("ADD"))
                    {
                        classCreate = "true";
                    }
                    else if (item.Equals("SAVE"))
                    {
                        classSave = "true";
                    }
                    else if (item.Equals("EDIT"))
                    {
                        classEdit = "true";
                    }
                    else if (item.Equals("DELETE"))
                    {
                        classDelete = "true";
                    }
                    else if (item.Equals("DELETEMULTI"))
                    {
                        classDeleteMulti = "true";
                    }
                }
            }

            ViewBag.classCreate = classCreate;
            ViewBag.classEdit = classEdit;
            ViewBag.classView = classView;
            ViewBag.classDelete = classDelete;
            ViewBag.classDeleteMulti = classDeleteMulti;
            ViewBag.classSave = classSave;
            //ViewBag.classList = "true";
            //ViewBag.classLogin = classLogin;
        }

    }
}