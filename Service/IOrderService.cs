// IOrderService.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using customOrder.Models;

namespace customOrder.Service
{
    public interface IOrderService
    {
        Task<bool> CreateOrderAsync(OrderData orderData);
        Task<Order> GetOrderByIdAsync(int id);
        Task<IEnumerable<Order>> GetOrdersByEmailAsync(string email);
        Task<bool> UpdateOrderStatusAsync(int orderId, string status);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
    }
}