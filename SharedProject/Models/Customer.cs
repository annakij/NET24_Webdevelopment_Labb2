using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FullstackAPI.Models;

	public class Customer
	{
	public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string? Phone { get; set; }
    public string Address { get; set; }
    }
