using FullstackAPI.Models;
using FullstackAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FullstackAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductController (IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    // GET: api/<ProductController>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
    {
        var products = await _productRepository.GetAllAsync();
        
        if (products == null)
        {
            return NotFound("There are no products listed.");
        }
        return Ok(products);
    }

    // GET api/<ProductController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Product?>> GetProductById(int id)
    {
        var product =  await _productRepository.GetByIdAsync(id);

        if (product == null)
        {
            return NotFound("There is no product in stock with that ID-number");
        }
        return Ok(product);
    }

	// GET api/<ProductController>/top
	[HttpGet("search-name/{name}")]
	public async Task<ActionResult<Product?>> GetProductByName(string name)
	{
		var product = await _productRepository.GetByNameAsync(name);

		if (product == null)
		{
			return NotFound("There is no product in stock with that name");
		}
		return Ok(product);
	}

	// POST api/<ProductController>
	[HttpPost]
    public async Task<ActionResult> AddProduct(Product product)
    {
        await _productRepository.AddAsync(product);

        return Ok("Product was succefully added.");
    }

    // PUT api/<ProductController>/5
    [HttpPut("{id}")]
	public async Task<ActionResult> UpdateProduct(int id, Product product)
	{
		var existingProduct = await _productRepository.GetByIdAsync(id);
        if (existingProduct == null)
        {
            return NotFound("There is no product with that ID-number. Couldn't complete requested action.");
        }

		await _productRepository.UpdateAsync(id, product);
		return NoContent();
	}

    // DELETE api/<ProductController>/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
		var product = await _productRepository.GetByIdAsync(id);
		if (product == null)
		{
			return NotFound("There is no product in stock with that ID-number. Could not complete requested action.");
		}

		await _productRepository.DeleteAsync(id);
		return NoContent();
	}
}
