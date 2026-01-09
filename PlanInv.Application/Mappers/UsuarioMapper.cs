using PlanInv.Application.Dtos;
using PlanInv.Domain.Entities;

namespace PlanInv.Application.Mappers;

public static class UsuarioMapper
{
    public static UsuarioDto ToDto(Usuario usuario)
    {
        return new UsuarioDto
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Idade = usuario.Idade,
            Cpf = usuario.Cpf.Numero,
            MetaDeAportesMensal = usuario.MetaDeAportesMensal,
            UsuarioAtivo = usuario.UsuarioAtivo,
            CreatedAt = usuario.CreatedAt,
            UpdatedAt = usuario.UpdatedAt,
        };
    }

    public static UsuarioResponseDto ToResponseDto(Usuario usuario)
    {
        return new UsuarioResponseDto
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Idade = usuario.Idade,
            Cpf = usuario.Cpf.Numero,
            MetaDeAportesMensal = usuario.MetaDeAportesMensal,
            CreatedAt = usuario.CreatedAt,
            UpdatedAt = usuario.UpdatedAt,
            UsuarioAtivo = usuario.UsuarioAtivo,
            Posicoes = usuario.Posicoes.Select(PosicaoMapper.ToDto).ToList()
        };
    }
}