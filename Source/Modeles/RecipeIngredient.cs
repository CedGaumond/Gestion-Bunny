using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_Bunny.Modeles
{
    [Table("recipe_ingredients", Schema = "public")]
    public class RecipeIngredient
    {
        [Column("recipe_id")] 
        [ForeignKey("Recipe")] 
        public int RecipeId { get; set; }
        public Recipe? Recipe { get; set; }

        [Column("ingredient_id")] 
        [ForeignKey("Ingredient")]
        public int IngredientId { get; set; }
        public Ingredient? Ingredient { get; set; }

        [Required]
        [Column("quantity")]
        [Range(0, double.MaxValue, ErrorMessage = "La quantité ne peut pas être négative.")]
        [DefaultValue(0)]
        public decimal Quantity { get; set; }
    }
}
