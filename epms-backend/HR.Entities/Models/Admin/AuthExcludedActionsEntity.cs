using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Ameen Alhayek
/// Pure Entity
/// </summary>
namespace HR.Entities.Admin
{
    [Table("adm_auth_actions_excluded")]
    public class AuthExcludedActionsEntity
    {
        [Key] [Required] public decimal id { get; set; }
        [Required] public decimal action_id { get; set; }
        [Required] public decimal is_readonly { get; set; }
        [Required] public decimal is_invisible { get; set; }
        [Required] public decimal role_id { get; set; }
    }
}