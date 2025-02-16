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
            return await _context.Bills.
            Where(b => b.BillFile  == null || b.BillFile.Length == 0).
            ToListAsync();
        }

        public async Task<List<Bill>> GetCompletedBillsAsync()
        {
            return await _context.Bills.
            Where(b => b.BillFile  != null || b.BillFile.Length != 0).
            ToListAsync();
        }

        public async Task<Bill> GetBillByIdAsync(int billId)
        {
            return await _context.Bills.FindAsync(billId);
        }

        public async Task<List<Recipe>> GetBillRecipesByIdAsync(int billId)
        {
            return await _context.BillRecipes
                .Where(br => br.BillId == billId)
                .Include(br => br.Recipe)
                .Select(br => br.Recipe) 
                .ToListAsync();
        }


        public async Task AddBillAsync(Bill bill)
        {
            await _context.Bills.AddAsync(bill);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBillAsync(Bill bill)
        {
            _context.Entry(bill).State = EntityState.Modified;
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

    }
}