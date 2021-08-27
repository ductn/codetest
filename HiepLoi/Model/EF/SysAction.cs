namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SysAction")]
    public partial class SysAction
    {
        public int Id { get; set; }

        [StringLength(500)]
        public string Title { get; set; }

        public int? CurrentStatus { get; set; }

        public int? NextStatus { get; set; }

        public string Description { get; set; }

        public string ButtonName { get; set; }

        public int IdSysProcedure { get; set; }

        public string UserGroups { get; set; }

        public int? DisplayOrder { get; set; }

        [NotMapped]
        public string lstUserGroup { get; set; }
    }
}
