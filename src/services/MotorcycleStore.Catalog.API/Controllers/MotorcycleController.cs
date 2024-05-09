using Microsoft.AspNetCore.Mvc;
using MotorcycleStore.Catalog.API.Models;
using MotorcycleStore.Catalog.API.Services;

namespace MotorcycleStore.Catalog.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MotorcycleController : ControllerBase
{
    private readonly MotorcycleService _motorcycleService;

    public MotorcycleController(MotorcycleService motorcycleService) =>
        _motorcycleService = motorcycleService;

    [HttpGet]
    public async Task<List<Motorcycle>> Get() =>
        await _motorcycleService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Motorcycle>> Get(string id)
    {
        var book = await _motorcycleService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        return book;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Motorcycle newMotorcycle)
    {
        await _motorcycleService.CreateAsync(newMotorcycle);

        return CreatedAtAction(nameof(Get), new { id = newMotorcycle.Id }, newMotorcycle);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Motorcycle updatedMotorcycle)
    {
        var book = await _motorcycleService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        updatedMotorcycle.Id = book.Id;

        await _motorcycleService.UpdateAsync(id, updatedMotorcycle);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var book = await _motorcycleService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        await _motorcycleService.RemoveAsync(id);

        return NoContent();
    }
}
