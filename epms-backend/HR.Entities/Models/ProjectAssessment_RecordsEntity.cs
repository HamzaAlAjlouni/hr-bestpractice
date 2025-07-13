using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.Entities.Infrastructure;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.Entities
{
    [Table("ProjectAssessment_Records")]
    public partial class ProjectAssessmentRecordsEntity
    {
        [Key] [Required] [Column("id")] public long id { set; get; }

        [Required] [Column("ProjectAssessmentId")] public int ProjectAssessmentId { set; get; }
        

        [Required] [Column("PerformanceDate")] public DateTime PerformanceDate { set; get; }
        [Required] [Column("Behavior")] public string Behavior { set; get; }

        [Required] [Column("Effect")] public bool Effect { set; get; }
        [Required] [Column("AgreedAction")] public string AgreedAction { set; get; }
        [Required] [Column("Comments")] public string Comment { set; get; }

        [Column("created_by")] public string CreatedBy { set; get; }

        [Required] [Column("created_date")] public DateTime CreatedDate { set; get; }

        [Column("modified_by")] public string ModifiedBy { set; get; }

        [Column("modified_date")] public DateTime? ModifiedDate { set; get; }
    }
}