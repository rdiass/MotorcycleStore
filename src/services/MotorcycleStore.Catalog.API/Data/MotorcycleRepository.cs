using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using MotorcycleStore.Catalog.API.Extensions;
using MotorcycleStore.Catalog.API.Models;
using MotorcycleStore.Core.Mediator;

namespace MotorcycleStore.Catalog.API.Data;

public class MotorcycleRepository : IMotorcycleRepository
{
    private readonly MotorcycleDbContext _motorcycleDbContext;
    private readonly IMediatorHandler _mediatorHandler;

    public MotorcycleRepository(MotorcycleDbContext motorcycleDbContext, IMediatorHandler mediatorHandler)
    {
        _motorcycleDbContext = motorcycleDbContext;
        _mediatorHandler = mediatorHandler;
    }

    public async Task<List<Motorcycle>> GetAsync()
    {
        var all = await _motorcycleDbContext.Motorcycles.ToListAsync();
        return all;
    }

    public async Task<Motorcycle?> GetAsync(string id)
    {
        var test = await _motorcycleDbContext.Motorcycles.Where(x => x.Id == new ObjectId(id)).FirstOrDefaultAsync();
        return test;
    }

    public async Task<Motorcycle> CreateAsync(Motorcycle newMotorcycle)
    {
        newMotorcycle.Id = ObjectId.GenerateNewId();
        var response = _motorcycleDbContext.Motorcycles.Add(newMotorcycle);

        _motorcycleDbContext.ChangeTracker.DetectChanges();
        Console.WriteLine(_motorcycleDbContext.ChangeTracker.DebugView.LongView);

        _motorcycleDbContext.SaveChanges();

        await _mediatorHandler.PublishEvents(response.Entity);

        return response.Entity;
    }

    public async Task UpdateAsync(string id, Motorcycle updatedMotorcycle)
    {
        var motorcycleToUpdate = _motorcycleDbContext.Motorcycles.FirstOrDefault(c => c.Id == new ObjectId(id));

        if (motorcycleToUpdate != null)
        {
            //motorcycleToUpdate.Model = updatedMotorcycle.Model;

            _motorcycleDbContext.Motorcycles.Update(motorcycleToUpdate);

            _motorcycleDbContext.ChangeTracker.DetectChanges();
            Console.WriteLine(_motorcycleDbContext.ChangeTracker.DebugView.LongView);

            _motorcycleDbContext.SaveChanges();

        }
        else
        {
            throw new ArgumentException("The motorcyle to update cannot be found. ");
        }
    }

    public async Task RemoveAsync(string id){
        var motorcycleToDelete = _motorcycleDbContext.Motorcycles.FirstOrDefault(c => c.Id == new ObjectId(id));

        if (motorcycleToDelete != null)
        {
            _motorcycleDbContext.Motorcycles.Remove(motorcycleToDelete);
            _motorcycleDbContext.ChangeTracker.DetectChanges();
            Console.WriteLine(_motorcycleDbContext.ChangeTracker.DebugView.LongView);
            _motorcycleDbContext.SaveChanges();
        }
        else
        {
            throw new ArgumentException("The motorcycle to delete cannot be found.");
        }
    }
}
