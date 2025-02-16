using Gestion_Bunny.Modeles;

namespace Gestion_Bunny.Services
{
    public interface IBillService
    {
        Task<List<Bill>> GetBillsAsync();
        Task<List<Bill>> GetPendingBillsAsync();
        Task<List<Bill>> GetCompletedBillsAsync();   
        Task<Bill> GetBillByIdAsync(int billId);
        Task AddBillAsync(Bill bill);
        Task UpdateBillAsync(Bill bill, List<BillRecipe> billRecipes);
        Task DeleteBillAsync(int billId);
        Task<List<(Recipe Recipe, int Quantity)>> GetBillRecipesByIdAsync(int billId);
        Task<int> GetTotalRecipeQuantityInBillAsync(int billId);
    }
}