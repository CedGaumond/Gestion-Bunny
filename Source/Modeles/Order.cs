using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Bunny.Modeles
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public Employee Employee { get; set; } 
        public DateTime OrderDate { get; set; }
        public Dictionary<Item, int> ItemQuantity { get; set; }
        
    }
}
