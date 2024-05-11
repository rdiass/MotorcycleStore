using MotorcycleStore.WebApp.MVC.Models;

namespace MotorcycleStore.WebApp.MVC.Services;

public interface ICatalogService
{
    Task<IEnumerable<MotorcycleViewModel>> GetCatalog();
    Task<MotorcycleViewModel> GetMotorcycleById(string id);
}
