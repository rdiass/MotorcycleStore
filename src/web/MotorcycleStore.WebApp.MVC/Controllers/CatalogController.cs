using Microsoft.AspNetCore.Mvc;
using MotorcycleStore.WebApp.MVC.Services;

namespace MotorcycleStore.WebApp.MVC.Controllers;

public class CatalogController : MainController
{
    private readonly ICatalogService _catalogService;

    public CatalogController(ICatalogService catalogService)
    {
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
        var product = await _catalogService.GetMotorcycleById(id);
        return View(product);
    }
}
