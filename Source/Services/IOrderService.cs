using Gestion_Bunny.Modeles;

namespace Gestion_Bunny.Services
{
   public interface IOrderService
   {
        Task<List<Order>> GetOrdersAsync();
        Task<Order> GetOrderByIdAsync(int orderId);
        Task AddOrderAsync(Order orderProvider);
        Task UpdateOrderAsync(Order orderProvider);
        Task DeleteOrderAsync(int orderId);
        Task UpdateOrderAsReceivedAsync(int orderId);
   }
}
