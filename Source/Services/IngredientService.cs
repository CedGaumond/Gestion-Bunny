using Gestion_Bunny.Data;
using Gestion_Bunny.Modeles;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gestion_Bunny.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly IngredientContext _context;

        public IngredientService(IngredientContext context)
        {
            _context = context;
        }

        public async Task<List<Ingredient>> GetAllIngredients()
        {
            return await _context.Ingredients.Where(i => !i.IsDeleted).ToListAsync();
        }

        public async Task<Ingredient> GetIngredientById(int id)
        {
            return await _context.Ingredients.FirstOrDefaultAsync(i => i.Id == id && !i.IsDeleted);
        }

        public async Task AddIngredient(string name, decimal quantityRemaining, decimal quantityPerDeliveryUnit, decimal minimumThresholdNotification)
        {
            var ingredient = new Ingredient
            {
                Name = name,
                QuantityRemaining = quantityRemaining,
                QuantityPerDeliveryUnit = quantityPerDeliveryUnit,
                MinimumThresholdNotification = minimumThresholdNotification,
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
                if (name != null) ingredient.Name = name;
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
