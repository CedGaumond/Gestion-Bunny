using Gestion_Bunny.Modeles;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gestion_Bunny.Services
{
    public interface IItemService
    {
        Task<List<ItemCategory>> GetCategoriesAsync();
        Task<List<Item>> GetRecipesAsync();
        Task DeleteRecipeAsync(int recipeId);
    }
}
