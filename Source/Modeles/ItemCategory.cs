using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Bunny.Modeles{

 public class ItemCategory
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<ItemRecipe> Recipes { get; set; } 

}



}
   
