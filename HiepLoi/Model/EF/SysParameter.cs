namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SysParameter")]
    public partial class SysParameter
    {
        public long Id { get; set; }

        [StringLength(255)]
        public string TenThamSo { get; set; }

        [StringLength(255)]
        public string KieuDuLieu { get; set; }

        [StringLength(255)]
        public string GiaTri { get; set; }

        [StringLength(255)]
        public string MoTa { get; set; }

        public bool? KichHoat { get; set; }
    }
}
