using PlanInv.Application.Requests;
using PlanInv.Application.Dtos;
using PlanInv.Application.Interfaces;
using PlanInv.Domain.Entities;
using PlanInv.Domain.Interfaces;

namespace PlanInv.Application.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _repository;

    public UsuarioService(IUsuarioRepository repository)
    {
        _repository = repository;
    }

    public async Task<UsuarioDto> CreateUsuarioAsync(CreateUsuarioRequest request)
    {
        var usuario = new Usuario(request.Nome,
            request.Idade,
            request.Cpf,
            request.MetaDeAportesMensal);
        await _repository.AddAsync(usuario);

        return MapToDto(usuario);
    }

    public async Task<UsuarioResponseDto?> GetByIdAsync(int id)
    {
        var usuario = await _repository.GetByIdWithPosicoesAsync(id);

        if (usuario == null)
            return null;

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
            Posicoes = usuario.Posicoes.Select(p => new PosicaoDto
            {
                Id = p.Id,
                Quantidade = p.Quantidade,
                PrecoMedio = p.PrecoMedio,
                ValorTotal = p.Quantidade * p.PrecoMedio,
                Ticker = p.Ativo.Ticker,
                TipoAtivo = p.Ativo.Tipo.GetType().Name 
            }).ToList()
        };
    }

    public async Task<UsuarioDto?> UpdateUsuarioAsync(int id, UpdateUsuarioRequest request)
    {
        var usuario = await _repository.GetByIdAsync(id);

        if (usuario == null)
            return null;

        if (request.Nome != null)
            usuario.CorrigirNome(request.Nome);

        if (request.MetaDeAportesMensal.HasValue)
            usuario.AtualizarMetaAporte(request.MetaDeAportesMensal.Value);

        await _repository.UpdateAsync(usuario);

        return MapToDto(usuario);
    }

    public async Task<UsuarioDto?> DesativarUsuarioAsync(int id)
    {
        var usuario = await _repository.GetByIdAsync(id);

        if (usuario == null)
            return null;

        usuario.Desativar();
        await _repository.UpdateAsync(usuario);

        return MapToDto(usuario);
    }



    public async Task<UsuarioDto?> AtivarUsuarioAsync(int id)
    {
        var usuario = await _repository.GetByIdIncludingInativosAsync(id);

        if (usuario == null)
            return null;

        if (usuario.UsuarioAtivo)
            return MapToDto(usuario);

        usuario.Ativar();
        await _repository.UpdateAsync(usuario);

        return MapToDto(usuario);
    }


    private static UsuarioDto MapToDto(Usuario usuario)
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
}