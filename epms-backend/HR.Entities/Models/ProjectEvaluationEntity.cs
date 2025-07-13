using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Entities.Models
{
    [Table("adm_project_evaluation")]
    public class ProjectEvaluationEntity
    {
        [Key] [Required] [Column("id")] public int ID { get; set; }

        [Required] [Column("evaluation_type")] public int Type { get; set; }


        [Column("WEIGHT")] public float Weight { set; get; }


        [Required] [Column("name")] public string Name { get; set; }

        [Required] public string created_by { get; set; }
        [Required] public DateTime created_date { get; set; }
        public string modified_by { get; set; }

        public DateTime? modified_date { get; set; }


        public virtual ICollection<ProjectEvaluationValuesEntity> ProjectEvaluationValues { get; set; }
    }
}