using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;
using MotorcycleStore.Core.DomainObjects;
using System.ComponentModel.DataAnnotations;

namespace MotorcycleStore.WebApp.MVC.Extensions;

public class CnpjAttibute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        return Cnpj.Validate(value.ToString()) ? ValidationResult.Success : new ValidationResult("Invalid CNPJ");
    }
}

public class CnpjAttibuteAdapter : AttributeAdapterBase<CnpjAttibute>
{
    public CnpjAttibuteAdapter(CnpjAttibute attribute, IStringLocalizer stringLocalizer) : base(attribute, stringLocalizer) { }

    public override void AddValidation(ClientModelValidationContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        MergeAttribute(context.Attributes, "data-val", "true");
        MergeAttribute(context.Attributes, "data-val-cnpj", GetErrorMessage(context));
    }

    public override string GetErrorMessage(ModelValidationContextBase validationContext)
    {
        return Attribute.ErrorMessage;
    }
}

public class CnpjValidationAttributeAdapterProvider : IValidationAttributeAdapterProvider
{
    private readonly IValidationAttributeAdapterProvider _baseProvider = new ValidationAttributeAdapterProvider();

    public IAttributeAdapter GetAttributeAdapter(ValidationAttribute attribute, IStringLocalizer stringLocalizer)
    {
        if (attribute is CnpjAttibute cnpjAttribute)
        {
            return new CnpjAttibuteAdapter(cnpjAttribute, stringLocalizer);
        }

        return _baseProvider.GetAttributeAdapter(attribute, stringLocalizer);
    }
}
