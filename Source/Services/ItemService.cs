using Gestion_Bunny.Modeles;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gestion_Bunny.Services
{
    public class ItemService : IItemService
    {
        private readonly ApplicationDbContext _context;

        public ItemService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ItemCategory>> GetCategoriesAsync()
        {
            return await _context.ItemCategories.ToListAsync();
        }

        public async Task<List<Item>> GetRecipesAsync()
        {
            return await _context.Items
                .Include(i => i.ItemCategory)
                .Include(i => i.ItemRecipes)
                    .ThenInclude(ir => ir.Ingredient)
                .Where(i => !i.IsDeleted)
                .ToListAsync();
        }

        public async Task<Item> GetRecipeByIdAsync(int id)
        {
            return await _context.Items
                .Include(i => i.ItemCategory)
                .Include(i => i.ItemRecipes)
                    .ThenInclude(ir => ir.Ingredient)
                .FirstOrDefaultAsync(i => i.Id == id && !i.IsDeleted);
        }

        public async Task UpdateRecipeAsync(Item recipe)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Clear tracking
                _context.ChangeTracker.Clear();

                // Load existing recipe
                var existingRecipe = await _context.Items
                    .Include(i => i.ItemRecipes)
                    .FirstOrDefaultAsync(i => i.Id == recipe.Id);

                if (existingRecipe == null)
                    throw new InvalidOperationException($"Recipe with ID {recipe.Id} not found.");

                // Update basic properties
                existingRecipe.Name = recipe.Name;
                existingRecipe.Price = recipe.Price;
                existingRecipe.Pic = recipe.Pic;
                existingRecipe.ItemCategoryId = recipe.ItemCategoryId;

                // Remove existing relationships
                _context.ItemRecipes.RemoveRange(existingRecipe.ItemRecipes);
                await _context.SaveChangesAsync();

                // Add new relationships
                var newItemRecipes = recipe.ItemRecipes.Select(ir => new ItemRecipe
                {
                    ItemId = existingRecipe.Id,
                    IngredientId = ir.IngredientId,
                    Quantity = ir.Quantity
                }).ToList();

                await _context.ItemRecipes.AddRangeAsync(newItemRecipes);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception($"Failed to update recipe: {ex.Message}", ex);
            }
        }

        public async Task AddRecipeAsync(Item recipe)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Clear tracking
                _context.ChangeTracker.Clear();

                // Create new recipe
                var newRecipe = new Item
                {
                    Name = recipe.Name,
                    Price = recipe.Price,
                    Pic = recipe.Pic,
                    ItemCategoryId = recipe.ItemCategoryId,
                    IsDeleted = false,
                    DeletedDate = null
                };

                await _context.Items.AddAsync(newRecipe);
                await _context.SaveChangesAsync();

                // Add relationships
                var newItemRecipes = recipe.ItemRecipes.Select(ir => new ItemRecipe
                {
                    ItemId = newRecipe.Id,
                    IngredientId = ir.IngredientId,
                    Quantity = ir.Quantity
                }).ToList();

                await _context.ItemRecipes.AddRangeAsync(newItemRecipes);
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
            var recipe = await _context.Items.FirstOrDefaultAsync(r => r.Id == recipeId);
            if (recipe != null)
            {
                recipe.IsDeleted = true;
                recipe.DeletedDate = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ItemCategory> GetCategoryByIdAsync(int categoryId)
        {
            return await _context.ItemCategories.FirstOrDefaultAsync(c => c.Id == categoryId);
        }
    }
}
