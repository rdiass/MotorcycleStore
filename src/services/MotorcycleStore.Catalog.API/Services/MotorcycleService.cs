using MongoDB.Bson;
using MotorcycleStore.Catalog.API.Data;
using MotorcycleStore.Catalog.API.Models;

namespace MotorcycleStore.Catalog.API.Services;

public class MotorcycleService
{
    private readonly IMotorcycleRepository _motorcycleRepository;

    public MotorcycleService(
        IMotorcycleRepository motorcycleRepository)
    {
        _motorcycleRepository = motorcycleRepository;
    }

    public async Task<List<Motorcycle>> GetAsync() =>
        await _motorcycleRepository.GetAsync();

    public async Task<Motorcycle?> GetAsync(string id) =>
        await _motorcycleRepository.GetAsync(id);

    public async Task<Motorcycle> CreateAsync(Motorcycle newBook) =>
        await _motorcycleRepository.CreateAsync(newBook);

    public async Task UpdateAsync(string id, Motorcycle updatedMotorcycle) =>
        await _motorcycleRepository.UpdateAsync(id, updatedMotorcycle);

    public async Task RemoveAsync(string id) =>
        await _motorcycleRepository.RemoveAsync(id);
}