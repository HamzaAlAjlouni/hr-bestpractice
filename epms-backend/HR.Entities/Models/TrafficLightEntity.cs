using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

/// <summary>
/// Ameen Alhayek
/// Pure Entity
/// </summary>
namespace HR.Entities
{
    [Table("adm_traffic_light_indicator")]
    public class TrafficLightEntity
    {
        [Key] [Required] [Column("id")] public int ID { set; get; }

        [Required] [Column("name")] public string name { set; get; }


        [Required] [Column("perc_from")] public float perc_from { set; get; }

        [Required] [Column("perc_to")] public float perc_to { set; get; }

        [Required] [Column("color")] public string color { set; get; }

        [Required] [Column("company_id")] public int company_id { set; get; }

        [Required] [Column("year")] public int year { set; get; }
    }
}