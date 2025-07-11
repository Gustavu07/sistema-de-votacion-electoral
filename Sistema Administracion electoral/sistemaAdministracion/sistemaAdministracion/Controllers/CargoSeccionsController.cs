using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sistemaAdministracion.Data;
using sistemaAdministracion.Dtos;
using sistemaAdministracion.Models;

namespace sistemaAdministracion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoSeccionsController : ControllerBase
    {
        private readonly sistemaAdministracionContext _context;

        public CargoSeccionsController(sistemaAdministracionContext context)
        {
            _context = context;
        }

        // GET: api/CargoSeccions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CargoSeccionDto>>> GetRelaciones()
        {
            var relaciones = await _context.CargoSecciones
                .Select(x => new CargoSeccionDto
                {
                    CargoId = x.CargoId,
                    SeccionId = x.SeccionId
                })
                .ToListAsync();

            return Ok(relaciones);
        }

        // POST: api/CargoSeccions
        [HttpPost]
        public async Task<ActionResult<CargoSeccionDto>> PostRelacion(CreateCargoSeccionDto dto)
        {
            var exists = await _context.CargoSecciones.AnyAsync(cs =>
                cs.CargoId == dto.CargoId && cs.SeccionId == dto.SeccionId);

            if (exists)
                return BadRequest("Ya existe la relación entre el cargo y la sección.");

            var relacion = new CargoSeccion
            {
                CargoId = dto.CargoId,
                SeccionId = dto.SeccionId
            };

            _context.CargoSecciones.Add(relacion);
            await _context.SaveChangesAsync();

            return Ok(new CargoSeccionDto
            {
                CargoId = relacion.CargoId,
                SeccionId = relacion.SeccionId
            });
        }

        // DELETE: api/CargoSeccions?cargoId=1&seccionId=2
        [HttpDelete]
        public async Task<IActionResult> DeleteRelacion([FromQuery] int cargoId, [FromQuery] int seccionId)
        {
            var relacion = await _context.CargoSecciones.FirstOrDefaultAsync(cs =>
                cs.CargoId == cargoId && cs.SeccionId == seccionId);

            if (relacion == null)
                return NotFound("Relación no encontrada.");

            _context.CargoSecciones.Remove(relacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
