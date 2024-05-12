using Microsoft.Extensions.Options;
using MotorcycleStore.WebApp.MVC.Extensions;
using MotorcycleStore.WebApp.MVC.Models;

namespace MotorcycleStore.WebApp.MVC.Services;

public class CatalogService : Service, ICatalogService
{
    private readonly HttpClient _httpClient;

    public CatalogService(HttpClient httpClient, IOptions<AppSettings> appSettings)
    {
        httpClient.BaseAddress = new Uri(appSettings.Value.CatalogUrl);
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<MotorcycleViewModel>> GetCatalog()
    {
        var response = await _httpClient.GetAsync("");

        TryError(response);
        try
        {
            var result = await response.Content.ReadFromJsonAsync<IEnumerable<MotorcycleViewModel>>();
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return null;
    }

    public async Task<MotorcycleViewModel> GetMotorcycleById(string id)
    {
        var response = await _httpClient.GetAsync($"{id}");

        TryError(response);

        return await response.Content.ReadFromJsonAsync<MotorcycleViewModel>();
    }
}
