using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

/// <summary>
/// Ameen Alhayek
/// Pure Entity
/// </summary>
namespace HR.Entities
{
    [Table("adm_projects_calculation_setup")]
    public class ProjectCalculationSetupEntity
    {
        [Key] [Required] [Column("id")] public int Id { set; get; }
        
        //1 -> kpi
        //2 -> objective
        [Required] [Column("calculation_setup")] public int Calculation { set; get; }
        

       
    }
}