using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotorcycleStore.Client.API.Models;
using MotorcycleStore.Client.API.Services;
using static MotorcycleStore.WebAPI.Core.Identity.CustomAuthorize;

namespace MotorcycleStore.Client.API.Controllers;

[ApiController]
//[Authorize]
[Route("api/client")]
public class ClientController : ControllerBase
{
    public readonly DeliveryManService deliveryManService;

    public ClientController(DeliveryManService deliveryManService)
    {
        this.deliveryManService = deliveryManService;
    }

    [HttpGet]
    public async Task<List<DeliveryMan>> Get() =>
        await deliveryManService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<DeliveryMan>> Get(string id)
    {
        var deliveryMan = await deliveryManService.GetAsync(id);

        if (deliveryMan is null)
        {
            return NotFound();
        }

        return deliveryMan;
    }

    [HttpPost]
    public async Task<IActionResult> Post(DeliveryMan newDeliveryMan)
    {
        await deliveryManService.CreateAsync(newDeliveryMan);

        return CreatedAtAction(nameof(Get), new { id = newDeliveryMan.Id }, newDeliveryMan);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, DeliveryMan updatedDeliveryMan)
    {
        var deliveryMan = await deliveryManService.GetAsync(id);

        if (deliveryMan is null)
        {
            return NotFound();
        }

        updatedDeliveryMan.Id = deliveryMan.Id;

        await deliveryManService.UpdateAsync(id, updatedDeliveryMan);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var deliveryMan = await deliveryManService.GetAsync(id);

        if (deliveryMan is null)
        {
            return NotFound();
        }

        await deliveryManService.RemoveAsync(id);

        return NoContent();
    }
}
