using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sistemaAdministracion.Data;
using sistemaAdministracion.Dtos;
using sistemaAdministracion.Models;

namespace sistemaAdministracion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeccionEleccionsController : ControllerBase
    {
        private readonly sistemaAdministracionContext _context;

        public SeccionEleccionsController(sistemaAdministracionContext context)
        {
            _context = context;
        }

        // GET: api/SeccionEleccions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SeccionEleccionDto>>> GetRelaciones()
        {
            var relaciones = await _context.SeccionElecciones
                .Select(x => new SeccionEleccionDto
                {
                    SeccionId = x.SeccionId,
                    EleccionId = x.EleccionId
                })
                .ToListAsync();

            return Ok(relaciones);
        }

        // POST: api/SeccionEleccions
        [HttpPost]
        public async Task<IActionResult> PostRelacion(CreateSeccionEleccionDto dto)
        {
            var exists = await _context.SeccionElecciones.AnyAsync(x =>
                x.SeccionId == dto.SeccionId && x.EleccionId == dto.EleccionId);

            if (exists)
                return BadRequest("Ya existe la relación entre la sección y la elección.");

            var relacion = new SeccionEleccion
            {
                SeccionId = dto.SeccionId,
                EleccionId = dto.EleccionId
            };

            _context.SeccionElecciones.Add(relacion);
            await _context.SaveChangesAsync();

            return Ok(new SeccionEleccionDto
            {
                SeccionId = relacion.SeccionId,
                EleccionId = relacion.EleccionId
            });
        }

        // DELETE: api/SeccionEleccions?seccionId=1&eleccionId=2
        [HttpDelete]
        public async Task<IActionResult> DeleteRelacion([FromQuery] int seccionId, [FromQuery] int eleccionId)
        {
            var relacion = await _context.SeccionElecciones.FirstOrDefaultAsync(x =>
                x.SeccionId == seccionId && x.EleccionId == eleccionId);

            if (relacion == null)
                return NotFound("Relación no encontrada.");

            _context.SeccionElecciones.Remove(relacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
