using Clientes.Aplicacion.DTOs.Auth;

namespace Clientes.Aplicacion.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto?> LoginAsync(string nombreUsuario, string contrasena);
    }
}