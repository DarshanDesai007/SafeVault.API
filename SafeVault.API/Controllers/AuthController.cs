using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using SafeVault.API.Data;
using SafeVault.API.Services;

namespace SafeVault.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly JwtService _jwt;

        public AuthController(AppDbContext db, JwtService jwt)
        {
            _db = db;
            _jwt = jwt;
        }

        [HttpPost("login")]
        public IActionResult Login(string username, string password)
        {
            var user = _db.Users.FirstOrDefault(u => u.Username == username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return Unauthorized();

            var token = _jwt.GenerateToken(user.Username, user.Role);
            return Ok(new { token });
        }
    }
}
