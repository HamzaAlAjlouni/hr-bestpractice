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
    [Table("tbl_perf_level_quota")]
    public partial class PerformancelevelsQuota
    {
        [Key] [Required] [Column("id")] public long Id { set; get; }

        [Required] [Column("lvl_number")] public long LevelNumber { set; get; }

        [Required] [Column("from_percentage")] public float FromPercentage { set; get; }

        [Required] [Column("to_percentage")] public float ToPercentage { set; get; }

        [Required] [Column("year_id")] public long YearId { set; get; }

        [Required] [Column("quota_type")] public int QuotaType { set; get; }
        // 0 => from 5 to 1 
        // 1 => from 1 to 5 
        [Required] [Column("quota_direction")] public int QuotaDirection { set; get; } 

        [Required] [Column("company_id")] public long CompanyId { set; get; }
    }
}