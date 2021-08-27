using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ClsCommon
{
    public static class CommonStringUnit
    {
        public static void SetCookies(string key, string value)
        {
            if (HttpContext.Current.Request.Cookies[key] == null)
            {
                HttpCookie cookie = new HttpCookie(key);
                cookie.Value = value;
                cookie.Expires = DateTime.Now.AddDays(365);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            else
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(key);
                cookie.Value = value;
                cookie.Expires = DateTime.Now.AddDays(365);
                HttpContext.Current.Response.Cookies.Set(cookie);
            }
        }
        public static string GetCookies(string key)
        {
            string cookies = "";
            try
            {
                if (HttpContext.Current.Request.Cookies[key] != null)
                {
                    HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(key);
                    cookies = cookie.Value;
                }
            }
            catch (Exception)
            {

            }
            return cookies;
        }
    }
}
