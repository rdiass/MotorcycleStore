using Microsoft.AspNetCore.Mvc;
using MotorcycleStore.Core.Messages.Integration;
using MotorcycleStore.MessaBus;
using MotorcycleStore.WebAPI.Core.Controllers;
using MotorcycleStore.WebApp.MVC.Models;
using MotorcycleStore.WebApp.MVC.Services;

namespace MotorcycleStore.WebApp.MVC.Controllers;

public class CatalogController : MainController
{
    private readonly ICatalogService _catalogService;
    private readonly IMessageBus _bus;

    public CatalogController(ICatalogService catalogService, IMessageBus bus)
    {
        _bus = bus;
        _catalogService = catalogService;
    }

    [HttpGet]
    [Route("")]
    [Route("vitrine")]
    public async Task<IActionResult> Index()
    {
        var catalog = await _catalogService.GetCatalog();
        return View(catalog);
    }

    [HttpGet]
    [Route("detail/{id}")]
    public async Task<IActionResult> Detail(string id)
    {
        var clientResult = await RegisterMotorcycle(new MotorcycleViewModel());

        if (!clientResult.ValidationResult.IsValid)
        {
            return CustomResponse(clientResult.ValidationResult);
        }

        return RedirectToAction("Index");

        //var product = await _catalogService.GetMotorcycleById(id);
        //return View(product);
    }

    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> Create(MotorcycleViewModel motorcycleViewModel)
    {
        var clientResult = await RegisterMotorcycle(motorcycleViewModel);

        if (!clientResult.ValidationResult.IsValid)
        {
            return CustomResponse(clientResult.ValidationResult);
        }

        return RedirectToAction("Index");
    }

    private async Task<ResponseMessage> RegisterMotorcycle(MotorcycleViewModel motorcycleViewModel)
    {
        var userRegisteredIntegrationEvent = new UserRegisteredIntegrationEvent(
            "61a6058e6c43f32854e51f51",
            "",
            "",
            DateTime.Now.ToString(),
            "",
            "A",
            "http://www.google.com");

        try
        {
            return await _bus.RequestAsync<UserRegisteredIntegrationEvent, ResponseMessage>(userRegisteredIntegrationEvent);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        throw new Exception("Error registering motorcycle");
    }
}
