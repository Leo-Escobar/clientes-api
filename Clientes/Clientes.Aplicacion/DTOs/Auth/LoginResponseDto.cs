namespace Clientes.Aplicacion.DTOs.Auth
{
    public class LoginResponseDto
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;

    }
}