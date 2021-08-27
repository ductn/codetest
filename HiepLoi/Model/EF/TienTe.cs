namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TienTe")]
    public partial class TienTe
    {
        public long Id { get; set; }

        [StringLength(255)]
        public string TenTien { get; set; }

        [StringLength(255)]
        public string MoTa { get; set; }
    }
}
