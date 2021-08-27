namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        public long ID { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(10)]
        public string Code { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(250)]
        public string Image { get; set; }

        public string MoreImages { get; set; }

        public decimal? Price { get; set; }

        public decimal? PromotionPrice { get; set; }

        public bool? IncludedVAT { get; set; }

        public int Quantity { get; set; }

        public long? CategoryID { get; set; }
        public int UnitId { get; set; }
        public int ProductCategoryParentId { get; set; }
        public int ProductCategoryId { get; set; }

        [Column(TypeName = "ntext")]
        public string Detail { get; set; }

        public int? Warranty { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        [StringLength(250)]
        public string MetaKeywords { get; set; }

        [StringLength(250)]
        public string MetaDescriptions { get; set; }

        public bool Status { get; set; }

        public DateTime? TopHot { get; set; }

        public int ViewCount { get; set; }

        public int? StoreId { get; set; }

        public int? StatusId { get; set; }

        public bool IsHot { get; set; }
        public bool IsDiscount { get; set; }
        public bool IsTrending { get; set; }

        public bool IsPromotion { get; set; }
        public bool IsMain { get; set; }
        public bool GoiYMuaSam { get; set; }

        public int? TinhTrangHang { get; set; }

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
