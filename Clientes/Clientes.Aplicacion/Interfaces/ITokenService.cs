using Clientes.Dominio.Entities;

namespace Clientes.Aplicacion.Interfaces
{
    public interface ITokenService
    {
        string GenerarToken(Usuario usuario);
    }
}