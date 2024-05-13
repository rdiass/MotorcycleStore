using FluentValidation.Results;
using MediatR;
using MotorcycleStore.Catalog.API.Application.Events;
using MotorcycleStore.Catalog.API.Data;
using MotorcycleStore.Catalog.API.Models;
using MotorcycleStore.Core.Messages;

namespace MotorcycleStore.Catalog.API.Application.Commands;

public class ClientCommandHandler : CommandHandler, IRequestHandler<RegisterClientCommand, ValidationResult>
{
    private readonly IMotorcycleRepository _motorcycleRepository;

    public ClientCommandHandler(IMotorcycleRepository motorcycleRepository)
    {
        _motorcycleRepository = motorcycleRepository;
    }

    public async Task<ValidationResult> Handle(RegisterClientCommand message, CancellationToken cancellationToken)
    {
        if (!message.IsValid()) return message.ValidationResult;

        //var deliveryManExists = await _motorcycleRepository.GetByCnh(message.Cnh);

        //if (deliveryManExists != null)
        //{
        //    AddError("Delivery man already exist");
        //    return ValidationResult;
        //}

        try
        {
            //var deliveryMan = new Motorcycle(message.Id, message.Name, message.Cnpj, message.BirthDate, message.Cnh, message.TypeCnh, message.CnhImage);
            var deliveryMan = new Motorcycle();
            deliveryMan.AddDomainEvent(new ClientRegisteredEvent(message.Id, message.Name, message.Cnpj, message.BirthDate, message.Cnh, message.TypeCnh, message.CnhImage));
            await _motorcycleRepository.CreateAsync(deliveryMan);
        }
        catch (Exception ex)
        {
            AddError(ex.Message);
        }

        return message.ValidationResult;
    }
}
