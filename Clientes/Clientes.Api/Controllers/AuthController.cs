using Clientes.Aplicacion.DTOs.Auth;
using Clientes.Aplicacion.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clientes.Api.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var response = await _authService.LoginAsync(request.NombreUsuario, request.Contrasena);

            if (response is null)
            {
                return Unauthorized(new { mensaje = "Credenciales incorrectas." });
            }

            return Ok(response);
        }
    }
}