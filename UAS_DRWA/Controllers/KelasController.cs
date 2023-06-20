using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;


namespace TodoApi.Controllers
{
    [Route("kelas")]
    [ApiController]
    public class KelasController : ControllerBase
    {
        private readonly TodoContext _context;

        public KelasController(TodoContext context)
        {
            _context = context;
        }

        ///<summary>
        /// Adds a new Kelas to the system.
        ///</summary>
        [HttpPost]
        // [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<KelasDTO>> PostKelas(KelasDTO kelasDTO)
        {
            var kelas = new Kelas
            {
                Nama = kelasDTO.Nama
            };

            _context.Kelas.Add(kelas);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetKelas),
                new { id = kelas.Id },
                KelasToDTO(kelas));
        }

        /// <summary>
        /// Get specific BooksItem.
        /// </summary>

        [HttpGet]
        // [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<KelasDTO>>> GetKelasItems()
        {
            var kelasItems = await _context.Kelas
                .Select(x => KelasToDTO(x))
                .ToListAsync();

            return Ok(kelasItems);
        }

        // GET: kelas/{id}
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<KelasDTO>> GetKelas(int id)
        {
            var kelas = await _context.Kelas.FindAsync(id);

            if (kelas == null)
            {
                return NotFound();
            }

            return KelasToDTO(kelas);
        }

        private bool KelasExists(long id)
        {
            return _context.Kelas.Any(e => e.Id == id);
        }

        private static KelasDTO KelasToDTO(Kelas kelas)
        {
            return new KelasDTO
            {
                Id = kelas.Id,
                Nama = kelas.Nama
            };
        }
        [HttpPut("{id:length(24)}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, Kelas updatedKelas)
        {
            var kelas = await _context.Kelas.FindAsync(id);

            if (kelas == null)
            {
                return NotFound();
            }

            kelas.Nama = updatedKelas.Nama;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(string id)
        {
            var kelas = await _context.Kelas.FindAsync(id);

            if (kelas == null)
            {
                return NotFound();
            }

            _context.Kelas.Remove(kelas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
