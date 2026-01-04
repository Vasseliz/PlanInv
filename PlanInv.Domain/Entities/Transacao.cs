using PlanInv.Domain.Enums;
using PlanInv.Domain.Exceptions;
using System;

namespace PlanInv.Domain.Entities;

public class Transacao : BaseEntity
{
    public int PosicaoId { get; private set; }

    public Posicao Posicao { get; private set; } = null!;

    public DateTime DataTransacao { get; private set; }
    public ETipoTransacao Tipo { get; private set; }
    public int QuantidadeCotas { get; private set; }
    public decimal PrecoUnitario { get; private set; }
    public decimal Custos { get; private set; }
    public ECorretora Corretora { get; private set; }
    public string? Observacoes { get; private set; }
    public decimal ValorBruto => QuantidadeCotas * PrecoUnitario;
    public decimal ValorLiquido => CalcularValorLiquido();


    protected Transacao() { }

    public Transacao(
        int posicaoId,
        ETipoTransacao tipo,
        int quantidadeCotas,
        decimal precoUnitario,
        decimal custos,
        ECorretora corretora,
        DateTime? dataTransacao = null,
        string? observacoes = null)
    {
        Validar(quantidadeCotas, precoUnitario, custos);

        PosicaoId = posicaoId;
        Tipo = tipo;
        QuantidadeCotas = quantidadeCotas;
        PrecoUnitario = precoUnitario;
        Custos = custos;
        Corretora = corretora;
        DataTransacao = dataTransacao ?? DateTime.Now;
        Observacoes = observacoes;
    }
    public void AlterarObservacoes(string? observacoes)
    {
        Observacoes = observacoes;
    }

    public void AlterarDataTransacao(DateTime novaData)
    {
        if (novaData > DateTime.Now)
            throw new DomainException("Data da transação não pode ser futura.");

        DataTransacao = novaData;
    }

    public bool IsCompra() => Tipo == ETipoTransacao.Compra;
    public bool IsVenda() => Tipo == ETipoTransacao.Venda;

    public decimal ObterImpactoNaPosicao()
    {
        return Tipo == ETipoTransacao.Compra
            ? -ValorLiquido
            : ValorLiquido;
    }


    private void Validar(int quantidade, decimal preco, decimal custos)
    {
        if (quantidade <= 0)
            throw new DomainException("Quantidade de cotas deve ser maior que zero.");

        if (preco <= 0)
            throw new DomainException("Preço unitário deve ser maior que zero.");

        if (custos < 0)
            throw new DomainException("Custos não podem ser negativos.");
    }

    private decimal CalcularValorLiquido()
    {
        return Tipo == ETipoTransacao.Compra
            ? ValorBruto + Custos
            : ValorBruto - Custos;
    }
}