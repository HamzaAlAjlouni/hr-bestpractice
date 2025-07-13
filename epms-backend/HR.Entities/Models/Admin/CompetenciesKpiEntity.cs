using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Ameen Alhayek
/// Pure Entity
/// </summary>
namespace HR.Entities.Admin
{
    [Table("adm_competencies_kpi")]
    public class CompetenciesKpiEntity
    {
        [Required] public string NAME { get; set; }
        [Required] public long c_kpi_type_id { get; set; }
        [Required] public long competence_id { get; set; }
        public string name2 { get; set; }
        [Key] [Required] public long ID { get; set; }
        [Required] public string created_by { get; set; }
        [Required] public DateTime created_date { get; set; }
        public string modified_by { get; set; }
        public DateTime? modified_date { get; set; }
    }
}