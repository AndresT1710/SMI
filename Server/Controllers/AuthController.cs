using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SMI.Server.Data;
using SMI.Shared.DTOs;
using SMI.Shared.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SMI.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;

        public AuthController(AppDbContext context, IConfiguration configuration, ILogger<AuthController> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }

        // Iniciar sesión
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDTO>> Login([FromBody] LoginDTO loginDto)
        {
            try
            {
                _logger.LogInformation("Intento de inicio de sesión para: {Email}", loginDto.Correo);

                var user = await _context.Usuario
                    .Include(u => u.Persona)
                    .FirstOrDefaultAsync(u =>
                        u.Correo == loginDto.Correo && u.Activo);

                if (user == null)
                {
                    _logger.LogWarning("Usuario no encontrado o no activo: {Email}", loginDto.Correo);
                    return Unauthorized("Usuario no encontrado o no activo.");
                }

                // Verifica la contraseña
                bool passwordValid = BCrypt.Net.BCrypt.Verify(loginDto.Clave, user.Clave);
                if (!passwordValid)
                {
                    _logger.LogWarning("Contraseña incorrecta para: {Email}", loginDto.Correo);
                    return Unauthorized("Contraseña incorrecta.");
                }

                var userDto = new UsuarioDTO
                {
                    Id = user.Id,
                    Correo = user.Correo,
                    IdPersona = user.Id_Persona,
                    Nombre = user.Persona.nombre,
                    Apellido = user.Persona.apellido
                };

                // Genera token y refresh token
                var token = GenerateJwtToken(userDto);
                var refreshToken = GenerateRefreshToken();

                // Actualiza el refresh token sin importar si era null antes
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Inicio de sesión exitoso para: {Email}", loginDto.Correo);

                return Ok(new AuthResponseDTO
                {
                    Usuario = userDto,
                    Token = token,
                    RefreshToken = refreshToken
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en el inicio de sesión para {Email}", loginDto.Correo);
                return BadRequest(new { mensaje = "Error en el servidor", error = ex.Message });
            }
        }

        // Renovación de tokens
        [HttpPost("refresh-token")]
        public async Task<ActionResult<AuthResponseDTO>> RefreshToken([FromBody] RefreshTokenDTO refreshTokenDto)
        {
            try
            {
                var principal = GetPrincipalFromExpiredToken(refreshTokenDto.Token);
                var email = principal.FindFirst(ClaimTypes.Email)?.Value;

                if (string.IsNullOrEmpty(email))
                    return BadRequest("Token inválido");

                var user = await _context.Usuario
                    .Include(u => u.Persona)
                    .FirstOrDefaultAsync(u => u.Correo == email && u.Activo);

                if (user == null ||
                    string.IsNullOrEmpty(user.RefreshToken) ||
                    user.RefreshToken != refreshTokenDto.RefreshToken ||
                    (user.RefreshTokenExpiryTime ?? DateTime.MinValue) <= DateTime.UtcNow)
                {
                    return BadRequest("Refresh token inválido o expirado");
                }

                var userDto = new UsuarioDTO
                {
                    Id = user.Id,
                    Correo = user.Correo,
                    IdPersona = user.Id_Persona,
                    Nombre = user.Persona.nombre,
                    Apellido = user.Persona.apellido
                };

                var newToken = GenerateJwtToken(userDto);
                var newRefreshToken = GenerateRefreshToken();

                user.RefreshToken = newRefreshToken;
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
                await _context.SaveChangesAsync();

                return Ok(new AuthResponseDTO
                {
                    Usuario = userDto,
                    Token = newToken,
                    RefreshToken = newRefreshToken
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al refrescar token");
                return BadRequest(new { mensaje = "Error al refrescar token", error = ex.Message });
            }
        }

        // Cerrar sesión
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutDTO logoutDto)
        {
            if (logoutDto == null || string.IsNullOrEmpty(logoutDto.Email))
            {
                return BadRequest("El correo electrónico es necesario.");
            }

            var user = await _context.Usuario.FirstOrDefaultAsync(u => u.Correo == logoutDto.Email);
            if (user != null)
            {
                user.RefreshToken = null;
                user.RefreshTokenExpiryTime = null;
                await _context.SaveChangesAsync();
            }
            return Ok();
        }


        // Migración de contraseñas
        [HttpPost("migrar-passwords")]
        public async Task<IActionResult> MigrarPasswords()
        {
            try
            {
                var usuarios = await _context.Usuario.ToListAsync();
                int actualizados = 0;

                foreach (var usuario in usuarios)
                {
                    if (!usuario.Clave.StartsWith("$2a$") && !usuario.Clave.StartsWith("$2b$") && !usuario.Clave.StartsWith("$2y$"))
                    {
                        string passwordPlano = usuario.Clave;
                        usuario.Clave = BCrypt.Net.BCrypt.HashPassword(passwordPlano, 12);
                        actualizados++;
                    }
                }

                await _context.SaveChangesAsync();
                return Ok(new { mensaje = "Migración de contraseñas completada", actualizados });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al migrar contraseñas");
                return BadRequest(new { mensaje = "Error en el servidor", error = ex.Message });
            }
        }

        #region Helper Methods

        private string GenerateJwtToken(UsuarioDTO user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.Nombre} {user.Apellido}"),
                new Claim(ClaimTypes.Email, user.Correo),
                new Claim("IdPersona", user.IdPersona.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidAudience = _configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Token inválido");
            }

            return principal;
        }

        #endregion
    }
}
