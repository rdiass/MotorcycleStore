using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MotorcycleStore.Client.API.Models;
using MotorcycleStore.Client.API.Settings;

namespace MotorcycleStore.Client.API.Data;

public class DeliveryManRepository : IDeliveryManRepository
{
    private readonly IMongoCollection<DeliveryMan> _deliveryMenCollection;

    public DeliveryManRepository(IOptions<DeliveryManDatabaseSettings> deliveryManDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            deliveryManDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            deliveryManDatabaseSettings.Value.DatabaseName);

        _deliveryMenCollection = mongoDatabase.GetCollection<DeliveryMan>(
            deliveryManDatabaseSettings.Value.DeliveryMenCollectionName);
    }

    public async Task<List<DeliveryMan>> GetAsync() =>
        await _deliveryMenCollection.Find(_ => true).ToListAsync();

    public async Task<DeliveryMan?> GetAsync(string id) =>
        await _deliveryMenCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(DeliveryMan newDelivery) =>
        await _deliveryMenCollection.InsertOneAsync(newDelivery);

    public async Task UpdateAsync(string id, DeliveryMan updatedDeliveryMan) =>
        await _deliveryMenCollection.ReplaceOneAsync(x => x.Id == id, updatedDeliveryMan);

    public async Task RemoveAsync(string id) =>
        await _deliveryMenCollection.DeleteOneAsync(x => x.Id == id);
}
