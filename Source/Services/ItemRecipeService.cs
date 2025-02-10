using Gestion_Bunny.Modeles;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestion_Bunny.Services
{
    public class ItemRecipeService : IItemRecipeService
    {
        private readonly ApplicationDbContext _context;

        public ItemRecipeService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Create an ItemRecipe
        public async Task CreateItemRecipeAsync(int itemId, int ingredientId, decimal quantity)
        {
            var itemRecipe = new ItemRecipe
            {
                ItemId = itemId,
                IngredientId = ingredientId,
                Quantity = quantity
            };

            _context.ItemRecipes.Add(itemRecipe);
            await _context.SaveChangesAsync();
        }

        // Get an ItemRecipe by ItemId and IngredientId
        public async Task<ItemRecipe> GetItemRecipeByIdsAsync(int itemId, int ingredientId)
        {
            return await _context.ItemRecipes
                .FirstOrDefaultAsync(ir => ir.ItemId == itemId && ir.IngredientId == ingredientId);
        }

        // Get all ItemRecipes
        public async Task<List<ItemRecipe>> GetAllItemRecipesAsync()
        {
            return await _context.ItemRecipes.ToListAsync();
        }

        // Update an existing ItemRecipe
        public async Task UpdateItemRecipeAsync(int itemId, int ingredientId, decimal quantity)
        {
            var itemRecipe = await _context.ItemRecipes
                .FirstOrDefaultAsync(ir => ir.ItemId == itemId && ir.IngredientId == ingredientId);

            if (itemRecipe != null)
            {
                itemRecipe.Quantity = quantity;
                await _context.SaveChangesAsync();
            }
        }

        // Delete an ItemRecipe by ItemId and IngredientId
        public async Task DeleteItemRecipeAsync(int itemId, int ingredientId)
        {
            var itemRecipe = await _context.ItemRecipes
                .FirstOrDefaultAsync(ir => ir.ItemId == itemId && ir.IngredientId == ingredientId);

            if (itemRecipe != null)
            {
                _context.ItemRecipes.Remove(itemRecipe);
                await _context.SaveChangesAsync();
            }
        }
    }
}
