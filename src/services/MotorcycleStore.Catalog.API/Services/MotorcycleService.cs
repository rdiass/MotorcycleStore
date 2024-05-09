using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MotorcycleStore.Catalog.API.Models;
using MotorcycleStore.Catalog.Settings;

namespace MotorcycleStore.Catalog.API.Services;

public class MotorcycleService
{
    private readonly IMongoCollection<Motorcycle> _motorcyclesCollection;

    public MotorcycleService(
        IOptions<CatalogDatabaseSettings> catalogDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            catalogDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            catalogDatabaseSettings.Value.DatabaseName);

        _motorcyclesCollection = mongoDatabase.GetCollection<Motorcycle>(
            catalogDatabaseSettings.Value.MotorcyclesCollectionName);
    }

    public async Task<List<Motorcycle>> GetAsync() =>
        await _motorcyclesCollection.Find(_ => true).ToListAsync();

    public async Task<Motorcycle?> GetAsync(string id) =>
        await _motorcyclesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Motorcycle newBook) =>
        await _motorcyclesCollection.InsertOneAsync(newBook);

    public async Task UpdateAsync(string id, Motorcycle updatedMotorcycle) =>
        await _motorcyclesCollection.ReplaceOneAsync(x => x.Id == id, updatedMotorcycle);

    public async Task RemoveAsync(string id) =>
        await _motorcyclesCollection.DeleteOneAsync(x => x.Id == id);
}