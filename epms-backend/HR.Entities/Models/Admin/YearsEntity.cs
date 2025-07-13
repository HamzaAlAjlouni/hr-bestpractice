using HR.Busniess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Ameen Alhayek
/// Pure Entity
/// </summary>
namespace HR.Entities.Admin
{
    [Table("adm_years")]
    public class YearEntity
    {
        [Key] [Required] [Column("ID")] public int id { set; get; }

        [Required] [Column("year")] public int year { set; get; }
        [Required] [Column("created_by")] public string CreatedBy { set; get; }

        [Required] [Column("created_date")] public DateTime CreatedDate { set; get; }


        [Column("modified_by")] public string ModifiedBy { set; get; }


        [Column("modified_date")] public DateTime? ModifiedDate { set; get; }

        [NotMapped] public Boolean isActive { set; get; }
    }
}