using MotorcycleStore.Core.DomainObjects;

namespace MotorcycleStore.Client.API.Models;

public class DeliveryMan : Entity
{
    public string IdFormated => Id.ToString();
    public string Name { get; set; }
    public Cnpj Cnpj { get; set; }
    public string BirthDate { get; set; }
    public Cnh Cnh { get; set; }
    public string TypeCnh { get; set; }
    public string CnhImage { get; set; }
    public string? Email { get; set; }

    public DeliveryMan()
    {
    }

    public DeliveryMan(string Id, string name, string cnpj, string birthDate, string cnh, string typeCnh, string cnhImage)
    {
        Id = Id;
        Name = name;
        Cnpj = new Cnpj(cnpj);
        BirthDate = birthDate;
        Cnh = new Cnh(cnh);
        TypeCnh = typeCnh;
        CnhImage = cnhImage;
    }
}
