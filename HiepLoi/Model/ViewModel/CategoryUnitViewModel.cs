﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class CategoryUnitViewModel
    {
        public int Id { set; get; }
        public int UnitId { set; get; }
        public string UnitTitle { set; get; }
        public string MetaTitleUnit { set; get; }
        public long CategoryID { set; get; }
        public string CategoryName { set; get; }
        public string CategoryMetaTitle { set; get; }
    }
}
