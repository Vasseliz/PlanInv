using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanInv.Application.Dtos;

public class UsuarioDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int Idade{ get; set; }
    public string Cpf { get; set; }
    public decimal MetaDeAportesMensal { get; set; }
}
