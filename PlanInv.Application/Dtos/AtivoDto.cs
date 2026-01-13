using PlanInv.Domain.Enums;

namespace PlanInv.Application.Dtos;

public class AtivoDto
{
    public int Id { get; set; }
    public string Ticker { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public string Cnpj { get; set; } = string.Empty;
    public decimal CotacaoAtual { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}