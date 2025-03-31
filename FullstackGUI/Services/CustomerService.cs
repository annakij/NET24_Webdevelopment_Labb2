using FullstackGUI.Models;

namespace FullstackGUI.Services;

public class CustomerService
{
    private readonly HttpClient _httpClient;

    public CustomerService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Customer>> GetCustomersAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Customer>>("customer");
    }

    public async Task<Customer> GetCustomerByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<Customer>($"customer/{id}");
    }

    public async Task<IEnumerable<Customer>> GetCustomerByEmailAsync(string email)
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<Customer>>($"customer/search-email/{email}");
    }
    public async Task AddCustomerAsync(Customer customer)
    {
        await _httpClient.PostAsJsonAsync("customer", customer);
    }
    public async Task UpdateCustomerAsync(int id, Customer updatedCustomer)
    {
        await _httpClient.PutAsJsonAsync($"customer/{id}", updatedCustomer);
    }
    public async Task DeleteCustomerAsync(int id)
    {
        await _httpClient.DeleteAsync($"customer/{id}");
    }

}
