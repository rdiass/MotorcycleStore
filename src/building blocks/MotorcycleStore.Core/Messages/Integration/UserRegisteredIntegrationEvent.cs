namespace MotorcycleStore.Core.Messages.Integration;

public class UserRegisteredIntegrationEvent : IntegrationEvent
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Cnpj { get; set; }
    public string BirthDate { get; set; }
    public string Cnh { get; set; }
    public string TypeCnh { get; set; }
    public string CnhImage { get; set; }

    public UserRegisteredIntegrationEvent(string id, string name, string cnpj, string birthDate, string cnh, string typeCnh, string cnhImage)
    {
        Id = id;
        Name = name;
        Cnpj = cnpj;
        BirthDate = birthDate;
        Cnh = cnh;
        TypeCnh = typeCnh;
        CnhImage = cnhImage;
    }
}
