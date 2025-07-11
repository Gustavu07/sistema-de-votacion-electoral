using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sistemaAdministracion.Data;
using sistemaAdministracion.Dtos;
using sistemaAdministracion.Models;

namespace sistemaAdministracion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JuradoesController : ControllerBase
    {
        private readonly sistemaAdministracionContext _context;

        public JuradoesController(sistemaAdministracionContext context)
        {
            _context = context;
        }

        // GET: api/Juradoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JuradoDto>>> GetJurados()
        {
            var jurados = await _context.Jurados
                .Select(j => new JuradoDto
                {
                    Id = j.Id,
                    NombreCompleto = j.NombreCompleto,
                    MesaElectoralId = j.MesaElectoralId
                })
                .ToListAsync();

            return Ok(jurados);
        }

        // GET: api/Juradoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JuradoDto>> GetJurado(int id)
        {
            var jurado = await _context.Jurados.FindAsync(id);

            if (jurado == null)
                return NotFound();

            var dto = new JuradoDto
            {
                Id = jurado.Id,
                NombreCompleto = jurado.NombreCompleto,
                MesaElectoralId = jurado.MesaElectoralId
            };

            return Ok(dto);
        }

        // POST: api/Juradoes
        [HttpPost]
        public async Task<ActionResult<JuradoDto>> PostJurado(CreateJuradoDto dto)
        {
            var mesa = await _context.MesasElectorales.FindAsync(dto.MesaElectoralId);
            if (mesa == null)
                return NotFound("Mesa Electoral no encontrada");

            var jurado = new Jurado
            {
                NombreCompleto = dto.NombreCompleto,
                MesaElectoralId = dto.MesaElectoralId
            };

            _context.Jurados.Add(jurado);
            await _context.SaveChangesAsync();

            var result = new JuradoDto
            {
                Id = jurado.Id,
                NombreCompleto = jurado.NombreCompleto,
                MesaElectoralId = jurado.MesaElectoralId
            };

            return CreatedAtAction(nameof(GetJurado), new { id = jurado.Id }, result);
        }

        // PUT: api/Juradoes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJurado(int id, CreateJuradoDto dto)
        {
            var jurado = await _context.Jurados.FindAsync(id);
            if (jurado == null)
                return NotFound();

            jurado.NombreCompleto = dto.NombreCompleto;
            jurado.MesaElectoralId = dto.MesaElectoralId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Juradoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJurado(int id)
        {
            var jurado = await _context.Jurados.FindAsync(id);
            if (jurado == null)
                return NotFound();

            _context.Jurados.Remove(jurado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JuradoExists(int id)
        {
            return _context.Jurados.Any(e => e.Id == id);
        }
    }
}
