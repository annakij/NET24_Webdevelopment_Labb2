using FullstackAPI.Data;
using FullstackAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FullstackAPI.Repositories
{
	public class ProductRepository : IProductRepository
	{
		private readonly StoreContext _dbContext;

		public ProductRepository(StoreContext dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task<IEnumerable<Product>> GetAllAsync()
		{
			return await _dbContext.Products.ToListAsync();
		}
		public async Task<Product?> GetByIdAsync(int id)
		{
			var product = await _dbContext.Products.FindAsync(id);
			return product;
		}
		public async Task<IEnumerable<Product?>> GetByNameAsync(string name)
		{
			var products = await _dbContext.Products
				.Where(p => p.ProductName.Contains(name))
				.ToListAsync();

			return products;
		}
		public async Task AddAsync(Product product)
		{
			_dbContext.Products.Add(product); 
			await _dbContext.SaveChangesAsync();
		}
		public async Task UpdateAsync(int id, Product updatedProduct)
		{
			var product = await _dbContext.Products.FindAsync(id);

			product.ProductName = ShouldUpdate(updatedProduct.ProductName) ? updatedProduct.ProductName : product.ProductName;
			product.Brand = ShouldUpdate(updatedProduct.Brand) ? updatedProduct.Brand : product.Brand;
			product.ProductDescription = ShouldUpdate(updatedProduct.ProductDescription) ? updatedProduct.ProductDescription : product.ProductDescription;
			product.ProductCategory = ShouldUpdate(updatedProduct.ProductCategory) ? updatedProduct.ProductCategory : product.ProductCategory;
			product.Price = updatedProduct.Price > 0 ? updatedProduct.Price : product.Price;
			product.IsInStock = updatedProduct.IsInStock;

			await _dbContext.SaveChangesAsync();
		}

		private static bool ShouldUpdate(string? value)
		{
			return !string.IsNullOrEmpty(value) && value != "string";
		}
		public async Task DeleteAsync(int id)
		{
			var product = await _dbContext.Products.FindAsync(id);
			_dbContext.Products.Remove(product);
			await _dbContext.SaveChangesAsync();
		}
	}
}
