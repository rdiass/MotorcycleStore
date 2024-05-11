using MotorcycleStore.Core.Utils;

namespace MotorcycleStore.Core.DomainObjects;

public class Cnpj
{
    public string Value { get; private set; }

    public Cnpj(string value)
    {
        if (!Validate(value))
            throw new DomainException("Invalid CNPJ");

        Value = value;
    }

    public static bool Validate(string cnpj)
    {
        if (string.IsNullOrWhiteSpace(cnpj))
            return false;

        cnpj = cnpj.OnlyNumbers();

        if (cnpj.Length != 14)
            return false;

        int[] multiplier1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplier2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        string tempCnpj = cnpj.Substring(0, 12);
        int sum = 0;

        for (int i = 0; i < 12; i++)
            sum += int.Parse(tempCnpj[i].ToString()) * multiplier1[i];

        int rest = sum % 11;
        rest = rest < 2 ? 0 : 11 - rest;

        string digit = rest.ToString();
        tempCnpj += digit;
        sum = 0;

        for (int i = 0; i < 13; i++)
            sum += int.Parse(tempCnpj[i].ToString()) * multiplier2[i];

        rest = sum % 11;
        rest = rest < 2 ? 0 : 11 - rest;

        digit += rest.ToString();

        return cnpj.EndsWith(digit);
    }
}
