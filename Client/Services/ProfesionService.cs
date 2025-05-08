using SMI.Shared.Models;
using System.Net.Http.Json;

namespace SMI.Client.Services
{
    public class ProfesionService
    {
        private readonly HttpClient _http;

        public ProfesionService(HttpClient http)
        {
            _http = http;
        }

        // Renombrado a GetProfesionesAsync
        public async Task<List<Profesion>> GetProfesionesAsync()
        {
            return await _http.GetFromJsonAsync<List<Profesion>>("api/profesion") ?? new();
        }
    }
}
