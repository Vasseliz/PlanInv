using Microsoft.EntityFrameworkCore;
using PlanInv.Domain.Entities;
using PlanInv.Domain.Interfaces;
using PlanInv.Infrastructure.Data;

namespace PlanInv.Infrastructure.Repositories;

public class AtivoRepository(ApplicationDbContext context) : IAtivoRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Ativo> AddAsync(Ativo ativo)
    {
        await _context.Ativos.AddAsync(ativo);
        await _context.SaveChangesAsync();
        return ativo;
    }

    public async Task<Ativo?> GetByIdAsync(int id)
    {
        return await _context.Ativos
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Ativo?> GetByIdWithPosicoesAsync(int id)
    {
        return await _context.Ativos
            .Include(a => a.Posicoes)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Ativo?> GetByTickerAsync(string ticker)
    {
        return await _context.Ativos
            .FirstOrDefaultAsync(a => a.Ticker == ticker.ToUpper());
    }

    public async Task<List<Ativo>> GetAllAsync()
    {
        return await _context.Ativos
            .ToListAsync();
    }

    public async Task UpdateAsync(Ativo ativo)
    {
        _context.Ativos.Update(ativo);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsByTickerAsync(string ticker)
    {
        return await _context.Ativos
            .AnyAsync(a => a.Ticker == ticker.ToUpper());
    }

    public async Task<bool> ExistsByCnpjAsync(string cnpj)
    {
        return await _context.Ativos
            .AnyAsync(a => a.Cnpj.Numero == cnpj);
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Ativos
            .AnyAsync(a => a.Id == id);
    }
}