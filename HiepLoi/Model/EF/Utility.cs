namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Utility")]
    public partial class Utility
    {
        public int Id { get; set; }
        public string Code { get; set; }

        public string Name { get; set; }

        public string Desription { get; set; }

        public int? Sort { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }
        public bool Status { get; set; }
    }
}
