using Gestion_Bunny.Modeles;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gestion_Bunny.Services
{
    public interface IItemService
    {
        Task<List<ItemCategory>> GetCategoriesAsync();
        Task<Item> GetRecipeByIdAsync(int id);
        Task<List<Item>> GetRecipesAsync();
        Task DeleteRecipeAsync(int recipeId);
        Task AddRecipeAsync(Item recipe);
        Task UpdateRecipeAsync(Item recipe);
        Task<ItemCategory> GetCategoryByIdAsync(int categoryId);
    }
}
