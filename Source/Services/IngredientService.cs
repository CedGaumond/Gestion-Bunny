
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

        public async Task<List<Ingredient>> GetAllIngredients()
        {
            return await _context.Ingredients.Where(i => !i.IsDeleted).ToListAsync();
        }

        public async Task<bool> IsIngredientNameExists(string name)
        {
            return await _context.Ingredients
                .AnyAsync(i => i.Name.ToLower() == name.ToLower() && !i.IsDeleted);
        }


        public async Task<Ingredient> GetIngredientById(int id)
        {
            return await _context.Ingredients.FirstOrDefaultAsync(i => i.Id == id && !i.IsDeleted);
        }

        public async Task AddIngredient(string name, decimal quantityRemaining, decimal quantityPerDeliveryUnit, decimal minimumThresholdNotification)
        {
            // Use ToLower() to perform a case-insensitive comparison
            var existingIngredient = await _context.Ingredients
                .FirstOrDefaultAsync(i => i.Name.ToLower() == name.ToLower() && !i.IsDeleted);

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
            await _context.SaveChangesAsync();
        }


        public async Task UpdateIngredient(int id, string name = null, decimal? quantityRemaining = null, decimal? quantityPerDeliveryUnit = null, decimal? minimumThresholdNotification = null)
        {
            var ingredient = await _context.Ingredients.FirstOrDefaultAsync(i => i.Id == id && !i.IsDeleted);

            if (ingredient != null)
            {

                if (name != null && !name.Equals(ingredient.Name, StringComparison.OrdinalIgnoreCase))
                {
                    var existingIngredient = await _context.Ingredients
                        .FirstOrDefaultAsync(i => i.Name.Equals(name, StringComparison.OrdinalIgnoreCase) && !i.IsDeleted);

                    if (existingIngredient != null)
                    {
                        throw new InvalidOperationException($"An ingredient with the name '{name}' already exists.");
                    }

                    ingredient.Name = name;
                }

                if (quantityRemaining.HasValue) ingredient.QuantityRemaining = quantityRemaining.Value;
                if (quantityPerDeliveryUnit.HasValue) ingredient.QuantityPerDeliveryUnit = quantityPerDeliveryUnit.Value;
                if (minimumThresholdNotification.HasValue) ingredient.MinimumThresholdNotification = minimumThresholdNotification.Value;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteIngredient(int id)
        {
            var ingredient = await _context.Ingredients.FirstOrDefaultAsync(i => i.Id == id && !i.IsDeleted);

            if (ingredient != null)
            {
                ingredient.IsDeleted = true;
                ingredient.DeletedDate = DateTime.UtcNow;

                await _context.SaveChangesAsync();
            }
        }
    }

}
