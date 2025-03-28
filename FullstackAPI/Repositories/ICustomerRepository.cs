using FullstackAPI.Models;

namespace FullstackAPI.Repositories;

public interface ICustomerRepository
{
	Task<IEnumerable<Customer>> GetAllAsync();
	Task<Customer?> GetByIdAsync(int id);
	Task<IEnumerable<Customer>> GetByEmailAsync(string email);
	Task AddAsync(Customer customer);
	Task UpdateAsync(int id, Customer customer);
	Task DeleteAsync(int id);
}