using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Model.EF
{
    [Table("Credential")]
    [Serializable]
    public class Credential
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string UserGroupID { set; get; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string RoleID { set; get; }

        [NotMapped]
        public int FunctionID { get; set; }

        [NotMapped]
        public String lstRole { get; set; }
    }
}
