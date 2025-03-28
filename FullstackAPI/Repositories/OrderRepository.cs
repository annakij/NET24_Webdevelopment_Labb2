using FullstackAPI.Data;
using FullstackAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FullstackAPI.Repositories;

public class OrderRepository : IOrderRepository
{
	private readonly StoreContext _dbContext;

	public OrderRepository(StoreContext dbContext)
	{ 
		_dbContext = dbContext;
	}
	public async Task<IEnumerable<Order>> GetAllAsync()
	{
		return await _dbContext.Orders.ToListAsync();
	}
	public async Task<Order?> GetByIdAsync(int id)
	{
		return await _dbContext.Orders.FindAsync(id);
	}
	public async Task<IEnumerable<Order>> GetByCustomerAsync(int id)
	{
		return await _dbContext.Orders
		.Where(o => o.CustomerId == id)
		.ToListAsync();
	}

	public async Task AddOrderAsync(int customerId, List<OrderProductRequest> orderProductRequests)
	{
		var customer = await _dbContext.Customers.FindAsync(customerId);
		if (customer == null)
		{
			throw new KeyNotFoundException($"Customer with ID {customerId} not found.");
		}

		var newOrder = new Order
		{
			CustomerId = customerId,
			OrderDate = DateTime.UtcNow,
		};
		_dbContext.Orders.Add(newOrder);
		await _dbContext.SaveChangesAsync();

		var orderProducts = orderProductRequests.Select(p => new OrderProducts
		{
			ProductId = p.ProductId,
			Quantity = p.Quantity,
			OrderId = newOrder.Id 
		}).ToList();
		_dbContext.OrderProducts.AddRange(orderProducts);
		await _dbContext.SaveChangesAsync();
	}

	public async Task DeleteAsync(int id)
	{
		var order = await _dbContext.Orders.FindAsync(id);
		if (order != null)
		{
			_dbContext.Orders.Remove(order);
			await _dbContext.SaveChangesAsync();
		}
	}

}
