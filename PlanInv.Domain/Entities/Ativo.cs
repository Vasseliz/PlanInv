using PlanInv.Domain.Enums;
using PlanInv.Domain.Exceptions;
using PlanInv.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanInv.Domain.Entities;

public class Ativo : BaseEntity
{
    public string Ticker { get; private set; }
    public ETipoAtivo Tipo { get; private set; }
    public Cnpj Cnpj { get; private set; }
    public decimal CotacaoAtual { get; private set; }

    private readonly List<Posicao> _posicoes = new();
    public IReadOnlyCollection<Posicao> Posicoes => _posicoes.AsReadOnly();

    // referente ao EF core
    protected Ativo () {}
    public Ativo(string ticker, ETipoAtivo tipo, string cnpj, decimal cotacaoAtual)
    {
        Validar(ticker, cotacaoAtual);

        Ticker = ticker.ToUpper();
        Tipo = tipo;
        Cnpj = new Cnpj(cnpj);
        CotacaoAtual = CotacaoAtual;
    }

    public void AtualizarCotacao(decimal newCotacao)
    {
        if (newCotacao <= 0)
            throw new DomainException("A cotação deve ser maior que 0.");
        CotacaoAtual = newCotacao;
    }
    private void Validar(string ticker, decimal cotacaoAtual)
    {
        if (string.IsNullOrWhiteSpace(ticker))
            throw new DomainException("O Ticker é obrigatório.");

        if (cotacaoAtual <= 0)
            throw new DomainException("A cotação inicial deve ser maior que zero.");
    }

}

// ainda temos que  definir no db context a questao do CNPJ E ETC. Foi criado uma nova pasta de configurations dentro da PlanInv.Infrastructure