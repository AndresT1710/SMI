using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using SMI.Client.Services;
using SMI.Shared.DTOs;
using SMI.Shared.Interfaces;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;
    private readonly AuthStateProvider _authStateProvider;
    private const string AuthDataKey = "authData";

    public AuthService(
        HttpClient httpClient,
        ILocalStorageService localStorage,
        AuthStateProvider authStateProvider)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
        _authStateProvider = authStateProvider;
    }

    public async Task<UsuarioDTO> Login(LoginDTO loginDto)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", loginDto);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Login fallido: {response.StatusCode}");

            var authResponse = await response.Content.ReadFromJsonAsync<AuthResponseDTO>();

            if (authResponse == null || authResponse.Usuario == null || string.IsNullOrEmpty(authResponse.Token))
                throw new Exception("Respuesta inválida del servidor");

            // Guardar authData completo
            await _localStorage.SetItemAsync(AuthDataKey, authResponse);

            // Establecer el token en el HttpClient
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResponse.Token);

            // Notificar al AuthStateProvider
            await _authStateProvider.NotifyUserAuthentication(authResponse);

            return authResponse.Usuario;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error en Login: {ex.Message}");
            throw;
        }
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync(AuthDataKey);
        _httpClient.DefaultRequestHeaders.Authorization = null;
        await _authStateProvider.NotifyUserLogout();
    }

    public async Task<bool> IsAuthenticated()
    {
        var authData = await _localStorage.GetItemAsync<AuthResponseDTO>(AuthDataKey);
        return authData != null && !string.IsNullOrWhiteSpace(authData.Token);
    }

    public async Task<UsuarioDTO?> GetCurrentUser()
    {
        var authData = await _localStorage.GetItemAsync<AuthResponseDTO>(AuthDataKey);
        return authData?.Usuario;
    }

    public async Task<bool> TryRefreshTokenAsync()
    {
        var authData = await _localStorage.GetItemAsync<AuthResponseDTO>(AuthDataKey);

        if (authData == null || string.IsNullOrWhiteSpace(authData.Token) || string.IsNullOrWhiteSpace(authData.RefreshToken))
            return false;

        var refreshRequest = new RefreshTokenDTO
        {
            Token = authData.Token,
            RefreshToken = authData.RefreshToken
        };

        var response = await _httpClient.PostAsJsonAsync("api/auth/refresh-token", refreshRequest);

        if (response.IsSuccessStatusCode)
        {
            var newAuthData = await response.Content.ReadFromJsonAsync<AuthResponseDTO>();

            if (newAuthData != null && !string.IsNullOrWhiteSpace(newAuthData.Token))
            {
                await _localStorage.SetItemAsync(AuthDataKey, newAuthData);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", newAuthData.Token);
                await _authStateProvider.NotifyUserAuthentication(newAuthData);
                return true;
            }
        }

        await Logout();
        return false;
    }
}
