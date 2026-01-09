using PlanInv.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PlanInv.Domain.ValueObjects; 
namespace PlanInv.Domain.Entities;

public class Usuario : BaseEntity
{
    public string Nome { get; private set; }
    public int Idade { get; private set; }
    public Cpf Cpf { get; private set; }
    public decimal MetaDeAportesMensal { get; private set; }

    private readonly List<Posicao> _posicoes = new();

    public IReadOnlyCollection<Posicao> Posicoes => _posicoes.AsReadOnly();

    public bool UsuarioAtivo { get; private set; } = true;
    public void Desativar()
    {
        if (!UsuarioAtivo)
            throw new InvalidOperationException("Usuário já está desativado");

        UsuarioAtivo = false;
    }

    public void Ativar()
    {
        if (UsuarioAtivo)
            throw new InvalidOperationException("Usuário já está ativo");

        UsuarioAtivo = true;
    }

    // referente ao EF core
    protected Usuario() { }

    public Usuario (string nome, int idade, string cpf, decimal metaDeAportesMensal)
    {
        Validar(nome, idade, metaDeAportesMensal);

        Nome = nome;
        Idade = idade;
        Cpf = new Cpf(cpf);
        MetaDeAportesMensal = metaDeAportesMensal;
    }

    public void AtualizarMetaAporte(decimal newMeta)
    {
        if (!UsuarioAtivo)
            throw new DomainException("Não é possível atualizar usuário desativado");

        if (newMeta <= 0)
            throw new DomainException("A meta de aporte não pode ser igual a 0 ou menor que 0.");
        MetaDeAportesMensal = newMeta;
    }

    public void CorrigirNome(string novoNome)
    {
        if (!UsuarioAtivo)
            throw new DomainException("Não é possível atualizar usuário desativado");

        if (string.IsNullOrWhiteSpace(novoNome))
            throw new DomainException("O Nome não pode ser vazio.");

        Nome = novoNome;
    }
    private void Validar(string nome, int idade, decimal metaDeAportesMensal)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new DomainException("Nome deve ser incluso.");

        if (idade < 18)
            throw new DomainException("O usuário deve ter idade superior a 18 anos.");


        if (metaDeAportesMensal <= 0)
            throw new DomainException("Meta de aporte mensal não pode ser menor que 0 ou igual a 0.");
    }


}

