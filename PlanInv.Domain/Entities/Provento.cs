using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanInv.Domain.Entities;

internal class Provento
{
    public int PosicaoId { get; set; }
    public ETipoProvento Tipo { get; set; }
    public DateTime DataPagamento { get; set; }
    public decimal ValorBruto { get; set; }
    public decimal Imposto { get; set; }
    public decimal ValorLiquido { get; set; }
    public string Observacoes { get; set; }
}
