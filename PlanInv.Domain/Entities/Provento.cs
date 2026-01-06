using PlanInv.Domain.Enums;
using PlanInv.Domain.Exceptions;
using System;

namespace PlanInv.Domain.Entities;
public class Provento : BaseEntity
{

    public int PosicaoId { get; private set; } 
    public ETipoProvento Tipo { get; private set; }
    public DateTime DataPagamento { get; private set; }
    public DateTime DataCom { get; private set; } // data "com" - último dia para ter direito
    public int QuantidadeCotas { get; private set; } // Quantidade de cotas que receberam o provento
    public decimal ValorPorCota { get; private set; }
    public Posicao Posicao { get; private set; } = null!;
    public decimal ValorBruto { get; private set; }
    public decimal Imposto { get; private set; }
    public string? Observacoes { get; private set; }

    public decimal ValorLiquido => ValorBruto - Imposto;

    protected Provento() { }

    public Provento(
        int posicaoId,
        ETipoProvento tipo,
        DateTime dataPagamento,
        DateTime dataCom,
        int quantidadeCotas,
        decimal valorPorCota,
        decimal imposto = 0,
        string? observacoes = null)
    {
        Validar(dataPagamento, dataCom, quantidadeCotas, valorPorCota, imposto);

        PosicaoId = posicaoId;
        Tipo = tipo;
        DataPagamento = dataPagamento;
        DataCom = dataCom;
        QuantidadeCotas = quantidadeCotas;
        ValorPorCota = valorPorCota;
        ValorBruto = quantidadeCotas * valorPorCota;
        Imposto = imposto;
        Observacoes = observacoes;
    }


    public static Provento CriarComValorBruto(
        int posicaoId,
        ETipoProvento tipo,
        DateTime dataPagamento,
        DateTime dataCom,
        int quantidadeCotas,
        decimal valorBruto,
        decimal imposto = 0,
        string? observacoes = null)
    {
        if (quantidadeCotas <= 0)
            throw new DomainException("Quantidade de cotas deve ser maior que zero.");

        var valorPorCota = valorBruto / quantidadeCotas;

        return new Provento(
            posicaoId,
            tipo,
            dataPagamento,
            dataCom,
            quantidadeCotas,
            valorPorCota,
            imposto,
            observacoes);
    }

    public void AlterarObservacoes(string? observacoes)
    {
        Observacoes = observacoes;
    }

    public void CorrigirImposto(decimal novoImposto)
    {
        if (novoImposto < 0)
            throw new DomainException("Imposto não pode ser negativo.");

        if (novoImposto > ValorBruto)
            throw new DomainException("Imposto não pode ser maior que o valor bruto.");

        Imposto = novoImposto;
    }

    public void AlterarDataPagamento(DateTime novaData)
    {
        if (novaData < DataCom)
            throw new DomainException("Data de pagamento não pode ser anterior à data COM.");

        DataPagamento = novaData;
    }

    public bool FoiPago() => DataPagamento <= DateTime.Now;


    public bool IsIsento() => Imposto == 0;


    public decimal CalcularYield(decimal precoAtivo)
    {
        if (precoAtivo <= 0)
            throw new DomainException("Preço do ativo deve ser maior que zero.");

        return (ValorPorCota / precoAtivo) * 100;
    }


    public string ObterDescricaoTipo()
    {
        return Tipo switch
        {
            ETipoProvento.Dividendo => "Dividendo",
            ETipoProvento.JurosCapitalProprio => "Juros sobre Capital Próprio (JCP)",
            ETipoProvento.Rendimento => "Rendimento",
            ETipoProvento.Bonificacao => "Bonificação em Cotas",
            ETipoProvento.Amortizacao => "Amortização",
            _ => "Outro"
        };
    }


    private void Validar(
        DateTime dataPagamento,
        DateTime dataCom,
        int quantidadeCotas,
        decimal valorPorCota,
        decimal imposto)
    {
        if (dataPagamento < dataCom)
            throw new DomainException("Data de pagamento não pode ser anterior à data COM.");

        if (quantidadeCotas <= 0)
            throw new DomainException("Quantidade de cotas deve ser maior que zero.");

        if (valorPorCota <= 0)
            throw new DomainException("Valor por cota deve ser maior que zero.");

        if (imposto < 0)
            throw new DomainException("Imposto não pode ser negativo.");

        var valorBrutoCalculado = quantidadeCotas * valorPorCota;
        if (imposto > valorBrutoCalculado)
            throw new DomainException("Imposto não pode ser maior que o valor bruto.");
    }
}