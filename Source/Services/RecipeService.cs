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

        public async Task<List<RecipeCategory>> GetCategoriesAsync()
        {
            return await _context.RecipeCategories.ToListAsync();
        }

        public async Task<List<Recipe>> GetRecipesAsync()
        {
            return await _context.Recipes
                .Include(i => i.RecipeCategory)
                .Include(i => i.RecipeIngredients)
                    .ThenInclude(ir => ir.Ingredient)
                .Where(i => !i.IsDeleted)
                .ToListAsync();
        }

        public async Task<Recipe> GetRecipeByIdAsync(int id)
        {
            return await _context.Recipes
                .Include(i => i.RecipeCategory)
                .Include(i => i.RecipeIngredients)
                    .ThenInclude(ir => ir.Ingredient)
                .FirstOrDefaultAsync(i => i.Id == id && !i.IsDeleted);
        }

        public async Task UpdateRecipeAsync(Recipe recipe)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Clear tracking
                _context.ChangeTracker.Clear();

                // Load existing recipe
                var existingRecipe = await _context.Recipes
                    .Include(i => i.RecipeIngredients)
                    .FirstOrDefaultAsync(i => i.Id == recipe.Id);

                if (existingRecipe == null)
                    throw new InvalidOperationException($"Recipe with ID {recipe.Id} not found.");

                // Update basic properties
                existingRecipe.Name = recipe.Name;
                existingRecipe.Price = recipe.Price;
                existingRecipe.Pic = recipe.Pic;
                existingRecipe.RecipeCategoryId = recipe.RecipeCategoryId;

                // Remove existing relationships
                _context.RecipeIngredients.RemoveRange(existingRecipe.RecipeIngredients);
                await _context.SaveChangesAsync();

                // Add new relationships
                var newItemRecipes = recipe.RecipeIngredients.Select(ir => new RecipeIngredient
                {
                    RecipeId = existingRecipe.Id,
                    IngredientId = ir.IngredientId,
                    Quantity = ir.Quantity
                }).ToList();

                await _context.RecipeIngredients.AddRangeAsync(newItemRecipes);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception($"Failed to update recipe: {ex.Message}", ex);
            }
        }

        public async Task AddRecipeAsync(Recipe recipe)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
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

                await _context.Recipes.AddAsync(newRecipe);
                await _context.SaveChangesAsync();

                // Add relationships
                var newItemRecipes = recipe.RecipeIngredients.Select(ir => new RecipeIngredient
                {
                    RecipeId = newRecipe.Id,
                    IngredientId = ir.IngredientId,
                    Quantity = ir.Quantity
                }).ToList();

                await _context.RecipeIngredients.AddRangeAsync(newItemRecipes);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception($"Failed to add recipe: {ex.Message}", ex);
            }
        }

        public async Task DeleteRecipeAsync(int recipeId)
        {
            var recipe = await _context.Recipes.FirstOrDefaultAsync(r => r.Id == recipeId);
            if (recipe != null)
            {
                recipe.IsDeleted = true;
                recipe.DeletedDate = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<RecipeCategory> GetCategoryByIdAsync(int categoryId)
        {
            return await _context.RecipeCategories.FirstOrDefaultAsync(c => c.Id == categoryId);
        }
    }
}
