namespace FullstackAPI.Models;

public class OrderOverview
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public string CustomerName { get; set; }
    public List<OrderProductInfo> Products { get; set; }
}

public class OrderProductInfo
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string Brand { get; set; }
    public int Quantity { get; set; }
}
