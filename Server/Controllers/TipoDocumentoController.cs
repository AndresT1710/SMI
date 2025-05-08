using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMI.Server.Data;
using SMI.Shared.DTOs;
using SMI.Shared.Models;

namespace SMI.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoDocumentoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TipoDocumentoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/TipoDocumento
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoDocumentoDTO>>> GetTiposDocumento()
        {
            var tipos = await _context.TipoDocumento
                .Select(td => new TipoDocumentoDTO
                {
                    id = td.id,
                    nombre = td.nombre
                })
                .ToListAsync();

            return Ok(tipos);
        }

        // POST: api/TipoDocumento
        [HttpPost]
        public async Task<ActionResult<TipoDocumento>> CrearTipoDocumento([FromBody] TipoDocumentoDTO dto)
        {
            var tipo = new TipoDocumento { nombre = dto.nombre };
            _context.TipoDocumento.Add(tipo);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTiposDocumento), new { id = tipo.id }, tipo);
        }

        // PUT: api/TipoDocumento/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarTipoDocumento(int id, [FromBody] TipoDocumentoDTO dto)
        {
            var tipo = await _context.TipoDocumento.FindAsync(id);
            if (tipo == null) return NotFound();

            tipo.nombre = dto.nombre;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/TipoDocumento/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarTipoDocumento(int id)
        {
            var tipo = await _context.TipoDocumento.FindAsync(id);
            if (tipo == null) return NotFound();

            _context.TipoDocumento.Remove(tipo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
