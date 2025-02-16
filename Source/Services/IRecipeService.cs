using Gestion_Bunny.Modeles;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gestion_Bunny.Services
{
    public interface IRecipeService
    {
        Task<List<RecipeCategory>> GetCategoriesAsync();
        Task<Recipe> GetRecipeByIdAsync(int id);
        Task<List<Recipe>> GetRecipesAsync();
        Task DeleteRecipeAsync(int recipeId);
        Task AddRecipeAsync(Recipe recipe);
        Task UpdateRecipeAsync(Recipe recipe);
        Task<RecipeCategory> GetCategoryByIdAsync(int categoryId);
    }
}
