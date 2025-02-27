using Gestion_Bunny.Modeles;


namespace Gestion_Bunny.Services
{
    public interface IIngredientService
    {
        List<Ingredient> GetAllIngredients();
        Ingredient GetIngredientById(int id);
        void AddIngredient(string name, decimal quantityRemaining, decimal quantityPerDeliveryUnit, decimal minimumThresholdNotification);
        void UpdateIngredient(int id, string name = null, decimal? quantityRemaining = null, decimal? quantityPerDeliveryUnit = null, decimal? minimumThresholdNotification = null);
        void DeleteIngredient(int id);
        bool IsIngredientUsedInItemRecipe(int ingredientId);
        bool IsIngredientNameExists(string name);
        string GetIngredientNameById(int id);
    }
}
