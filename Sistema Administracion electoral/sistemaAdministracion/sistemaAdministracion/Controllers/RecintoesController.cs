using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sistemaAdministracion.Data;
using sistemaAdministracion.Dtos;
using sistemaAdministracion.Models;

namespace sistemaAdministracion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecintoesController : ControllerBase
    {
        private readonly sistemaAdministracionContext _context;

        public RecintoesController(sistemaAdministracionContext context)
        {
            _context = context;
        }

        // GET: api/Recintoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecintoDto>>> GetRecintos()
        {
            var recintos = await _context.Recintos
                .Select(r => new RecintoDto
                {
                    Id = r.Id,
                    Nombre = r.Nombre,
                    Direccion = r.Direccion,
                    Coordenadas = r.Coordenadas
                })
                .ToListAsync();

            return Ok(recintos);
        }

        // GET: api/Recintoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RecintoDto>> GetRecinto(int id)
        {
            var recinto = await _context.Recintos.FindAsync(id);

            if (recinto == null)
            {
                return NotFound();
            }

            var dto = new RecintoDto
            {
                Id = recinto.Id,
                Nombre = recinto.Nombre,
                Direccion = recinto.Direccion,
                Coordenadas = recinto.Coordenadas
            };

            return Ok(dto);
        }

        // POST: api/Recintoes
        [HttpPost]
        public async Task<ActionResult<RecintoDto>> PostRecinto(CreateRecintoDto dto)
        {
            var recinto = new Recinto
            {
                Nombre = dto.Nombre,
                Direccion = dto.Direccion,
                Coordenadas = dto.Coordenadas
            };

            _context.Recintos.Add(recinto);
            await _context.SaveChangesAsync();

            var resultDto = new RecintoDto
            {
                Id = recinto.Id,
                Nombre = recinto.Nombre,
                Direccion = recinto.Direccion,
                Coordenadas = recinto.Coordenadas
            };

            return CreatedAtAction(nameof(GetRecinto), new { id = recinto.Id }, resultDto);
        }

        // PUT: api/Recintoes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecinto(int id, CreateRecintoDto dto)
        {
            var recinto = await _context.Recintos.FindAsync(id);
            if (recinto == null)
            {
                return NotFound();
            }

            recinto.Nombre = dto.Nombre;
            recinto.Direccion = dto.Direccion;
            recinto.Coordenadas = dto.Coordenadas;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Recintoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecinto(int id)
        {
            var recinto = await _context.Recintos.FindAsync(id);
            if (recinto == null)
            {
                return NotFound();
            }

            _context.Recintos.Remove(recinto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecintoExists(int id)
        {
            return _context.Recintos.Any(e => e.Id == id);
        }
    }
}
