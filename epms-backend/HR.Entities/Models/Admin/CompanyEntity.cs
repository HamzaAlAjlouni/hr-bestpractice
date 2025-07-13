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
    [Table("adm_company")]
    public class CompanyEntity
    {
        [Key] [Required] [Column("id")] public int id { set; get; }

        [Required] [Column("Code")] public string Code { set; get; }

        [Required] [Column("NAME")] public string Name { set; get; }

        [Required] [Column("ADDRESS")] public string Address { set; get; }

        [Required] [Column("FAX")] public string Fax { set; get; }

        [Required] [Column("PHONE1")] public string Phone1 { set; get; }

        [Required] [Column("PHONE2")] public string Phone2 { set; get; }

        [Required] [Column("EMAIL")] public string Email { set; get; }

        [Required] [Column("WEBSITE")] public string Website { set; get; }
        [Required] public string created_by { get; set; }
        [Required] public DateTime created_date { get; set; }
        public string modified_by { get; set; }

        public DateTime? modified_date { get; set; }
        public string name2 { get; set; }

        [Column("mission")] public string Mission { get; set; }

        [Column("company_values")] public string company_values { get; set; }


        [Column("vision")] public string Vision { get; set; }
        [Required] [Column("currency_c")] public int CurrencyCode { get; set; }

        [Required]
        [Column("projects_link")] //link project with kpi or plan	 
        public int ProjectsLink { get; set; }

        [Required]
        [Column("plan_link")] //خطة تنفيذية تحت المشاريع او من خلال الخطط الفردية الموظفيين	 
        public int PlansLink { get; set; }


        [Required]
        [Column("objective_factor")]
        public float ObjectiveFactor { get; set; }


        [Required]
        [Column("competency_factor")]
        public float CompetencyFactor { get; set; }
    }
}