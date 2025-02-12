using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Gestion_Bunny.Modeles
{
    public class Ingredient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public required string Name { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "La quantité restante ne peut pas être négative.")]
        public decimal QuantityRemaining { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "La quantité par unité de livraison ne peut pas être négative.")]
        public decimal QuantityPerDeliveryUnit { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Le seuil de notification minimum ne peut pas être négatif.")]
        public decimal MinimumThresholdNotification { get; set; }

        public DateTime? DeletedDate { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<ItemRecipe> ItemRecipes { get; set; } = new List<ItemRecipe>();
    }

}
