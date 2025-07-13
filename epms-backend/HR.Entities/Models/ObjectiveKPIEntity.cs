using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

/// <summary>
/// Ameen Alhayek
/// Pure Entity
/// </summary>
namespace HR.Entities
{
    [Table("adm_Objective_kpi")]
    public class ObjectiveKPIEntity
    {
        [Key] [Required] [Column("id")] public int ID { set; get; }

        [Required] [Column("objective_id")] public int objective_id { set; get; }

        [Required] [Column("name")] public string Name { set; get; }


        [Column("name2")] public string Name2 { set; get; }

        [Required] [Column("company_id")] public long CompanyId { set; get; }

        [Column("weight")] public float Weight { set; get; }

        [Required] [Column("target")] public float Target { set; get; }

        [Column("result")] public float result { set; get; }

        [Required] [Column("branch_id")] public int BranchId { set; get; }

        [Required] [Column("bsc")] public int bsc { set; get; }
        
        //1 better up, 2 better down
        [Required] [Column("better_up_down")] public int BetterUpDown { set; get; } = 1;

        [Required] [Column("measurement")] public int measurement { set; get; }

        [Column("description")] public string Description { set; get; }


        [Required] [Column("created_by")] public string CreatedBy { set; get; }

        [Required] [Column("created_date")] public DateTime CreatedDate { set; get; }


        [Column("modified_by")] public string ModifiedBy { set; get; }


        [Column("modified_date")] public DateTime? ModifiedDate { set; get; }

        [Required] [Column("is_obj_kpi")] public int is_obj_kpi { set; get; }


        [Required] [Column("C_KPI_CYCLE_ID")] public int C_KPI_CYCLE_ID { set; get; }

        [Required] [Column("C_KPI_TYPE_ID")] public int C_KPI_TYPE_ID { set; get; }

        [Required]
        [Column("C_RESULT_UNIT_ID")]
        public int C_RESULT_UNIT_ID { set; get; }
    }
}