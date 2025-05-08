using System.Net.Http.Json;
using SMI.Shared.Interfaces;
using SMI.Shared.Models;

namespace SMI.Client.Services
{
    public class TipoDocumentoService : ITipoDocumentoService
    {
        private readonly HttpClient _http;

        public TipoDocumentoService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<TipoDocumento>> GetTiposDocumentoAsync()
        {
            return await _http.GetFromJsonAsync<List<TipoDocumento>>("api/TipoDocumento");
        }
    }

}
