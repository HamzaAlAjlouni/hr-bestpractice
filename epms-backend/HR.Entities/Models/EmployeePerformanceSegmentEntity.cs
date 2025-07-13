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
    [Table("adm_emp_perf_segments")]
    public partial class EmployeePerformanceSegmentEntity
    {
        [Key] [Required] [Column("id")] public long id { set; get; }

        [Required] [Column("name")] public string name { set; get; }

        [Column("description")] public string description { set; get; }

        [Required] [Column("segment")] public int segment { set; get; }

        [Required] [Column("year")] public int year { set; get; }

        [Required] [Column("percentage_from")] public float percentage_from { set; get; }

        [Required] [Column("percentage_to")] public float percentage_to { set; get; }

        [Required] [Column("company_id")] public long CompanyId { set; get; }
    }
}