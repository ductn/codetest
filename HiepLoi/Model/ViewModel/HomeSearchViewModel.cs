using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class HomeSearchViewModel
    {
        public int? HuyenId { get; set; }
        public int? XaPhuongId { get; set; }
        public int? LoaiHinh { get; set; }
        public int? LinhVuc { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FromDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ToDate { get; set; }
    }
}
