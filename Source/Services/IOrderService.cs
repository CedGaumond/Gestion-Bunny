using Gestion_Bunny.Modeles;

namespace Gestion_Bunny.Services
{
   public interface IOrderService
   {
        List<Order> GetOrders();
        Order GetOrderById(int orderId);
        void AddOrder(Order orderProvider);
        void UpdateOrder(Order orderProvider);
        void DeleteOrder(int orderId);
        void UpdateOrderAsReceived(int orderId);
   }
}
