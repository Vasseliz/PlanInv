using PlanInv.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PlanInv.Domain.ValueObjects; //cpf
namespace PlanInv.Domain.Entities;

public class Usuario : BaseEntity
{
    public string Nome { get; private set; }
    public int Idade { get; private set; }
    public Cpf Cpf { get; private set; }
    public decimal MetaDeAportesMensal { get; private set; }

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
        if (newMeta <= 0)
            throw new DomainException("A meta de aporte não pode ser igual a 0 ou menor que 0.");
        MetaDeAportesMensal = newMeta;
    }

    public void CorrigirNome(string novoNome)
    {
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


// ainda temos que  definir no db context a questao do CPF. Foi criado uma nova pasta de configurations dentro da PlanInv.Infrastructure