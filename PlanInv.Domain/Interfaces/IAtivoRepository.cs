using PlanInv.Domain.Entities;

namespace PlanInv.Domain.Interfaces;

public interface IAtivoRepository
{
    Task<Ativo> AddAsync(Ativo ativo);
    Task<Ativo?> GetByIdAsync(int id);
    Task<Ativo?> GetByIdWithPosicoesAsync(int id);
    Task<Ativo?> GetByTickerAsync(string ticker);
    Task<List<Ativo>> GetAllAsync();
    Task UpdateAsync(Ativo ativo);
    Task<bool> ExistsByTickerAsync(string ticker);
    Task<bool> ExistsByCnpjAsync(string cnpj);
    Task<bool> ExistsAsync(int id);
}