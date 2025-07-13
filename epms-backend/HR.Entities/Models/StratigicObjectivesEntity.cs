using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

/// <summary>
/// Ameen Alhayek
/// Pure Entity
/// </summary>
namespace HR.Entities
{
    [Table("adm_stratigic_objectives")]
    public class StratigicObjectivesEntity
    {
        [Key] [Required] [Column("ID")] public int id { set; get; }


        [Column("CODE")] public string Code { set; get; }


        [Column("NAME")] public string Name { set; get; }


        [Column("name2")] public string Name2 { set; get; }

        [Required] [Column("COMPANY_ID")] public long CompanyId { set; get; }

        [Column("bsc")] public int? bsc { set; get; }

        [Column("ORDER")] public int Order { set; get; }


        [Column("WEIGHT")] public float Weight { set; get; }


        [Required] [Column("year_id")] public int Year { set; get; }


        [Column("DESCRIPTION")] public string Description { set; get; }


        [Required] [Column("created_by")] public string CreatedBy { set; get; }

        [Required] [Column("created_date")] public DateTime CreatedDate { set; get; }


        [Column("modified_by")] public string ModifiedBy { set; get; }


        [Column("modified_date")] public DateTime? ModifiedDate { set; get; }


        [Column("Result_Percentage")] public float? ResultPercentage { set; get; }

        [Column("Result_Weight_Percentage")] public float? ResultWeightPercentage { set; get; }

        [Column("actual_cost")] public float? ActualCost { set; get; }

        [Column("planned_cost")] public float? PlannedCost { set; get; }
    }
}