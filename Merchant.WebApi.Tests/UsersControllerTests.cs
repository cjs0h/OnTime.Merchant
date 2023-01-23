using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Merchant.Application.Forms;
using Merchant.Application.Forms.Users;
using Merchant.Application.Interfaces.Shared;
using Merchant.Common.Const;
using Merchant.WebApi.Controllers;
using Merchant.WebApi.Tests.MockData;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Merchant.WebApi.Tests;

public class UsersControllerTests
{
    private UsersController _usersController;

    private void ResetMocks(bool fail = false)
    {
        var userService = new Mock<IUserService>();
        if (!fail)
        {
            userService.Setup(x => x.GetUser(It.IsAny<Guid>()))
                .ReturnsAsync(UserMockData.OkGetUser());
            userService.Setup(x => x.GetList(It.IsAny<GetListBaseForm>()))
                .ReturnsAsync(UserMockData.OkGetList());
            userService.Setup(x => x.Update(It.IsAny<Guid>(), It.IsAny<UpdateUserForm>()))
                .ReturnsAsync(UserMockData.OkUpdateUser());
            userService.Setup(x => x.UpdatePassword(It.IsAny<Guid>(), It.IsAny<UpdatePasswordForm>()))
                .ReturnsAsync(UserMockData.OkUpdatePasswordUser());
            userService.Setup(x => x.Delete(It.IsAny<Guid>()))
                .ReturnsAsync(UserMockData.OkDeleteUser());
            userService.Setup(x => x.ActivateUser(It.IsAny<Guid>()))
                .ReturnsAsync(UserMockData.OkActivateUser());
            userService.Setup(x => x.Register(It.IsAny<RegisterForm>(), It.IsAny<Enums.UserRole>()))
                .ReturnsAsync(UserMockData.OkRegister());
        }
        else
        {
            userService.Setup(x => x.GetUser(It.IsAny<Guid>()))
                .ReturnsAsync(UserMockData.FailGetUser());
            userService.Setup(x => x.GetList(It.IsAny<GetListBaseForm>()))
                .ReturnsAsync(UserMockData.FailGetList());
            userService.Setup(x => x.Update(It.IsAny<Guid>(), It.IsAny<UpdateUserForm>()))
                .ReturnsAsync(UserMockData.FailUpdateUser());
            userService.Setup(x => x.UpdatePassword(It.IsAny<Guid>(), It.IsAny<UpdatePasswordForm>()))
                .ReturnsAsync(UserMockData.FailUpdatePasswordUser());
            userService.Setup(x => x.Delete(It.IsAny<Guid>()))
                .ReturnsAsync(UserMockData.FailDeleteUser());
            userService.Setup(x => x.ActivateUser(It.IsAny<Guid>()))
                .ReturnsAsync(UserMockData.FailActivateUser());
            userService.Setup(x => x.Register(It.IsAny<RegisterForm>(), It.IsAny<Enums.UserRole>()))
                .ReturnsAsync(UserMockData.FailRegister());
        }

        _usersController = new UsersController(userService.Object);
    }

    [Fact]
    public async Task Test_GetById_Ok()
    {
        // Arrange
        ResetMocks();

        // act
        var result = (ObjectResult)await _usersController.GetById(Guid.NewGuid());

        // assert
        result.StatusCode.Should().Be((int)HttpStatusCode.OK);
    }

    [Fact]
    public async Task Test_GetById_BadRequest()
    {
        // Arrange
        ResetMocks(true);

        // act
        var result = (ObjectResult)await _usersController.GetById(Guid.Empty);

        // assert
        result.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Test_GetList_Ok()
    {
        // Arrange
        ResetMocks();

        // act
        var result = (ObjectResult)await _usersController.Get(new GetListBaseForm());

        // assert
        result.StatusCode.Should().Be((int)HttpStatusCode.OK);
    }

    [Fact]
    public async Task Test_GetList_BadRequest()
    {
        // Arrange
        ResetMocks(true);

        // act
        var result = (ObjectResult)await _usersController.Get(new GetListBaseForm());

        // assert
        result.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Test_Update_Ok()
    {
        // Arrange
        ResetMocks();

        // act
        var result = (ObjectResult)await _usersController.Update(Guid.NewGuid(), new UpdateUserForm());

        // assert
        result.StatusCode.Should().Be((int)HttpStatusCode.OK);
    }

    [Fact]
    public async Task Test_Update_BadRequest()
    {
        // Arrange
        ResetMocks(true);

        // act
        var result = (ObjectResult)await _usersController.Update(Guid.Empty, new UpdateUserForm());

        // assert
        result.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Test_UpdatePassword_Ok()
    {
        // Arrange
        ResetMocks();

        // act
        var result = (ObjectResult)await _usersController.UpdatePassword(Guid.NewGuid(), new UpdatePasswordForm());

        // assert
        result.StatusCode.Should().Be((int)HttpStatusCode.OK);
    }

    [Fact]
    public async Task Test_UpdatePassword_BadRequest()
    {
        // Arrange
        ResetMocks(true);

        // act
        var result = (ObjectResult)await _usersController.UpdatePassword(Guid.Empty, new UpdatePasswordForm());

        // assert
        result.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Test_DeleteUser_Ok()
    {
        // Arrange
        ResetMocks();

        // act
        var result = (ObjectResult)await _usersController.Delete(Guid.NewGuid());

        // assert
        result.StatusCode.Should().Be((int)HttpStatusCode.OK);
    }

    [Fact]
    public async Task Test_DeleteUser_BadRequest()
    {
        // Arrange
        ResetMocks(true);

        // act
        var result = (ObjectResult)await _usersController.Delete(Guid.Empty);

        // assert
        result.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Test_ActivateUser_Ok()
    {
        // Arrange
        ResetMocks();

        // act
        var result = (ObjectResult)await _usersController.ActivateUser(Guid.NewGuid());

        // assert
        result.StatusCode.Should().Be((int)HttpStatusCode.OK);
    }

    [Fact]
    public async Task Test_ActivateUser_BadRequest()
    {
        // Arrange
        ResetMocks(true);

        // act
        var result = (ObjectResult)await _usersController.ActivateUser(Guid.Empty);

        // assert
        result.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Test_AddMerchant_Ok()
    {
        // Arrange
        ResetMocks();

        // act
        var result = (ObjectResult)await _usersController.AddMerchant(new RegisterForm());

        // assert
        result.StatusCode.Should().Be((int)HttpStatusCode.OK);
    }

    [Fact]
    public async Task Test_AddMerchant_BadRequest()
    {
        // Arrange
        ResetMocks(true);

        // act
        var result = (ObjectResult)await _usersController.AddMerchant(new RegisterForm());

        // assert
        result.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
    }
}