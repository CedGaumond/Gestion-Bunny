
using Gestion_Bunny.Modeles;
using Microsoft.EntityFrameworkCore;



namespace Gestion_Bunny.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly ApplicationDbContext _context;

        public IngredientService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Ingredient> GetAllIngredients()
        {
            return _context.Ingredients.Where(i => !i.IsDeleted).ToList();
        }

        public bool IsIngredientNameExists(string name)
        {
            return _context.Ingredients
                .Any(i => i.Name.ToLower() == name.ToLower() && !i.IsDeleted);
        }


        public Ingredient GetIngredientById(int id)
        {
            return _context.Ingredients.FirstOrDefault(i => i.Id == id && !i.IsDeleted);
        }

        public void AddIngredient(string name, decimal quantityRemaining, decimal quantityPerDeliveryUnit, decimal minimumThresholdNotification)
        {
            var existingIngredient = _context.Ingredients
                .FirstOrDefault(i => i.Name.ToLower() == name.ToLower() && !i.IsDeleted);

            if (existingIngredient != null)
            {
                throw new InvalidOperationException($"An ingredient with the name '{name}' already exists.");
            }

            var ingredient = new Ingredient
            {
                Name = name,
                QuantityRemaining = quantityRemaining,
                QuantityPerDeliveryUnit = quantityPerDeliveryUnit,
                MinimumThresholdNotification = minimumThresholdNotification,
                DeletedDate = null,
                IsDeleted = false
            };

            _context.Ingredients.Add(ingredient);
            _context.SaveChangesAsync();
        }

        public string GetIngredientNameById(int id)
        {
            var ingredient = _context.Ingredients
                .FirstOrDefault(i => i.Id == id && !i.IsDeleted);

            if (ingredient == null)
            {
                throw new KeyNotFoundException($"Ingredient with ID {id} not found.");
            }

            return ingredient.Name;
        }
        public void UpdateIngredient(int id, string name = null, decimal? quantityRemaining = null, decimal? quantityPerDeliveryUnit = null, decimal? minimumThresholdNotification = null)
        {
            var ingredient = _context.Ingredients.FirstOrDefault(i => i.Id == id && !i.IsDeleted);

            if (ingredient != null)
            {

                if (name != null && !name.Equals(ingredient.Name, StringComparison.OrdinalIgnoreCase))
                {
                    var existingIngredient = _context.Ingredients
                        .FirstOrDefault(i => i.Name.Equals(name, StringComparison.OrdinalIgnoreCase) && !i.IsDeleted);

                    if (existingIngredient != null)
                    {
                        throw new InvalidOperationException($"An ingredient with the name '{name}' already exists.");
                    }

                    ingredient.Name = name;
                }

                if (quantityRemaining.HasValue) ingredient.QuantityRemaining = quantityRemaining.Value;
                if (quantityPerDeliveryUnit.HasValue) ingredient.QuantityPerDeliveryUnit = quantityPerDeliveryUnit.Value;
                if (minimumThresholdNotification.HasValue) ingredient.MinimumThresholdNotification = minimumThresholdNotification.Value;

                _context.SaveChanges();
            }
        }
        public bool IsIngredientUsedInItemRecipe(int ingredientId)
        {
            return _context.RecipeIngredients
                .Any(ir => ir.IngredientId == ingredientId);
        }


        public void DeleteIngredient(int id)
        {
            var ingredient = _context.Ingredients.FirstOrDefault(i => i.Id == id && !i.IsDeleted);

            if (ingredient != null)
            {
                ingredient.IsDeleted = true;
                ingredient.DeletedDate = DateTime.UtcNow;

                 _context.SaveChanges();
            }
        }
    }

}
