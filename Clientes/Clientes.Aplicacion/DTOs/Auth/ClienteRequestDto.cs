namespace Clientes.Aplicacion.DTOs.Clientes
{
    public class ClienteRequestDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string CorreoElectronico { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public bool Estatus { get; set; }
    }
}