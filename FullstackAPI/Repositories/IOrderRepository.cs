using FullstackAPI.Models;

namespace FullstackAPI.Repositories;

public interface IOrderRepository
{
	Task<IEnumerable<Order>> GetAllAsync();
	Task<IEnumerable<Order>> GetByCustomerAsync(int id);
	Task<Order?> GetByIdAsync(int id);
	Task AddOrderAsync(int customerId, List<OrderProductRequest> products);
	Task DeleteAsync(int id);
}
