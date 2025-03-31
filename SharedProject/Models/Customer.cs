using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FullstackAPI.Models;

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
