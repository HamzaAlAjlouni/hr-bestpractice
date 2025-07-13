using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Ameen Alhayek
/// Pure Entity
/// </summary>
namespace HR.Entities.Admin
{
    [Table("adm_employee_education")]
    public class EmployeeEducationEntity
    {
        [Key] [Required] public long id { get; set; }

        [Required] public int year_id { get; set; }
        [Required] public int employee_id { get; set; }
        [Required] public int company_id { get; set; }

        public string field { get; set; }

        public string field2 { get; set; }
        [Required] public int type { get; set; }
        [Required] public int method { get; set; }
        [Required] public int priority { get; set; }
        [Required] public int execution_period { get; set; }
        [Required] public int status { get; set; }

        public string notes { get; set; }

        public int? emp_competency_id { get; set; }

        public int? emp_obj_id { get; set; }

        [Required] public string created_by { get; set; }
        [Required] public DateTime created_date { get; set; }
        public string modified_by { get; set; }

        public DateTime? modified_date { get; set; }
    }
}