using FluentValidation.Results;
using MediatR;
using MotorcycleStore.Client.API.Application.Events;
using MotorcycleStore.Client.API.Data;
using MotorcycleStore.Client.API.Models;
using MotorcycleStore.Core.Messages;

namespace MotorcycleStore.Client.API.Application.Commands;

public class ClientCommandHandler : CommandHandler, IRequestHandler<RegisterClientCommand, ValidationResult>
{
    private readonly IDeliveryManRepository _deliveryManRepository;

    public ClientCommandHandler(IDeliveryManRepository deliveryManRepository)
    {
        _deliveryManRepository = deliveryManRepository;
    }

    public async Task<ValidationResult> Handle(RegisterClientCommand message, CancellationToken cancellationToken)
    {
        if (!message.IsValid()) return message.ValidationResult;

        //var deliveryManExists = await _deliveryManRepository.GetByCnh(message.Cnh);

        //if (deliveryManExists != null)
        //{
        //    AddError("Delivery man already exist");
        //    return ValidationResult;
        //}

        try
        {
            var deliveryMan = new DeliveryMan(message.Id, message.Name, message.Cnpj, message.BirthDate, message.Cnh, message.TypeCnh, message.CnhImage);
            deliveryMan.AddDomainEvent(new ClientRegisteredEvent(message.Id, message.Name, message.Cnpj, message.BirthDate, message.Cnh, message.TypeCnh, message.CnhImage));
            await _deliveryManRepository.CreateAsync(deliveryMan);
        }
        catch (Exception ex)
        {
            AddError(ex.Message);
        }

        return message.ValidationResult;
    }
}
