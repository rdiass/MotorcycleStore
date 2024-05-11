namespace MotorcycleStore.Core.DomainObjects;

public class Email
{
    public string Value { get; private set; }

    public Email(string value)
    {
        if (!Validate(value))
            throw new DomainException("Invalid email");

        Value = value;
    }

    public static bool Validate(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        return email.Contains("@") && email.Contains(".");
    }
}
