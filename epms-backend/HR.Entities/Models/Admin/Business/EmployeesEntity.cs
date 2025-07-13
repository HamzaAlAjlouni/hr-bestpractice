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
    [Table("adm_employees")]
    public partial class EmployeesEntity
    {
        [Key] [Required] [Column("ID")] public int ID { set; get; }

        [Required] [Column("employee_number")] public string employee_number { set; get; }

        [Required] [Column("name1_1")] public string name1_1 { set; get; }

        [Column("name1_2")] public string name1_2 { set; get; }

        [Column("name1_3")] public string name1_3 { set; get; }

        [Required] [Column("name1_4")] public string name1_4 { set; get; }

        [Required]
        [Column("UNIT_ID")]
        [ForeignKey("UNITS")]
        public int UNIT_ID { set; get; }


        [Required]
        [Column("COMPANY_ID")]
        [ForeignKey("Companies")]
        public int COMPANY_ID { set; get; }


        [Column("ADDRESS")] public string ADDRESS { set; get; }

        [Column("PHONE1")] public string PHONE1 { set; get; }

        [Column("PHONE2")] public string PHONE2 { set; get; }

        [Column("PARENT_ID")]
        [ForeignKey("Parent")]
        public int? PARENT_ID { set; get; }

        [Required] [Column("IS_STATUS")] public int IS_STATUS { set; get; }

        [Column("IMAGE")] public string IMAGE { set; get; }

        [Required]
        [Column("BRANCH_ID")]
        [ForeignKey("Branches")]
        public int BRANCH_ID { set; get; }

        [Required]
        [Column("SCALE_ID")]
        [ForeignKey("Scales")]
        public int SCALE_ID { set; get; }

        [Required] [Column("START_DATE")] public DateTime START_DATE { set; get; }

        [Column("END_DATE")] public DateTime? END_DATE { set; get; }

        [Column("created_by")] public string created_by { set; get; }

        [Column("created_date")] public DateTime created_date { set; get; }

        [Column("modified_by")] public string modified_by { set; get; }

        [Column("modified_date")] public DateTime? modified_date { set; get; }

        [Column("name2_1")] public string name2_1 { set; get; }

        [Column("name2_2")] public string name2_2 { set; get; }

        [Column("name2_3")] public string name2_3 { set; get; }

        [Column("name2_4")] public string name2_4 { set; get; }

        //added by yousef sleit
        [NotMapped] public int positionId { set; get; }

        public virtual ScalesEntity Scales { set; get; }
        public virtual UnitEntity UNITS { set; get; }
        public virtual CompanyEntity Companies { set; get; }
        public virtual EmployeesEntity Parent { set; get; }
        public virtual BranchesEntity Branches { set; get; }
    }
}