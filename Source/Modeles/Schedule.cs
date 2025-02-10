using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Bunny.Modeles
{
    public class Schedule
    {
        [Key]
        public int Id { get; set; }

        public DateTime ShiftStart { get; set; }
        public DateTime ShiftEnd { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
