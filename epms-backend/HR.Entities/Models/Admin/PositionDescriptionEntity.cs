using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Ameen Alhayek
/// Pure Entity
/// </summary>
namespace HR.Entities.Admin
{
    [Table("adm_pos_description")]
    public class PositionDescriptionEntity
    {
        [Key] [Required] [Column("id")] public int id { set; get; }
        [Required] [Column("position_id")] public int Position_ID { set; get; }


        [Required] [Column("name")] public string Name { set; get; }
        public string name2 { get; set; }

        [Required] public string created_by { get; set; }
        [Required] public DateTime created_date { get; set; }
        public string modified_by { get; set; }

        public DateTime? modified_date { get; set; }
    }
}