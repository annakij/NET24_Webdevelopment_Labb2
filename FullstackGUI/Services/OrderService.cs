using FullstackGUI.Models;

namespace FullstackGUI.Services;

public class OrderService
{
    private HttpClient _httpClient;

    public OrderService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Order>> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Order>>("order");
    }

    public async Task<Order?> GetByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<Order>($"order/{id}");
    }

    public async Task<IEnumerable<Order>> GetByCustomerAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<Order>>($"order/search-by-customer/{id}");
    }

}
