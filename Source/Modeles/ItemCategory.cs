using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_Bunny.Modeles
{
    [Table("item_category", Schema = "public")] 
    public class ItemCategory
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Column("name")] 
        public string Name { get; set; }

        public ICollection<Item> Items { get; set; } = new List<Item>(); 
    }
}
