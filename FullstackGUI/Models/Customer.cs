using System.Text.Json.Serialization;

namespace FullstackGUI.Models;

public class Customer
{
    [JsonIgnore]
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public string? Phone { get; set; }
    public required string Address { get; set; }
}
