namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BusinessPartner")]
    public partial class BusinessPartner
    {
        public long Id { get; set; }

        public string Image { get; set; }

        public string Slogan { get; set; }

        public string SDT { get; set; }

        public string DiaChi { get; set; }

        public string Email { get; set; }

        public string MoTa { get; set; }

        public string TieuDe { get; set; }

        public string Url { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayXuatBan { get; set; }

        public string NoiDung { get; set; }

        public string QueryString { get; set; }

        public string StatusId { get; set; }
    }
}
