// En SMI.Client/Services/AuthService.cs
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using SMI.Shared.DTOs;
using SMI.Shared.Interfaces;
using System.Net.Http.Json;

namespace SMI.Client.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthStateProvider _authStateProvider;
        private const string UserKey = "currentUser";

        public AuthService(HttpClient httpClient, ILocalStorageService localStorage, AuthenticationStateProvider authStateProvider)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _authStateProvider = (AuthStateProvider)authStateProvider;
        }

        public async Task<UsuarioDTO?> Login(LoginDTO loginDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/auth/login", loginDto);

                if (response.IsSuccessStatusCode)
                {
                    var authResponse = await response.Content.ReadFromJsonAsync<AuthResponseDTO>();

                    if (authResponse?.Usuario != null)
                    {
                        await _localStorage.SetItemAsync("authToken", authResponse.Token);
                        await _localStorage.SetItemAsync("refreshToken", authResponse.RefreshToken);
                        await _localStorage.SetItemAsync(UserKey, authResponse.Usuario);

                        // Cambiar a pasar el AuthResponseDTO completo
                        await _authStateProvider.NotifyUserAuthentication(authResponse); // Aquí pasa el AuthResponseDTO

                        return authResponse.Usuario;
                    }

                }

                return null;
            }
            catch
            {
                return null;
            }
        }


        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            await _localStorage.RemoveItemAsync("refreshToken");
            await _localStorage.RemoveItemAsync(UserKey);
            _authStateProvider.NotifyUserLogout();
        }


        public async Task<bool> IsAuthenticated()
        {
            var user = await _localStorage.GetItemAsync<UsuarioDTO>(UserKey);
            return user != null;
        }

        public async Task<UsuarioDTO?> GetCurrentUser()
        {
            return await _localStorage.GetItemAsync<UsuarioDTO>(UserKey);
        }

        public async Task<bool> TryRefreshTokenAsync()
        {
            // Implementación para refrescar el token si estás usando JWT
            // Por ahora, simplemente devolvemos true si el usuario está autenticado
            return await IsAuthenticated();
        }
    }
}