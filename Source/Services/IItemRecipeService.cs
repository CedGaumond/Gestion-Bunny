using Gestion_Bunny.Modeles;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gestion_Bunny.Services
{
    public interface IItemRecipeService
    {
        Task<List<ItemCategory>> GetCategoriesAsync();
        Task<List<Recipe>> GetRecipesAsync();
        Task DeleteRecipeAsync(int recipeId);
    }
}
