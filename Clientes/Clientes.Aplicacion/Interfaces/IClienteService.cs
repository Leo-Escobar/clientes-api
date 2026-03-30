using Clientes.Dominio.Entities;

namespace Clientes.Aplicacion.Interfaces
{
    public interface IClienteService
    {
        Task<IEnumerable<Cliente>> ObtenerTodosAsync();
        Task<Cliente?> ObtenerPorIdAsync(int id);
        Task<int> InsertarAsync(Cliente cliente);
        Task<int> ActualizarAsync(Cliente cliente);
        Task<int> EliminarAsync(int id);
    }
}