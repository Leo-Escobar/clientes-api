using Clientes.Aplicacion.DTOs.Clientes;
using Clientes.Aplicacion.Interfaces;
using Clientes.Dominio.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clientes.Api.Controllers
{
    [ApiController]
    [Route("clientes")]
    [Authorize]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var clientes = await _clienteService.ObtenerTodosAsync();

            var response = clientes.Select(c => new ClienteResponseDto
            {
                Id = c.Id,
                Nombre = c.Nombre,
                CorreoElectronico = c.CorreoElectronico,
                Telefono = c.Telefono,
                Estatus = c.Estatus
            });

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var cliente = await _clienteService.ObtenerPorIdAsync(id);

            if (cliente is null)
            {
                return NotFound(new { mensaje = "Cliente no encontrado." });
            }

            var response = new ClienteResponseDto
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                CorreoElectronico = cliente.CorreoElectronico,
                Telefono = cliente.Telefono,
                Estatus = cliente.Estatus
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Insertar([FromBody] ClienteRequestDto request)
        {
            var cliente = new Cliente
            {
                Nombre = request.Nombre,
                CorreoElectronico = request.CorreoElectronico,
                Telefono = request.Telefono,
                Estatus = request.Estatus
            };

            var idGenerado = await _clienteService.InsertarAsync(cliente);

            return Ok(new { Id = idGenerado, mensaje = "Cliente creado correctamente." });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ClienteRequestDto request)
        {
            var cliente = new Cliente
            {
                Id = id,
                Nombre = request.Nombre,
                CorreoElectronico = request.CorreoElectronico,
                Telefono = request.Telefono,
                Estatus = request.Estatus
            };

            var filasAfectadas = await _clienteService.ActualizarAsync(cliente);

            if (filasAfectadas == 0)
            {
                return NotFound(new { mensaje = "Cliente no encontrado." });
            }

            return Ok(new { mensaje = "Cliente actualizado correctamente." });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var filasAfectadas = await _clienteService.EliminarAsync(id);

            if (filasAfectadas == 0)
            {
                return NotFound(new { mensaje = "Cliente no encontrado." });
            }

            return Ok(new { mensaje = "Cliente eliminado correctamente." });
        }
    }
}