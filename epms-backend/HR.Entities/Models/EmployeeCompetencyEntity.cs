using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

/// <summary>
/// Ameen Alhayek
/// Pure Entity
/// </summary>
namespace HR.Entities
{
    [Table("adm_emp_competency")]
    public class EmployeeCompetencyEntity
    {
        [Key] [Required] [Column("id")] public long Id { set; get; }

        [Required] [Column("competency_id")] public long CompetencyId { set; get; }

        [Required]
        [Column("competency_level_id")]
        public long? CompetencyLevelId { set; get; }

        [Column("weight")] public float? Weight { set; get; }

        [Column("result_without_round")] public float? ResultWithoutRound { set; get; }

        [Required] [Column("target")] public float Target { set; get; }

        [Column("result_after_round")] public float? ResultAfterRound { set; get; }

        [Required]
        [Column("emp_assessment_id")]
        public int EmployeeAssessmentId { set; get; }

        [Required] [Column("created_by")] public string CreatedBy { set; get; }

        [Required] [Column("created_date")] public DateTime CreatedDate { set; get; }

        [Column("modified_by")] public string ModifiedBy { set; get; }

        [Column("modified_date")] public DateTime? ModifiedDate { set; get; }


        public string project_desc { get; set; }
    }
}