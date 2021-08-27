namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GioHang")]
    public partial class GioHang
    {
        public long Id { get; set; }

        public string Code { get; set; }

        public string UserKhach { get; set; }

        [StringLength(50)]
        public string TypeKhach { get; set; }

        public long? IdProduct { get; set; }

        public int? SoLuong { get; set; }

        [StringLength(50)]
        public string SoDienThoai { get; set; }

        public string Email { get; set; }

        public int? Status { get; set; }

        public int? StorId { get; set; }

    }
}
