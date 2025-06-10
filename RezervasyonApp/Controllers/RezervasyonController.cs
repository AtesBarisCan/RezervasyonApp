using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RezervasyonApp.Data;
using RezervasyonApp.Models;
using Microsoft.EntityFrameworkCore;

namespace RezervasyonApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RezervasyonController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RezervasyonController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rezervasyon>>> GetRezervasyonlar()
        {
            return await _context.Rezervasyonlar
                .Include(r => r.Kullanici)
                .Include(r => r.Mekan)
                .ToListAsync();
        }

        // GET id
        [HttpGet("{id}")]
        public async Task<ActionResult<Rezervasyon>> GetRezervasyon(int id)
        {
            var rezervasyon = await _context.Rezervasyonlar
                .Include(r => r.Kullanici)
                .Include(r => r.Mekan)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (rezervasyon == null)
                return NotFound();

            return rezervasyon;
        }

        // POST 
        [HttpPost]
        public async Task<ActionResult<Rezervasyon>> PostRezervasyon(Rezervasyon rezervasyon)
        {
            _context.Rezervasyonlar.Add(rezervasyon);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRezervasyon), new { id = rezervasyon.Id }, rezervasyon);
        }

        // PUT id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRezervasyon(int id, Rezervasyon rezervasyon)
        {
            if (id != rezervasyon.Id)
                return BadRequest("ID uyuşmuyor.");

            _context.Entry(rezervasyon).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Rezervasyonlar.Any(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        //DELETE id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRezervasyon(int id)
        {
            var rezervasyon = await _context.Rezervasyonlar.FindAsync(id);
            if (rezervasyon == null)
                return NotFound();

            _context.Rezervasyonlar.Remove(rezervasyon);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //GET MekanId
        [HttpGet("Mekan/{mekanId}")]
        public async Task<ActionResult<IEnumerable<Rezervasyon>>> GetRezervasyonlarByMekan(int mekanId)
        {
            var rezervasyonlar = await _context.Rezervasyonlar
                .Include(r => r.Kullanici)
                .Include(r => r.Mekan)
                .Where(r => r.MekanId == mekanId)
                .ToListAsync();

            return rezervasyonlar;
        }
    }
}
