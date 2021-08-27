namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CategoryUnit")]
    public partial class CategoryUnit
    {
        public int Id { get; set; }
        public int UnitId { get; set; }
        public int CategoryId { get; set; }
    }
}
