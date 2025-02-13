using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_Bunny.Modeles
{
    [Table("bill_ingredients", Schema = "public")]
    public class BillIngredient
    {
        [Column("bill_customer_id")]
        public int BillCustomerId { get; set; }
        public BillCustomer BillCustomer { get; set; }

        [Column("ingredient_id")]
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }

        [Required]
        [Column("quantity")]
        [Range(0, double.MaxValue, ErrorMessage = "La quantité ne peut pas être négative.")]
        public float Quantity { get; set; }
    }
}
