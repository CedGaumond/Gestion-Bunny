using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Bunny.Modeles
{
    public class RestaurantProfile
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string RestaurantName { get; set; }

        [Required]
        [StringLength(255)]
        public string RestaurantAddress { get; set; }

        [StringLength(100)]
        public string OpeningHours { get; set; }
    }
}
