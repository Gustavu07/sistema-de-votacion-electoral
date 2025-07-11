using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sistemaAdministracion.Data;
using sistemaAdministracion.Dtos;
using sistemaAdministracion.Models;

namespace sistemaAdministracion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EleccionsController : ControllerBase
    {
        private readonly sistemaAdministracionContext _context;

        public EleccionsController(sistemaAdministracionContext context)
        {
            _context = context;
        }

        // GET: api/Eleccions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EleccionDto>>> GetElecciones()
        {
            var elecciones = await _context.Elecciones
                .Select(e => new EleccionDto
                {
                    Id = e.Id,
                    Tipo = e.Tipo,
                    Fecha = e.Fecha
                })
                .ToListAsync();

            return Ok(elecciones);
        }

        // GET: api/Eleccions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EleccionDto>> GetEleccion(int id)
        {
            var eleccion = await _context.Elecciones.FindAsync(id);

            if (eleccion == null)
            {
                return NotFound();
            }

            var dto = new EleccionDto
            {
                Id = eleccion.Id,
                Tipo = eleccion.Tipo,
                Fecha = eleccion.Fecha
            };

            return Ok(dto);
        }

        // POST: api/Eleccions
        [HttpPost]
        public async Task<ActionResult<EleccionDto>> PostEleccion(CreateEleccionDto dto)
        {
            var eleccion = new Eleccion
            {
                Tipo = dto.Tipo,
                Fecha = dto.Fecha
            };

            _context.Elecciones.Add(eleccion);
            await _context.SaveChangesAsync();

            var resultDto = new EleccionDto
            {
                Id = eleccion.Id,
                Tipo = eleccion.Tipo,
                Fecha = eleccion.Fecha
            };

            return CreatedAtAction(nameof(GetEleccion), new { id = eleccion.Id }, resultDto);
        }

        // PUT: api/Eleccions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEleccion(int id, CreateEleccionDto dto)
        {
            var eleccion = await _context.Elecciones.FindAsync(id);
            if (eleccion == null)
            {
                return NotFound();
            }

            eleccion.Tipo = dto.Tipo;
            eleccion.Fecha = dto.Fecha;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Eleccions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEleccion(int id)
        {
            var eleccion = await _context.Elecciones.FindAsync(id);
            if (eleccion == null)
            {
                return NotFound();
            }

            _context.Elecciones.Remove(eleccion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EleccionExists(int id)
        {
            return _context.Elecciones.Any(e => e.Id == id);
        }
    }
}
