using AutoMapper;
using Merchant.Application;
using Merchant.Application.DTOs.Users;
using Merchant.Application.Forms;
using Merchant.Application.Forms.Users;
using Merchant.Application.Interfaces.Shared;
using Merchant.Common.Const;
using Merchant.Common.Extensions;
using Merchant.Common.Helpers;
using Merchant.Domain.Entities;
using Merchant.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Merchant.Shared.Services;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UserService> _logger;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UserService> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ServiceResponse<UserDto>> GetUser(Guid id)
    {
        var user = await _unitOfWork.Repository<User, Guid>().FindByCondition(x => x.Id == id).FirstOrDefaultAsync();
        return user == null
            ? new ServiceResponse<UserDto>().Failed().WithError(Constants.Error.UserNotFound.Item1,
                Constants.Error.UserNotFound.Item2, "")
            : new ServiceResponse<UserDto>().Successful().WithData(_mapper.Map<UserDto>(user));
    }

    public async Task<ServiceResponse<UserDto>> Register(RegisterForm form, Enums.UserRole userRole)
    {
        var serviceResponse = new ServiceResponse<UserDto>();
        try
        {
            var user = _mapper.Map<User>(form);
            user.Role = (int)userRole;
            user.Password = form.Password.HashPassword();
            await _unitOfWork.Repository<User, Guid>().Insert(user);
            await _unitOfWork.SaveChangesAsync();
            serviceResponse.Successful().WithData(_mapper.Map<UserDto>(user));
        }
        catch (Exception e)
        {
            ServicesHelper.HandleServiceError(ref serviceResponse, _logger, e, e.Message);
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<User>> DoLogin(LoginForm form)
    {
        var serviceResponse = new ServiceResponse<User>().Successful();
        try
        {
            var user = await _unitOfWork.Repository<User, Guid>().FindByCondition(x => x.UserName == form.UserName)
                .FirstOrDefaultAsync();
            IsValidLoginUser(user, form.Password);
            serviceResponse.Successful().WithData(user!);
        }
        catch (Exception ex)
        {
            ServicesHelper.HandleServiceError(ref serviceResponse, _logger, ex, ex.Message);
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<List<UserDto>>> GetList(GetListBaseForm form)
    {
        var serviceResponse = new ServiceResponse<List<UserDto>>().Successful();
        try
        {
            var query = _unitOfWork.Repository<User, Guid>().FindAll().Select(x => _mapper.Map<UserDto>(x));
            var count = await query.CountAsync();
            var list = await query.Skip(form.Start).Take(form.Take).ToListAsync();
            serviceResponse.Successful().WithData(list).WithCount(count);
        }
        catch (Exception e)
        {
            ServicesHelper.HandleServiceError(ref serviceResponse, _logger, e, e.Message);
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<UserDto>> Update(Guid id, UpdateUserForm form)
    {
        var serviceResponse = new ServiceResponse<UserDto>().Successful();
        try
        {
            var user = await _unitOfWork.Repository<User, Guid>()
                .FindItemByCondition(x => x.Id.Equals(id), disableTracking: false);
            if (user is null)
            {
                serviceResponse.Failed().WithError(Constants.Error.UserNotFound.Item1,
                    Constants.Error.UserNotFound.Item2, "");
            }
            else
            {
                _mapper.Map(form, user);
                await _unitOfWork.Repository<User, Guid>().Update(user);
                await _unitOfWork.SaveChangesAsync();
                serviceResponse.Successful().WithData(_mapper.Map<UserDto>(user));
            }
        }
        catch (Exception e)
        {
            ServicesHelper.HandleServiceError(ref serviceResponse, _logger, e, e.Message);
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<UserDto>> UpdatePassword(Guid id, UpdatePasswordForm form)
    {
        var serviceResponse = new ServiceResponse<UserDto>().Successful();
        try
        {
            var user = await _unitOfWork.Repository<User, Guid>()
                .FindItemByCondition(x => x.Id.Equals(id), disableTracking: false);
            if (user is null)
            {
                serviceResponse.Failed().WithError(Constants.Error.UserNotFound.Item1,
                    Constants.Error.UserNotFound.Item2, "");
            }
            else if (!user.Password.VerifyHash(form.OldPassword))
            {
                serviceResponse.Failed().WithError(Constants.Error.UpdatePasswordInvalidOldPassword.Item1,
                    Constants.Error.UpdatePasswordInvalidOldPassword.Item2, "");
            }
            else
            {
                user.Password = form.NewPassword.HashPassword();
                await _unitOfWork.Repository<User, Guid>().Update(user);
                await _unitOfWork.SaveChangesAsync();
                serviceResponse.Successful().WithData(_mapper.Map<UserDto>(user)).WithMessage("Password updated.");
            }
        }
        catch (Exception e)
        {
            ServicesHelper.HandleServiceError(ref serviceResponse, _logger, e, e.Message);
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<bool>> Delete(Guid id)
    {
        var serviceResponse = new ServiceResponse<bool>().Successful();
        try
        {
            var user = await _unitOfWork.Repository<User, Guid>()
                .FindItemByCondition(x => x.Id.Equals(id), disableTracking: false);
            if (user is null)
            {
                serviceResponse.Failed().WithError(Constants.Error.UserNotFound.Item1,
                    Constants.Error.UserNotFound.Item2, "");
            }
            else
            {
                await _unitOfWork.Repository<User, Guid>().Remove(user);
                await _unitOfWork.SaveChangesAsync();
                serviceResponse.Successful().WithData(true).WithMessage("Deleted.");
            }
        }
        catch (Exception e)
        {
            ServicesHelper.HandleServiceError(ref serviceResponse, _logger, e, e.Message);
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<bool>> ActivateUser(Guid id)
    {
        var serviceResponse = new ServiceResponse<bool>().Successful();
        try
        {
            var user = await _unitOfWork.Repository<User, Guid>()
                .FindItemByCondition(x => x.Id.Equals(id), disableTracking: false);
            if (user is null)
            {
                serviceResponse.Failed().WithError(Constants.Error.UserNotFound.Item1,
                    Constants.Error.UserNotFound.Item2, "");
            }
            else if (user.IsActive)
            {
                serviceResponse.Failed().WithError(Constants.Error.UserAlreadyActivated.Item1,
                    Constants.Error.UserAlreadyActivated.Item2, "");
            }
            else
            {
                user.IsActive = true;
                await _unitOfWork.Repository<User, Guid>().Update(user);
                await _unitOfWork.SaveChangesAsync();
                serviceResponse.Successful().WithData(true);
            }
        }
        catch (Exception e)
        {
            ServicesHelper.HandleServiceError(ref serviceResponse, _logger, e, e.Message);
        }

        return serviceResponse;
    }

    private void IsValidLoginUser(User? user, string password)
    {
        if (user == null || !user.Password.VerifyHash(password))
            throw new Exception(Constants.Error.LoginError.Item2);
        if (!user.IsActive)
            throw new Exception(Constants.Error.UserNotActive.Item2);
    }

    public void Dispose()
    {
    }
}