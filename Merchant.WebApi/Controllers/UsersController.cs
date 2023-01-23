using Merchant.Application.DTOs.Users;
using Merchant.Application.Forms;
using Merchant.Application.Forms.Users;
using Merchant.Application.Interfaces.Shared;
using Merchant.Common.Const;
using Merchant.Common.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Merchant.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("Merchant")]
    [ProducesResponseType(typeof(ClientResponse<UserDto>), 200)]
    [ProducesResponseType(typeof(ClientResponse<string>), 400)]
    public async Task<IActionResult> AddMerchant(RegisterForm form)
    {
        var serviceResponse = await _userService.Register(form, Enums.UserRole.User);
        if (serviceResponse.Failed || serviceResponse.Data is null)
        {
            return BadRequest(new ClientResponse<string>(true, serviceResponse.MessageWithErrors));
        }

        return Ok(new ClientResponse<UserDto>(serviceResponse.Data, serviceResponse.ItemsCount));
    }

    [HttpGet]
    [ProducesResponseType(typeof(ClientResponse<List<UserDto>>), 200)]
    [ProducesResponseType(typeof(ClientResponse<string>), 400)]
    public async Task<IActionResult> Get([FromQuery] GetListBaseForm form)
    {
        var serviceResponse = await _userService.GetList(form);
        if (serviceResponse.Failed || serviceResponse.Data is null)
        {
            return BadRequest(new ClientResponse<string>(true, serviceResponse.MessageWithErrors));
        }

        return Ok(new ClientResponse<List<UserDto>>(serviceResponse.Data, serviceResponse.ItemsCount));
    }

    [HttpGet("{id:Guid}")]
    [ProducesResponseType(typeof(ClientResponse<UserDto>), 200)]
    [ProducesResponseType(typeof(ClientResponse<string>), 400)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var serviceResponse = await _userService.GetUser(id);
        if (serviceResponse.Failed || serviceResponse.Data is null)
        {
            return BadRequest(new ClientResponse<string>(true, serviceResponse.MessageWithErrors));
        }

        return Ok(new ClientResponse<UserDto>(serviceResponse.Data, serviceResponse.ItemsCount));
    }

    [HttpPut("{id:Guid}")]
    [ProducesResponseType(typeof(ClientResponse<UserDto>), 200)]
    [ProducesResponseType(typeof(ClientResponse<string>), 400)]
    public async Task<IActionResult> Update(Guid id, UpdateUserForm form)
    {
        var serviceResponse = await _userService.Update(id, form);
        if (serviceResponse.Failed || serviceResponse.Data is null)
        {
            return BadRequest(new ClientResponse<string>(true, serviceResponse.MessageWithErrors));
        }

        return Ok(new ClientResponse<UserDto>(serviceResponse.Data, serviceResponse.ItemsCount));
    }

    [HttpPatch("{id:Guid}")]
    [ProducesResponseType(typeof(ClientResponse<UserDto>), 200)]
    [ProducesResponseType(typeof(ClientResponse<string>), 400)]
    public async Task<IActionResult> UpdatePassword(Guid id, UpdatePasswordForm form)
    {
        var serviceResponse = await _userService.UpdatePassword(id, form);
        if (serviceResponse.Failed || serviceResponse.Data is null)
        {
            return BadRequest(new ClientResponse<string>(true, serviceResponse.MessageWithErrors));
        }

        return Ok(new ClientResponse<UserDto>(serviceResponse.Data, serviceResponse.ItemsCount));
    }

    [HttpPatch("{id:Guid}/Activate")]
    [ProducesResponseType(typeof(ClientResponse<UserDto>), 200)]
    [ProducesResponseType(typeof(ClientResponse<string>), 400)]
    public async Task<IActionResult> ActivateUser(Guid id)
    {
        var serviceResponse = await _userService.ActivateUser(id);
        if (serviceResponse.Failed || serviceResponse.Data is false)
        {
            return BadRequest(new ClientResponse<string>(true, serviceResponse.MessageWithErrors));
        }

        return Ok(new ClientResponse<bool>(serviceResponse.Data, serviceResponse.ItemsCount));
    }

    [HttpDelete("{id:Guid}")]
    [ProducesResponseType(typeof(ClientResponse<bool>), 200)]
    [ProducesResponseType(typeof(ClientResponse<string>), 400)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var serviceResponse = await _userService.Delete(id);
        if (serviceResponse.Failed || serviceResponse.Data is false)
        {
            return BadRequest(new ClientResponse<string>(true, serviceResponse.MessageWithErrors));
        }

        return Ok(new ClientResponse<bool>(serviceResponse.Data, serviceResponse.ItemsCount));
    }
}