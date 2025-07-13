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
    [Table("adm_units")]
    public class UnitEntity
    {
        [Key] [Required] public int ID { get; set; }
        [Required] public string NAME { get; set; }
        [Required] public string CODE { get; set; }
        public string ADDRESS { get; set; }
        public string PHONE1 { get; set; }
        public string PHONE2 { get; set; }
        public string FAX { get; set; }
        public int? parent_id { get; set; }
        [Required] public int COMPANY_ID { get; set; }
        [Required] public int C_UNIT_TYPE_ID { get; set; }

        [Required] public string created_by { get; set; }
        [Required] public DateTime created_date { get; set; }
        public string modified_by { get; set; }

        public DateTime? modified_date { get; set; }
        public string name2 { get; set; }
    }
}