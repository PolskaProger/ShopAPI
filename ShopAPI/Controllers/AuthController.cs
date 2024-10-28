using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopAPI.Domain.DB;
using ShopAPI.Domain.DTO;
using ShopAPI.Domain.Entities;
using ShopAPI.services;

namespace ShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly TokenService _tokenService;

        public AuthController(AppDbContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
            {
                return BadRequest("Email is already in use.");
            }
            if (request.Email == "admin@gmail.com")
            {
                var user = new User
                {
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password), // Захешируем пароль
                    PhoneNumber = request.PhoneNumber,
                    Role = "Role"
                };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

            }
            else
            {
                var user = new User
                {
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password), // Захешируем пароль
                    PhoneNumber = request.PhoneNumber,
                    Role = "User"
                };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }

            

            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == request.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return Unauthorized("Invalid email or password.");
            }

            var token = _tokenService.GenerateToken(user);
            return Ok(new { Token = token });
        }
    }

}
