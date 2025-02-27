using Gestion_Bunny.Modeles;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestion_Bunny.Services
{
    public class IngredientValidationService : IIngredientValidationService
    {
        private readonly ApplicationDbContext _context;

        public IngredientValidationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<(bool isAvailable, List<string> unavailableItems)> CheckAndReserveIngredientsForRecipe(int recipeId, int quantity)
        {
            List<string> unavailableItems = new List<string>();
            bool isAvailable = true;

            var recipe = await _context.Recipes
                .Include(r => r.RecipeIngredients)
                .ThenInclude(ri => ri.Ingredient)
                .FirstOrDefaultAsync(r => r.Id == recipeId);

            if (recipe == null)
            {
                return (false, new List<string> { "Recette introuvable" });
            }

            foreach (var recipeIngredient in recipe.RecipeIngredients)
            {
                var ingredient = recipeIngredient.Ingredient;
                var requiredQuantity = recipeIngredient.Quantity * quantity;

                if (ingredient.QuantityRemaining < requiredQuantity)
                {
                    isAvailable = false;
                    if (!unavailableItems.Contains(recipe.Name))
                    {
                        unavailableItems.Add(recipe.Name);
                    }
                }
            }

            if (isAvailable)
            {
                foreach (var recipeIngredient in recipe.RecipeIngredients)
                {
                    var ingredient = recipeIngredient.Ingredient;
                    var requiredQuantity = recipeIngredient.Quantity * quantity;

                    ingredient.QuantityRemaining -= requiredQuantity;
                    _context.Ingredients.Update(ingredient);
                }

                await _context.SaveChangesAsync();
            }

            return (isAvailable, unavailableItems);
        }

        public async Task ReleaseIngredientsForRecipe(int recipeId, int quantity)
        {
            var recipe = await _context.Recipes
                .Include(r => r.RecipeIngredients)
                .ThenInclude(ri => ri.Ingredient)
                .FirstOrDefaultAsync(r => r.Id == recipeId);

            if (recipe == null)
            {
                return;
            }

            foreach (var recipeIngredient in recipe.RecipeIngredients)
            {
                var ingredient = recipeIngredient.Ingredient;
                var quantityToRelease = recipeIngredient.Quantity * quantity;

                ingredient.QuantityRemaining += quantityToRelease;
                _context.Ingredients.Update(ingredient);
            }

            await _context.SaveChangesAsync();
        }

        public async Task FinalizeOrderIngredients(int billId)
        {
            await Task.CompletedTask;
        }

        public async Task CancelOrderAndReleaseIngredients(List<(int recipeId, int quantity)> orderItems)
        {
            foreach (var (recipeId, quantity) in orderItems)
            {
                await ReleaseIngredientsForRecipe(recipeId, quantity);
            }
        }
    }
}