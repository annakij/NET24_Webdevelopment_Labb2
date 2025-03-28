using FullstackAPI.Data;
using FullstackAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FullstackAPI.Repositories;

public class CustomerRepository : ICustomerRepository
{
	private readonly StoreContext _dbContext;

	public CustomerRepository(StoreContext dbContext)
	{
		_dbContext = dbContext;
	}
	public async Task<IEnumerable<Customer>> GetAllAsync()
	{
		return await _dbContext.Customers.ToListAsync();
	}
	public async Task<Customer?> GetByIdAsync(int id)
	{
		var customer = await _dbContext.Customers.FindAsync(id);
		return customer;
	}
	public async Task<IEnumerable<Customer>> GetByEmailAsync(string email)
{
    var customers = await _dbContext.Customers
        .Where(c => c.Email.Contains(email))
        .ToListAsync();

    return customers;
}

	public async Task AddAsync(Customer customer)
	{
		_dbContext.Customers.Add(customer);
		await _dbContext.SaveChangesAsync();
	}
	public async Task UpdateAsync(int id, Customer updatedCustomer)
	{
		var customer = await _dbContext.Customers.FindAsync(id);

		customer.FirstName = ShouldUpdate(updatedCustomer.FirstName) ? updatedCustomer.FirstName : customer.FirstName;
		customer.LastName = ShouldUpdate(updatedCustomer.LastName) ? updatedCustomer.LastName : customer.LastName;
		customer.Email = ShouldUpdate(updatedCustomer.Email) ? updatedCustomer.Email : customer.Email;
		customer.Phone = ShouldUpdate(updatedCustomer.Phone) ? updatedCustomer.Phone : customer.Phone;
		customer.Address = ShouldUpdate(updatedCustomer.Address) ? updatedCustomer.Address : customer.Address;

		await _dbContext.SaveChangesAsync();
	}

	private bool ShouldUpdate(string? value)
	{
		return !string.IsNullOrEmpty(value) && value != "string";
	}

	public async Task DeleteAsync(int id)
	{
		var customer = await _dbContext.Customers.FindAsync(id);
		_dbContext.Customers.Remove(customer);
		await _dbContext.SaveChangesAsync();
	}
}
