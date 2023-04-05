using AltaBancaApi.Models.Models;
using AltaBancaApi.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AltaBancaApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class UsuarioController : ControllerBase
    {
        IUsuariosService _usuarioService;
        public UsuarioController(IUsuariosService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllAsync()
        {
            var usuarios = await _usuarioService.GetUsuarios();

            if (usuarios == null)
            {
                return NotFound();
            }
            return Ok(usuarios);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var usuario = await _usuarioService.GetUsuario(id);

            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }
        [HttpGet("{nombre}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SearchAsync(string nombre)
        {
            var usuarios = await _usuarioService.GetUsuariosPorNombre(nombre);

            if (usuarios == null)
                return NotFound();

            return Ok(usuarios);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> InsertAsync([FromBody] Usuario usuario)
        {
            if (_usuarioService.ValidarModelo(usuario))
            {
                var usuarioCreado = await _usuarioService.CreateUsuario(usuario);
                
                return CreatedAtAction("Get", new { id = usuarioCreado.Id }, usuarioCreado);
            }
            return BadRequest();
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] Usuario usuario)
        {
            if (_usuarioService.ValidarModelo(usuario))
            {
                await _usuarioService.UpdateUsuario(id, usuario);

                return CreatedAtAction("Get", new { id = usuario.Id }, usuario);
            }
            return BadRequest();
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _usuarioService.DeleteUsuario(id);
            return Ok();
        }
    }
}
