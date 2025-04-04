using System.Text.Json.Serialization;

namespace FullstackAPI.Models;

public class Product
{
	public int Id { get; set; }
	public string ProductName { get; set; }
	public string? Brand { get; set; }
	public string ProductDescription { get; set; } = string.Empty;
	public string? ProductCategory { get; set; }
	public int Price { get; set; }
	public bool IsInStock { get; set; }
}
