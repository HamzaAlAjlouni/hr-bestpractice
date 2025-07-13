using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

/// <summary>
/// Ameen Alhayek
/// Pure Entity
/// </summary>
namespace HR.Entities
{
    [Table("adm_approval_setup")]
    public class ApprovalSetupEntity
    {
        [Key] [Required] [Column("id")] public int ID { set; get; }

        [Required] [Column("name")] public string name { set; get; }

        [Required] [Column("page_url")] public string page_url { set; get; }

        [Required] [Column("reviewing_user")] public string reviewing_user { set; get; }

        [Required] [Column("company_id")] public int company_id { set; get; }
    }
}