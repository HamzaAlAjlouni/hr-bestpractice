using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Entities.Models
{
    [Table("adm_project_evaluated_values")]
    public class ProjectEvaluatedValuesEntity
    {
        [Key] [Column("ID")] public int ID { get; set; }

        [Required]
        [Column("EVALUATION_VALUE_ID")]
        public int EVALUATION_VALUE_ID { get; set; }

        [Required] [Column("PROJECT_ID")] public int PROJECT_ID { get; set; }
    }
}