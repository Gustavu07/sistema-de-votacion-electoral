using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sistemaAdministracion.Data;
using sistemaAdministracion.Dtos;
using sistemaAdministracion.Models;

namespace sistemaAdministracion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MesaElectoralsController : ControllerBase
    {
        private readonly sistemaAdministracionContext _context;

        public MesaElectoralsController(sistemaAdministracionContext context)
        {
            _context = context;
        }

        // GET: api/MesaElectorals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MesaElectoralDto>>> GetMesasElectorales()
        {
            var mesas = await _context.MesasElectorales
                .Select(m => new MesaElectoralDto
                {
                    Id = m.Id,
                    Numero = m.Numero,
                    RecintoId = m.RecintoId
                })
                .ToListAsync();

            return Ok(mesas);
        }

        // GET: api/MesaElectorals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MesaElectoralDto>> GetMesaElectoral(int id)
        {
            var mesa = await _context.MesasElectorales.FindAsync(id);

            if (mesa == null)
            {
                return NotFound();
            }

            var dto = new MesaElectoralDto
            {
                Id = mesa.Id,
                Numero = mesa.Numero,
                RecintoId = mesa.RecintoId
            };

            return Ok(dto);
        }

        // POST: api/MesaElectorals
        [HttpPost]
        public async Task<ActionResult<MesaElectoralDto>> PostMesaElectoral(CreateMesaElectoralDto dto)
        {
            var recinto = await _context.Recintos.FindAsync(dto.RecintoId);
            if (recinto == null)
            {
                return NotFound("Recinto no encontrado");
            }

            var mesa = new MesaElectoral
            {
                Numero = dto.Numero,
                RecintoId = dto.RecintoId
            };

            _context.MesasElectorales.Add(mesa);
            await _context.SaveChangesAsync();

            var resultDto = new MesaElectoralDto
            {
                Id = mesa.Id,
                Numero = mesa.Numero,
                RecintoId = mesa.RecintoId
            };

            return CreatedAtAction(nameof(GetMesaElectoral), new { id = mesa.Id }, resultDto);
        }

        // PUT: api/MesaElectorals/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMesaElectoral(int id, CreateMesaElectoralDto dto)
        {
            var mesa = await _context.MesasElectorales.FindAsync(id);
            if (mesa == null)
            {
                return NotFound();
            }

            mesa.Numero = dto.Numero;
            mesa.RecintoId = dto.RecintoId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/MesaElectorals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMesaElectoral(int id)
        {
            var mesa = await _context.MesasElectorales.FindAsync(id);
            if (mesa == null)
            {
                return NotFound();
            }

            _context.MesasElectorales.Remove(mesa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MesaElectoralExists(int id)
        {
            return _context.MesasElectorales.Any(e => e.Id == id);
        }
    }
}
