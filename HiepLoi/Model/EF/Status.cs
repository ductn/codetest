namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Status
    {
        public int Id { get; set; }

        public int? StatusId { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(50)]
        public string TitleHoSo { get; set; }

        public int? FunctionId { get; set; }

        [StringLength(50)]
        public string ColorStatus { get; set; }

        public int IdSysProcedure { get; set; }

        [NotMapped]
        public int Test { get; set; }
    }
}
