using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Ameen Alhayek
/// Pure Entity
/// </summary>
namespace HR.Entities.Admin
{
    [Table("adm_actions")]
    public class ActionsEntity
    {
        [Key] [Required] public decimal id { get; set; }
        [Required] public string name { get; set; }
        [Required] public decimal menu_id { get; set; }
    }
}