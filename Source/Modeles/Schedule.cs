using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_Bunny.Modeles
{
    [Table("schedule", Schema = "public")]
    public class Schedule
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("shift_start")]
        public DateTime ShiftStart { get; set; }

        [Required]
        [Column("shift_end")]
        public DateTime ShiftEnd { get; set; }

        [Required]
        [Column("employee_id")]
        public int? EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee? Employee { get; set; }
    }
}