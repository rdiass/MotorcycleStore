using MotorcycleStore.WebApp.MVC.Models;

namespace MotorcycleStore.WebApp.MVC.Services;

public interface ICatalogoServiceRefit
{
    Task<IEnumerable<MotorcycleViewModel>> GetCatalog();
    Task<MotorcycleViewModel> GetMotorcycleById(Guid id);
}
