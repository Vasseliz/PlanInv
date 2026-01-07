using PlanInv.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlanInv.Domain.Entities;
public class Posicao : BaseEntity
{
    public int UsuarioId { get; private set; }
    public int AtivoId { get; private set; }

    public Usuario Usuario { get; private set; } = null!;
    public Ativo Ativo { get; private set; } = null!;


    private readonly List<Transacao> _transacoes = new();
    public IReadOnlyCollection<Transacao> Transacoes => _transacoes.AsReadOnly();

    private readonly List<Provento> _proventos = new();
    public IReadOnlyCollection<Provento> Proventos => _proventos.AsReadOnly();


    public int Quantidade { get; private set; }
    public decimal PrecoMedio { get; private set; }
    public decimal ValorInvestido { get; private set; }
    public DateTime DataPrimeiraCompra { get; private set; }
    public DateTime DataUltimaTransacao { get; private set; }
    public bool Ativa { get; private set; }


    public decimal ValorAtual => Quantidade * PrecoMedio;
    public bool PossuiCotas => Quantidade > 0;
    public int QuantidadeTransacoes => _transacoes.Count;
    public decimal TotalProventosRecebidos => _proventos.Sum(p => p.ValorLiquido);

    protected Posicao() { }


    public Posicao(
        int usuarioId,
        int ativoId,
        int quantidadeInicial,
        decimal precoInicial,
        DateTime? dataPrimeiraCompra = null)
    {
        ValidarCriacaoPosicao(quantidadeInicial, precoInicial);

        UsuarioId = usuarioId;
        AtivoId = ativoId;
        Quantidade = quantidadeInicial;
        PrecoMedio = precoInicial;
        ValorInvestido = quantidadeInicial * precoInicial;
        DataPrimeiraCompra = dataPrimeiraCompra ?? DateTime.UtcNow;
        DataUltimaTransacao = DataPrimeiraCompra;
        Ativa = true;
    }


    public void RegistrarCompra(int quantidade, decimal preco, DateTime dataTransacao)
    {
        if (!Ativa)
            throw new DomainException("Não é possível comprar cotas de uma posição inativa.");

        ValidarQuantidadePreco(quantidade, preco);

       
        var valorAtualInvestido = Quantidade * PrecoMedio;
        var novoValorInvestido = quantidade * preco;
        var novaQuantidadeTotal = Quantidade + quantidade;

        PrecoMedio = (valorAtualInvestido + novoValorInvestido) / novaQuantidadeTotal;
        Quantidade = novaQuantidadeTotal;
        ValorInvestido += novoValorInvestido;
        DataUltimaTransacao = dataTransacao;
    }


    public void RegistrarVenda(int quantidade, DateTime dataTransacao)
    {
        if (!Ativa)
            throw new DomainException("Não é possível vender cotas de uma posição inativa.");

        if (quantidade <= 0)
            throw new DomainException("Quantidade de venda deve ser maior que zero.");

        if (quantidade > Quantidade)
            throw new DomainException($"Quantidade insuficiente. Disponível: {Quantidade}, solicitado: {quantidade}");

        Quantidade -= quantidade;
        DataUltimaTransacao = dataTransacao;

        if (Quantidade == 0) 
        {
            Ativa = false;
        }
    }

    public void AdicionarTransacao(Transacao transacao)
    {
        if (transacao.PosicaoId != Id)
            throw new DomainException("Transação não pertence a esta posição.");

        _transacoes.Add(transacao);
    }

    public void RegistrarProvento(Provento provento)
    {
        if (provento.PosicaoId != Id)
            throw new DomainException("Provento não pertence a esta posição.");

        _proventos.Add(provento);
    }


    public void Reativar()
    {
        if (Ativa)
            throw new DomainException("Posição já está ativa.");

        Ativa = true;
    }

    public void Desativar()
    {
        if (!Ativa)
            throw new DomainException("Posição já está inativa.");

        Ativa = false;
    }

    private void ValidarCriacaoPosicao(int quantidade, decimal preco)
    {
        if (quantidade <= 0)
            throw new DomainException("Quantidade inicial deve ser maior que zero.");

        if (preco <= 0)
            throw new DomainException("Preço inicial deve ser maior que zero.");
    }

    private void ValidarQuantidadePreco(int quantidade, decimal preco)
    {
        if (quantidade <= 0)
            throw new DomainException("Quantidade deve ser maior que zero.");

        if (preco <= 0)
            throw new DomainException("Preço deve ser maior que zero.");
    }
}