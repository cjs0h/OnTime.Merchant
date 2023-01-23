using Ardalis.GuardClauses;
using Merchant.Domain.Base;

namespace Merchant.Domain.Entities;

public class Customer : BaseEntity<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string? Company { get; set; }
    public string? Email { get; set; }
    public string PhoneNumber { get; set; }
    public int Role { get; set; }
}