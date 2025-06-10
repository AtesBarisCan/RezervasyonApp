using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RezervasyonApp.Data;
using RezervasyonApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace RezervasyonApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MekanController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MekanController(ApplicationDbContext context)
        {
            _context = context;
        }

        //POST 
        [HttpPost]
        [Authorize(Roles = "Isletme")]
        public async Task<ActionResult<Mekan>> PostMekan([FromBody]Mekan mekan)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                Console.WriteLine(" Kullanıcı kimliği token'da yok!");
                return Unauthorized("Kullanıcı kimliği bulunamadı.");
            }

            int kullaniciId = int.Parse(userIdClaim.Value);

            var kullanici = await _context.Kullanicilar.FindAsync(kullaniciId);
            if (kullanici == null)
                return NotFound("Kullanıcı bulunamadı.");

            mekan.KullaniciId = kullaniciId;
            // mekan.Kullanici = kullanici; ← BUNU SİL

            _context.Mekanlar.Add(mekan);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Hata:", ex.Message);
                if (ex.InnerException != null)
                    Console.WriteLine("👉 Inner: " + ex.InnerException.Message);

                return StatusCode(500, new
                {
                    mesaj = "Sunucu hatası oluştu",
                    detay = ex.InnerException?.Message ?? ex.Message
                });
            }

            return CreatedAtAction(nameof(GetMekanById), new { id = mekan.Id }, mekan);
        }



        //GET 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mekan>>> GetMekanlar()
        {
            return await _context.Mekanlar
                .Include(m => m.Kullanici).ToListAsync();
        }

        
        // GET id
        [HttpGet("{id}")]
        public async Task<ActionResult<Mekan>> GetMekanById(int id)
        {
            var mekan = await _context.Mekanlar
                .Include(m => m.Kullanici)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mekan == null)
                return NotFound();

            return mekan;
        }

        // DELETE id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMekan(int id)
        {
            var mekan = await _context.Mekanlar.FindAsync(id);
            if (mekan == null)
                return NotFound();

            _context.Mekanlar.Remove(mekan);
            await _context.SaveChangesAsync();

            return NoContent(); 
        }

        //filtre
        [HttpGet("filtreli")]
        [AllowAnonymous]
        public async Task <ActionResult<IEnumerable<Mekan>>> GetFiltreliMekanlar([FromQuery] string? ad, [FromQuery] string? adres)
        {
            var query = _context.Mekanlar
                .Include(m => m.Kullanici)
                .AsQueryable();

            if (!string.IsNullOrEmpty(ad))
                query = query.Where(m => m.Ad.Contains(ad));

            if (!string.IsNullOrEmpty(adres))
                query = query.Where(m => m.Adres.Contains(adres));

            return await query.ToListAsync();
        }

        [HttpPost("gorsel-yukle")]
        [Authorize(Roles = "Isletme")]
        public async Task<IActionResult> GorselYukle(IFormFile dosya)
        {
            if (dosya == null || dosya.Length == 0)
                return BadRequest("Dosya yok");

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var dosyaAdi = Guid.NewGuid().ToString() + Path.GetExtension(dosya.FileName);
            var dosyaYolu = Path.Combine(uploadsFolder, dosyaAdi);

            using (var stream = new FileStream(dosyaYolu, FileMode.Create))
            {
                await dosya.CopyToAsync(stream);
            }

            var url = $"{Request.Scheme}://{Request.Host}/uploads/{dosyaAdi}";
            return Ok(new { url });
        }


    }
}
