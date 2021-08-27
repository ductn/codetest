using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
    public class ChangePassModel
    {
        public long UserID { set; get; }
        public string PasswordOld { set; get; }
        public string PasswordNew1 { set; get; }
        public string PasswordNew2 { set; get; }

    }
}