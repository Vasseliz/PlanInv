using PlanInv.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace PlanInv.Api.Requests;

public class CreateUsuarioRequest
{
    [Required(ErrorMessage = "Nome é obrigatório")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "CPF é obrigatório")]
    public string Cpf { get; set; }

    [Required(ErrorMessage = "Meta de aporte mensal é obrigatória")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Meta deve ser maior que zero" )]
    public decimal MetaAporteMensal { get; set; }

    [Required(ErrorMessage = "Idade é obrigatório")]
    public int Idade { get; set; }

}



