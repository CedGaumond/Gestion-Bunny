using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_Bunny.Modeles
{
    [Table("recipes", Schema = "public")] 
    public class Recipe
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Column("name")]
        public string? Name { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Le prix ne peut pas être négatif.")]
        [Column("price")]  
        public decimal Price { get; set; }

        [Column("pic")]  
        public byte[]? Pic { get; set; }

        [Column("deleted_date")]  
        public DateTime? DeletedDate { get; set; }

        [Column("is_deleted")] 
        [DefaultValue(false)] 
        public bool IsDeleted { get; set; }

        [Column("recipe_category_id")] 
        public int RecipeCategoryId { get; set; }

        [ForeignKey("RecipeCategoryId")]
        public RecipeCategory? RecipeCategory { get; set; }

        public ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
        public ICollection<BillRecipe> BillRecipes { get; set; } = new List<BillRecipe>();
    }
}
