using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Entities.Models
{
    [Table("adm_project_approvals_history")]
    public class ProjectApprovalHistoryEntity
    {
        [Key] [Required] [Column("ID")] public int ID { set; get; }

        [Required] [Column("project_id")] public int ProjectId { set; get; }

        [Required] [Column("status")] public int Status { set; get; }


        [Column("note")] public string Note { get; set; }


        [Required] [Column("created_by")] public string CreatedBy { set; get; }

        [Required] [Column("created_date")] public DateTime CreatedDate { set; get; }
    }
}