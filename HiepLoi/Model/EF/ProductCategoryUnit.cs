namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductCategoryUnit")]
    public partial class ProductCategoryUnit
    {
        public int Id { get; set; }
        public int UnitId { get; set; }
        public int CategoryId { get; set; }
        public int ProductCategoryId { get; set; }
    }
}
