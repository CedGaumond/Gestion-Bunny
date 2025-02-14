using Gestion_Bunny.Modeles;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Bunny.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetOrdersAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int billProviderId)
        {
            return await _context.Orders.FindAsync(billProviderId);
        }

        public async Task AddOrderAsync(Order billProvider)
        {
            await _context.Orders.AddAsync(billProvider);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(Order billProvider)
        {
            _context.Entry(billProvider).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int billProviderId)
        {
            var billProvider = await _context.Orders.FindAsync(billProviderId);
            if (billProvider != null)
            {
                _context.Orders.Remove(billProvider);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateOrderAsReceivedAsync(int orderId)
        {
            var billProvider = await _context.Orders.FindAsync(orderId);
            if (billProvider != null)
            {
                billProvider.IsDelivered = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
