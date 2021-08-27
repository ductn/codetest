namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Unit")]
    public partial class Unit
    {
        public int Id { get; set; }
        public string Icons { get; set; }

        public int? CodeId { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        [StringLength(200)]
        public string Title { get; set; }
        public string MetaTitleUnit { get; set; }

        public int? ParentId { get; set; }

        public int? Sort { get; set; }

        [StringLength(200)]
        public string DiaChi { get; set; }

        [StringLength(50)]
        public string DienThoai { get; set; }

        [StringLength(50)]
        public string Fax { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(200)]
        public string Website { get; set; }

        public int? IsDiaPhuong { get; set; }
        public int? UnitLevel { get; set; }
        public bool ShowOnHome { get; set; }

    }
}
