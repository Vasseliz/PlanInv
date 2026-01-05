using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using PlanInv.Domain.Exceptions;

namespace PlanInv.Domain.ValueObjects;

public record Cpf
{
    public string Numero { get; }

    public Cpf(string numero)
    {
        if (!Validar(numero))
            throw new DomainException("Cpf Inválido.");

        Numero = LimparFormatacao(numero);
    }

    private string LimparFormatacao(string cpf)
    {
        return Regex.Replace(cpf, "[^0-9]", "");
    }

    private bool Validar(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf)) return false;

        var cpfLimpo = LimparFormatacao(cpf);

        if (cpfLimpo.Length != 11) return false;

        if (new string(cpfLimpo[0], cpfLimpo.Length) == cpfLimpo) return false;

        int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        string tempCpf = cpfLimpo.Substring(0, 9);
        int soma = 0;

        for (int i = 0; i < 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

        int resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        string digito = resto.ToString();
        tempCpf = tempCpf + digito;
        soma = 0;

        for (int i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

        resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        digito = digito + resto.ToString();

        return cpfLimpo.EndsWith(digito);
    }

    public override string ToString()
    {
        return Convert.ToUInt64(Numero).ToString(@"000\.000\.000\-00");
    }


}
