using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMI.Shared.Models;
using SMI.Shared.DTOs;
using SMI.Server.Data;
using BCrypt.Net;

namespace SMI.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PersonasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Personas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Persona>>> GetPersonas()
        {
            return await _context.Persona.ToListAsync();
        }

        // GET: api/Personas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Persona>> GetPersona(int id)
        {
            var persona = await _context.Persona.FindAsync(id);

            if (persona == null)
            {
                return NotFound();
            }

            return persona;
        }

        // PUT: api/Personas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersona(int id, Persona persona)
        {
            if (id != persona.id)
            {
                return BadRequest();
            }

            _context.Entry(persona).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Personas
        [HttpPost]
        public async Task<ActionResult<Persona>> PostPersona(Persona persona)
        {
            _context.Persona.Add(persona);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersona", new { id = persona.id }, persona);
        }

        // DELETE: api/Personas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersona(int id)
        {
            var persona = await _context.Persona.FindAsync(id);
            if (persona == null)
            {
                return NotFound();
            }

            _context.Persona.Remove(persona);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonaExists(int id)
        {
            return _context.Persona.Any(e => e.id == id);
        }

        // Método para crear solo persona con profesión
        [HttpPost("CrearSoloPersona")]
        public async Task<IActionResult> CreateSoloPersona([FromBody] PersonaConProfesionDTO dto)
        {
            if (dto?.persona == null) return BadRequest("Datos incompletos");

            // 1) Crear Persona
            _context.Persona.Add(dto.persona);
            await _context.SaveChangesAsync();

            // 2) Solo insertar si es Profesion 1 o 2
            if (dto.idProfesion == 1 || dto.idProfesion == 2)
            {
                await _context.Database.ExecuteSqlInterpolatedAsync($@"
            INSERT INTO PersonaProfesion (id_Persona, id_Profesion)
            VALUES ({dto.persona.id}, {dto.idProfesion})");
            }

            return Ok(dto.persona);
        }

        // Método para crear persona con usuario y profesión
        [HttpPost("CrearPersonaYUsuario")]
        public async Task<IActionResult> CreatePersonaYUsuario([FromBody] UsuarioCompletoDTO dto)
        {
            try
            {
                if (dto?.persona == null
                    || string.IsNullOrWhiteSpace(dto.correo)
                    || string.IsNullOrWhiteSpace(dto.clave)
                    || dto.idProfesion == 0)
                    return BadRequest("Datos incompletos");

                // 1) Crear Persona
                _context.Persona.Add(dto.persona);
                await _context.SaveChangesAsync(); // Aquí se genera el id

                // 2) Crear Usuario con el id generado
                var user = new Usuario
                {
                    Id_Persona = dto.persona.id, // Aquí ya está asignado
                    Correo = dto.correo,
                    Clave = BCrypt.Net.BCrypt.HashPassword(dto.clave),
                    Activo = true,
                    RefreshToken = Guid.NewGuid().ToString() ,
                    RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7) // Establecer un valor por defecto
                };
                _context.Usuario.Add(user);

                await _context.SaveChangesAsync(); // Guarda el usuario

                // 3) Relacionar profesión con el id generado
                await _context.Database.ExecuteSqlRawAsync(
                    "INSERT INTO PersonaProfesion (id_Persona, id_Profesion) VALUES ({0}, {1})",
                    dto.persona.id, // Ya tiene el valor correcto
                    dto.idProfesion
                );

                var resultDTO = new UsuarioCreadoDTO
                {
                    Id = user.Id,
                    Id_Persona = dto.persona.id,
                    Correo = user.Correo,
                    Activo = user.Activo
                };

                return Ok(resultDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        // Obtener Persona con profesión
        [HttpGet("ObtenerPersonaConProfesion/{id}")]
        public async Task<ActionResult<PersonaConProfesionDTO>> ObtenerPersonaConProfesion(int id)
        {
            var persona = await _context.Persona.FindAsync(id);
            if (persona == null) return NotFound();

            var profesion = await _context.PersonaProfesion
                .Where(pp => pp.id_Persona == id)
                .Select(pp => pp.id_Profesion)
                .FirstOrDefaultAsync();

            return new PersonaConProfesionDTO
            {
                persona = persona,
                idProfesion = profesion
            };
        }

        // Obtener Persona completa con profesión y correo
        [HttpGet("ObtenerPersonaCompleta/{id}")]
        public async Task<ActionResult<PersonaConProfesionYCorreoDTO>> ObtenerPersonaCompleta(int id)
        {
            var persona = await _context.Persona
                .Include(p => p.Usuarios)
                .FirstOrDefaultAsync(p => p.id == id);

            if (persona == null) return NotFound();

            var profesion = await _context.PersonaProfesion
                .Where(pp => pp.id_Persona == id)
                .Select(pp => pp.id_Profesion)
                .FirstOrDefaultAsync();

            var correo = persona.Usuarios.FirstOrDefault()?.Correo;

            return new PersonaConProfesionYCorreoDTO
            {
                persona = persona,
                idProfesion = profesion,
                correo = correo
            };
        }

        // Obtener profesión por ID de persona
        [HttpGet("{id}/Profesion")]
        public async Task<ActionResult<int>> GetProfesionPorPersonaId(int id)
        {
            var profesion = await _context.PersonaProfesion
                .Where(pp => pp.id_Persona == id)
                .Select(pp => pp.id_Profesion)
                .FirstOrDefaultAsync();

            if (profesion == 0)
                return 3;

            return profesion;
        }

        // Obtener usuario por ID de persona
        [HttpGet("{id}/Usuario")]
        public async Task<ActionResult<UsuarioDTO>> GetUsuarioPorPersonaId(int id)
        {
            var usuario = await _context.Usuario
                .Where(u => u.Id_Persona == id)
                .Select(u => new UsuarioDTO
                {
                    Correo = u.Correo,
                    Clave = u.Clave
                })
                .FirstOrDefaultAsync();

            if (usuario == null)
                return NotFound();

            return usuario;
        }
        [HttpPut("ActualizarSoloPersona")]
        public async Task<IActionResult> ActualizarSoloPersona([FromBody] PersonaConProfesionDTO dto)
        {
            if (dto?.persona == null) return BadRequest("Datos incompletos");

            // 1) Actualizar Persona
            _context.Entry(dto.persona).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonaExists(dto.persona.id))
                    return NotFound();
                else
                    throw;
            }

            // 2) Actualizar o Insertar profesión
            var existe = await _context.PersonaProfesion
                .AnyAsync(pp => pp.id_Persona == dto.persona.id);

            if (existe)
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "UPDATE PersonaProfesion SET id_Profesion = {0} WHERE id_Persona = {1}",
                    dto.idProfesion, dto.persona.id);
            }
            else
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "INSERT INTO PersonaProfesion (id_Persona, id_Profesion) VALUES ({0}, {1})",
                    dto.persona.id, dto.idProfesion);
            }

            return NoContent();
        }

        [HttpPut("ActualizarPersonaYUsuario")]
        public async Task<IActionResult> ActualizarPersonaYUsuario([FromBody] UsuarioCompletoDTO dto)
        {
            if (dto?.persona == null
                || string.IsNullOrWhiteSpace(dto.correo)
                || dto.idProfesion == 0)
                return BadRequest("Datos incompletos");

            // 1) Actualizar Persona
            _context.Entry(dto.persona).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            // 2) Actualizar Usuario
            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(u => u.Id_Persona == dto.persona.id);

            if (usuario == null)
            {
                // Si no existe el usuario, lo creamos
                usuario = new Usuario
                {
                    Id_Persona = dto.persona.id,
                    Correo = dto.correo,
                    Clave = BCrypt.Net.BCrypt.HashPassword(dto.clave),  // Aplicar hash si no hay contraseña previa
                    Activo = true
                };
                _context.Usuario.Add(usuario);
            }
            else
            {
                usuario.Correo = dto.correo;

                // Solo actualizamos la clave si la nueva no es nula o vacía
                if (!string.IsNullOrWhiteSpace(dto.clave))
                {
                    usuario.Clave = BCrypt.Net.BCrypt.HashPassword(dto.clave); // Se aplica un hash solo si hay una clave nueva
                }
            }

            await _context.SaveChangesAsync();

            // 3) Actualizar o Insertar profesión
            var existe = await _context.PersonaProfesion
                .AnyAsync(pp => pp.id_Persona == dto.persona.id);

            var sql = existe
                ? "UPDATE PersonaProfesion SET id_Profesion = {0} WHERE id_Persona = {1}"
                : "INSERT INTO PersonaProfesion (id_Persona, id_Profesion) VALUES ({1}, {0})";

            await _context.Database.ExecuteSqlRawAsync(
                sql,
                dto.idProfesion,
                dto.persona.id
            );

            return NoContent();
        }


        [HttpPost("CrearPersonaConDocumentos")]
        public async Task<IActionResult> CrearPersonaConDocumentos([FromBody] PersonaConDocumentosDTO dto)
        {
            if (dto?.persona == null) return BadRequest("Datos incompletos");

            // TEMPORAL: Loguear lo que llega
            Console.WriteLine($"Recibido: {dto.documentos?.Count} documentos");

            foreach (var doc in dto.documentos)
            {
                Console.WriteLine($"Tipo: {doc.id_TipoDocumento}, Número: {doc.numeroDocumento}");
            }

            // 1) Guardar persona
            _context.Persona.Add(dto.persona);
            await _context.SaveChangesAsync();

            // 2) Guardar documentos
            foreach (var doc in dto.documentos)
            {
                var personaDoc = new PersonaDocumento
                {
                    id_Persona = dto.persona.id,
                    id_TipoDocumento = doc.id_TipoDocumento,
                    numeroDocumento = doc.numeroDocumento
                };
                _context.PersonaDocumento.Add(personaDoc);
            }

            await _context.SaveChangesAsync();
            return Ok(dto);
        }


        [HttpPut("ActualizarPersonaDocumentos")]
        public async Task<IActionResult> ActualizarPersonaDocumentos([FromBody] PersonaConDocumentosDTO dto)
        {
            if (dto?.persona == null) return BadRequest("Datos incompletos");

            // 1) Actualizar persona
            _context.Entry(dto.persona).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            // 2) Eliminar documentos anteriores
            var docsAntiguos = await _context.PersonaDocumento
                .Where(d => d.id_Persona == dto.persona.id)
                .ToListAsync();

            _context.PersonaDocumento.RemoveRange(docsAntiguos);

            // 3) Agregar los nuevos documentos
            foreach (var doc in dto.documentos)
            {
                _context.PersonaDocumento.Add(new PersonaDocumento
                {
                    id_Persona = dto.persona.id,
                    id_TipoDocumento = doc.id_TipoDocumento,
                    numeroDocumento = doc.numeroDocumento
                });
            }

            await _context.SaveChangesAsync();
            return Ok(dto);
        }

        [HttpGet("ObtenerPersonaConDocumentos/{id}")]
        public async Task<ActionResult<PersonaConDocumentosDTO>> ObtenerPersonaConDocumentos(int id)
        {
            // Obtener persona
            var persona = await _context.Persona.FindAsync(id);
            if (persona == null) return NotFound("Persona no encontrada");

            // Obtener documentos
            var documentos = await _context.PersonaDocumento
                .Where(d => d.id_Persona == id)
                .Select(d => new DocumentoDTO
                {
                    id_TipoDocumento = d.id_TipoDocumento,
                    numeroDocumento = d.numeroDocumento
                })
                .ToListAsync();

            // Armar y devolver el DTO
            var dto = new PersonaConDocumentosDTO
            {
                persona = persona,
                documentos = documentos
            };

            return Ok(dto);
        }

        // Obtener todas las personas con sus documentos
        [HttpGet("ListarPersonasConDocumentos")]
        public async Task<ActionResult<List<PersonaConDocumentosDTO>>> ListarPersonasConDocumentos()
        {
            // Obtener todas las personas
            var personas = await _context.Persona.ToListAsync();

            // Obtener todos los documentos relacionados
            var documentosPorPersona = await _context.PersonaDocumento
                .GroupBy(d => d.id_Persona)
                .ToDictionaryAsync(
                    g => g.Key,
                    g => g.Select(d => new DocumentoDTO
                    {
                        id_TipoDocumento = d.id_TipoDocumento,
                        numeroDocumento = d.numeroDocumento
                    }).ToList()
                );

            // Armar la lista de DTOs
            var listaDTO = personas.Select(p => new PersonaConDocumentosDTO
            {
                persona = p,
                documentos = documentosPorPersona.ContainsKey(p.id) ? documentosPorPersona[p.id] : new List<DocumentoDTO>()
            }).ToList();

            return Ok(listaDTO);
        }




    }
}
