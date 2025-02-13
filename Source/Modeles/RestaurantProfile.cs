using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_Bunny.Modeles
{
    [Table("restaurant_profile", Schema = "public")] 
    public class RestaurantProfile
    {
        [Key]
        [Column("id")] 
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Column("restaurant_name")]
        public string RestaurantName { get; set; }

        [Required]
        [StringLength(255)]
        [Column("restaurant_address")] 
        public string RestaurantAddress { get; set; }

        [StringLength(100)]
        [Column("opening_hours")]
        public string OpeningHours { get; set; }
    }
}
