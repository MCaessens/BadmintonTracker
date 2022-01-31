using Imi.Project.Api.Core.Entities;
using Imi.Project.Api.Core.Interfaces.Services;
using Imi.Project.Common.Dtos.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Imi.Project.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAccountsService _accountsService;

        public AuthController(IAccountsService accountsService)
        {
            _accountsService = accountsService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserRequestDto registration)
        {
            var response = await _accountsService.RegisterAsync(registration);
            return response;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserRequestDto login)
        {
            var response = await _accountsService.LoginAsync(login);
            return response;
        }
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var response = await _accountsService.LogoutAsync();
            return response;
        }

    }
}
