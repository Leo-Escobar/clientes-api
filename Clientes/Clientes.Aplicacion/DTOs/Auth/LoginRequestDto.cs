namespace Clientes.Aplicacion.DTOs.Auth
{
    public class LoginRequestDto
    {
        public string NombreUsuario { get; set; } = string.Empty;
        public string Contrasena { get; set; } = string.Empty;
    }
}