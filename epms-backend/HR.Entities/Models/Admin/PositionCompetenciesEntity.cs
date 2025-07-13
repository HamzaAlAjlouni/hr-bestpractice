using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Ameen Alhayek
/// Pure Entity
/// </summary>
namespace HR.Entities.Admin
{
    [Table("adm_position_competencies")]
    public class PositionCompetenciesEntity
    {
        [Key] [Required] public int ID { get; set; }

        [Required] public int position_id { get; set; }

        [Required] public int competence_id { get; set; }

        [Required] public int competence_level { get; set; }
    }
}