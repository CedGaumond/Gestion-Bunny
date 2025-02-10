using Gestion_Bunny.Modeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Bunny.Services
{
   public interface IBillProviderService
   {
        Task<List<BillProvider>> GetBillProvidersAsync();
        Task<BillProvider> GetBillProviderByIdAsync(int billProviderId);
        Task AddBillProviderAsync(BillProvider billProvider);
        Task UpdateBillProviderAsync(BillProvider billProvider);
        Task DeleteBillProviderAsync(int billProviderId);
   }
}
