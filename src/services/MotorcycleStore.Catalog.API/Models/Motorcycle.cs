using MotorcycleStore.Core.DomainObjects;

namespace MotorcycleStore.Catalog.API.Models;

public class Motorcycle : Entity
{
    public string IdFormated => Id.ToString();
    public string Model { get; set; }
    public decimal Year { get; set; }    
    public string Plate { get; set; }
    public bool Active { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Image { get; set; }
    public int Stock { get; set; }
    public decimal Price { get; set; }
}
