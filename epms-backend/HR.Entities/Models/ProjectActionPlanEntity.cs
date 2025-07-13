using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

/// <summary>
/// Ameen Alhayek
/// Pure Entity
/// </summary>
namespace HR.Entities
{
    [Table("adm_action_plan")]
    public class ProjectActionPlanEntity
    {
        [Key] [Required] [Column("id")] public int ID { set; get; }

        [Required] [Column("project_id")] public int projectID { set; get; }

        [Required] [Column("project_kpi_id")] public int project_kpi_id { set; get; }

        [Required] [Column("emp_id")] public int emp_id { set; get; }

        [Required] [Column("action_name")] public string action_name { set; get; }

        [Required] [Column("action_req")] public string action_req { set; get; }

        [Column("action_cost")] public decimal? action_cost { set; get; }

        [Required] [Column("action_date")] public DateTime action_date { set; get; }

        [Required] [Column("action_weight")] public float action_weight { set; get; }

        [Column("action_notes")] public string action_notes { set; get; }

        [Required] [Column("created_by")] public string CreatedBy { set; get; }

        [Required] [Column("created_date")] public DateTime CreatedDate { set; get; }


        [Column("modified_by")] public string ModifiedBy { set; get; }


        [Column("modified_date")] public DateTime? ModifiedDate { set; get; }

        [Column("attachment")] public string attachment { set; get; }

        [Required] [Column("planned_status")] public int planned_status { set; get; }

        [Required]
        [Column("assessment_status")]
        public int assessment_status { set; get; }
    }
}