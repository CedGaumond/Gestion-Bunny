namespace Gestion_Bunny.Services
{
    public interface IIngredientValidationService
    {
        (bool isAvailable, List<string> unavailableItems) CheckAndReserveIngredientsForRecipe(int recipeId, int quantity);
        void ReleaseIngredientsForRecipe(int recipeId, int quantity);
        void CancelOrderAndReleaseIngredients(List<(int recipeId, int quantity)> orderItems);
    }
}