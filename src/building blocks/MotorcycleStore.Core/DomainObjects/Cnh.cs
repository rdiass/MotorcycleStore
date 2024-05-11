using MotorcycleStore.Core.Utils;

namespace MotorcycleStore.Core.DomainObjects;

public class Cnh
{
    public string Value { get; private set; }

    public Cnh(string value)
    {
        if (!Validate(value))
            throw new DomainException("Invalid CNH");

        Value = value;
    }

    public static bool Validate(string cnh)
    {
        if (string.IsNullOrWhiteSpace(cnh))
            return false;

        return cnh.OnlyNumbers().Length == 11;
    }
}
