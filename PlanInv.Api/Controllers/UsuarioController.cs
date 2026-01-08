using Microsoft.AspNetCore.Mvc;
using PlanInv.Api.Requests;
using PlanInv.Application.Dtos;

namespace PlanInv.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]  
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _service = usuarioService;
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioDto>> CreateUsuario([FromBody] CreateUsuarioRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuarioDto = await _service.CreateUsuarioAsync(request.Nome, request.Idade, request.Cpf, 
                request.MetaDeAportesMensal);

            return CreatedAtAction(nameof(GetUsuarioById), new { id = usuarioDto.Id }, usuarioDto);
        }

        [HttpGet("{id:int}")]  
        public async Task<ActionResult<UsuarioDto>> GetUsuarioById(int id)
        {
            var usuario = await _service.GetByIdAsync(id);
            
            if (usuario == null)
                return NotFound(new { error = $"Usuário com ID {id} não encontrado" });

            return Ok(usuario);
        }
    }
}