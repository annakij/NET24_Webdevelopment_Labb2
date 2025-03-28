using FullstackAPI.Models;
using FullstackAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FullstackAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerController(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    // GET: api/<CustomerController>
    [HttpGet]
	public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomers()
	{
		var customer = await _customerRepository.GetAllAsync();

		if (customer == null)
		{
			return NotFound("There are no customers listed.");
		}
		return Ok(customer);
	}

	// GET api/<CustomerController>/5
	[HttpGet("{id}")]
    public async Task<ActionResult<Customer?>> GetCustomerById(int id)
    {
		var customer = await _customerRepository.GetByIdAsync(id);

		if (customer == null)
		{
			return NotFound("There is no customer with that ID-number");
		}
		return Ok(customer);
	}

	// GET api/<CustomerController>/5
	[HttpGet("search-email/{email}")]
	public async Task<ActionResult<Customer?>> GetCustomerByEmail(string email)
	{
		var customer = await _customerRepository.GetByEmailAsync(email);

		if (customer == null)
		{
			return NotFound("There is no customer with that email");
		}
		return Ok(customer);
	}

	// POST api/<CustomerController>
	[HttpPost]
    public async Task<ActionResult> AddCustomer(Customer customer)
    {
		await _customerRepository.AddAsync(customer);
		return Ok("Customer was succefully added.");
    }

    // PUT api/<CustomerController>/5
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCustomer(int id, Customer customer)
    {
		var existingCustomer = await _customerRepository.GetByIdAsync(id);
		if (existingCustomer == null)
		{
			return NotFound("There is no customer with that ID-number. Could not complete requested action.");
		}
		await _customerRepository.UpdateAsync(id, customer);
		return NoContent();
	}

    // DELETE api/<CustomerController>/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCustomer(int id)
    {
		var customer = await _customerRepository.GetByIdAsync(id);
		if (customer == null)
		{
			return NotFound("There is no customer with that ID-number. Could not complete requested action.");
		}
		await _customerRepository.DeleteAsync(id);
		return NoContent();
    }
}
