using PlanInv.Domain.Exceptions;
using System.Collections.Generic;

namespace PlanInv.Domain.Entities;

public class Corretora : BaseEntity
{
    public string Nome { get; private set; } = string.Empty;
    public bool Ativa { get; private set; }

    private readonly List<Transacao> _transacoes = new();
    public IReadOnlyCollection<Transacao> Transacoes => _transacoes.AsReadOnly();
    protected Corretora() { }

    public Corretora(string nome)
    {
        ValidarNome(nome);

        Nome = nome;
        Ativa = true;
    }

    public void AlterarNome(string novoNome)
    {
        ValidarNome(novoNome);
        Nome = novoNome;
    }


    public void Desativar()
    {
        Ativa = false;
    }

    public void Reativar()
    {
        Ativa = true;
    }
    private void ValidarNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new DomainException("Nome da corretora é obrigatório.");

        if (nome.Length < 2)
            throw new DomainException("Nome da corretora deve ter no mínimo 2 caracteres.");

        if (nome.Length > 100)
            throw new DomainException("Nome da corretora deve ter no máximo 100 caracteres.");
    }
}