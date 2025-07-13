using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Entities.Models
{
    [Table("adm_project_evaluation_values")]
    public class ProjectEvaluationValuesEntity
    {
        [Key] [Required] [Column("id")] public int ID { get; set; }

        [Column("WEIGHT")] public float Weight { set; get; }

        [Required]
        [ForeignKey(name: "ProjectEvaluation")]
        [Column("EVALUATION_ID")]
        public int EVALUATION_ID { get; set; }

        [Required] [Column("name")] public string Name { get; set; }

        [Required] public string created_by { get; set; }
        [Required] public DateTime created_date { get; set; }
        public string modified_by { get; set; }

        public DateTime? modified_date { get; set; }

        public virtual ProjectEvaluationEntity ProjectEvaluation { get; set; }
    }
}