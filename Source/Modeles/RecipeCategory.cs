using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_Bunny.Modeles
{
    [Table("recipe_category", Schema = "public")] 
    public class RecipeCategory
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Column("name")] 
        public required string Name { get; set; }

        public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>(); 
    }
}
