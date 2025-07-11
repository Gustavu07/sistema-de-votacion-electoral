using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sistemaAdministracion.Data;
using sistemaAdministracion.Dtos;
using sistemaAdministracion.Models;

namespace sistemaAdministracion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotantesController : ControllerBase
    {
        private readonly sistemaAdministracionContext _context;

        public VotantesController(sistemaAdministracionContext context)
        {
            _context = context;
        }

        // GET: api/Votantes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VotanteDto>>> GetVotantes()
        {
            var votantes = await _context.Votantes
                .Select(v => new VotanteDto
                {
                    Id = v.Id,
                    NombreCompleto = v.NombreCompleto,
                    ApellidoPaterno = v.ApellidoPaterno,
                    MesaElectoralId = v.MesaElectoralId
                })
                .ToListAsync();

            return Ok(votantes);
        }

        // GET: api/Votantes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VotanteDto>> GetVotante(int id)
        {
            var votante = await _context.Votantes.FindAsync(id);

            if (votante == null)
                return NotFound();

            var dto = new VotanteDto
            {
                Id = votante.Id,
                NombreCompleto = votante.NombreCompleto,
                ApellidoPaterno = votante.ApellidoPaterno,
                MesaElectoralId = votante.MesaElectoralId
            };

            return Ok(dto);
        }

        // POST: api/Votantes
        [HttpPost]
        public async Task<ActionResult<VotanteDto>> PostVotante(CreateVotanteDto dto)
        {
            // Buscar las mesas del recinto
            var mesas = await _context.MesasElectorales
                .Where(m => m.RecintoId == dto.RecintoId)
                .Include(m => m.Votantes)
                .ToListAsync();

            if (!mesas.Any())
                return BadRequest("No hay mesas registradas para el recinto");

            // Elegir la mesa con menos votantes
            var mesaAsignada = mesas.OrderBy(m => m.Votantes.Count).First();

            var votante = new Votante
            {
                NombreCompleto = dto.NombreCompleto,
                ApellidoPaterno = dto.ApellidoPaterno,
                MesaElectoralId = mesaAsignada.Id
            };

            _context.Votantes.Add(votante);
            await _context.SaveChangesAsync();

            var result = new VotanteDto
            {
                Id = votante.Id,
                NombreCompleto = votante.NombreCompleto,
                ApellidoPaterno = votante.ApellidoPaterno,
                MesaElectoralId = votante.MesaElectoralId
            };

            return CreatedAtAction(nameof(GetVotante), new { id = votante.Id }, result);
        }

        // PUT: api/Votantes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVotante(int id, CreateVotanteDto dto)
        {
            var votante = await _context.Votantes.FindAsync(id);
            if (votante == null)
                return NotFound();

            // Buscar nueva mesa si cambia de recinto
            var mesas = await _context.MesasElectorales
                .Where(m => m.RecintoId == dto.RecintoId)
                .Include(m => m.Votantes)
                .ToListAsync();

            if (!mesas.Any())
                return BadRequest("No hay mesas registradas para el recinto");

            var mesaAsignada = mesas.OrderBy(m => m.Votantes.Count).First();

            votante.NombreCompleto = dto.NombreCompleto;
            votante.ApellidoPaterno = dto.ApellidoPaterno;
            votante.MesaElectoralId = mesaAsignada.Id;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Votantes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVotante(int id)
        {
            var votante = await _context.Votantes.FindAsync(id);
            if (votante == null)
                return NotFound();

            _context.Votantes.Remove(votante);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VotanteExists(int id)
        {
            return _context.Votantes.Any(e => e.Id == id);
        }
    }
}
