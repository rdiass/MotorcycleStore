using MongoDB.Bson;

namespace MotorcycleStore.WebApp.MVC.Models;

public class MotorcycleViewModel
{
    public ObjectId? Id { get; set; }
    public string IdFormated => Id?.ToString();
    public string Model { get; set; }
    public string Description { get; set; }
    public decimal Year { get; set; }
    public string Plate { get; set; }
    public bool Active { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Image { get; set; }
    public int Stock { get; set; }
    public decimal Price { get; set; }
}
