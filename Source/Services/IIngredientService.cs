using Gestion_Bunny.Modeles;


namespace Gestion_Bunny.Services
{
    public interface IIngredientService
    {
        Task<List<Ingredient>> GetAllIngredients(); // Add this method
        Task<Ingredient> GetIngredientById(int id); // Example for getting ingredient by ID
        Task AddIngredient(string name, decimal quantityRemaining, decimal quantityPerDeliveryUnit, decimal minimumThresholdNotification);
        Task UpdateIngredient(int id, string name = null, decimal? quantityRemaining = null, decimal? quantityPerDeliveryUnit = null, decimal? minimumThresholdNotification = null);
        Task DeleteIngredient(int id);
    }
}
