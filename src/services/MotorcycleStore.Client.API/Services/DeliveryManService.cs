using MotorcycleStore.Client.API.Data;
using MotorcycleStore.Client.API.Models;

namespace MotorcycleStore.Client.API.Services;

public class DeliveryManService
{
    private readonly IDeliveryManRepository _deliveryManRepository;

    public DeliveryManService(IDeliveryManRepository deliveryManRepository) =>
        _deliveryManRepository = deliveryManRepository;

    public async Task<List<DeliveryMan>> GetAsync() =>
        await _deliveryManRepository.GetAsync();

    public async Task<DeliveryMan?> GetAsync(string id) =>
        await _deliveryManRepository.GetAsync(id);

    public async Task CreateAsync(DeliveryMan newDelivery) =>
        await _deliveryManRepository.CreateAsync(newDelivery);

    public async Task UpdateAsync(string id, DeliveryMan updatedDeliveryMan) =>
        await _deliveryManRepository.UpdateAsync(id, updatedDeliveryMan);

    public async Task RemoveAsync(string id) => await _deliveryManRepository.RemoveAsync(id);
}
