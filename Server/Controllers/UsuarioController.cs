using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMI.Server.Data;
using SMI.Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMI.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _context.Usuario
                                 .Include(u => u.Persona) // Incluye la relación con Persona
                                 .ToListAsync();
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Usuario
                                        .Include(u => u.Persona)
                                        .FirstOrDefaultAsync(u => u.Id_Persona == id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // POST: api/Usuarios/Login
        [HttpPost("Login")]
        public async Task<ActionResult<Usuario>> Login([FromBody] LoginRequest request)
        {
            var usuario = await _context.Usuario
                                        .Include(u => u.Persona)
                                        .FirstOrDefaultAsync(u => u.Correo == request.Username && u.Clave == request.Password);

            if (usuario == null)
                return Unauthorized();

            return usuario;
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuario.Any(e => e.Id_Persona == id);
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
