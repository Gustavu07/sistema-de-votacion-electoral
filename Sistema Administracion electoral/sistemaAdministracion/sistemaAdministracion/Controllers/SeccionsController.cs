using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sistemaAdministracion.Data;
using sistemaAdministracion.Dtos;
using sistemaAdministracion.Models;

namespace sistemaAdministracion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeccionsController : ControllerBase
    {
        private readonly sistemaAdministracionContext _context;

        public SeccionsController(sistemaAdministracionContext context)
        {
            _context = context;
        }

        // GET: api/Seccions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SeccionDto>>> GetSecciones()
        {
            var secciones = await _context.Secciones
                .Select(s => new SeccionDto
                {
                    Id = s.Id,
                    Nombre = s.Nombre,
                    Coordenadas = s.Coordenadas
                })
                .ToListAsync();

            return Ok(secciones);
        }

        // GET: api/Seccions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SeccionDto>> GetSeccion(int id)
        {
            var s = await _context.Secciones.FindAsync(id);

            if (s == null)
            {
                return NotFound();
            }

            var dto = new SeccionDto
            {
                Id = s.Id,
                Nombre = s.Nombre,
                Coordenadas = s.Coordenadas
            };

            return Ok(dto);
        }

        // POST: api/Seccions
        [HttpPost]
        public async Task<ActionResult<SeccionDto>> PostSeccion(CreateSeccionDto dto)
        {
            var seccion = new Seccion
            {
                Nombre = dto.Nombre,
                Coordenadas = dto.Coordenadas
            };

            _context.Secciones.Add(seccion);
            await _context.SaveChangesAsync();

            var resultDto = new SeccionDto
            {
                Id = seccion.Id,
                Nombre = seccion.Nombre,
                Coordenadas = seccion.Coordenadas
            };

            return CreatedAtAction(nameof(GetSeccion), new { id = seccion.Id }, resultDto);
        }

        // PUT: api/Seccions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSeccion(int id, CreateSeccionDto dto)
        {
            var seccion = await _context.Secciones.FindAsync(id);
            if (seccion == null)
            {
                return NotFound();
            }

            seccion.Nombre = dto.Nombre;
            seccion.Coordenadas = dto.Coordenadas;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Seccions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeccion(int id)
        {
            var seccion = await _context.Secciones.FindAsync(id);
            if (seccion == null)
            {
                return NotFound();
            }

            _context.Secciones.Remove(seccion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SeccionExists(int id)
        {
            return _context.Secciones.Any(e => e.Id == id);
        }
    }
}
