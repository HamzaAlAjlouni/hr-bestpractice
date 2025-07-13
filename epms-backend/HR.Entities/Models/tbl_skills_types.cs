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
    [Table("adm_skills_types")]
    public partial class tbl_skills_types
    {
        [Key] [Required] [Column("id")] public long id { set; get; }

        [Required] [Column("code")] public string code { set; get; }


        [Column("name")] public string name { set; get; }

        [Required] [Column("created_by")] public string created_by { set; get; }
        [Required] [Column("created_date")] public DateTime created_date { set; get; }


        [Column("name2")] public string name2 { set; get; }
    }
}