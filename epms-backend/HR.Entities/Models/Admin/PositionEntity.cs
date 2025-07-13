using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Ameen Alhayek
/// Pure Entity
/// </summary>
namespace HR.Entities.Admin
{
    [Table("adm_positions")]
    public class PositionEntity
    {
        [Key] [Required] public int ID { get; set; }

        [Required] public string CODE { get; set; }
        [Required] public string NAME { get; set; }
        [Required] public int IS_MANAGMENT { get; set; }
        [Required] public int COMPANY_ID { get; set; }
        public string name2 { get; set; }


        [Required] public string created_by { get; set; }
        [Required] public DateTime created_date { get; set; }
        public string modified_by { get; set; }
        public DateTime? modified_date { get; set; }
    }
}