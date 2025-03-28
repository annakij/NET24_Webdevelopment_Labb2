using System.Text.Json.Serialization;

namespace FullstackGUI.Models
{
	public class Product
	{
		[JsonIgnore]
		public int Id { get; set; }
		public string ProductName { get; set; } = string.Empty;
		public string? Brand { get; set; }
		public string ProductDescription { get; set; } = string.Empty;
		public string? ProductCategory { get; set; }
		public int Price { get; set; }
		public bool IsInStock { get; set; }
	}
}
