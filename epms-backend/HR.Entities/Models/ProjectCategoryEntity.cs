using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Entities.Models
{
    [Table("adm_project_category")]
    public class ProjectCategoryEntity
    {
        [Key] [Required] [Column("ID")] public int ID { set; get; }

        [Required] [Column("CODE")] public string Code { set; get; }

        [Required] [Column("NAME")] public string Name { set; get; }
    }
}