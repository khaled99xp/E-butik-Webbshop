using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Models;
using API.Filters;
using API.DTOs;
using System.Linq;

namespace API.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET /api/categories
        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = _context.Categories.ToList();
            return Ok(categories);
        }

        // POST /api/categories
        [HttpPost]
        [ApiKey]
        public IActionResult CreateCategory([FromBody] CategoryCreateDto dto)
        {
            if (_context.Categories.Any(c => c.Name == dto.Name))
            {
                return Conflict(new { message = "Category already exists" });
            }
            var category = new Category
            {
                Name = dto.Name
            };
            _context.Categories.Add(category);
            _context.SaveChanges();
            return Ok(new { message = "Category created successfully" });
        }
    }

    [ApiController]
    [Route("api/category/{categoryId}/products")]
    public class CategoryProductsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CategoryProductsController(AppDbContext context)
        {
            _context = context;
        }

        // GET /api/category/{categoryId}/products
        [HttpGet]
        public IActionResult GetProductsByCategory(int categoryId)
        {
            var products = _context.Products.Where(p => p.CategoryId == categoryId).ToList();
            return Ok(products);
        }
    }
}
