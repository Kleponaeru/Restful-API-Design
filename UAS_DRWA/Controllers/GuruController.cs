using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("guru")]
    [ApiController]
    public class GuruController : ControllerBase
    {
        private readonly TodoContext _context;

        public GuruController(TodoContext context)
        {
            _context = context;
        }

        // POST: guru
        [HttpPost]
        public async Task<ActionResult<GuruDTO>> PostGuru(GuruDTO guruDTO)
        {
            var guru = new Guru
            {
                Nama = guruDTO.Nama,
                Kelas = guruDTO.Kelas,
                NIP = guruDTO.NIP
            };

            _context.Gurus.Add(guru);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetGuru),
                new { id = guru.Id },
                GuruToDTO(guru));
        }

        // GET: guru
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GuruDTO>>> GetGurus()
        {
            return await _context.Gurus
                .Select(x => GuruToDTO(x))
                .ToListAsync();
        }

        // GET: guru/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<GuruDTO>> GetGuru(int id)
        {
            var guru = await _context.Gurus.FindAsync(id);

            if (guru == null)
            {
                return NotFound();
            }

            return GuruToDTO(guru);
        }

        // PUT: guru/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGuru(int id, GuruDTO guruDTO)
        {
            if (id != guruDTO.Id)
            {
                return BadRequest();
            }

            var guru = await _context.Gurus.FindAsync(id);

            if (guru == null)
            {
                return NotFound();
            }

            guru.Nama = guruDTO.Nama;
            guru.Kelas = guruDTO.Kelas;
            guru.NIP = guruDTO.NIP;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: guru/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGuru(int id)
        {
            var guru = await _context.Gurus.FindAsync(id);

            if (guru == null)
            {
                return NotFound();
            }

            _context.Gurus.Remove(guru);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GuruExists(int id)
        {
            return _context.Gurus.Any(e => e.Id == id);
        }

        private static GuruDTO GuruToDTO(Guru guru) =>
            new GuruDTO
            {
                Id = guru.Id,
                Nama = guru.Nama,
                Kelas = guru.Kelas,
                NIP = guru.NIP
            };
    }
}
