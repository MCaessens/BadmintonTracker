using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Imi.Project.Api.Core.Entities;
using Imi.Project.Api.Core.Helpers;
using Imi.Project.Api.Core.Interfaces.Services;
using Imi.Project.Common.Dtos.Accounts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Imi.Project.Api.Core.Services
{
    public class AccountsService : IAccountsService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _context;

        public AccountsService(SignInManager<User> signInManager,
            IConfiguration configuration,
            UserManager<User> userManager,
            IHttpContextAccessor context)
        {
            _signInManager = signInManager;
            _configuration = configuration;
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> LoginAsync(LoginUserRequestDto loginDto)
        {
            var userByUserName = await _userManager.FindByNameAsync(loginDto.UserNameOrEmail);
            var userByEmail = await _userManager.FindByEmailAsync(loginDto.UserNameOrEmail);
            User user;

            if (userByEmail == null && userByUserName == null) return ServiceHelper.BadRequest("E-mail or username was not found in our system.");
            else if (userByEmail != null) user = userByEmail;
            else user = userByUserName;

            var result = await _signInManager.PasswordSignInAsync(user,
                loginDto.Password,
                isPersistent: false,
                lockoutOnFailure: false);
            if (!result.Succeeded) return ServiceHelper.BadRequest("E-mail / username and password combination were incorrect.");

            JwtSecurityToken token = await GenerateTokenAsync(user);

            string serializedToken = new JwtSecurityTokenHandler().WriteToken(token);

            return ServiceHelper.Ok(new LoginUserResponseDto {Token = serializedToken});
        }

        public async Task<IActionResult> RegisterAsync(RegisterUserRequestDto registerDto)
        {
            var newUser = new User
            {
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                DateOfBirth = registerDto.DateOfBirth,
            };

            if (newUser.DateOfBirth >= DateTime.Today) return ServiceHelper.BadRequest("Please select a Date Of Birth that is higher than today.");

            var result = await _userManager.CreateAsync(newUser, registerDto.Password);
            if (!result.Succeeded) ServiceHelper.BadRequest("Registration failed, make sure everything is filled in correctly.");

            newUser = await _userManager.FindByEmailAsync(registerDto.Email);
            await _userManager.AddToRoleAsync(newUser, Constants.UserRoleName);
            await _userManager.AddClaimAsync(newUser,
                new Claim(CustomClaimTypes.RegistrationDate, DateTime.UtcNow.ToString("yy-MM-dd"))
            );
            await _userManager.AddClaimAsync(newUser,
                new Claim(CustomClaimTypes.Email, newUser.Email)
            );
            await _userManager.AddClaimAsync(newUser,
                new Claim(CustomClaimTypes.NameIdentifier, newUser.Id.ToString())
            );
            await _userManager.AddClaimAsync(newUser,
                new Claim(CustomClaimTypes.AccountIntegrityId, newUser.AccountIntegrityId.ToString())
            );

            return ServiceHelper.Ok("Successfully created");
        }

        public async Task<IActionResult> LogoutAsync()
        {
            if (!_context.HttpContext.User.HasClaim(c => c.Type == ClaimTypes.Email)) return ServiceHelper.BadRequest();

            // Setup
            var emailClaim = _context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(emailClaim?.Value);
            var oldAccountIntegrityIdClaim = (await _userManager.GetClaimsAsync(user)).FirstOrDefault(c => c.Type == CustomClaimTypes.AccountIntegrityId);
            var newAccountIntegrityId = Guid.NewGuid();
            var claim = new Claim(CustomClaimTypes.AccountIntegrityId, newAccountIntegrityId.ToString());

            // Replace old integrityId
            await _userManager.ReplaceClaimAsync(user, oldAccountIntegrityIdClaim, claim);
            user.AccountIntegrityId = newAccountIntegrityId;
            await _userManager.UpdateAsync(user);
            await _signInManager.SignOutAsync();
            return ServiceHelper.Ok("You have been successfully logged out.");
        }

        private async Task<JwtSecurityToken> GenerateTokenAsync(User user)
        {
            var claims = new List<Claim>();

            // Loading the user Claims
            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            // Loading the roles and put them in a claim of a Role ClaimType
            var roleClaims = await _userManager.GetRolesAsync(user);
            claims.AddRange(roleClaims.Select(roleClaim => new Claim(CustomClaimTypes.Role, roleClaim)));
            var expirationDays = _configuration.GetValue<int>("JWTConfiguration:TokenExpirationDays");
            var signingKey = Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JWTConfiguration:SigningKey"));
            var token = new JwtSecurityToken
            (
                issuer: _configuration.GetValue<string>("JWTConfiguration:Issuer"),
                audience: _configuration.GetValue<string>("JWTConfiguration:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromDays(expirationDays)),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(signingKey),
                    SecurityAlgorithms.HmacSha256)
            );
            return token;
        }
    }
}