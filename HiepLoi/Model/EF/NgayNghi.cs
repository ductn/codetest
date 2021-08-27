namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NgayNghi")]
    public partial class NgayNghi
    {
        public long Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Ngay { get; set; }

        [StringLength(255)]
        public string MoTa { get; set; }
    }
}
