using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Bunny.Modeles
{
    public class BillItem
    {
        [Key]
        public int BillCustomerId { get; set; }
        public BillCustomer BillCustomer { get; set; }

        [Key]
        public int ItemId { get; set; }
        public Item Item { get; set; }

        public decimal Quantity { get; set; }
        public int QuantityDeleted { get; set; }
    }
}
