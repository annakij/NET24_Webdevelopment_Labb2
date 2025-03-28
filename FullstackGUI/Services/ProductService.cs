using FullstackGUI.Models;

namespace FullstackGUI.Services;

public class ProductService
{
	private readonly HttpClient _httpClient;

	public ProductService(HttpClient httpClient)
	{
		_httpClient = httpClient;
	}

	public async Task<List<Product>> GetProductsAsync()
	{
		return await _httpClient.GetFromJsonAsync<List<Product>>("product");
	}

	public async Task<Product> GetProductByIdAsync(int id)
	{
		return await _httpClient.GetFromJsonAsync<Product>($"product/{id}");
	}
	public async Task<IEnumerable<Product>> GetProductByNameAsync(string name)
	{
		return await _httpClient.GetFromJsonAsync<IEnumerable<Product>>($"product/search-name/{name}");
    }

	public async Task AddProductAsync(Product product)
	{
		await _httpClient.PostAsJsonAsync("product", product);
	}

	public async Task UpdateProductAsync(int id, Product product)
	{
		await _httpClient.PutAsJsonAsync($"product/{id}", product);
	}

	public async Task DeleteProductAsync(int id)
	{
		await _httpClient.DeleteAsync($"product/{id}");
	}
}
