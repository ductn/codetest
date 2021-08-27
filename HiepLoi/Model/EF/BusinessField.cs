namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BusinessField")]
    public partial class BusinessField
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }

        [StringLength(500)]
        public string Code { get; set; }

        [StringLength(500)]
        public string Name { get; set; }

        [StringLength(2000)]
        public string Desription { get; set; }

        public int? Sort { get; set; }

        public DateTime? Created { get; set; }

        [StringLength(50)]
        public string Creator { get; set; }

        public DateTime? Modified { get; set; }

        [StringLength(50)]
        public string Modifier { get; set; }

        public string Status { get; set; }
        public int? MapId { get; set; }

    }
}
