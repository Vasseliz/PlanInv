using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanInv.Application.Dtos;

public class UsuarioResponseDto

{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int Idade { get; set; }
    public string Cpf { get; set; }
    public decimal MetaDeAportesMensal { get; set; }

    public DateTime CreatedAt { get; set; }

    public List<PosicaoDto> Posicoes  { get; set; }

    public decimal TotalInvestido => Posicoes?.Sum(p => p.ValorTotal) ?? 0;
    public int QuantidadePosices => Posicoes?.Count ?? 0;


}
