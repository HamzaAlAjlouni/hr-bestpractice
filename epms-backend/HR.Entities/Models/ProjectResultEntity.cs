using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

/// <summary>
/// Ameen Alhayek
/// Pure Entity
/// </summary>
namespace HR.Entities
{
    [Table("adm_prj_results")]
    public class ProjectResultEntity
    {
        [Key] [Required] [Column("id")] public long Id { set; get; }

        [Required] [Column("period_no")] public int PeriodNo { set; get; }

        [Required] [Column("plan_result")] public float PlannedResult { set; get; }

        [Required] [Column("actual_result")] public float? ActualResult { set; get; }

        [Required] [Column("project_id")] public long ProjectId { set; get; }

        [Required] [Column("kpi_id")] public long kpi_id { set; get; }

        [Required] [Column("created_by")] public string CreatedBy { set; get; }

        [Required] [Column("created_date")] public DateTime CreatedDate { set; get; }


        [Column("modified_by")] public string ModifiedBy { set; get; }


        [Column("modified_date")] public DateTime? ModifiedDate { set; get; }
    }
}