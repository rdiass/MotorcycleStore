using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MotorcycleStore.Catalog.API.Models;

public class Motorcycle
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public bool Active { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Image { get; set; }
    public int Stock { get; set; }    
}
