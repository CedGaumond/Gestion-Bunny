using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_Bunny.Modeles
{
    [Table("item_recipe", Schema = "public")]
    public class ItemRecipe
    {
        [Column("item_id")] 
        [ForeignKey("Item")] 
        public int ItemId { get; set; }
        public Item Item { get; set; }

        [Column("ingredient_id")] 
        [ForeignKey("Ingredient")]
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }

        [Required]
        [Column("quantity")]
        [Range(0, double.MaxValue, ErrorMessage = "La quantité ne peut pas être négative.")]
        [DefaultValue(0)]
        public decimal Quantity { get; set; }
    }
}
