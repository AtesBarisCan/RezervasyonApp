using Microsoft.AspNetCore.Mvc;
using RezervasyonApp.Data; 
using RezervasyonApp.Models; 
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace RezervasyonApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto request)
        {
            var user = _context.Kullanicilar.FirstOrDefault(u =>
                u.Eposta == request.Email && u.Sifre == request.Password);

            if (user == null)
                return Unauthorized(new { message = "Giriş başarısız!" });

            // Claims
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), 
                new Claim(ClaimTypes.Name, user.Ad),
                new Claim(ClaimTypes.Email, user.Eposta),
                new Claim(ClaimTypes.Role, user.Rol)
            };


            // Güçlü key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("supergizlibirtokenanahtari_2025_kanKaaa!"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Token oluştur
            var token = new JwtSecurityToken(
                issuer: "rezervasyonApp",
                audience: "rezervasyonAppClient",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            // Token ve kullanıcı bilgilerini frontend'e döndür
            return Ok(new
            {
                token = tokenString,
                id = user.Id,
                ad = user.Ad,
                rol = user.Rol
                
            });
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] Kullanici kullanici)
        {
            if (_context.Kullanicilar.Any(u => u.Eposta == kullanici.Eposta))
            {
                return BadRequest("Bu e-posta adresi zaten kayıtlı.");
            }

            _context.Kullanicilar.Add(kullanici);
            _context.SaveChanges();

            return Ok(new { message = "Kayıt başarılı!" });
        }
    }

    public class LoginRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
