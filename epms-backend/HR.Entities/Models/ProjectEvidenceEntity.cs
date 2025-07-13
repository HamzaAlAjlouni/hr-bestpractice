using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

/// <summary>
/// Ameen Alhayek
/// Pure Entity
/// </summary>
namespace HR.Entities
{
    [Table("adm_project_evidence")]
    public class ProjectEvidenceEntity
    {
        [Key] [Required] [Column("id")] public int ID { set; get; }

        [Column("file_name")] public string FileName { set; get; }

        [Column("file_url")] public string FileUrl { set; get; }

        [Column("file_type")] public string FileType { set; get; }

        [Required] [Column("doc_name")] public string doc_name { set; get; }


        [Column("project_id")] public long? ProjectId { set; get; }

        [Column("objective_id")] public long? objective_id { set; get; }

        [Column("objectiveKPI_id")] public long? ObjectiveKPI_id { set; get; }


        [Required] [Column("created_by")] public string CreatedBy { set; get; }

        [Required] [Column("created_date")] public DateTime CreatedDate { set; get; }


        [Column("modified_by")] public string ModifiedBy { set; get; }


        [Column("modified_date")] public DateTime? ModifiedDate { set; get; }
    }
}