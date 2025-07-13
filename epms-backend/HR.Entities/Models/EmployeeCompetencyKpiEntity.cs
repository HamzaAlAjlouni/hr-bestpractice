using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

/// <summary>
/// Ameen Alhayek
/// Pure Entity
/// </summary>
namespace HR.Entities
{
    [Table("adm_emp_competency_kpi")]
    public class EmployeeCompetencyKpiEntity
    {
        [Key] [Required] [Column("id")] public long Id { set; get; }

        [Required]
        [Column("emp_competency_id")]
        public long EmployeeCompetencyId { set; get; }

        [Required]
        [Column("competency_kpi_id")]
        public long EmployeeCompetencyKpiId { set; get; }

        [Required] [Column("created_by")] public string CreatedBy { set; get; }

        [Required] [Column("created_date")] public DateTime CreatedDate { set; get; }

        [Column("target")] public float Target { set; get; }

        [Column("result")] public float? Result { get; set; }

        [Column("note")] public string Note { get; set; }

        [Column("result_pref_segment_id")] public long? SegmentId { get; set; }
    }
}