using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Merchant.Application.DTOs.Users;
using Merchant.Common.Extensions;
using Merchant.Common.Helpers;
using Merchant.Domain.Entities;

namespace Merchant.WebApi.Tests.MockData;

public class UserMockData
{
    public static ServiceResponse<User> OkLoginUser()
    {
        return new ServiceResponse<User>().Successful().WithData(new User()
        {
            Id = Guid.Parse("cbc10c4e-1c9d-4e8a-8b8f-a66cfc040450"),
            UserName = "username1",
            Password = "password1".HashPassword(),
            FirstName = "firstname1",
            LastName = "lastname1",
            Email = "test1@test.com",
            PhoneNumber = "1234567891",
            IsActive = true
        });
    }

    public static ServiceResponse<User> FailLoginUser()
    {
        return new ServiceResponse<User>().Failed();
    }

    public static ServiceResponse<UserDto> OkGetUser()
    {
        return new ServiceResponse<UserDto>().Successful().WithData(new UserDto()
        {
            Id = Guid.Parse("cbc10c4e-1c9d-4e8a-8b8f-a66cfc040450"),
            UserName = "username1",
            Name = "firstname1",
            Email = "test1@test.com",
            PhoneNumber = "1234567891",
            IsActive = true
        });
    }

    public static ServiceResponse<UserDto> FailGetUser()
    {
        return new ServiceResponse<UserDto>().Failed();
    }

    public static ServiceResponse<List<UserDto>> OkGetList()
    {
        return new ServiceResponse<List<UserDto>>().Successful().WithData(new List<UserDto>()
        {
            new UserDto()
            {
                Id = Guid.Parse("cbc10c4e-1c9d-4e8a-8b8f-a66cfc040450"),
                UserName = "username1",
                Name = "firstname1",
                Email = "test1@test.com",
                PhoneNumber = "1234567891",
                IsActive = true
            }
        });
    }

    public static ServiceResponse<List<UserDto>> FailGetList()
    {
        return new ServiceResponse<List<UserDto>>().Failed();
    }

    public static ServiceResponse<UserDto> OkUpdateUser()
    {
        return new ServiceResponse<UserDto>().Successful().WithData(new UserDto()
        {
            Id = Guid.Parse("cbc10c4e-1c9d-4e8a-8b8f-a66cfc040450"),
            UserName = "username1",
            Name = "firstname1",
            Email = "test1@test.com",
            PhoneNumber = "1234567891",
            IsActive = true
        });
    }

    public static ServiceResponse<UserDto> FailUpdateUser()
    {
        return new ServiceResponse<UserDto>().Failed();
    }

    public static ServiceResponse<UserDto> OkUpdatePasswordUser()
    {
        return new ServiceResponse<UserDto>().Successful().WithData(new UserDto()
        {
            Id = Guid.Parse("cbc10c4e-1c9d-4e8a-8b8f-a66cfc040450"),
            UserName = "username1",
            Name = "firstname1",
            Email = "test1@test.com",
            PhoneNumber = "1234567891",
            IsActive = true
        });
    }

    public static ServiceResponse<UserDto> FailUpdatePasswordUser()
    {
        return new ServiceResponse<UserDto>().Failed();
    }

    public static ServiceResponse<bool> OkDeleteUser()
    {
        return new ServiceResponse<bool>().Successful().WithData(true);
    }

    public static ServiceResponse<bool> FailDeleteUser()
    {
        return new ServiceResponse<bool>().Failed();
    }

    public static ServiceResponse<UserDto> OkRegister()
    {
        return new ServiceResponse<UserDto>().Successful().WithData(new UserDto()
        {
            Id = Guid.Parse("cbc10c4e-1c9d-4e8a-8b8f-a66cfc040450"),
            UserName = "username1",
            Name = "firstname1",
            Email = "test1@test.com",
            PhoneNumber = "1234567891",
            IsActive = false
        });
    }

    public static ServiceResponse<UserDto> FailRegister()
    {
        return new ServiceResponse<UserDto>().Failed();
    }

    public static ServiceResponse<bool> OkActivateUser()
    {
        return new ServiceResponse<bool>().Successful().WithData(true);
    }

    public static ServiceResponse<bool> FailActivateUser()
    {
        return new ServiceResponse<bool>().Failed();
    }
}