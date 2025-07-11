using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sistemaAdministracion.Data;
using sistemaAdministracion.Dtos;
using sistemaAdministracion.Models;

namespace sistemaAdministracion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PapeletasController : ControllerBase
    {
        private readonly sistemaAdministracionContext _context;

        public PapeletasController(sistemaAdministracionContext context)
        {
            _context = context;
        }

        // GET: api/Papeletas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PapeletaDto>>> GetPapeletas()
        {
            var papeletas = await _context.Papeletas
                .Include(p => p.Candidatos)
                .Select(p => new PapeletaDto
                {
                    Id = p.Id,
                    SeccionId = p.SeccionId,
                    EleccionId = p.EleccionId,
                    CandidaturaIds = p.Candidatos.Select(c => c.Id).ToList()
                })
                .ToListAsync();

            return Ok(papeletas);
        }

        // GET: api/Papeletas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PapeletaDto>> GetPapeleta(int id)
        {
            var papeleta = await _context.Papeletas
                .Include(p => p.Candidatos)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (papeleta == null)
                return NotFound();

            var dto = new PapeletaDto
            {
                Id = papeleta.Id,
                SeccionId = papeleta.SeccionId,
                EleccionId = papeleta.EleccionId,
                CandidaturaIds = papeleta.Candidatos.Select(c => c.Id).ToList()
            };

            return Ok(dto);
        }

        // POST: api/Papeletas
        [HttpPost]
        public async Task<ActionResult<PapeletaDto>> PostPapeleta(CreatePapeletaDto dto)
        {
            var seccion = await _context.Secciones.FindAsync(dto.SeccionId);
            var eleccion = await _context.Elecciones.FindAsync(dto.EleccionId);

            if (seccion == null || eleccion == null)
                return BadRequest("Sección o Elección no válida.");

            var candidaturas = await _context.Candidaturas
                .Where(c => dto.CandidaturaIds.Contains(c.Id))
                .ToListAsync();

            var papeleta = new Papeleta
            {
                SeccionId = dto.SeccionId,
                EleccionId = dto.EleccionId,
                Candidatos = candidaturas
            };

            _context.Papeletas.Add(papeleta);
            await _context.SaveChangesAsync();

            var result = new PapeletaDto
            {
                Id = papeleta.Id,
                SeccionId = papeleta.SeccionId,
                EleccionId = papeleta.EleccionId,
                CandidaturaIds = papeleta.Candidatos.Select(c => c.Id).ToList()
            };

            return CreatedAtAction(nameof(GetPapeleta), new { id = papeleta.Id }, result);
        }

        // PUT: api/Papeletas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPapeleta(int id, CreatePapeletaDto dto)
        {
            var papeleta = await _context.Papeletas
                .Include(p => p.Candidatos)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (papeleta == null)
                return NotFound();

            papeleta.SeccionId = dto.SeccionId;
            papeleta.EleccionId = dto.EleccionId;

            var candidaturas = await _context.Candidaturas
                .Where(c => dto.CandidaturaIds.Contains(c.Id))
                .ToListAsync();

            papeleta.Candidatos = candidaturas;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Papeletas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePapeleta(int id)
        {
            var papeleta = await _context.Papeletas.FindAsync(id);
            if (papeleta == null)
                return NotFound();

            _context.Papeletas.Remove(papeleta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PapeletaExists(int id)
        {
            return _context.Papeletas.Any(p => p.Id == id);
        }
    }
}
