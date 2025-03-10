using Gestion_Bunny.Modeles;
using System.Collections.Generic;

namespace Gestion_Bunny.Services
{
    public interface INotificationService
    {
        List<Ingredient> GetLowStockIngredients();
        int GetLowStockCount();
        bool HasLowStockNotifications();
    }
}