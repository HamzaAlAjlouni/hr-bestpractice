using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

/// <summary>
/// Ameen Alhayek
/// Pure Entity
/// </summary>
namespace HR.Entities
{
    [Table("adm_emp_comp_kpi_ass")]
    public class EmployeeCompetencyKpiAssessmentEntity
    {
        [Key] [Required] [Column("id")] public long Id { set; get; }

        [Required] [Column("period_no")] public int PeriodNo { set; get; }

        [Required]
        [Column("emp_competency_kpi_id")]
        public int EmployeeCompetencyKpiId { set; get; }

        [Column("result")] public float? Result { set; get; }

        [Required] [Column("target")] public float? Target { set; get; }

        [Required] [Column("created_by")] public string CreatedBy { set; get; }

        [Required] [Column("created_date")] public DateTime CreatedDate { set; get; }

        [Column("modified_by")] public string ModifiedBy { set; get; }

        [Column("modified_date")] public DateTime? ModifiedDate { set; get; }
    }
}