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


    /// <summary>
    /// Returns all customers from the database.
    /// </summary>
    /// <response code="200">Return a list of customers.</response>
    /// <response code="404">Return "There are no customers listed."</response>
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

    /// <summary>
    /// Returns customer with selected ID from the database.
    /// </summary>
    /// <response code="200">Return selected customer.</response>
    /// <response code="404">Return "There is no customer with that ID-number."</response>
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

    /// <summary>
    /// Returns all customers with email that contains search query from the database.
    /// </summary>
    /// <response code="200">Returns a list of customers.</response>
    /// <response code="404">Return "There is no customer with that email."</response>
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

    /// <summary>
    /// Add customer to database.
    /// </summary>
    /// <response code="200">Return "Customer was succefully added."</response>
    // POST api/<CustomerController>
    [HttpPost]
    public async Task<ActionResult> AddCustomer(Customer customer)
    {
		await _customerRepository.AddAsync(customer);
		return Ok("Customer was succefully added.");
    }

    /// <summary>
    /// Update customer information.
    /// </summary>
    /// <response code="200">Return no content</response>
    /// <response code="404">Return "There is no customer with that ID-number. Could not complete requested action."</response>
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

    /// <summary>
    /// Delete customer from database.
    /// </summary>
    /// <response code="200">Return no content</response>
    /// <response code="404">Return "There is no customer with that ID-number. Could not complete requested action."</response>
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
