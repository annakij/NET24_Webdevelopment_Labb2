using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FullstackAPI.Models;

public class Order
{
	[JsonIgnore]
	public int Id { get; set; }
	public int CustomerId { get; set; }
	public DateTime OrderDate { get; set; }
	public List<OrderProducts> Products { get; set; }
}

public class OrderRequest
{
	public int CustomerId { get; set; }
	public List<OrderProductRequest> Products { get; set; }
}



