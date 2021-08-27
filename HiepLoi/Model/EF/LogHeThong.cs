namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LogHeThong")]
    public partial class LogHeThong
    {
        public long Id { get; set; }

        [StringLength(255)]
        public string NoiDung { get; set; }

        [StringLength(255)]
        public string ChucNang { get; set; }

        [StringLength(255)]
        public string SuKien { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ThoiDiem { get; set; }

        [StringLength(255)]
        public string Ip { get; set; }

        [StringLength(255)]
        public string NguoiDung { get; set; }
    }
}
