using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_Bunny.Modeles
{
    [Table("items", Schema = "public")] 
    public class Item
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Column("name")]
        public string Name { get; set; }

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

        // Foreign Key property
        [ForeignKey("ItemCategory")]  
        [Column("item_category_id")] 
        public int ItemCategoryId { get; set; }

        public ItemCategory ItemCategory { get; set; }

        public ICollection<ItemRecipe> ItemRecipes { get; set; } = new List<ItemRecipe>();
        public ICollection<BillItem> BillItems { get; set; } = new List<BillItem>();
    }
}
