namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LogAction")]
    public partial class LogAction
    {
        public long Id { get; set; }

        public int? SysProcedureId { get; set; }

        public int? ObjId { get; set; }

        [StringLength(100)]
        public string CreatorUserName { get; set; }

        public int? UserIdCreator { get; set; }

        [StringLength(500)]
        public string CreatorName { get; set; }

        [StringLength(100)]
        public string ReceiverUserName { get; set; }

        public int? UserIdReceiver { get; set; }

        [StringLength(500)]
        public string ReceiverName { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? PublishedDate { get; set; }

        public DateTime? eEffectiveDate { get; set; }

        public int? CurrentStatusId { get; set; }

        public int? ActionId { get; set; }

        public string Note { get; set; }
    }
}
