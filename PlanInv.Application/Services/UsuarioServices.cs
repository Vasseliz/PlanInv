using PlanInv.Application.Requests;
using PlanInv.Application.Dtos;
using PlanInv.Application.Interfaces;
using PlanInv.Application.Mappers; 
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

        return UsuarioMapper.ToDto(usuario); 
    }

    public async Task<UsuarioResponseDto?> GetByIdAsync(int id)
    {
        var usuario = await _repository.GetByIdWithPosicoesAsync(id);

        if (usuario == null)
            return null;

        return UsuarioMapper.ToResponseDto(usuario); 
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

        return UsuarioMapper.ToDto(usuario); 
    }

    public async Task<UsuarioDto?> DesativarUsuarioAsync(int id)
    {
        var usuario = await _repository.GetByIdAsync(id);

        if (usuario == null)
            return null;

        usuario.Desativar();
        await _repository.UpdateAsync(usuario);

        return UsuarioMapper.ToDto(usuario); 
    }

    public async Task<UsuarioDto?> AtivarUsuarioAsync(int id)
    {
        var usuario = await _repository.GetByIdIncludingInativosAsync(id);

        if (usuario == null)
            return null;

        if (usuario.UsuarioAtivo)
            return UsuarioMapper.ToDto(usuario); 

        usuario.Ativar();
        await _repository.UpdateAsync(usuario);

        return UsuarioMapper.ToDto(usuario); 
    }

}