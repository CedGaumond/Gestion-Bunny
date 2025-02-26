using Gestion_Bunny.Modeles;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Bunny.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly IIngredientService _ingredientService;

        public OrderService(ApplicationDbContext context, IIngredientService ingredientService)
        {
            _context = context;
            _ingredientService = ingredientService;
        }

        public List<Order> GetOrders()
        {
            return _context.Orders.OrderByDescending(b => b.OrderDate.Date).ToList();
        }

        public Order GetOrderById(int orderId)
        {
            return _context.Orders.Find(orderId);
        }

        public void AddOrder(Order orderId)
        {
            _context.Orders.Add(orderId);
            _context.SaveChanges();
        }

        public void UpdateOrder(Order orderId)
        {
            _context.Entry(orderId).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteOrder(int orderId)
        {
            var order= _context.Orders.Find(orderId);
            if (order != null)
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }
        }

        public void UpdateOrderAsReceived(int orderId)
        {
            _context.Database.ExecuteSqlRaw("CALL mark_order_received({0})", orderId);
            _context.SaveChanges();

            var order = _context.Orders
                .Include(o => o.OrderIngredients)
                .ThenInclude(oi => oi.Ingredient)
                .FirstOrDefault(o => o.Id == orderId);

            if (order != null)
            {
                order.IsDelivered = true;

                var updatedIngredients = _context.Ingredients
                .Where(i => i.OrderIngredients.Any(oi => oi.OrderId == orderId))
                .ToList();

                foreach (var ingredient in updatedIngredients)
                {
                    _context.Entry(ingredient).Reload();
                }
                _ingredientService.NotifyIngredientUpdated();
            }
        }
    }
}