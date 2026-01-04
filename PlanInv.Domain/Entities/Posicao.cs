using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanInv.Domain.Entities;

public class Posicao
{
    public int UsuarioId { get; set; }
    public int AtivoId { get; set; }
    public int Quantidade { get; set; }
    public decimal PrecoMedio { get; set; }
    public decimal ValorInvestido { get; set; }
    public DateTime DataPrimeiraCompra { get; set; }
    public bool Ativa { get; set;  }
}