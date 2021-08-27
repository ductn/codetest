namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SysProcedure")]
    public partial class SysProcedure
    {
        public int Id { get; set; }

        [StringLength(500)]
        public string Code { get; set; }

        public string Title { get; set; }

        public int? Sort { get; set; }

        public string Description { get; set; }
    }
}
