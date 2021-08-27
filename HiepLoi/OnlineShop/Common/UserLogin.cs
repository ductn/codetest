using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop
{
    [Serializable]
    public class UserLogin
    {
        public long UserID { set; get; }
        public string UserName { set; get; }
        public string Name { set; get; }
        public string Avatar { set; get; }
        public string GroupID { set; get; }
        public string UnitCode { set; get; }
    }
}