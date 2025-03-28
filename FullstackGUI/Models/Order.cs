using System.Text.Json.Serialization;

namespace FullstackGUI.Models;

public class Order
{
    [JsonIgnore]
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
}
