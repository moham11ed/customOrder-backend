// OrderService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using customOrder.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace customOrder.Service
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<OrderService> _logger;

        public OrderService(AppDbContext context, ILogger<OrderService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> CreateOrderAsync(OrderData orderData)
        {
            if (orderData == null)
            {
                _logger.LogWarning("CreateOrderAsync called with null orderData");
                return false;
            }

            try
            {
                var order = new Order
                {
                    // Product information
                    ProductType = orderData.ProductType,
                    ProductTypeId = orderData.ProductTypeId,
                    ProductName = orderData.ProductName,
                    Quantity = orderData.Quantity,

                    // Customization options
                    SelectedOilsJson = JsonSerializer.Serialize(orderData.SelectedOils),
                    ShapeId = orderData.ShapeId,
                    ShapeImageUrl = orderData.ShapeImageUrl,
                    DesignId = orderData.DesignId,
                    DesignUrl = orderData.DesignUrl,
                    CustomImage = orderData.CustomImage,

                    // Client information
                    ClientName = orderData.ClientInfo?.Name,
                    ClientEmail = orderData.ClientInfo?.Email,
                    ClientPhone = orderData.ClientInfo?.Phone,
                    Country = orderData.ClientInfo?.Country,
                    City = orderData.ClientInfo?.City,
                    Street = orderData.ClientInfo?.Street,

                    // Metadata
                    Status = "Received"
                };

                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Order created successfully with ID {OrderId}", order.Id);
                return true;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Database error while creating order");
                return false;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "JSON serialization error for selected oils");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error creating order");
                return false;
            }
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            try
            {
                return await _context.Orders
                    .AsNoTracking()
                    .FirstOrDefaultAsync(o => o.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving order with ID {OrderId}", id);
                return null;
            }
        }

        public async Task<IEnumerable<Order>> GetOrdersByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                _logger.LogWarning("GetOrdersByEmailAsync called with empty email");
                return Enumerable.Empty<Order>();
            }

            try
            {
                return await _context.Orders
                    .AsNoTracking()
                    .Where(o => o.ClientEmail == email)
                    .OrderByDescending(o => o.CreatedAt)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving orders for email {Email}", email);
                return Enumerable.Empty<Order>();
            }
        }

        public async Task<bool> UpdateOrderStatusAsync(int orderId, string status)
        {
            if (string.IsNullOrWhiteSpace(status))
            {
                _logger.LogWarning("UpdateOrderStatusAsync called with empty status");
                return false;
            }

            try
            {
                var order = await _context.Orders.FindAsync(orderId);
                if (order == null)
                {
                    _logger.LogWarning("Order with ID {OrderId} not found", orderId);
                    return false;
                }

                order.Status = status;
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Order {OrderId} status updated to {Status}", orderId, status);
                return true;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Database error updating order status");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error updating order status");
                return false;
            }
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            try
            {
                return await _context.Orders
                    .AsNoTracking()
                    .OrderByDescending(o => o.CreatedAt)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all orders");
                return Enumerable.Empty<Order>();
            }
        }
    }
}