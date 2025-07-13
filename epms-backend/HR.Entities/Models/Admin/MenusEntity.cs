using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Ameen Alhayek
/// Pure Entity
/// </summary>
namespace HR.Entities.Admin
{
    [Table("adm_menus")]
    public class MenusEntity
    {
        [Key] [Required] public decimal ID { get; set; }
        [Required] public string NAME { get; set; }
        public string URL { get; set; }
        public string ICONE { get; set; }

        public decimal? PARENT_ID { get; set; }
        [Required] public decimal COMPANY_ID { get; set; }
        [Required] public string created_by { get; set; }
        [Required] public DateTime created_date { get; set; }
        public string modified_by { get; set; }

        public DateTime? modified_date { get; set; }
        public string name2 { get; set; }

        [Required] public decimal order { get; set; }

        [Required] public string application_code { get; set; }
        [Required] public string system_code { get; set; }
        public string year_grade { get; set; }
    }
}