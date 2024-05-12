using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using MotorcycleStore.Client.API.Extensions;
using MotorcycleStore.Client.API.Models;
using MotorcycleStore.Core.Mediator;

namespace MotorcycleStore.Client.API.Data;

public class DeliveryManRepository : IDeliveryManRepository
{
    private readonly DeliveryManDbContext _deliveryManDbContext;
    private readonly IMediatorHandler _mediatorHandler;

    public DeliveryManRepository(DeliveryManDbContext deliveryManDbContext, IMediatorHandler mediatorHandler)
    {
        _deliveryManDbContext = deliveryManDbContext;
        _mediatorHandler = mediatorHandler;
    }

    public async Task<List<DeliveryMan>> GetAsync() =>
        await _deliveryManDbContext.DeliveryMen.ToListAsync();

    public async Task<DeliveryMan?> GetAsync(string id) =>
        await _deliveryManDbContext.DeliveryMen.Where(x => x.Id == new ObjectId(id)).FirstOrDefaultAsync();

    public async Task<DeliveryMan> CreateAsync(DeliveryMan newDelivery)
    {
        newDelivery.Id = ObjectId.GenerateNewId();
        var response = _deliveryManDbContext.DeliveryMen.Add(newDelivery);

        _deliveryManDbContext.ChangeTracker.DetectChanges();
        Console.WriteLine(_deliveryManDbContext.ChangeTracker.DebugView.LongView);

        _deliveryManDbContext.SaveChanges();

        await _mediatorHandler.PublishEvents(response.Entity);    

        return response.Entity;
    }

    public async Task UpdateAsync(string id, DeliveryMan updatedDeliveryMan) 
    {
        var motorcycleToUpdate = _deliveryManDbContext.DeliveryMen.FirstOrDefault(c => c.Id == new ObjectId(id));

        if (motorcycleToUpdate != null)
        {
            //motorcycleToUpdate.Model = updatedMotorcycle.Model;
            _deliveryManDbContext.DeliveryMen.Update(motorcycleToUpdate);

            _deliveryManDbContext.ChangeTracker.DetectChanges();
            Console.WriteLine(_deliveryManDbContext.ChangeTracker.DebugView.LongView);

            _deliveryManDbContext.SaveChanges();
        }
        else
        {
            throw new ArgumentException("The delivery man to update cannot be found. ");
        }
    }

    public async Task RemoveAsync(string id)
    {
        var motorcycleToDelete = _deliveryManDbContext.DeliveryMen.FirstOrDefault(c => c.Id == new ObjectId(id));

        if (motorcycleToDelete != null)
        {
            _deliveryManDbContext.DeliveryMen.Remove(motorcycleToDelete);
            _deliveryManDbContext.ChangeTracker.DetectChanges();
            Console.WriteLine(_deliveryManDbContext.ChangeTracker.DebugView.LongView);
            _deliveryManDbContext.SaveChanges();
        }
        else
        {
            throw new ArgumentException("The motorcycle to delete cannot be found.");
        }
    }

    public async Task<DeliveryMan?> GetByCnh(string cnh) =>
        await _deliveryManDbContext.DeliveryMen.Where(x => x.Cnh.Value.Equals(cnh)).FirstOrDefaultAsync();
}