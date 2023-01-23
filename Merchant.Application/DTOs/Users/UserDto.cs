namespace Merchant.Application.DTOs.Users;

public class UserDto:DtoBase<Guid>
{
    public string Name { get; set; }
    public string? Address { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsActive { get; set; } = false;
    public int Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int Role { get; set; }
}