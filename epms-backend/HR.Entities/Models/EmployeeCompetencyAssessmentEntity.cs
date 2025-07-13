using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

/// <summary>
/// Ameen Alhayek
/// Pure Entity
/// </summary>
namespace HR.Entities
{
    [Table("adm_emp_comp_ass")]
    public class EmployeeCompetencyAssessmentEntity
    {
        [Key] [Required] [Column("id")] public long Id { set; get; }

        [Required] [Column("period_no")] public int PeriodNo { set; get; }


        [Required]
        [Column("emp_competency_id")]
        public long EmployeeCompetencyId { set; get; }

        [Column("result_before_round")] public float? ResultBeforeRound { set; get; }

        [Required] [Column("target")] public float? Target { set; get; }

        [Column("result_after_round")] public float? ResultAfterRound { set; get; }

        [Column("weight_result_without_round")]
        public float? WeightResultWithoutRound { set; get; }

        [Column("weight_result_after_round")] public float? WeightResultAfterRound { set; get; }


        [Required] [Column("created_by")] public string CreatedBy { set; get; }

        [Required] [Column("created_date")] public DateTime CreatedDate { set; get; }

        [Column("modified_by")] public string ModifiedBy { set; get; }

        [Column("modified_date")] public DateTime? ModifiedDate { set; get; }
    }
}