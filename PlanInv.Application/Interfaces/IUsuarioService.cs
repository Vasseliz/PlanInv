using PlanInv.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanInv.Application.Interfaces;

public interface IUsuarioService
{
    Task<UsuarioDto> CreateUsuarioAsync(string nome, int idade, string cpf, decimal metaDeAportesMensal);
    Task<UsuarioResponseDto> GetByIdAsync(int id);

}
