using PlanInv.Application.Dtos;
using PlanInv.Application.Interfaces;
using PlanInv.Application.Mappers;
using PlanInv.Application.Requests;
using PlanInv.Domain.Entities;
using PlanInv.Domain.Interfaces;

namespace PlanInv.Application.Services;

public class AtivoService : IAtivoService
{
    private readonly IAtivoRepository _repository;

    public AtivoService(IAtivoRepository repository)
    {
        _repository = repository;
    }

    public async Task<AtivoDto> CreateAtivoAsync(CreateAtivoRequest request)
    {
        var ativo = new Ativo(
            request.Ticker,
            request.Tipo,
            request.Cnpj,
            request.CotacaoAtual
        );

        await _repository.AddAsync(ativo);
        return AtivoMapper.ToDto(ativo);
    }

    public async Task<AtivoResponseDto?> GetByIdAsync(int id)
    {
        var ativo = await _repository.GetByIdWithPosicoesAsync(id);

        if (ativo == null)
            return null;

        return AtivoMapper.ToResponseDto(ativo);
    }

    public async Task<AtivoDto?> GetByTickerAsync(string ticker)
    {
        var ativo = await _repository.GetByTickerAsync(ticker);

        if (ativo == null)
            return null;

        return AtivoMapper.ToDto(ativo);
    }

    public async Task<List<AtivoDto>> GetAllAsync()
    {
        var ativos = await _repository.GetAllAsync();
        return ativos.Select(AtivoMapper.ToDto).ToList();
    }

    public async Task<AtivoDto?> UpdateAtivoAsync(int id, UpdateAtivoRequest request)
    {
        var ativo = await _repository.GetByIdAsync(id);

        if (ativo == null)
            return null;

        if (request.CotacaoAtual.HasValue)
            ativo.AtualizarCotacao(request.CotacaoAtual.Value);

        await _repository.UpdateAsync(ativo);
        return AtivoMapper.ToDto(ativo);
    }
}