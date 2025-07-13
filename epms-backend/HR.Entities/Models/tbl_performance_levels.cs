using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.Entities.Infrastructure;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.Entities
{
    [Table("tbl_performance_levels")]
    public partial class tbl_performance_levels
    {
        [Key] [Required] [Column("id")] public long id { set; get; }


        [Column("lvl_code")] public string lvl_code { set; get; }

        [Required] [Column("lvl_name")] public string lvl_name { set; get; }

        [Required] [Column("lvl_number")] public long lvl_number { set; get; }

        [Required] [Column("lvl_percent")] public long lvl_percent { set; get; }

        [Required] [Column("lvl_year")] public long lvl_year { set; get; }


        [Required] [Column("company_id")] public long company_id { set; get; }
    }
}