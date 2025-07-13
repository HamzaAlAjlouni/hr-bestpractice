using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Ameen Alhayek
/// Pure Entity
/// </summary>
namespace HR.Entities.Admin
{
    [Table("adm_assessment_map")]
    public class AssessmentMapping
    {
        [Key] [Required] public int id { get; set; }
        [Required] public long company_id { get; set; }
        [Required] public float from { get; set; }
        [Required] public float to { get; set; }
        [Required] public float point { get; set; }
        [Required] public string color { get; set; }
    }
}