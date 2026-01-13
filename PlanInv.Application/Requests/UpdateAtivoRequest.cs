using System.ComponentModel.DataAnnotations;

namespace PlanInv.Application.Requests;

public class UpdateAtivoRequest
{
    [Range(0.01, double.MaxValue, ErrorMessage = "Cotação deve ser maior que zero")]
    public decimal? CotacaoAtual { get; set; }

    public bool TemAlgumCampo() => CotacaoAtual.HasValue;
}