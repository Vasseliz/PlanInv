using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanInv.Application.Dtos;

public class PosicaoDto
{
    public int Id { get; set; }
    public string Ticker { get; set; }  
    public int Quantidade { get; set; }
    public decimal PrecoMedio { get; set; }
    public decimal ValorTotal { get; set; }
    public DateTime DataCompra { get; set; }
    public string TipoAtivo { get; set; } 
}