using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Merchant.Application.Forms;
using Merchant.Application.Forms.Users;
using Merchant.Application.Interfaces.Shared;
using Merchant.Common.Const;
using Merchant.Common.Extensions;
using Merchant.Domain.Entities;
using Merchant.Shared.Services;
using Merchant.Tests;
using Xunit;

namespace Merchant.Shared.Tests.Services
{
    public class UserServiceTests : TestBase
    {
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userService = (UserService)ServiceProvider.GetService(typeof(IUserService))!;
        }

        [Theory]
        [InlineData("", "", false)] // invalid username or password
        [InlineData("username1", "password1", true)]
        [InlineData("username1", "password2", false)] // user is not active
        public async Task UserLogin(string username, string password, bool expected)
        {
            // act
            AddUserData(Context);
            var form = new LoginForm() { UserName = username, Password = password };
            var actual = await _userService.DoLogin(form);

            // Assert
            Assert.Equal(expected, actual.Succeeded);
        }

        [Fact]
        public async Task GetUserReturnTrue()
        {
            // act
            AddUserData(Context);
            const string expected = "1234567891";
            var actual = await _userService.GetUser(Guid.Parse("cbc10c4e-1c9d-4e8a-8b8f-a66cfc040450"));

            // Assert
            Assert.NotNull(actual.Data);
            Assert.Equal(expected, actual.Data.PhoneNumber);
        }

        [Fact]
        public async Task GetUserReturnFalse()
        {
            // act
            AddUserData(Context);
            const int expected = 1;
            var actual = await _userService.GetUser(Guid.NewGuid());

            // Assert
            Assert.NotNull(actual.Errors);
            Assert.Equal(expected, actual.Errors.First().ErrorCode);
        }

        [Theory]
        [InlineData(0, 10, 4)]
        [InlineData(10, 10, 0)]
        public async Task GetList(int start, int take, int expected)
        {
            // act
            AddUserData(Context);
            var form = new GetListBaseForm() { Start = start, Take = take };
            var actual = await _userService.GetList(form);

            // Assert
            Assert.Equal(expected, actual.Data?.Count);
            Assert.True(actual.Succeeded);
        }

        [Fact]
        public async Task UpdateUserReturnTrue()
        {
            // act
            AddUserData(Context);
            var expected = new DateTime(1996, 1, 1).ToUniversalTime();
            var form = new UpdateUserForm()
            {
                FirstName = "Ahmed",
                DateOfBirth = new DateTime(1996, 1, 1)
            };
            var actual = await _userService.Update(Guid.Parse("cbc10c4e-1c9d-4e8a-8b8f-a66cfc040450"), form);

            // Assert
            Assert.True(actual.Succeeded);
            Assert.NotNull(actual.Data);
            Assert.Equal(expected, actual.Data?.DateOfBirth);
        }

        [Fact]
        public async Task UpdateUser_InvalidId_ReturnFalse()
        {
            // act
            AddUserData(Context);
            var actual = await _userService.Update(Guid.NewGuid(), new UpdateUserForm());

            // Assert
            Assert.True(actual.Failed);
            Assert.NotNull(actual.Errors?.Any());
        }

        [Fact]
        public async Task UpdatePasswordReturnTrue()
        {
            // act
            AddUserData(Context);
            var expected = "pass1".HashPassword();
            var form = new UpdatePasswordForm()
            {
                OldPassword = "password1",
                NewPassword = "pass1",
                NewPasswordConfirm = "pass1"
            };
            var actual = await _userService.UpdatePassword(Guid.Parse("cbc10c4e-1c9d-4e8a-8b8f-a66cfc040450"), form);

            // Assert
            Assert.True(actual.Succeeded);
            Assert.NotNull(actual.Data);
        }

        [Fact]
        public async Task UpdatePassword_InvalidId_ReturnFalse()
        {
            // act
            AddUserData(Context);
            var form = new UpdatePasswordForm()
            {
                OldPassword = "password1",
                NewPassword = "pass1",
                NewPasswordConfirm = "pass1"
            };
            var actual = await _userService.UpdatePassword(Guid.NewGuid(), form);

            // Assert
            Assert.True(actual.Failed);
            Assert.NotNull(actual.Errors?.Any());
        }

        [Fact]
        public async Task UpdatePassword_InvalidOldPassword_ReturnFalse()
        {
            // act
            AddUserData(Context);
            var expected = "pass1".HashPassword();
            var form = new UpdatePasswordForm()
            {
                OldPassword = "invalid",
                NewPassword = "pass1",
                NewPasswordConfirm = "pass1"
            };
            var actual = await _userService.UpdatePassword(Guid.Parse("cbc10c4e-1c9d-4e8a-8b8f-a66cfc040450"), form);

            // Assert
            Assert.True(actual.Failed);
            Assert.NotNull(actual.Errors?.Any());
        }

        [Fact]
        public async Task DeleteUserReturnTrue()
        {
            // act
            AddUserData(Context);
            const bool expected = true;
            var actual = await _userService.Delete(Guid.Parse("cbc10c4e-1c9d-4e8a-8b8f-a66cfc040450"));

            // Assert
            Assert.True(actual.Succeeded);
            Assert.Equal(expected, actual.Data);
        }

        [Fact]
        public async Task DeleteUserReturnFalse()
        {
            // act
            AddUserData(Context);
            const bool expected = false;
            var actual = await _userService.Delete(Guid.Empty);

            // Assert
            Assert.True(actual.Failed);
            Assert.Equal(expected, actual.Data);
        }

        [Fact]
        public async Task AddMerchant_ReturnTrue()
        {
            // act
            AddUserData(Context);
            const bool expected = false;
            var form = new RegisterForm()
            {
                DateOfBirth = new DateTime(2000, 1, 1),
                Email = "A@b.c",
                EmailConfirm = "A@b.c",
                FirstName = "a",
                LastName = "j",
                UserName = "ahmed",
                Password = "1123456",
                PasswordConfirm = "1123456",
                PhoneNumber = "07812345678",
                Gender = Enums.Gender.Male,
            };
            var actual = await _userService.Register(form, Enums.UserRole.User);

            // Assert
            Assert.True(actual.Succeeded);
            Assert.Equal(expected, actual.Data?.IsActive);
        }

        [Fact]
        public async Task ActivateUser_ReturnTrue()
        {
            // act
            AddUserData(Context);
            var actual = await _userService.ActivateUser(Guid.Parse("ebc10b4e-1c9d-4e0a-8b8f-a66cfc010451"));

            // Assert
            Assert.True(actual.Succeeded);
            Assert.True(actual.Data);
        }

        [Fact]
        public async Task ActivateUser_InvalidId_ReturnFalse()
        {
            // act
            AddUserData(Context);
            var actual = await _userService.ActivateUser(Guid.NewGuid());

            // Assert
            Assert.True(actual.Failed);
            Assert.NotNull(actual.Errors?.Any());
            Assert.Equal(actual.Errors?.First().ErrorCode, Constants.Error.UserNotFound.Item1);
        }

        [Fact]
        public async Task ActivateUser_Activated_ReturnFalse()
        {
            // act
            AddUserData(Context);
            var actual = await _userService.ActivateUser(Guid.Parse("cbc10c4e-1c9d-4e8a-8b8f-a66cfc040450"));

            // Assert
            Assert.True(actual.Failed);
            Assert.NotNull(actual.Errors?.Any());
            Assert.Equal(actual.Errors?.First().ErrorCode, Constants.Error.UserAlreadyActivated.Item1);
        }
    }
}