using Clientes.Dominio.Entities;
using Clientes.Dominio.Interfaces;
using Clientes.Infraestructura.Data;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Clientes.Infraestructura.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly SqlConnectionFactory _connectionFactory;

        public UsuarioRepository(SqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<Usuario?> ObtenerPorNombreUsuarioAsync(string nombreUsuario)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand("dbo.usp_Auth_Login", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);

            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new Usuario
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    NombreUsuario = reader["NombreUsuario"].ToString() ?? string.Empty,
                    Contrasena = reader["Contrasena"].ToString() ?? string.Empty
                };
            }

            return null;
        }
    }
}