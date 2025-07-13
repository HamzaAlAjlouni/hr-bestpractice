using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.Entities.Infrastructure;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Entities.Admin;

namespace HR.Busniess
{
    [Table("adm_employee_positions")]
    public partial class EmployeePositionsEntity
    {
        [Key] [Required] [Column("ID")] public int ID { set; get; }

        [Required]
        [Column("YEAR")]
        [ForeignKey("Years")]
        public int YEAR { set; get; }

        [Required]
        [Column("POSITION_ID")]
        [ForeignKey("Positions")]
        public int POSITION_ID { set; get; }

        [Required]
        [Column("EMP_ID")]
        [ForeignKey("Employees")]
        public int EMP_ID { set; get; }

        [Column("created_by")] public string created_by { set; get; }

        [Column("created_date")] public DateTime created_date { set; get; }

        [Column("modified_by")] public string modified_by { set; get; }

        [Column("modified_date")] public DateTime? modified_date { set; get; }


        public virtual EmployeesEntity Employees { set; get; }
        public virtual PositionEntity Positions { set; get; }
        public virtual YearEntity Years { set; get; }
    }
}