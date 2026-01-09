using Microsoft.EntityFrameworkCore;
using PlanInv.Domain.Entities;
using PlanInv.Domain.Interfaces;
using PlanInv.Infrastructure.Data;
using System;

namespace PlanInv.Infrastructure.Repositories;

public class UsuarioRepository(ApplicationDbContext context) : IUsuarioRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Usuario> AddAsync(Usuario usuario)
    {
        await _context.Usuarios.AddAsync(usuario);
        await _context.SaveChangesAsync();
        return usuario;
    }

    public async Task<Usuario?> GetByIdAsync(int id)
    {
        return await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<Usuario?> GetByIdWithPosicoesAsync(int id)
    {
        return await _context.Usuarios
            .Include(u => u.Posicoes)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<Usuario?> GetByIdIncludingInativosAsync(int id)
    {
        return await _context.Usuarios
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<Usuario?> GetByCpfAsync(string cpf)
    {
        return await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Cpf.Numero == cpf);
    }

    public async Task<List<Usuario>> GetAllAsync()
    {
        return await _context.Usuarios
            .Include(u => u.Posicoes)
            .ToListAsync();
    }

    public async Task UpdateAsync(Usuario usuario)
    {
        _context.Usuarios.Update(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsByCpfAsync(string cpf)
    {
        return await _context.Usuarios
            .AnyAsync(u => u.Cpf.Numero == cpf);
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Usuarios
            .AnyAsync(u => u.Id == id);
    }
}
