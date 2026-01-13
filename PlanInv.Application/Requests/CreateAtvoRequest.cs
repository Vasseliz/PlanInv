using PlanInv.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PlanInv.Application.Requests;

public class CreateAtivoRequest
{
    [Required(ErrorMessage = "Ticker é obrigatório")]
    [StringLength(10, MinimumLength = 4, ErrorMessage = "Ticker deve ter entre 4 e 10 caracteres")]
    public required string Ticker { get; set; }

    [Required(ErrorMessage = "Tipo do ativo é obrigatório")]
    public ETipoAtivo Tipo { get; set; }

    [Required(ErrorMessage = "CNPJ é obrigatório")]
    public required string Cnpj { get; set; }

    [Required(ErrorMessage = "Cotação atual é obrigatória")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Cotação deve ser maior que zero")]
    public decimal CotacaoAtual { get; set; }
}