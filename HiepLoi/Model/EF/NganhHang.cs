namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    public partial class NganhHang
    {
        public string DomainName { get; set; }
        public long CategoryId { get; set; }

        public int UnitId { get; set; }

        public int ProductCategoryParentId { get; set; }

        public int ProductCategoryId { get; set; }
    }
}
