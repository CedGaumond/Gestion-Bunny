namespace Gestion_Bunny.Services
{
    public interface IIngredientValidationService
    {
        Task<(bool isAvailable, List<string> unavailableItems)> CheckAndReserveIngredientsForRecipe(int recipeId, int quantity);
        Task ReleaseIngredientsForRecipe(int recipeId, int quantity);
        Task FinalizeOrderIngredients(int billId);
        Task CancelOrderAndReleaseIngredients(List<(int recipeId, int quantity)> orderItems);
    }
}