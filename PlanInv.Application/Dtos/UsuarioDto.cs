using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanInv.Application.Dtos;

public class UsuarioDto
{
    public int Id { get; set; }
    public required string Nome { get; set; }
    public int Idade{ get; set; }
    public required string Cpf { get; set; }
    public decimal MetaDeAportesMensal { get; set; }
    public bool UsuarioAtivo { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
