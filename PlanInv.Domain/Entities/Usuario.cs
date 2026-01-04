using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanInv.Domain.Entities;

internal class Usuario : BaseEntity
{
    public string Nome { get; set; }
    public int Idade { get; set; }
    public string Cpf { get; set; }
    public decimal MetaDeAportesMensal { get; set; }
}
