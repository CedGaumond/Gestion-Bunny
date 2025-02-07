using Gestion_Bunny.Modeles;


namespace Gestion_Bunny.Services
{
    public interface IIngredientService
    {
        Task<List<Ingredient>> GetAllIngredients();
        Task<Ingredient> GetIngredientById(int id);
        Task AddIngredient(string name, decimal quantityRemaining, decimal quantityPerDeliveryUnit, decimal minimumThresholdNotification);
        Task UpdateIngredient(int id, string name = null, decimal? quantityRemaining = null, decimal? quantityPerDeliveryUnit = null, decimal? minimumThresholdNotification = null);
        Task DeleteIngredient(int id);
    }
}
