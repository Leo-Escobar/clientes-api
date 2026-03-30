using Clientes.Dominio.Entities;
using Clientes.Dominio.Interfaces;
using Clientes.Infraestructura.Data;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Clientes.Infraestructura.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly SqlConnectionFactory _connectionFactory;

        public ClienteRepository(SqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<Cliente>> ObtenerTodosAsync()
        {
            var clientes = new List<Cliente>();

            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand("dbo.usp_Clientes_ObtenerTodos", connection);

            command.CommandType = CommandType.StoredProcedure;

            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                clientes.Add(new Cliente
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Nombre = reader["Nombre"].ToString() ?? string.Empty,
                    CorreoElectronico = reader["CorreoElectronico"].ToString() ?? string.Empty,
                    Telefono = reader["Telefono"].ToString() ?? string.Empty,
                    Estatus = Convert.ToBoolean(reader["Estatus"])
                });
            }

            return clientes;
        }

        public async Task<Cliente?> ObtenerPorIdAsync(int id)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand("dbo.usp_Clientes_ObtenerPorId", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new Cliente
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Nombre = reader["Nombre"].ToString() ?? string.Empty,
                    CorreoElectronico = reader["CorreoElectronico"].ToString() ?? string.Empty,
                    Telefono = reader["Telefono"].ToString() ?? string.Empty,
                    Estatus = Convert.ToBoolean(reader["Estatus"])
                };
            }

            return null;
        }

        public async Task<int> InsertarAsync(Cliente cliente)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand("dbo.usp_Clientes_Insertar", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Nombre", cliente.Nombre);
            command.Parameters.AddWithValue("@CorreoElectronico", cliente.CorreoElectronico);
            command.Parameters.AddWithValue("@Telefono", cliente.Telefono);
            command.Parameters.AddWithValue("@Estatus", cliente.Estatus);

            await connection.OpenAsync();

            var result = await command.ExecuteScalarAsync();

            return Convert.ToInt32(result);
        }

        public async Task<int> ActualizarAsync(Cliente cliente)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand("dbo.usp_Clientes_Actualizar", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", cliente.Id);
            command.Parameters.AddWithValue("@Nombre", cliente.Nombre);
            command.Parameters.AddWithValue("@CorreoElectronico", cliente.CorreoElectronico);
            command.Parameters.AddWithValue("@Telefono", cliente.Telefono);
            command.Parameters.AddWithValue("@Estatus", cliente.Estatus);

            await connection.OpenAsync();

            var result = await command.ExecuteScalarAsync();

            return Convert.ToInt32(result);
        }

        public async Task<int> EliminarAsync(int id)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand("dbo.usp_Clientes_Eliminar", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            await connection.OpenAsync();

            var result = await command.ExecuteScalarAsync();

            return Convert.ToInt32(result);
        }
    }
}