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
    [Table("adm_scales")]
    public class ScalesEntity
    {
        [Key] [Required] [Column("ID")] public int ID { get; set; }

        [Required] [Column("SCALE_CODE")] public string SCALE_CODE { get; set; }

        [Required] [Column("NAME")] public string NAME { get; set; }

        [Required] [Column("SCALE_NUMBER")] public int SCALE_NUMBER { get; set; }

        [Required] [Column("COMPANY_ID")] public int COMPANY_ID { get; set; }

        [Required] [Column("created_by")] public string created_by { get; set; }

        [Required] [Column("created_date")] public DateTime created_date { get; set; }

        [Column("modified_by")] public string modified_by { get; set; }

        [Column("modified_date")] public DateTime? modified_date { get; set; }

        [Column("name2")] public string name2 { get; set; }
    }
}