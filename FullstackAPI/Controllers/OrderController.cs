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

    /// <summary>
    /// Returns all orders from the database.
    /// </summary>
    /// <response code="200">Return a list of orders.</response>
    /// <response code="404">Return "There are no orders listed."</response>
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

    /// <summary>
    /// Returns order from selected customer from the database.
    /// </summary>
    /// <response code="200">Return selected orders.</response>
    /// <response code="404">Return "No orders with that customer-id was found."</response>
    // GET api/<OrderController>/5
    [HttpGet("search-by-customer/{id}")]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByCustomer(int id)
    {
        var orders = await _orderRepository.GetByCustomerAsync(id);

        if (orders == null)
        {
            return NotFound("No orders with that customer-id was found.");
        }
        return Ok(orders);
    }

    /// <summary>
    /// Add order to database.
    /// </summary>
    /// <response code="200">Return "Order added successfully."</response>
    /// <response code="400">Return "Invalid order data."</response>
    // POST api/<OrderController>
    [HttpPost]
	public async Task<IActionResult> Post([FromBody] OrderRequest orderRequest)
	{
		if (orderRequest == null || orderRequest.Products == null || !orderRequest.Products.Any())
		{
			return BadRequest("Invalid order data.");
		}

		await _orderRepository.AddOrderAsync(orderRequest.CustomerId, orderRequest.Products);

		return Ok("Order added successfully.");
	}

    /// <summary>
    /// Returns order with selected ID from the database.
    /// </summary>
    /// <response code="200">Return selected order.</response>
    /// <response code="404">Return not found</response>
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

    /// <summary>
    /// Delete order from database.
    /// </summary>
    /// <response code="200">Return no content</response>
    /// <response code="404">Return "There is no order with that ID-number. Could not complete requested action."</response>
    // DELETE api/<OrderController>/5
    [HttpDelete("{id}")]
	public async Task<IActionResult> DeleteOrder(int id)
	{
		var order = await _orderRepository.GetByIdAsync(id);
		if (order == null)
		{
			return NotFound("There is no order with that ID-number. Could not complete requested action.");
		}

		await _orderRepository.DeleteAsync(id);
		return NoContent();
	}

    /// <summary>
    /// Returns order view from database.
    /// </summary>
    /// <response code="200">Return view</response>
    [HttpGet("overview")]
    public async Task<ActionResult<IEnumerable<OrderOverview>>> GetOrderOverview()
    {
        var orders = await _orderRepository.GetOrderOverviewAsync();
        return Ok(orders);
    }

}
