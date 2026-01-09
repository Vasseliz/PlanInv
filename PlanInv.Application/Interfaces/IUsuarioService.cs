using PlanInv.Application.Requests;
using PlanInv.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanInv.Application.Interfaces;

public interface IUsuarioService
{
    Task<UsuarioDto> CreateUsuarioAsync(CreateUsuarioRequest request); 
    Task<UsuarioResponseDto?> GetByIdAsync(int id);
    Task<UsuarioDto?> UpdateUsuarioAsync(int id, UpdateUsuarioRequest request); 
    Task<UsuarioDto?> DesativarUsuarioAsync(int id);  
    Task<UsuarioDto?> AtivarUsuarioAsync(int id);     

}
