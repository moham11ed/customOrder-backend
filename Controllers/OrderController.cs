// OrderController.cs
using customOrder.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace customOrder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(
            IOrderService orderService,
            ILogger<OrdersController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderData orderData)
        {
            try
            {
                if (orderData == null)
                {
                    _logger.LogWarning("CreateOrder called with null orderData");
                    return BadRequest("Order data is required");
                }

                if (string.IsNullOrWhiteSpace(orderData.ClientInfo?.Email))
                {
                    return BadRequest("Email is required");
                }

                var success = await _orderService.CreateOrderAsync(orderData);

                return success
                    ? Ok(new { Success = true, Message = "Order created successfully" })
                    : StatusCode(500, "Failed to create order");
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validation error in CreateOrder");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CreateOrder");
                return StatusCode(500, "An error occurred while processing your order");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            try
            {
                var order = await _orderService.GetOrderByIdAsync(id);
                return order != null
                    ? Ok(order)
                    : NotFound($"Order with ID {id} not found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving order with ID {OrderId}", id);
                return StatusCode(500, "Error retrieving order");
            }
        }

        [HttpGet("{id}/status")]
        public async Task<IActionResult> GetOrderStatus(int id)
        {
            try
            {
                var status = await _orderService.GetOrderStatusAsync(id);

                if (status == null)
                {
                    return NotFound($"Order with ID {id} not found");
                }

                return Ok(new { Status = status });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving status for order {OrderId}", id);
                return StatusCode(500, "Error retrieving order status");
            }
        }


        [HttpGet("by-email/{email}")]
        public async Task<IActionResult> GetOrdersByEmail(string email)
        {
            try
            {
                var orders = await _orderService.GetOrdersByEmailAsync(email);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving orders for email {Email}", email);
                return StatusCode(500, "Error retrieving orders");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                var orders = await _orderService.GetAllOrdersAsync();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all orders");
                return StatusCode(500, "Error retrieving orders");
            }
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] string status)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(status))
                {
                    return BadRequest("Status is required");
                }

                var success = await _orderService.UpdateOrderStatusAsync(id, status);

                return success
                    ? Ok(new { Success = true, Message = "Order status updated" })
                    : NotFound($"Order with ID {id} not found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating status for order {OrderId}", id);
                return StatusCode(500, "Error updating order status");
            }
        }
    }
}