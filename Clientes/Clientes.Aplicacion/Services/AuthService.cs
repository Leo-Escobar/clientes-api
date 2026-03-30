using Clientes.Aplicacion.DTOs.Auth;
using Clientes.Aplicacion.Interfaces;
using Clientes.Dominio.Interfaces;

namespace Clientes.Aplicacion.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ITokenService _tokenService;
        private readonly IPasswordHashService _passwordHashService;

        public AuthService(
            IUsuarioRepository usuarioRepository,
            ITokenService tokenService,
            IPasswordHashService passwordHashService)
        {
            _usuarioRepository = usuarioRepository;
            _tokenService = tokenService;
            _passwordHashService = passwordHashService;
        }

        public async Task<LoginResponseDto?> LoginAsync(string nombreUsuario, string contrasena)
        {
            var usuario = await _usuarioRepository.ObtenerPorNombreUsuarioAsync(nombreUsuario);

            if (usuario is null)
            {
                return null;
            }

            var passwordValido = _passwordHashService.VerifyPassword(contrasena, usuario.Contrasena);

            if (!passwordValido)
            {
                return null;
            }

            var token = _tokenService.GenerarToken(usuario);

            return new LoginResponseDto
            {
                Id = usuario.Id,
                NombreUsuario = usuario.NombreUsuario,
                Token = token
            };
        }
    }
}