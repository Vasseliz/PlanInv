using PlanInv.Application.Dtos;
using PlanInv.Domain.Entities;

namespace PlanInv.Application.Mappers;

public static class AtivoMapper
{
    public static AtivoDto ToDto(Ativo ativo)
    {
        return new AtivoDto
        {
            Id = ativo.Id,
            Ticker = ativo.Ticker,
            Tipo = ativo.Tipo.ToString(),
            Cnpj = ativo.Cnpj.Numero,
            CotacaoAtual = ativo.CotacaoAtual,
            CreatedAt = ativo.CreatedAt,
            UpdatedAt = ativo.UpdatedAt
        };
    }

    public static AtivoResponseDto ToResponseDto(Ativo ativo)
    {
        return new AtivoResponseDto
        {
            Id = ativo.Id,
            Ticker = ativo.Ticker,
            Tipo = ativo.Tipo.ToString(),
            Cnpj = ativo.Cnpj.Numero,
            CotacaoAtual = ativo.CotacaoAtual,
            CreatedAt = ativo.CreatedAt,
            UpdatedAt = ativo.UpdatedAt,
            Posicoes = ativo.Posicoes.Select(PosicaoMapper.ToDto).ToList()
        };
    }
}