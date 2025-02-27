using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace Gestion_Bunny.Services
{
    public class IngredientValidationService : IIngredientValidationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IIngredientService _ingredientService;

        public IngredientValidationService(ApplicationDbContext context, IIngredientService ingredientService)
        {
            _context = context;
            _ingredientService = ingredientService;
        }

        public (bool isAvailable, List<string> unavailableItems) CheckAndReserveIngredientsForRecipe(int recipeId, int quantity)
        {
            List<string> unavailableItems = new List<string>();
            bool isAvailable = true;

            var recipe = _context.Recipes
                .Include(r => r.RecipeIngredients)
                .ThenInclude(ri => ri.Ingredient)
                .FirstOrDefault(r => r.Id == recipeId);

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
                    var ingredient = _context.Ingredients.FirstOrDefault(i => i.Id == recipeIngredient.IngredientId && !i.IsDeleted);
                    var requiredQuantity = recipeIngredient.Quantity * quantity;

                    ingredient.QuantityRemaining -= requiredQuantity;
                    _context.Ingredients.Update(ingredient);
                    _context.SaveChanges();
                }
            }

            return (isAvailable, unavailableItems);
        }

        public void ReleaseIngredientsForRecipe(int recipeId, int quantity)
        {
            var recipe = _context.Recipes
                .Include(r => r.RecipeIngredients)
                .ThenInclude(ri => ri.Ingredient)
                .FirstOrDefault(r => r.Id == recipeId);

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
                _context.Entry(ingredient).Reload();
            }
            //_ingredientService.NotifyIngredientUpdated();
            _context.SaveChanges();
        }
        public void CancelOrderAndReleaseIngredients(List<(int recipeId, int quantity)> orderItems)
        {
            foreach (var (recipeId, quantity) in orderItems)
            {
                ReleaseIngredientsForRecipe(recipeId, quantity);
            }
        }
    }
}