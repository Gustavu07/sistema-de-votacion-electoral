using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sistemaAdministracion.Data;
using sistemaAdministracion.Dtos;
using sistemaAdministracion.Models;

namespace sistemaAdministracion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoesController : ControllerBase
    {
        private readonly sistemaAdministracionContext _context;

        public CargoesController(sistemaAdministracionContext context)
        {
            _context = context;
        }

        // GET: api/Cargoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CargoDto>>> GetCargos()
        {
            var cargos = await _context.Cargos
                .Select(c => new CargoDto
                {
                    Id = c.Id,
                    Nombre = c.Nombre
                })
                .ToListAsync();

            return Ok(cargos);
        }

        // GET: api/Cargoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CargoDto>> GetCargo(int id)
        {
            var cargo = await _context.Cargos.FindAsync(id);

            if (cargo == null)
            {
                return NotFound();
            }

            var dto = new CargoDto
            {
                Id = cargo.Id,
                Nombre = cargo.Nombre
            };

            return Ok(dto);
        }

        // POST: api/Cargoes
        [HttpPost]
        public async Task<ActionResult<CargoDto>> PostCargo(CreateCargoDto dto)
        {
            var cargo = new Cargo
            {
                Nombre = dto.Nombre
            };

            _context.Cargos.Add(cargo);
            await _context.SaveChangesAsync();

            var resultDto = new CargoDto
            {
                Id = cargo.Id,
                Nombre = cargo.Nombre
            };

            return CreatedAtAction(nameof(GetCargo), new { id = cargo.Id }, resultDto);
        }

        // PUT: api/Cargoes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCargo(int id, CreateCargoDto dto)
        {
            var cargo = await _context.Cargos.FindAsync(id);
            if (cargo == null)
            {
                return NotFound();
            }

            cargo.Nombre = dto.Nombre;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Cargoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCargo(int id)
        {
            var cargo = await _context.Cargos.FindAsync(id);
            if (cargo == null)
            {
                return NotFound();
            }

            _context.Cargos.Remove(cargo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CargoExists(int id)
        {
            return _context.Cargos.Any(e => e.Id == id);
        }
    }
}
