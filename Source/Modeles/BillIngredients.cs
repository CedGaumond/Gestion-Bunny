using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Bunny.Modeles
{
    public class BillIngredient
    {
        [Key]
        public int BillCustomerId { get; set; }
        public BillCustomer BillCustomer { get; set; }

        [Key]
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }

        public float Quantity { get; set; }
    }
}
