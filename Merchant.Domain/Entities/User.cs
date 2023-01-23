using System.Text.Json.Serialization;
using Merchant.Domain.Base;

namespace Merchant.Domain.Entities;

public class User : BaseEntity<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Address { get; set; }
    public string UserName { get; set; }
    [JsonIgnore]
    public string Password { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsActive { get; set; } = false;
    public int Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int Role { get; set; }
}