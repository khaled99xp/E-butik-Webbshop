using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Models;
using API.Filters;
using API.DTOs;
using System.Linq;

namespace API.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        // GET /api/products
        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _context.Products.ToList();
            return Ok(products);
        }

        // GET /api/products/{id}
        [HttpGet("{id:int}")]
        public IActionResult GetProduct(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound(new { message = "Product not found" });
            return Ok(product);
        }

        // POST /api/products - خاص بالإدمن (محمي بمفتاح API)
        [HttpPost]
        [ApiKey]
        public IActionResult CreateProduct([FromBody] ProductCreateDto dto)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id.ToString() == dto.Category);
            if (category == null)
            {
                return BadRequest(new { message = "Invalid category" });
            }

            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Stock = dto.Stock,
                CategoryId = category.Id,
                ImageUrl = "https://placehold.in/600"
            };

            _context.Products.Add(product);
            _context.SaveChanges();

            return Ok(new { message = "Product created successfully" });
        }
    }
}
