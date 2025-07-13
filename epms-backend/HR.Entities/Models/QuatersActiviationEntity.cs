using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Entities.Models
{
    [Table("adm_quaters_activation")]
    public class QuatersActiviationEntity
    {
        [Key] [Required] [Column("ID")] public int ID { set; get; }


        [Required] [Column("status")] public int Status { set; get; }


        [Column("name")] public string Name { get; set; }


        [Required] [Column("created_by")] public string CreatedBy { set; get; }

        [Required] [Column("created_date")] public DateTime CreatedDate { set; get; }


        [Column("modified_by")] public string ModifiedBy { set; get; }


        [Column("modified_date")] public DateTime? ModifiedDate { set; get; }
    }
}