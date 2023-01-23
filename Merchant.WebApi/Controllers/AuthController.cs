using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Merchant.Application.Forms;
using Merchant.Application.Forms.Users;
using Merchant.Application.Interfaces.Shared;
using Merchant.Common.Const;
using Merchant.Common.Extensions;
using Merchant.Common.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Merchant.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _service;

        public AuthController(IUserService service)
        {
            _service = service;
        }
        [ActionName("Token"), AllowAnonymous,
         HttpPost, ProducesResponseType(type: typeof(ClientResponse<string>), statusCode: 200),
         ProducesResponseType(type: typeof(ClientResponse<string>), statusCode: 400)]
        public async Task<IActionResult> Token([FromBody] LoginForm form)
        {
            if (!ModelState.IsValid)
                return BadRequest(
                    error: new ClientResponse<string>(
                        data: "please provide username and password"));

            var serviceResponse = await _service.DoLogin(form: form);
            if (serviceResponse.Failed || serviceResponse.Data is null)
                return BadRequest(new ClientResponse<string>(true, serviceResponse.MessageWithErrors));

            var claims = new List<Claim> {
                new Claim(type: JwtRegisteredClaimNames.Sub,
                    value: serviceResponse.Data.Id.ToString()),
                new Claim(type: JwtRegisteredClaimNames.Email, value: serviceResponse.Data.Email),
                new Claim(type: ClaimTypes.Name, value: $"{serviceResponse.Data.FirstName} {serviceResponse.Data.LastName}"),
                new Claim(type: "id", value: serviceResponse.Data.Id.ToString()),
                new Claim(type: "Role", value: ((Enums.UserRole)serviceResponse.Data.Role).GetDisplayName()),
                new Claim(type: JwtRegisteredClaimNames.Jti, value: Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(claims: claims,
                expires: DateTime.UtcNow.AddDays(value: 3), notBefore: DateTime.UtcNow,
                audience: "Audience", issuer: "Issuer",
                signingCredentials: new SigningCredentials(
                    key: new SymmetricSecurityKey(
                        key: Encoding.UTF8.GetBytes(s: "dmiWqigAEvWmCq5TgJLhuHvByNY5IonA")),
                    algorithm: SecurityAlgorithms.HmacSha256));
            return Ok(value: new { token = new JwtSecurityTokenHandler().WriteToken(token: token) });
        }
    }
}
