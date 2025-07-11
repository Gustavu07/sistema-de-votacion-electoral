using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sistemaAdministracion.Data;
using sistemaAdministracion.Dtos;
using sistemaAdministracion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sistemaAdministracion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartidoesController : ControllerBase
    {
        private readonly sistemaAdministracionContext _context;

        public PartidoesController(sistemaAdministracionContext context)
        {
            _context = context;
        }

        // GET: api/Partidoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PartidoDto>>> GetPartidos()
        {
            var partidos = await _context.Partidos
                .Select(p => new PartidoDto
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Sigla = p.Sigla,
                    Color = p.Color
                })
                .ToListAsync();

            return Ok(partidos);
        }

        // GET: api/Partidoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PartidoDto>> GetPartido(int id)
        {
            var partido = await _context.Partidos.FindAsync(id);

            if (partido == null)
            {
                return NotFound();
            }

            var dto = new PartidoDto
            {
                Id = partido.Id,
                Nombre = partido.Nombre,
                Sigla = partido.Sigla,
                Color = partido.Color
            };

            return Ok(dto);
        }

        // POST: api/Partidoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PartidoDto>> PostPartido(CreatePartidoDto dto)
        {
            var partido = new Partido
            {
                Nombre = dto.Nombre,
                Sigla = dto.Sigla,
                Color = dto.Color
            };

            _context.Partidos.Add(partido);
            await _context.SaveChangesAsync();

            var resultDto = new PartidoDto
            {
                Id = partido.Id,
                Nombre = partido.Nombre,
                Sigla = partido.Sigla,
                Color = partido.Color
            };

            return CreatedAtAction(nameof(GetPartido), new { id = partido.Id }, resultDto);
        }

        // PUT: api/Partidos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPartido(int id, CreatePartidoDto dto)
        {
            var partido = await _context.Partidos.FindAsync(id);
            if (partido == null)
            {
                return NotFound();
            }

            partido.Nombre = dto.Nombre;
            partido.Sigla = dto.Sigla;
            partido.Color = dto.Color;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Partidos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePartido(int id)
        {
            var partido = await _context.Partidos.FindAsync(id);
            if (partido == null)
            {
                return NotFound();
            }

            _context.Partidos.Remove(partido);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PartidoExists(int id)
        {
            return _context.Partidos.Any(e => e.Id == id);
        }
    }
}
