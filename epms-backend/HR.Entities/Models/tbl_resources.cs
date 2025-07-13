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
    [Table("tbl_resources")]
    public partial class tbl_resources
    {
        [Key] [Required] [Column("id")] public long id { set; get; }

        [Required] [Column("url")] public string url { set; get; }

        [Required] [Column("resource_key")] public string resource_key { set; get; }

        [Required] [Column("resource_value")] public string resource_value { set; get; }

        [Required] [Column("culture_name")] public string culture_name { set; get; }

        [Required] [Column("org_id")] public long org_id { set; get; }
    }
}