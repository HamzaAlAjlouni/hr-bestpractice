using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

/// <summary>
/// Ameen Alhayek
/// Pure Entity
/// </summary>
namespace HR.Entities
{
    [Table("adm_company_obj_performance")]
    public class CompanyObjectivesPerformanceEntity
    {
        [Key] [Required] [Column("id")] public long Id { set; get; }

        [Required] [Column("year_id")] public long Year { set; get; }

        [Required] [Column("COMPANY_ID")] public long CompanyId { set; get; }

        [Column("total_employee")] public int? TotalEmployee { set; get; }

        [Column("Level1_Employee")] public float? Level1EmployeeCount { set; get; }

        [Column("Level2_Employee")] public float? Level2EmployeeCount { set; get; }

        [Column("Level3_Employee")] public float? Level3EmployeeCount { set; get; }

        [Column("Level4_Employee")] public float? Level4EmployeeCount { set; get; }

        [Column("Level5_Employee")] public float? Level5EmployeeCount { set; get; }

        [Column("Level1_Result_Employee")] public float? Level1ResultEmployee { set; get; }

        [Column("Level2_Result_Employee")] public float? Level2ResultEmployee { set; get; }

        [Column("Level3_Result_Employee")] public float? Level3ResultEmployee { set; get; }

        [Column("Level4_Result_Employee")] public float? Level4ResultEmployee { set; get; }

        [Column("Level5_Result_Employee")] public float? Level5ResultEmployee { set; get; }

        [Column("Result_Percentage")] public float? Result_Percentage { set; get; }

        [Column("actual_cost")] public float? ActualCost { set; get; }

        [Column("planned_cost")] public float? PlannedCost { set; get; }
    }
}