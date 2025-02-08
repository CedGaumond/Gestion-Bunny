using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Bunny.Modeles
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Le prix ne peut pas être négatif.")]
        public decimal Price { get; set; }

        public byte[]? Pic { get; set; }

        public DateTime? DeletedDate { get; set; }

        public bool IsDeleted { get; set; }


        public int ItemCategoryId { get; set; }
        public ItemCategory ItemCategory { get; set; }

        public List<ItemRecipe> ItemRecipes { get; set; } = new List<ItemRecipe>();
        public List<BillItem> BillItems { get; set; } = new List<BillItem>();
    }
}
