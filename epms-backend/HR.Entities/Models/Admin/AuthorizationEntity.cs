using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Ameen Alhayek
/// Pure Entity
/// </summary>
namespace HR.Entities.Admin
{
    [Table("adm_authorization")]
    public class AuthorizationEntity
    {
        [Key] [Required] public decimal ID { get; set; }
        [Required] public decimal MENU_ID { get; set; }
        [Required] public decimal ROLE_ID { get; set; }
        [Required] public string created_by { get; set; }
        [Required] public DateTime created_date { get; set; }
        public string modified_by { get; set; }

        public DateTime? modified_date { get; set; }
    }
}