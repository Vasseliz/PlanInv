using PlanInv.Domain.Entities;

namespace PlanInv.Domain.Interfaces;

public interface IUsuarioRepository
{
    Task<Usuario> AddAsync(Usuario usuario);
    Task<Usuario?> GetByIdIncludingInativosAsync(int id);
    Task<Usuario?> GetByIdAsync(int id);
    Task<Usuario?> GetByIdWithPosicoesAsync(int id);
    Task<Usuario?> GetByCpfAsync(string cpf);
    Task<List<Usuario>> GetAllAsync();
    Task UpdateAsync(Usuario usuario);
    Task<bool> ExistsByCpfAsync(string cpf);
    Task<bool> ExistsAsync(int id);
}