using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Ameen Alhayek
/// Pure Entity
/// </summary>
namespace HR.Entities.Admin
{
    [Table("adm_roles")]
    public class RolesEntity
    {
        [Key] [Required] public decimal ID { get; set; }
        [Required] public string NAME { get; set; }
        [Required] public decimal COMPANY_ID { get; set; }
        [Required] public decimal IS_SYSTEM_ROLE { get; set; }
        [Required] public string created_by { get; set; }
        [Required] public DateTime created_date { get; set; }
        public string modified_by { get; set; }

        public DateTime? modified_date { get; set; }
        public string name2 { get; set; }
    }
}