using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Models;
using API.Helpers;
using API.DTOs;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace API.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;
        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        // POST /register
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegisterDto dto)
        {
            if (_context.Users.Any(u => u.Email == dto.Email))
            {
                return Conflict(new { message = "User already exists" });
            }

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = PasswordHelper.HashPassword(dto.Password),
                IsAdmin = false
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(new { message = "User registered successfully" });
        }

        // POST /login
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDto dto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == dto.Email);
            if (user == null || !PasswordHelper.VerifyPassword(dto.Password, user.PasswordHash))
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }

            var token = user.IsAdmin ? "1234test1234" : "user-token";
            // حفظ بيانات الجلسة
            HttpContext.Session.SetString("UserEmail", user.Email);
            HttpContext.Session.SetString("UserRole", user.IsAdmin ? "admin" : "user");

            return Ok(new { token = token });
        }
    }
}
