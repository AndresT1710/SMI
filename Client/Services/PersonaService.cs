using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SMI.Shared.DTOs;
using SMI.Shared.Models;

namespace SMI.Client.Services
{
    public class PersonaService
    {
        private readonly HttpClient _httpClient;

        public PersonaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Obtener todos los usuarios
        public async Task<List<Persona>> GetUsuariosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Persona>>("api/Personas");
        }

        // Crear un nuevo usuario
        public async Task CreateUsuarioAsync(Persona persona)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Personas", persona);
            response.EnsureSuccessStatusCode();
        }

        // Actualizar usuario
        public async Task UpdateUsuarioAsync(Persona persona)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Personas/{persona.id}", persona);
            response.EnsureSuccessStatusCode();
        }

        // Eliminar un usuario
        public async Task DeleteUsuarioAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Personas/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task CreateSoloPersonaAsync(Persona persona, int idProfesion)
        {
            var dto = new PersonaConProfesionDTO
            {
                persona = persona,
                idProfesion = idProfesion
            };

            var response = await _httpClient.PostAsJsonAsync("api/Personas/CrearSoloPersona", dto);
            response.EnsureSuccessStatusCode();
        }

        public async Task CreateUsuarioYPersonaAsync(Persona persona, int idProfesion, string correo, string clave)
        {
            var dto = new UsuarioCompletoDTO
            {
                persona = persona,
                idProfesion = idProfesion,
                correo = correo,
                clave = clave
            };

            var response = await _httpClient.PostAsJsonAsync("api/Personas/CrearPersonaYUsuario", dto);
            response.EnsureSuccessStatusCode();
        }

        // Obtener la profesión por ID de persona
        public async Task<int> GetProfesionPorPersonaIdAsync(int personaId)
        {
            return await _httpClient.GetFromJsonAsync<int>($"api/Personas/{personaId}/Profesion");
        }

        // Obtener los datos del usuario por ID de persona
        public async Task<UsuarioDTO> GetUsuarioPorPersonaIdAsync(int personaId)
        {
            return await _httpClient.GetFromJsonAsync<UsuarioDTO>($"api/Personas/{personaId}/Usuario");
        }

        // EDITAR Persona sin Usuario
        public async Task UpdateSoloPersonaAsync(Persona persona, int idProfesion)
        {
            var dto = new PersonaConProfesionDTO
            {
                persona = persona,
                idProfesion = idProfesion
            };

            var response = await _httpClient.PutAsJsonAsync("api/Personas/ActualizarSoloPersona", dto);
            response.EnsureSuccessStatusCode();
        }

        // EDITAR Persona con Usuario
        public async Task UpdateUsuarioYPersonaAsync(Persona persona, int idProfesion, string correo, string clave)
        {
            var dto = new UsuarioCompletoDTO
            {
                persona = persona,
                idProfesion = idProfesion,
                correo = correo,
                clave = clave
            };

            var response = await _httpClient.PutAsJsonAsync("api/Personas/ActualizarPersonaYUsuario", dto);
            response.EnsureSuccessStatusCode();
        }

        public async Task CreatePersonaConDocumentoAsync(Persona persona, int idTipoDocumento, string numeroDocumento)
        {
            var dto = new
            {
                persona = persona,
                documentos = new[]
                {
            new
            {
                id_TipoDocumento = idTipoDocumento,
                numeroDocumento = numeroDocumento
            }
        }
            };

            await _httpClient.PostAsJsonAsync("api/persona/CrearPersonaConDocumentos", dto);
        }


        // Obtener tipo de documento por ID
        public async Task<TipoDocumento> GetTipoDocumentoPorIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<TipoDocumento>($"api/TipoDocumento/{id}");
        }

        // Obtener todos los tipos de documento
        public async Task<List<TipoDocumento>> GetTiposDocumentoAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<TipoDocumento>>("api/TipoDocumento");
        }
    }
}
