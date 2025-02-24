using Gestion_Bunny.Modeles;

namespace Gestion_Bunny.Services
{
    public interface IBillService
    {
        List<Bill> GetBills();
        List<Bill> GetPendingBills();
        List<Bill> GetCompletedBills();   
        Bill GetBillById(int billId);
        void AddBill(Bill bill);
        void UpdateBill(Bill bill, List<BillRecipe> billRecipes);
        void DeleteBill(int billId);
        List<(Recipe Recipe, int Quantity)> GetBillRecipesById(int billId);
        int GetTotalRecipeQuantityInBill(int billId);
    }
}