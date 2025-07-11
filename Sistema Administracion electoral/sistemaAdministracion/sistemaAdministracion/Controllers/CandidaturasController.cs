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
    public class CandidaturasController : ControllerBase
    {
        private readonly sistemaAdministracionContext _context;

        public CandidaturasController(sistemaAdministracionContext context)
        {
            _context = context;
        }

        // GET: api/Candidaturas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CandidaturaDto>>> GetCandidaturas()
        {
            var candidaturas = await _context.Candidaturas
                .Include(c => c.Cargo)
                .Include(c => c.Partido)
                .Select(c => new CandidaturaDto
                {
                    Id = c.Id,
                    NombreCandidato = c.NombreCandidato,
                    NombreCargo = c.Cargo.Nombre,
                    NombrePartido = c.Partido.Nombre
                })
                .ToListAsync();

            return Ok(candidaturas);
        }

        // GET: api/Candidaturas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CandidaturaDto>> GetCandidatura(int id)
        {
            var c = await _context.Candidaturas
                .Include(c => c.Cargo)
                .Include(c => c.Partido)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (c == null)
            {
                return NotFound();
            }

            var dto = new CandidaturaDto
            {
                Id = c.Id,
                NombreCandidato = c.NombreCandidato,
                NombreCargo = c.Cargo.Nombre,
                NombrePartido = c.Partido.Nombre
            };

            return Ok(dto);
        }

        // POST: api/Candidaturas
        [HttpPost]
        public async Task<ActionResult<CandidaturaDto>> PostCandidatura(CreateCandidaturaDto dto)
        {
            var cargo = await _context.Cargos.FindAsync(dto.CargoId);
            var partido = await _context.Partidos.FindAsync(dto.PartidoId);

            if (cargo == null || partido == null)
            {
                return BadRequest("Cargo o Partido no encontrado.");
            }

            var candidatura = new Candidatura
            {
                NombreCandidato = dto.NombreCandidato,
                CargoId = dto.CargoId,
                PartidoId = dto.PartidoId
            };

            _context.Candidaturas.Add(candidatura);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCandidatura), new { id = candidatura.Id }, new CandidaturaDto
            {
                Id = candidatura.Id,
                NombreCandidato = candidatura.NombreCandidato,
                NombreCargo = cargo.Nombre,
                NombrePartido = partido.Nombre
            });
        }


        // PUT: api/Candidaturas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCandidatura(int id, CreateCandidaturaDto dto)
        {
            var candidatura = await _context.Candidaturas.FindAsync(id);
            if (candidatura == null)
            {
                return NotFound();
            }

            var cargo = await _context.Cargos.FindAsync(dto.CargoId);
            var partido = await _context.Partidos.FindAsync(dto.PartidoId);

            if (cargo == null || partido == null)
            {
                return BadRequest("Cargo o Partido no encontrado.");
            }

            candidatura.NombreCandidato = dto.NombreCandidato;
            candidatura.CargoId = dto.CargoId;
            candidatura.PartidoId = dto.PartidoId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Candidaturas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCandidatura(int id)
        {
            var candidatura = await _context.Candidaturas.FindAsync(id);
            if (candidatura == null)
            {
                return NotFound();
            }

            _context.Candidaturas.Remove(candidatura);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CandidaturaExists(int id)
        {
            return _context.Candidaturas.Any(e => e.Id == id);
        }
    }
}

