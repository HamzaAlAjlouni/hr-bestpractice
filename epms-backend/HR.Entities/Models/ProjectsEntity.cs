using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

/// <summary>
/// Ameen Alhayek
/// Pure Entity
/// </summary>
namespace HR.Entities
{
    [Table("adm_projects")]
    public class ProjectEntity
    {
        [Key] [Required] [Column("ID")] public int ID { set; get; }

        [Required] [Column("CODE")] public string Code { set; get; }

        [Required] [Column("NAME")] public string Name { set; get; }


        [Column("name2")] public string Name2 { set; get; }

        [Required] [Column("COMPANY_ID")] public long CompanyId { set; get; }

        [Required] [Column("PROJECT_ORDER")] public int Order { set; get; }


        [Column("WEIGHT")] public float Weight { set; get; }


        [Required] [Column("UNIT_ID")] public int UnitId { set; get; }

        [Required] [Column("TARGET")] public int Target { set; get; }

        [Required] [Column("C_KPI_CYCLE_ID")] public int KPICycleId { set; get; }

        [Required] [Column("C_KPI_TYPE_ID")] public int KPITypeId { set; get; }

        [Required]
        [Column("C_RESULT_UNIT_ID")]
        public int ResultUnitId { set; get; }


        [Required]
        [Column("STARG_OBJ_ID")]
        [ForeignKey("stratigyObject")]
        public int StratigicObjectiveId { set; get; }

        [Required] [Column("BRANCH_ID")] public int BranchId { set; get; }

        [Column("DESCRIPTION")] public string Description { set; get; }

        [Column("KPI")] public string KPI { set; get; }


        [Required] [Column("created_by")] public string CreatedBy { set; get; }

        [Required] [Column("created_date")] public DateTime CreatedDate { set; get; }


        [Column("modified_by")] public string ModifiedBy { set; get; }


        [Column("modified_date")] public DateTime? ModifiedDate { set; get; }

        [Column("Weight_From_Objective")] public float? WeightFromObjective { set; get; }

        [Column("Result")] public float? Result { set; get; }

        [Column("Result_Percentage")] public float? ResultPercentage { set; get; }

        [Column("Result_Weight_Percentage")] public float? ResultWeightPercentage { set; get; }

        [Column("Result_Weight_Perc_From_Obj")]
        public float? ResultWeightPercentageFromObjectives { set; get; }

        [Column("actual_cost")] public float? ActualCost { set; get; }

        [Column("planned_cost")] public float? PlannedCost { set; get; }

        [Column("p_type")] public int? p_type { set; get; }

        [Required] [Column("planned_status")] public int planned_status { set; get; }


        [Required]
        [Column("assessment_status")]
        public int assessment_status { set; get; }

        //added by yousef sleit
        [NotMapped] public string StratigicObjectiveName { set; get; }

        [NotMapped] public string UnitName { set; get; }

        //[NotMapped]
        public int? Year { set; get; }
        [Column("cateogry")] public int? Category { set; get; }

        public virtual StratigicObjectivesEntity stratigyObject { set; get; }

        [NotMapped] public float Q1_Target { set; get; }

        [NotMapped] public float Q2_Target { set; get; }
        [NotMapped] public float Q3_Target { set; get; }
        [NotMapped] public float Q4_Target { set; get; }
        [NotMapped] public string languageCode { set; get; }
    }
}