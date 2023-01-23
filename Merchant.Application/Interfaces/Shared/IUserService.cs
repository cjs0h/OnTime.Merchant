using Merchant.Application.DTOs.Users;
using Merchant.Application.Forms;
using Merchant.Application.Forms.Users;
using Merchant.Common.Const;
using Merchant.Common.Helpers;
using Merchant.Domain.Entities;

namespace Merchant.Application.Interfaces.Shared;

public interface IUserService : IDisposable
{
    Task<ServiceResponse<UserDto>> GetUser(Guid id);
    Task<ServiceResponse<UserDto>> Register(RegisterForm form,Enums.UserRole userRole);
    Task<ServiceResponse<User>> DoLogin(LoginForm form);
    Task<ServiceResponse<List<UserDto>>> GetList(GetListBaseForm form);
    Task<ServiceResponse<UserDto>> Update(Guid id, UpdateUserForm form);
    Task<ServiceResponse<UserDto>> UpdatePassword(Guid id, UpdatePasswordForm form);
    Task<ServiceResponse<bool>> Delete(Guid id);
    Task<ServiceResponse<bool>> ActivateUser(Guid id);
}