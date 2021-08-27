namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserChangerPaswword
    {
        public int UserId { get; set; }
        public string Password { get; set; }
        public string CurrentPassword { get; set; }
    }
}
