using Microsoft.AspNetCore.Mvc;
using PlanInv.Application.Dtos;
using PlanInv.Application.Interfaces;
using PlanInv.Application.Requests;

namespace PlanInv.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AtivosController : ControllerBase
{
    private readonly IAtivoService _service;

    public AtivosController(IAtivoService ativoService)
    {
        _service = ativoService;
    }

    [HttpPost]
    public async Task<ActionResult<AtivoDto>> CreateAtivo(CreateAtivoRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var ativoDto = await _service.CreateAtivoAsync(request);
        return CreatedAtAction(nameof(GetAtivoById), new { id = ativoDto.Id }, ativoDto);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AtivoResponseDto>> GetAtivoById(int id)
    {
        var ativo = await _service.GetByIdAsync(id);

        if (ativo == null)
            return NotFound(new { error = $"Ativo com ID {id} não encontrado" });

        return Ok(ativo);
    }

    [HttpGet("ticker/{ticker}")]
    public async Task<ActionResult<AtivoDto>> GetAtivoByTicker(string ticker)
    {
        var ativo = await _service.GetByTickerAsync(ticker);

        if (ativo == null)
            return NotFound(new { error = $"Ativo com ticker {ticker} não encontrado" });

        return Ok(ativo);
    }

    [HttpGet]
    public async Task<ActionResult<List<AtivoDto>>> GetAllAtivos()
    {
        var ativos = await _service.GetAllAsync();
        return Ok(ativos);
    }

    [HttpPatch("{id:int}")]
    public async Task<ActionResult<AtivoDto>> UpdateAtivo(int id, UpdateAtivoRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!request.TemAlgumCampo())
            return BadRequest(new { error = "Nenhum campo para atualizar" });

        var ativoDto = await _service.UpdateAtivoAsync(id, request);

        if (ativoDto == null)
            return NotFound(new { error = $"Ativo com ID {id} não encontrado" });

        return Ok(ativoDto);
    }
}