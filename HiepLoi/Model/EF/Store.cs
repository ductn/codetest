namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Store")]
    public partial class Store
    {
        public int StoreId { get; set; }

        [StringLength(200)]
        public string Title { get; set; }

        [StringLength(200)]
        public string QueryString { get; set; }

        [StringLength(500)]
        public string Slogan { get; set; }

        public string Description { get; set; }

        public string ContactUs { get; set; }

        [StringLength(300)]
        public string URLWEB { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(300)]
        public string ImgLogo { get; set; }

        public int? StoreTypeId { get; set; }

        public int? UserId { get; set; }

        public double? Viewed { get; set; }

        public double? Mark { get; set; }

        public int? TotalVote { get; set; }

        public bool IsHot { get; set; }

        public int? StatusId { get; set; }

        public DateTime? bEffectiveDate { get; set; }

        public DateTime? PublishedDate { get; set; }

        public DateTime? eEffectiveDate { get; set; }

        [StringLength(300)]
        public string BigBanner { get; set; }

        [StringLength(300)]
        public string ImageSlider { get; set; }

        [StringLength(50)]
        public string Zalo { get; set; }

        [StringLength(50)]
        public string SkypeId { get; set; }

        public int? TinhThanhId { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        [StringLength(500)]
        public string DiaChi { get; set; }

        [StringLength(200)]
        public string HoTen { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(50)]
        public string PhoneOther { get; set; }

        public bool? IsVip { get; set; }

        [StringLength(500)]
        public string HeaderImage { get; set; }

        public string Map { get; set; }

        public string StoreTypeName { get; set; }

        public int? NganhNgheId { get; set; }
        
        public string NganhNgheName { get; set; }

        public string QuyMoVon { get; set; }

        public int? NhanLuc { get; set; }

        public string DoanhThu { get; set; }

        public string LoiNhuan { get; set; }

        public long? CountView { get; set; }

        public string StoreImage { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(500)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(500)]
        public string ModifiedBy { get; set; }

        public int? LinhVucKinhDoanhId { get; set; }

        [NotMapped]
        public int SysActionId { get; set; }

        [NotMapped]
        public String LogActionNote { get; set; }

        [NotMapped]
        public List<int> ListStatusId { get; set; }

        [NotMapped]
        public String hidListFile { get; set; }

        [NotMapped]
        public String hidIndexFile { get; set; }

        [NotMapped]
        public String hidFileNote { get; set; }

        [NotMapped]
        public String hidFileLinkDown { get; set; }
    }
}
