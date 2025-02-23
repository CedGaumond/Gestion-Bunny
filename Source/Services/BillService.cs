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

        public async Task<List<Bill>> GetBillsAsync()
        {
            return await _context.Bills.ToListAsync();
        }

        public async Task<List<Bill>> GetPendingBillsAsync()
        {
            return await _context.Bills
                .Where(b => b.BillFile == null || b.BillFile.Length == 0)
                .OrderByDescending(b => b.OrderDate.Date)  
                .ToListAsync();
        }

        public async Task<List<Bill>> GetCompletedBillsAsync()
        {
            return await _context.Bills
                .Include(b => b.User)
                .Where(b => b.BillFile != null)
                .OrderByDescending(b => b.OrderDate.Date)
                .ToListAsync();
        }

        public async Task<Bill> GetBillByIdAsync(int billId)
        {
            return await _context.Bills.FindAsync(billId);
        }

        public async Task<List<(Recipe Recipe, int Quantity)>> GetBillRecipesByIdAsync(int billId)
        {
            return await _context.BillRecipes
                .Where(br => br.BillId == billId)
                .Include(br => br.Recipe) 
                .Select(br => new { br.Recipe, br.Quantity }) 
                .ToListAsync()
                .ContinueWith(task => task.Result.Select(br => (br.Recipe, br.Quantity)).ToList()); 
        }


        public async Task AddBillAsync(Bill bill)
        {
            await _context.Bills.AddAsync(bill);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBillAsync(Bill bill, List<BillRecipe> newBillRecipes)
        {
            _context.Entry(bill).State = EntityState.Modified;

            var existingBillRecipes = await _context.BillRecipes
                .Where(br => br.BillId == bill.Id)
                .ToListAsync();

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
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBillAsync(int billId)
        {
            var billCustomer = await _context.Bills.FindAsync(billId);
            if (billCustomer != null)
            {
                _context.Bills.Remove(billCustomer);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> GetTotalRecipeQuantityInBillAsync(int billId)
        {
            return await _context.BillRecipes
                .Where(br => br.BillId == billId)
                .SumAsync(br => br.Quantity);
        }
    }
}