using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using SMI.Shared.DTOs;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SMI.Client.Services
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationState _anonymous;
        private const string AuthDataKey = "authData";

        public AuthStateProvider(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
            _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var authData = await _localStorage.GetItemAsync<AuthResponseDTO>(AuthDataKey);

                if (authData == null || authData.Usuario == null || string.IsNullOrEmpty(authData.Token))
                    return _anonymous;

                var usuario = authData.Usuario;
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, $"{usuario.Nombre} {usuario.Apellido}"),
                    new Claim(ClaimTypes.Email, usuario.Correo),
                    new Claim("IdPersona", usuario.IdPersona.ToString())
                };

                var identity = new ClaimsIdentity(claims, "jwt");
                var principal = new ClaimsPrincipal(identity);

                return new AuthenticationState(principal);
            }
            catch
            {
                return _anonymous;
            }
        }

        public async Task NotifyUserAuthentication(AuthResponseDTO authData)
        {
            var usuario = authData.Usuario;
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, $"{usuario.Nombre} {usuario.Apellido}"),
                new Claim(ClaimTypes.Email, usuario.Correo),
                new Claim("IdPersona", usuario.IdPersona.ToString())
            };

            var identity = new ClaimsIdentity(claims, "jwt");
            var principal = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
        }

        public Task NotifyUserLogout()
        {
            NotifyAuthenticationStateChanged(Task.FromResult(_anonymous));
            return Task.CompletedTask;
        }
    }
}