using MotorcycleStore.Core.DomainObjects;

namespace MotorcycleStore.Client.API.Models;

public class DeliveryMan : Entity
{
    public string Name { get; private set; }
    public Cnpj Cnpj { get; private set; }
    public string BirthDate { get; private set; }
    public Cnh Cnh { get; private set; }
    public string TypeCnh { get; private set; }
    public string CnhImage { get; set; }
    public Email Email { get; private set; }
    //public DeliveryMan(string Id, string name, string cnpj, string birthDate, string cnh, string typeCnh, string cnhImage)
    //{
    //    Id = Id;
    //    Name = name;
    //    Cnpj = new Cnpj(cnpj);
    //    BirthDate = birthDate;
    //    Cnh = new Cnh(cnh);
    //    TypeCnh = typeCnh;
    //    CnhImage = cnhImage;
    //}
}
