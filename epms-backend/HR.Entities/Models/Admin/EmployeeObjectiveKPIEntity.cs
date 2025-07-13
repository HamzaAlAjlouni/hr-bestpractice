using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Ameen Alhayek
/// Pure Entity
/// </summary>
namespace HR.Entities.Admin
{
    [Table("adm_emp_obj_kpi")]
    public class EmployeeObjectiveKPIEntity
    {
        /*

id int(11) AI PK
name varchar(1000)
name2 varchar(1000)
created_by varchar(50)
created_date date
modified_by varchar(50)
modified_date date
emp_obj_id int(11)
target float(18,3)
*/
        [Key] [Required] public long ID { get; set; }
        [Required] public string name { get; set; }
        public string name2 { get; set; }
        [Required] public string created_by { get; set; }
        [Required] public DateTime created_date { get; set; }
        public string modified_by { get; set; }

        public DateTime? modified_date { get; set; }
        
        //1 better up, 2 better down
        [Required] [Column("better_up_down")] public int BetterUpDown { set; get; } = 1;


        [Required] public long emp_obj_id { get; set; }
        [Required] public decimal target { get; set; }

        [Column("result")] public float? Result { get; set; }

        [Column("note")] public string Note { get; set; }

        [Column("result_pref_segment_id")] public long? SegmentId { get; set; }
    }
}