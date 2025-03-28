using FullstackAPI.Models;

namespace FullstackAPI.Repositories;

public interface IProductRepository
{
	Task<IEnumerable<Product>> GetAllAsync();
	Task<Product?> GetByIdAsync(int id);
	Task<IEnumerable<Product?>> GetByNameAsync(string name);
	Task AddAsync(Product product);
	Task UpdateAsync(int id, Product product);
	Task DeleteAsync(int id);
}

  