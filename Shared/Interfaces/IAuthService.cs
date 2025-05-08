using SMI.Shared.DTOs;
using System.Threading.Tasks;

namespace SMI.Shared.Interfaces
{
    public interface IAuthService
    {
        Task<UsuarioDTO?> Login(LoginDTO login);
        Task Logout();
        Task<bool> IsAuthenticated();
        Task<UsuarioDTO?> GetCurrentUser();
        Task<bool> TryRefreshTokenAsync();
    }
}