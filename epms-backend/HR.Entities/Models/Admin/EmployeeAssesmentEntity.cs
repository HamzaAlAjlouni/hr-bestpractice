using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Ameen Alhayek
/// Pure Entity
/// </summary>
namespace HR.Entities.Admin
{
    [Table("adm_emp_assesment")]
    public class EmployeeAssesmentEntity
    {
        [Required] public int c_kpi_cycle { get; set; }

        public long? emp_reviewer_id { get; set; }
        [Required] public int emp_position_id { get; set; }
        [Required] public DateTime agreement_date { get; set; }

        [Key] [Required] [Column("id")] public long ID { get; set; }
        [Required] public int year_id { get; set; }
        [Required] public int employee_id { get; set; }
        [Required] public string created_by { get; set; }
        [Required] public DateTime created_date { get; set; }
        public string modified_by { get; set; }

        public DateTime? modified_date { get; set; }
        public string attachment { get; set; }

        public float? target { get; set; }
        public float? objectives_weight { get; set; }
        public float? competencies_weight { get; set; }
        public float? objectives_result { get; set; }
        public float? competencies_result { get; set; }
        public float? objectives_weight_result { get; set; }
        public float? competencies_weight_result { get; set; }
        public float? result_before_round { get; set; }
        public float? result_after_round { get; set; }
        public float? objectives_result_after_round { get; set; }
        public float? competencies_result_after_round { get; set; }
        public float? objectives_weight_result_after_round { get; set; }
        public float? competencies_weight_result_after_round { get; set; }

        public int status { get; set; }

        public Boolean isQuarter1 { get; set; }
        public Boolean isQuarter2 { get; set; }
        public Boolean isQuarter3 { get; set; }
        public Boolean isQuarter4 { get; set; }
        public long? emp_manager_id { get; set; }
        public DateTime? final_date { get; set; }
        public float? final_result { get; set; }
    }
}