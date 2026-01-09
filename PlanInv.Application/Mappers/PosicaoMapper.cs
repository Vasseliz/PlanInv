using PlanInv.Application.Dtos;

namespace PlanInv.Application.Mappers;

internal class PosicaoMapper
{
    public static PosicaoDto ToDto(Domain.Entities.Posicao posicao)
    {
        return new PosicaoDto
        {
            Id = posicao.Id,
            Quantidade = posicao.Quantidade,
            PrecoMedio = posicao.PrecoMedio,
            ValorTotal = posicao.ValorAtual,
            Ticker = posicao.Ativo.Ticker,
            TipoAtivo = posicao.Ativo.Tipo.ToString()
        };
    }
}
