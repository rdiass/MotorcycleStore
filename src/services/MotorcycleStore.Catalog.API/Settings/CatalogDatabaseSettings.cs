namespace MotorcycleStore.Catalog.Settings;

public class CatalogDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string MotorcyclesCollectionName { get; set; } = null!;
}
