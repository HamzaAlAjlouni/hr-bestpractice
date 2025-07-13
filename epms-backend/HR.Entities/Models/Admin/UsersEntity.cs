using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Ameen Alhayek
/// Pure Entity
/// </summary>
namespace HR.Entities.Admin
{
    [Table("adm_users")]
    public class UsersEntity
    {
        [Key] [Required] public string USERNAME { get; set; }
        [Required] public string NAME { get; set; }
        [Required] public decimal COMPANY_ID { get; set; }
        [Required] public string PASSWORD { get; set; }

        public int unit_id { get; set; }

        [Required] public string created_by { get; set; }
        [Required] public DateTime created_date { get; set; }
        public string modified_by { get; set; }

        public DateTime? modified_date { get; set; }
        public string name2 { get; set; }
    }
}