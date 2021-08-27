namespace Model.EF
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("User")]
    public partial class User
    {
        public long ID { get; set; }

        [StringLength(50)]
        [Display(Name = "User_UserName", ResourceType = typeof(StaticResources.Resources))]
        public string UserName { get; set; }

        [StringLength(32)]
        [Display(Name = "User_Password", ResourceType = typeof(StaticResources.Resources))]
        public string Password { get; set; }

        [StringLength(20)]
        [Display(Name = "User_GroupID", ResourceType = typeof(StaticResources.Resources))]
        public string GroupID { set; get; }

        [StringLength(50)]
        [Display(Name = "User_Name", ResourceType = typeof(StaticResources.Resources))]
        public string Name { get; set; }

        [StringLength(50)]
        [Display(Name = "User_Address", ResourceType = typeof(StaticResources.Resources))]
        public string Address { get; set; }

        [StringLength(50)]
        [Display(Name = "User_Email", ResourceType = typeof(StaticResources.Resources))]
        public string Email { get; set; }

        [StringLength(50)]
        [Display(Name = "User_Phone", ResourceType = typeof(StaticResources.Resources))]
        public string Phone { get; set; }

        //[Display(Name = "User_ProvinceID", ResourceType = typeof(StaticResources.Resources))]
        public int? ProvinceID { set; get; }

        [Display(Name = "User_DistrictID", ResourceType = typeof(StaticResources.Resources))]
        public int? DistrictID { set; get; }

        public int? ChucVu { set; get; }
        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        [Display(Name = "User_Status", ResourceType = typeof(StaticResources.Resources))]
        public bool Status { get; set; }
        public string Avatar { get; set; }

        [StringLength(50)]
        public string UnitCode { get; set; }
        public string AutoRememberLogin { get; set; }
        public string ConfigThemes { get; set; }
        public int? HuyenID { set; get; }
        public int? XaID { set; get; }
        public bool IsLock { get; set; }
        public string ckQuenMK { get; set; }
        public string tokenQuenMK { get; set; }


        public bool ckHo { get; set; }

        public bool ckDN { get; set; }

        public bool ckHTX { get; set; }

        [StringLength(4000)]
        public string urlFileDinhKem { get; set; }
        [StringLength(4000)]
        public string tenFileDinhKem { get; set; }
        public bool checkXacMinh { get; set; }
        [NotMapped]
        public string loaiTK { set; get; }
    }
}
