using Clientes.Aplicacion.Interfaces;
using Clientes.Dominio.Entities;
using Clientes.Dominio.Interfaces;

namespace Clientes.Aplicacion.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<IEnumerable<Cliente>> ObtenerTodosAsync()
        {
            return await _clienteRepository.ObtenerTodosAsync();
        }

        public async Task<Cliente?> ObtenerPorIdAsync(int id)
        {
            return await _clienteRepository.ObtenerPorIdAsync(id);
        }

        public async Task<int> InsertarAsync(Cliente cliente)
        {
            return await _clienteRepository.InsertarAsync(cliente);
        }

        public async Task<int> ActualizarAsync(Cliente cliente)
        {
            return await _clienteRepository.ActualizarAsync(cliente);
        }

        public async Task<int> EliminarAsync(int id)
        {
            return await _clienteRepository.EliminarAsync(id);
        }
    }
}