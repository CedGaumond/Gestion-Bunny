using Gestion_Bunny.Modeles;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gestion_Bunny.Services
{
    public interface IRecipeService
    {
        List<RecipeCategory> GetCategories();
        Recipe GetRecipeById(int id);
        List<Recipe> GetRecipes();
        void DeleteRecipe(int recipeId);
        void AddRecipe(Recipe recipe);
        void UpdateRecipe(Recipe recipe);
        RecipeCategory GetCategoryById(int categoryId);
    }
}
