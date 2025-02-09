using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Bunny.Modeles
{
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

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

        [Required]
        public bool IsDeleted { get; set; }

        public List<ItemRecipe> ItemRecipes { get; set; } = new List<ItemRecipe>();
        public List<BillIngredient> BillIngredients { get; set; } = new List<BillIngredient>();

    }

}
