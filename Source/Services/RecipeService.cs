using Gestion_Bunny.Modeles;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Bunny.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly ApplicationDbContext _context;

        public RecipeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<RecipeCategory> GetCategories()
        {
            return _context.RecipeCategories.ToList();
        }

        public List<Recipe> GetRecipes()
        {
            return  _context.Recipes
                .Include(i => i.RecipeCategory)
                .Include(i => i.RecipeIngredients)
                    .ThenInclude(ir => ir.Ingredient)
                .Where(i => !i.IsDeleted)
                .ToList();
        }

        public Recipe GetRecipeById(int id)
        {
            return _context.Recipes
                .Include(i => i.RecipeCategory)
                .Include(i => i.RecipeIngredients)
                    .ThenInclude(ir => ir.Ingredient)
                .FirstOrDefault(i => i.Id == id && !i.IsDeleted);
        }

        public void UpdateRecipe(Recipe recipe)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                // Clear tracking
                _context.ChangeTracker.Clear();

                // Load existing recipe
                var existingRecipe = _context.Recipes
                    .Include(i => i.RecipeIngredients)
                    .FirstOrDefault(i => i.Id == recipe.Id);

                if (existingRecipe == null)
                    throw new InvalidOperationException($"Recipe with ID {recipe.Id} not found.");

                // Update basic properties
                existingRecipe.Name = recipe.Name;
                existingRecipe.Price = recipe.Price;
                existingRecipe.Pic = recipe.Pic;
                existingRecipe.RecipeCategoryId = recipe.RecipeCategoryId;

                // Remove existing relationships
                _context.RecipeIngredients.RemoveRange(existingRecipe.RecipeIngredients);
                _context.SaveChanges();

                // Add new relationships
                var newItemRecipes = recipe.RecipeIngredients.Select(ir => new RecipeIngredient
                {
                    RecipeId = existingRecipe.Id,
                    IngredientId = ir.IngredientId,
                    Quantity = ir.Quantity
                }).ToList();

                _context.RecipeIngredients.AddRange(newItemRecipes);
                _context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception($"Failed to update recipe: {ex.Message}", ex);
            }
        }

        public void AddRecipe(Recipe recipe)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                // Clear tracking
                _context.ChangeTracker.Clear();

                // Create new recipe
                var newRecipe = new Recipe
                {
                    Name = recipe.Name,
                    Price = recipe.Price,
                    Pic = recipe.Pic,
                    RecipeCategoryId = recipe.RecipeCategoryId,
                    IsDeleted = false,
                    DeletedDate = null
                };

                _context.Recipes.Add(newRecipe);
                _context.SaveChanges();

                // Add relationships
                var newItemRecipes = recipe.RecipeIngredients.Select(ir => new RecipeIngredient
                {
                    RecipeId = newRecipe.Id,
                    IngredientId = ir.IngredientId,
                    Quantity = ir.Quantity
                }).ToList();

                _context.RecipeIngredients.AddRange(newItemRecipes);
                _context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception($"Failed to add recipe: {ex.Message}", ex);
            }
        }

        public void DeleteRecipe(int recipeId)
        {
            var recipe = _context.Recipes.FirstOrDefault(r => r.Id == recipeId);
            if (recipe != null)
            {
                recipe.IsDeleted = true;
                recipe.DeletedDate = DateTime.UtcNow;
                _context.SaveChanges();
            }
        }

        public RecipeCategory GetCategoryById(int categoryId)
        {
            return _context.RecipeCategories.FirstOrDefault(c => c.Id == categoryId);
        }
    }
}
