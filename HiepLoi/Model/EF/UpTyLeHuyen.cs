namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UpTyLeHuyen
    {
        public string Huyen { get; set; }
        public string TiepNhan { get; set; }
        public string XuLy { get; set; }
        public string TreHen { get; set; }
        public string DungHen { get; set; }
        public string TyLe { get; set; }
    }
}
