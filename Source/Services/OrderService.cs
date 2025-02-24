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

        public List<Order> GetOrders()
        {
            return _context.Orders.ToList();
        }

        public Order GetOrderById(int billProviderId)
        {
            return _context.Orders.Find(billProviderId);
        }

        public void AddOrder(Order billProvider)
        {
            _context.Orders.Add(billProvider);
            _context.SaveChanges();
        }

        public void UpdateOrder(Order billProvider)
        {
            _context.Entry(billProvider).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteOrder(int billProviderId)
        {
            var billProvider = _context.Orders.Find(billProviderId);
            if (billProvider != null)
            {
                _context.Orders.Remove(billProvider);
                _context.SaveChanges();
            }
        }

        public void UpdateOrderAsReceived(int orderId)
        {
            var billProvider = _context.Orders.Find(orderId);
            if (billProvider != null)
            {
                billProvider.IsDelivered = true;
                _context.SaveChanges();
            }
        }
    }
}
