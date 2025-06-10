using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RezervasyonApp.Data;
using RezervasyonApp.Models;
using Microsoft.EntityFrameworkCore;

namespace RezervasyonApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KullaniciController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public KullaniciController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kullanici>>> GetKullanicilar()
        {
            return await _context.Kullanicilar.ToListAsync();
        }

        // GET id
        [HttpGet("{id}")]
        public async Task<ActionResult<Kullanici>> GetKullanici(int id)
        {
            var kullanici = await _context.Kullanicilar.FindAsync(id);
            if (kullanici == null)
                return NotFound();

            return kullanici;
        }

        // POST 
        [HttpPost]
        public async Task<ActionResult<Kullanici>> PostKullanici(Kullanici kullanici)
        {
            _context.Kullanicilar.Add(kullanici);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetKullanici), new { id = kullanici.Id }, kullanici);
        }

        // DELETE id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKullanici(int id)
        {
            var kullanici = await _context.Kullanicilar.FindAsync(id);
            if (kullanici == null)
                return NotFound();

            _context.Kullanicilar.Remove(kullanici);
            await _context.SaveChangesAsync();

            return NoContent(); 
        }
    }
}
