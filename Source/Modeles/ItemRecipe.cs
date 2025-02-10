using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Bunny.Modeles
{
    public class ItemRecipe
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public int ItemId { get; set; }
        public decimal Quantity { get; set; }

        // Add navigation properties if necessary
        public Recipe Recipe { get; set; }
        public Item Item { get; set; }
    }

}
