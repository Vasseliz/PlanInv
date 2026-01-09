using Microsoft.AspNetCore.Mvc;
using PlanInv.Application.Requests;
using PlanInv.Application.Dtos;
using PlanInv.Application.Interfaces;

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
        public async Task<ActionResult<UsuarioDto>> CreateUsuario(CreateUsuarioRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuarioDto = await _service.CreateUsuarioAsync(request);

            return CreatedAtAction(nameof(GetUsuarioById), new { id = usuarioDto.Id }, usuarioDto);
        }

        [HttpGet("{id:int}")]  
        public async Task<ActionResult<UsuarioResponseDto>> GetUsuarioById(int id)
        {
            var usuario = await _service.GetByIdAsync(id);
            
            if (usuario == null)
                return NotFound(new { error = $"Usuário com ID {id} não encontrado" });

            return Ok(usuario);
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult<UsuarioDto>> UpdateUsuario(
    int id, UpdateUsuarioRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!request.TemAlgumCampo())
                return BadRequest(new { error = "Nenhum campo para atualizar" });

            var usuarioDto = await _service.UpdateUsuarioAsync(id, request);

            if (usuarioDto == null)
                return NotFound(new { error = $"Usuário com ID {id} não encontrado" });

            return Ok(usuarioDto);
        }

        [HttpPatch("{id:int}/desativar")]
        public async Task<ActionResult<UsuarioDto>> DesativarUsuario(int id)
        {
            var usuarioDto = await _service.DesativarUsuarioAsync(id);

            if (usuarioDto == null)
                return NotFound(new { error = $"Usuário com ID {id} não encontrado" });

            return Ok(usuarioDto);
        }


        [HttpPatch("{id:int}/ativar")]
        public async Task<ActionResult<UsuarioDto>> AtivarUsuario(int id)
        {
            var usuarioDto = await _service.AtivarUsuarioAsync(id);

            if (usuarioDto == null)
                return NotFound(new { error = $"Usuário com ID {id} não encontrado" });

            return Ok(usuarioDto);
        }
    }
}