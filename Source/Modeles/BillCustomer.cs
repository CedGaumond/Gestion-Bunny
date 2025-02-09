using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Bunny.Modeles
{
    public class BillCustomer
    {
        [Key]
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }
        public byte[] BillFile { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public ICollection<BillItem> BillItems { get; set; } 
        public ICollection<BillIngredient> BillIngredients { get; set; }
    }
}
