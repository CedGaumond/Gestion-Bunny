using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_Bunny.Modeles
{
    [Table("bill_recipes", Schema = "public")]  
    public class BillRecipe
    {
        [Column("bill_id")] 
        public int BillId { get; set; }
        public  Bill? Bill { get; set; }

        [Column("recipe_id")]  
        public int RecipeId { get; set; }
        public Recipe? Recipe { get; set; }

        [Required]
        [Column("quantity")]  
        [Range(0, int.MaxValue, ErrorMessage = "La quantité ne peut pas être négative.")]
        public int Quantity { get; set; }

        [Required]
        [Column("quantity_deleted")] 
        public int QuantityDeleted { get; set; }
    }
}
