using FullstackAPI.Models;
using static System.Net.WebRequestMethods;

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
    public async Task PlaceOrderAsync(OrderRequest orderRequest)
    {
        var response = await _httpClient.PostAsJsonAsync("api/order", orderRequest);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to place order. Status: {response.StatusCode}");
        }
    }
    public async Task<IEnumerable<Order>> GetByCustomerAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<Order>>($"order/search-by-customer/{id}");
    }
    public async Task<List<OrderOverview>> GetOrderOverviewAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<OrderOverview>>("order/overview");
    }

}
