using PlanInv.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace PlanInv.Domain.ValueObjects;

public record Cnpj
{
    public string Numero { get; }

    public Cnpj(string numero)
    {
        if (!Validar(numero))
            throw new DomainException("CNPJ inválido.");

        Numero = LimparFormatacao(numero);
    }

    private string LimparFormatacao(string cnpj)
    {
        return Regex.Replace(cnpj, "[^0-9]", "");
    }

    private bool Validar(string cnpj)
    {
        if (string.IsNullOrWhiteSpace(cnpj)) return false;

        var cnpjLimpo = LimparFormatacao(cnpj);

        if (cnpjLimpo.Length != 14) return false;

        int[] multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        string tempCnpj = cnpjLimpo.Substring(0, 12);
        int soma = 0;

        for (int i = 0; i < 12; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

        int resto = (soma % 11);
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        string digito = resto.ToString();
        tempCnpj = tempCnpj + digito;
        soma = 0;

        for (int i = 0; i < 13; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

        resto = (soma % 11);
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        digito = digito + resto.ToString();

        return cnpjLimpo.EndsWith(digito);
    }

    
    public override string ToString()
    {
        return Convert.ToUInt64(Numero).ToString(@"00\.000\.000\/0000\-00");
    }
}