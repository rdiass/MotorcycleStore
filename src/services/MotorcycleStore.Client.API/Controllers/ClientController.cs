using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MotorcycleStore.Client.API.Application.Commands;
using MotorcycleStore.Client.API.Models;
using MotorcycleStore.Client.API.Services;
using MotorcycleStore.Core.Mediator;
using MotorcycleStore.WebAPI.Core.Controllers;

namespace MotorcycleStore.Client.API.Controllers;

//[Authorize]
[Route("api/client")]
public class ClientController : MainController
{
    public readonly DeliveryManService _deliveryManService;
    private readonly IMediatorHandler _mediatorHandler;

    public ClientController(DeliveryManService deliveryManService, IMediatorHandler mediatorHandler)
    {
        _deliveryManService = deliveryManService;
        _mediatorHandler = mediatorHandler;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var response = await _mediatorHandler.SendCommand(new RegisterClientCommand(
            ObjectId.GenerateNewId().ToString(),
            "Rafael Dias Santos",
            GerarCnpj(),
            DateTime.Now.ToString(),
            GenerateCNH(),
            "A",
            "http://image.com"));

        return CustomResponse(response);
    }

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<DeliveryMan>> Get(string id)
    {
        var deliveryMan = await _deliveryManService.GetAsync(id);

        if (deliveryMan is null)
        {
            return NotFound();
        }

        return deliveryMan;
    }

    [HttpPost]
    public async Task<IActionResult> Post(DeliveryMan newDeliveryMan)
    {
        await _deliveryManService.CreateAsync(newDeliveryMan);

        return CreatedAtAction(nameof(Get), new { id = newDeliveryMan.Id }, newDeliveryMan);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, DeliveryMan updatedDeliveryMan)
    {
        var deliveryMan = await _deliveryManService.GetAsync(id);

        if (deliveryMan is null)
        {
            return NotFound();
        }

        updatedDeliveryMan.Id = deliveryMan.Id;

        await _deliveryManService.UpdateAsync(id, updatedDeliveryMan);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var deliveryMan = await _deliveryManService.GetAsync(id);

        if (deliveryMan is null)
        {
            return NotFound();
        }

        await _deliveryManService.RemoveAsync(id);

        return NoContent();
    }

    public static String GenerateCNH()
    {
        // generate cnh number random
        Random rng = new Random();
        int number = rng.Next(1, 1000000000);
        string digits = number.ToString("000000000");
        return digits;
    }

    public static String GerarCnpj()
    {
        int soma = 0, resto = 0;
        int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        Random rnd = new Random();
        string semente = rnd.Next(10000000, 99999999).ToString() + "0001";

        for (int i = 0; i < 12; i++)
            soma += int.Parse(semente[i].ToString()) * multiplicador1[i];

        resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        semente = semente + resto;
        soma = 0;

        for (int i = 0; i < 13; i++)
            soma += int.Parse(semente[i].ToString()) * multiplicador2[i];

        resto = soma % 11;

        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        semente = semente + resto;
        return semente;
    }
}
