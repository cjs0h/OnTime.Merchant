using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Merchant.Application.Forms;
using Merchant.Application.Forms.Users;
using Merchant.Application.Interfaces.Shared;
using Merchant.Common.Helpers;
using Merchant.Domain.Entities;
using Merchant.Shared.Services;
using Merchant.Tests;
using Merchant.WebApi.Controllers;
using Merchant.WebApi.Tests.MockData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Xunit;

namespace Merchant.WebApi.Tests
{
    public class AuthControllerTests : TestBase
    {
        
        private AuthController _authController;
        private void ResetMocks(bool fail = false)
        {
            var userService = new Mock<IUserService>();
            if(!fail)
                userService.Setup(x => x.DoLogin(It.IsAny<LoginForm>()))
                    .ReturnsAsync(UserMockData.OkLoginUser());
            else
                userService.Setup(x => x.DoLogin(It.IsAny<LoginForm>()))
                    .ReturnsAsync(UserMockData.FailLoginUser());
            _authController = new AuthController(userService.Object);
        }
        [Fact]
        public async Task Test_Login_Ok()
        {
            // Arrange
            ResetMocks();

            // act
            var result = (ObjectResult)await _authController.Token(new LoginForm());

            // assert
            result.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }
        [Fact]
        public async Task Test_Login_BadRequest()
        {
            // Arrange
            ResetMocks(true);

            // act
            var result = (ObjectResult)await _authController.Token(new LoginForm());

            // assert
            result.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }
    }
}