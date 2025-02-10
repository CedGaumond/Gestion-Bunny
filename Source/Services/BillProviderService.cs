using Gestion_Bunny.Modeles;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Bunny.Services
{
    public class BillProviderService : IBillProviderService
    {
        private readonly ApplicationDbContext _context;

        public BillProviderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<BillProvider>> GetBillProvidersAsync()
        {
            return await _context.BillProviders.ToListAsync();
        }

        public async Task<BillProvider> GetBillProviderByIdAsync(int billProviderId)
        {
            return await _context.BillProviders.FindAsync(billProviderId);
        }

        public async Task AddBillProviderAsync(BillProvider billProvider)
        {
            await _context.BillProviders.AddAsync(billProvider);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBillProviderAsync(BillProvider billProvider)
        {
            _context.Entry(billProvider).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBillProviderAsync(int billProviderId)
        {
            var billProvider = await _context.BillProviders.FindAsync(billProviderId);
            if (billProvider != null)
            {
                _context.BillProviders.Remove(billProvider);
                await _context.SaveChangesAsync();
            }
        }
    }
}
