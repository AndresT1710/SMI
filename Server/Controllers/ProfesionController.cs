using Microsoft.AspNetCore.Mvc;
using SMI.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using SMI.Server.Data;

namespace SMI.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfesionController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProfesionController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Profesion>>> GetProfesiones()
        {
            return await _context.Profesion.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Profesion>> GetProfesion(int id)
        {
            var profesion = await _context.Profesion.FindAsync(id);
            if (profesion == null)
            {
                return NotFound();
            }
            return profesion;
        }
        [HttpPost]
        public async Task<ActionResult<Profesion>> CreateProfesion(Profesion profesion)
        {
            _context.Profesion.Add(profesion);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProfesion), new { id = profesion.id }, profesion);
        }
    }

}
