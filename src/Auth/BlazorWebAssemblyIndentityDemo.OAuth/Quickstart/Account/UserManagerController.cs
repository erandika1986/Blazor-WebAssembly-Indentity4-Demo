﻿using BlazorWebAssemblyIdentityDemo.OAuth.Model;
using BlazorWebAssemblyIdentityDemo.Shared.DTO.Common;
using BlazorWebAssemblyIdentityDemo.Shared.DTO.User;
using BlazorWebAssemblyIdentityDemo.Shared.Helper;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static Dynami.IdentityServer4.IdentityServerConstants;

namespace BlazorWebAssemblyIdentityDemo.OAuth.Quickstart.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagerController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public UserManagerController(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        [HttpPost("Registration")]
        [Authorize]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {
            if (userForRegistration == null || !ModelState.IsValid)
                return BadRequest();

            var user = new User
            {
                FirstName = userForRegistration.FirstName,
                LastName = userForRegistration.LastName,
                UserName = userForRegistration.Email,
                Email = userForRegistration.Email,
                Position = userForRegistration.Position,
                EmailConfirmed = true
            };

            var claims = new List<Claim>()
            {
                new Claim(JwtClaimTypes.Name, $"{userForRegistration.FirstName} {userForRegistration.LastName}"),
                new Claim(JwtClaimTypes.GivenName, userForRegistration.FirstName),
                new Claim(JwtClaimTypes.FamilyName, userForRegistration.LastName),
                new Claim(JwtClaimTypes.WebSite, "http://demo.com"),
                new Claim("position", EnumHelper.GetEnumDescription(userForRegistration.Position)),
                new Claim("country", userForRegistration.Country)
            };

            var result = await _userManager.CreateAsync(user, userForRegistration.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return BadRequest(ResponseDto.Failure(errors));
            }

            foreach (var roleId in userForRegistration.AssignedRoleIds)
            {
                var role = await _roleManager.FindByIdAsync(roleId);

                claims.Add(new Claim(JwtClaimTypes.Role, role.Name));

                await _userManager.AddToRoleAsync(user, role.Name);
            }

            result = _userManager.AddClaimsAsync(user, claims).Result;

            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            return StatusCode(201);
        }

        [HttpGet("unauthorized")]
        public IActionResult UnauthorizedTestAction() =>
            Ok(new { Message = "Access is allowed for unauthorized users" });


        [HttpGet]
        [Route("privacy")]
        public IEnumerable<string> Privacy()
        {
            var claims = User.Claims.Select(c => $"{c.Type}: {c.Value}").ToList();

            return claims;
        }
    }
}
