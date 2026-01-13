using PlanInv.Application.Dtos;
using PlanInv.Application.Requests;

namespace PlanInv.Application.Interfaces;

public interface IAtivoService
{
    Task<AtivoDto> CreateAtivoAsync(CreateAtivoRequest request);
    Task<AtivoResponseDto?> GetByIdAsync(int id);
    Task<AtivoDto?> GetByTickerAsync(string ticker);
    Task<List<AtivoDto>> GetAllAsync();
    Task<AtivoDto?> UpdateAtivoAsync(int id, UpdateAtivoRequest request);
}