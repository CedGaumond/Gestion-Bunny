using Gestion_Bunny.Modeles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gestion_Bunny.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IIngredientService _ingredientService;

        public NotificationService(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        public List<Ingredient> GetLowStockIngredients()
        {
            var allIngredients = _ingredientService.GetAllIngredients();
            return allIngredients
                .Where(i => !i.IsDeleted && i.QuantityRemaining <= i.MinimumThresholdNotification)
                .OrderBy(i => i.QuantityRemaining / i.MinimumThresholdNotification)
                .ToList();
        }

        public int GetLowStockCount()
        {
            return GetLowStockIngredients().Count;
        }

        public bool HasLowStockNotifications()
        {
            return GetLowStockCount() > 0;
        }
    }
}