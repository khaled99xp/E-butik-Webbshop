using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Models;
using API.Filters;
using API.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace API.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;
        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        // POST /api/orders
        [HttpPost]
        public IActionResult CreateOrder([FromBody] OrderCreateDto dto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == dto.User);
            if (user == null)
            {
                return BadRequest(new { message = "Invalid user" });
            }

            List<OrderItem> orderItems = new List<OrderItem>();
            foreach (var p in dto.Products)
            {
                if (!int.TryParse(p.Id, out int productId))
                {
                    return BadRequest(new { message = "Invalid product id" });
                }
                var product = _context.Products.FirstOrDefault(prod => prod.Id == productId);
                if (product == null)
                {
                    return BadRequest(new { message = "Product not found" });
                }
                if (product.Stock < p.Quantity)
                {
                    return Conflict(new { message = "Product out of stock" });
                }
                product.Stock -= p.Quantity;
                orderItems.Add(new OrderItem { ProductId = product.Id, Quantity = p.Quantity });
            }

            var order = new Order
            {
                UserEmail = dto.User,
                OrderItems = orderItems
            };

            _context.Orders.Add(order);
            _context.SaveChanges();
            return Ok(new { message = "Order created successfully" });
        }

        // GET /api/orders 
        [HttpGet]
        [ApiKey]
        public IActionResult GetOrders()
        {
            var orders = _context.Orders.Select(o => new
            {
                o.Id,
                o.UserEmail,
                Products = o.OrderItems.Select(oi => new
                {
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity
                })
            }).ToList();
            return Ok(orders);
        }
    }
}
