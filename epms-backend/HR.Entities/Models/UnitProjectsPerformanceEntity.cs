using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

/// <summary>
/// Ameen Alhayek
/// Pure Entity
/// </summary>
namespace HR.Entities
{
    [Table("adm_unit_projects_performance")]
    public class UnitProjectsPerformanceEntity
    {
        [Key] [Required] [Column("id")] public long Id { set; get; }

        [Required] [Column("year_id")] public long Year { set; get; }

        [Required] [Column("COMPANY_ID")] public long CompanyId { set; get; }

        [Required] [Column("branch_id")] public long BranchId { set; get; }

        [Required] [Column("unit_id")] public long UnitId { set; get; }


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

        [Column("Projects_Weight_Perc_From_Objs")]
        public float? ProjectsWeightPercentageFromObjectives { set; get; }

        [Column("Result_Weight_Perc_From_Objs")]
        public float? ResultWeightPercentageFromObjectives { set; get; }

        [Column("Employee_Percentage")] public float? EmployeePercentage { set; get; }


        [Column("Prjs_Level1_Employee")] public float? PrjectsLevel1Employee { set; get; }

        [Column("Prjs_Level2_Employee")] public float? PrjectsLevel2Employee { set; get; }

        [Column("Prjs_Level3_Employee")] public float? PrjectsLevel3Employee { set; get; }

        [Column("Prjs_Level4_Employee")] public float? PrjectsLevel4Employee { set; get; }

        [Column("Prjs_Level5_Employee")] public float? PrjectsLevel5Employee { set; get; }

        [Column("actual_cost")] public float? ActualCost { set; get; }


        [Column("planned_cost")] public float? PlannedCost { set; get; }
    }
}