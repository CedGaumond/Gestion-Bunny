using Gestion_Bunny.Modeles;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Bunny.Services
{
    public class BillService : IBillService
    {
        private readonly ApplicationDbContext _context;

        public BillService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Bill> GetBills()
        {
            return _context.Bills.ToList();
        }

        public List<Bill> GetPendingBills()
        {
            return _context.Bills
                .Where(b => b.BillFile == null || b.BillFile.Length == 0)
                .OrderByDescending(b => b.OrderDate.Date)  
                .ToList();
        }

        public List<Bill> GetCompletedBills()
        {
            return _context.Bills
                .Include(b => b.User)
                .Where(b => b.BillFile != null)
                .OrderByDescending(b => b.OrderDate.Date)
                .ToList();
        }

        public Bill GetBillById(int billId)
        {
            return _context.Bills.Find(billId);
        }

        public List<(Recipe Recipe, int Quantity)> GetBillRecipesById(int billId)
        {
            var result = _context.BillRecipes
                .Where(br => br.BillId == billId)
                .Include(br => br.Recipe)
                .Select(br => new { br.Recipe, br.Quantity })
                .ToList();

            return result.Select(br => (br.Recipe, br.Quantity)).ToList();
        }

        public void AddBill(Bill bill)
        {
            _context.Bills.Add(bill);
            _context.SaveChanges();
        }

        public void UpdateBill(Bill bill, List<BillRecipe> newBillRecipes)
        {
            _context.Entry(bill).State = EntityState.Modified;

            var existingBillRecipes = _context.BillRecipes
                .Where(br => br.BillId == bill.Id)
                .ToList();

            foreach (var newBr in newBillRecipes)
            {
                var existingBr = existingBillRecipes
                    .FirstOrDefault(br => br.RecipeId == newBr.RecipeId);

                if (existingBr != null)
                {
                    existingBr.Quantity = newBr.Quantity;
                }
                else
                {
                    _context.BillRecipes.Add(newBr);
                }
            }
            _context.SaveChanges();
        }

        public void DeleteBill(int billId)
        {
            var billCustomer = _context.Bills.Find(billId);
            if (billCustomer != null)
            {
                _context.Bills.Remove(billCustomer);
                _context.SaveChanges();
            }
        }

        public int GetTotalRecipeQuantityInBill(int billId)
        {
            return _context.BillRecipes
                .Where(br => br.BillId == billId)
                .Sum(br => br.Quantity);
        }

        public void UpdateBillWithRemovedItems(Bill bill, List<BillRecipe> newBillRecipes, List<int> recipeIdsToRemove)
        {
                _context.Bills.Update(bill);

                var billRecipesToRemove = _context.BillRecipes
                    .Where(br => br.BillId == bill.Id && recipeIdsToRemove.Contains(br.RecipeId))
                    .ToList();

                _context.BillRecipes.RemoveRange(billRecipesToRemove);

                foreach (var newBillRecipe in newBillRecipes)
                {
                    var existingBillRecipe = _context.BillRecipes
                        .FirstOrDefault(br => br.BillId == bill.Id && br.RecipeId == newBillRecipe.RecipeId);

                    if (existingBillRecipe != null)
                    {
                        existingBillRecipe.Quantity = newBillRecipe.Quantity;
                        existingBillRecipe.QuantityDeleted = newBillRecipe.QuantityDeleted;
                        _context.BillRecipes.Update(existingBillRecipe);
                    }
                    else
                    {
                        _context.BillRecipes.Add(newBillRecipe);
                    }
                }

                _context.SaveChanges();

            }
        }
    }