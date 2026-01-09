using System.ComponentModel.DataAnnotations;

namespace PlanInv.Application.Requests;

public class UpdateUsuarioRequest
{
    [StringLength(200, MinimumLength = 3, ErrorMessage = "Nome deve ter entre 3 e 200 caracteres")]
    public string? Nome { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Meta deve ser maior que zero")]
    public decimal? MetaDeAportesMensal { get; set; }

    public bool TemAlgumCampo() =>
        Nome != null ||
        MetaDeAportesMensal.HasValue;
}
