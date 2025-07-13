using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Ameen Alhayek
/// Pure Entity
/// </summary>
namespace HR.Entities.Admin
{
    [Table("adm_emp_objective")]
    public class EmployeeObjectiveEntity
    {
        /*

 emp_assesment_id int(11)
 project_id int(11)
 pos_desc_id int(11)*/
        [Required] public long emp_assesment_id { get; set; }

        public long? project_id { get; set; }

        public int objective_competency_id { get; set; } = 0;

        public long? pos_desc_id { get; set; }

        [Key] [Required] public long ID { get; set; }
        [Required] public string name { get; set; }
        public string name2 { get; set; }
        [Required] public string code { get; set; }

        public float? weight { get; set; }
        public string note { get; set; }


        [Required] public string created_by { get; set; }
        [Required] public DateTime created_date { get; set; }
        public string modified_by { get; set; }

        public DateTime? modified_date { get; set; }

        public float? target { get; set; }
        public float? result_without_round { get; set; }
        public float? result_after_round { get; set; }

        public string project_desc { get; set; }
        public float? final_point_result { get; set; }

        [Required] public long target_type { get; set; }
        public int? KPI_type { get; set; }
    }
}