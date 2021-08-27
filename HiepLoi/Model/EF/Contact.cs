namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Contact")]
    public partial class Contact
    {
        public int ID { get; set; }

        public string Content { get; set; }

        public bool Status { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Email { get; set; }

        [StringLength(500)]
        public string Title { get; set; }

        public bool IsAnswer { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(500)]
        public string CreatedBy { get; set; }

        public DateTime? DateAnswer { get; set; }

        [StringLength(200)]
        public string UserAnswer { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(500)]
        public string ModifiedBy { get; set; }

        public int? StatusId { get; set; }

        public DateTime? bEffectiveDate { get; set; }

        public DateTime? eEffectiveDate { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        public string AnswerContent { get; set; }

        public string FileActtach { get; set; }

        [NotMapped]
        public int SysActionId { get; set; }

        [NotMapped]
        public String LogActionNote { get; set; }

        [NotMapped]
        public List<int> ListStatusId { get; set; }
    }
}
