using MotorcycleStore.Core.Messages;

namespace MotorcycleStore.Client.API.Application.Events;

public class ClientRegisteredEvent : Event
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Cnpj { get; set; }
    public string BirthDate { get; set; }
    public string Cnh { get; set; }
    public string TypeCnh { get; set; }
    public string CnhImage { get; set; }

    public ClientRegisteredEvent(string id, string name, string cnpj, string birthDate, string cnh, string typeCnh, string cnhImage)
    {
        AggregateId = id;
        Id = id;
        Name = name;
        Cnpj = cnpj;
        BirthDate = birthDate;
        Cnh = cnh;
        TypeCnh = typeCnh;
        CnhImage = cnhImage;
    }
}
