using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanInv.Domain.Entities;

internal class Ativo
{
    public string Ticker { get; set; }
    public ETipoAtivo Tipo { get; private set; }
    public ESetor Setor { get; set; }
    public string Cnpj { get; set; }
    public decimal CotacaoAtual{ get; set; }

}
