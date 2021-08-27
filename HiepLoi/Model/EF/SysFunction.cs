namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SysFunction")]
    public partial class SysFunction
    {
        [Key]
        public int FunctionID { get; set; }

        [StringLength(500)]
        public string FunctionName { get; set; }

        [StringLength(500)]
        public string Link { get; set; }

        [StringLength(50)]
        public string Target { get; set; }

        public int? ParentId { get; set; }

        public int? DisplayOrder { get; set; }

        [StringLength(50)]
        public string Icons { get; set; }

        public int? TypeID { get; set; }

        [StringLength(50)]
        public string isController { get; set; }

        [StringLength(50)]
        public string isModule { get; set; }

        [StringLength(50)]
        public string RoleID { get; set; }
        public bool Status { get; set; }
        public string NameModule { get; set; }
        public string ColorModule { get; set; }
        public string IconsModule { get; set; }
        [NotMapped]
        public string DanhSachRole { get; set; }
    }
}
