using Clientes.Dominio.Entities;

namespace Clientes.Dominio.Interfaces
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> ObtenerTodosAsync();
        Task<Cliente?> ObtenerPorIdAsync(int id);
        Task<int> InsertarAsync(Cliente cliente);
        Task<int> ActualizarAsync(Cliente cliente);
        Task<int> EliminarAsync(int id);
    }
}