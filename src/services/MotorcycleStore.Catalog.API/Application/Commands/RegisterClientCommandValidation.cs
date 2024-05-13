using FluentValidation;

namespace MotorcycleStore.Catalog.API.Application.Commands;

public class RegisterClientCommandValidation : AbstractValidator<RegisterClientCommand>
{
    public RegisterClientCommandValidation()
    {
        RuleFor(c => c.Id)
            .NotEqual(string.Empty)
            .WithMessage("Invalid client ID");

        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage("Delivery name not field");

        RuleFor(c => c.Cnpj)
            .Must(ValidateCnpj)
            .WithMessage("Invalid CNPJ");

        RuleFor(c => c.BirthDate)
            .Must(BeAValidDate)
            .WithMessage("Birth data invalid");

        RuleFor(c => c.Cnh)
            .Must(ValidateCnh)
            .WithMessage("Invalid CNH");

        RuleFor(c => c.TypeCnh)
            .NotEmpty()
            .WithMessage("CNH type not field");

        RuleFor(c => c.CnhImage)
            .NotEmpty()
            .WithMessage("CNH image missing");
    }

    protected static bool ValidateCnpj(string cnpj)
    {
        return Core.DomainObjects.Cnpj.Validate(cnpj);
    }

    protected static bool ValidateCnh(string cnh)
    {
        return Core.DomainObjects.Cnh.Validate(cnh);
    }

    protected static bool BeAValidDate(string date)
    {
        return DateTime.TryParse(date, out _);
    }
}
