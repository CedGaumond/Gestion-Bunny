using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_Bunny.Modeles
{
    [Table("ingredients", Schema = "public")] 
    public class Ingredient
    {
        [Key]
        [Column("id")] 
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Column("name")]
        public required string Name { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "La quantité restante ne peut pas être négative.")]
        [Column("quantity_remaining")] 
        public decimal QuantityRemaining { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "La quantité par unité de livraison ne peut pas être négative.")]
        [Column("quantity_per_delivery_unit")]
        public decimal QuantityPerDeliveryUnit { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Le seuil de notification minimum ne peut pas être négatif.")]
        [Column("minimum_threshold_notification")] 
        public decimal MinimumThresholdNotification { get; set; }

        [Column("deleted_date")] 
        public DateTime? DeletedDate { get; set; }

        [Column("is_deleted")]
        [DefaultValue(false)] 
        public bool IsDeleted { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Le prix ne peut pas être négatif.")]
        [Column("price")]
        public decimal Price { get; set; }

        public ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();

        public ICollection<OrderIngredient> OrderIngredients { get; set; } = new List<OrderIngredient>();
    }
}
