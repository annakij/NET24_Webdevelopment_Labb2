using FullstackAPI.Models;
using FullstackAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FullstackAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
	private readonly IOrderRepository _orderRepository;
	public OrderController(IOrderRepository orderRepository)
	{
		_orderRepository = orderRepository;
	}

	// GET: api/<OrderController>
	[HttpGet]
    public async Task<ActionResult<IEnumerable<Order>>> GetAllOrders()
    {
        var orders = await _orderRepository.GetAllAsync();

        if (orders == null)
        {
            return NotFound("There are no orders listed.");
        }
        return Ok(orders);
    }

    // GET api/<OrderController>/5
    [HttpGet("search-by-customer/{id}")]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByCustomer(int id)
    {
        var orders = await _orderRepository.GetByCustomerAsync(id);

        if (orders == null)
        {
            return NotFound("No orders with that id was found.");
        }
        return Ok(orders);
    }

	// POST api/<OrderController>
	[HttpPost]
	public async Task<IActionResult> Post([FromBody] OrderRequest orderRequest)
	{
		if (orderRequest == null || orderRequest.Products == null || !orderRequest.Products.Any())
		{
			return BadRequest("Invalid order data.");
		}

		// Skicka bara customerId och produkterna, ingen OrderId behövs
		await _orderRepository.AddOrderAsync(orderRequest.CustomerId, orderRequest.Products);

		return Ok("Order products added successfully.");
	}


	// GET api/<OrderController>/5
	[HttpGet("{id}")]
	public async Task<IActionResult> GetOrderById(int id)
	{
		var order = await _orderRepository.GetByIdAsync(id);

		if (order == null)
		{
			return NotFound();
		}

		return Ok(order);
	}

	// DELETE api/<OrderController>/5
	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteOrder(int id)
	{
		var order = await _orderRepository.GetByIdAsync(id);
		if (order == null)
		{
			return NotFound($"Order with ID {id} not found.");
		}

		await _orderRepository.DeleteAsync(id);
		return NoContent();
	}
}
