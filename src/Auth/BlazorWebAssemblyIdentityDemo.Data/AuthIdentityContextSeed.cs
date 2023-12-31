﻿using BlazorWebAssemblyIdentityDemo.OAuth.Model;
using BlazorWebAssemblyIdentityDemo.Shared.Enums;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebAssemblyIdentityDemo.OAuth.Data
{
    public class AuthIdentityContextSeed
    {
        private readonly ILogger<AuthIdentityContextSeed> _logger;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly AuthIdentityContext _context;

        public AuthIdentityContextSeed(
            ILogger<AuthIdentityContextSeed> logger, 
            UserManager<User> userManager, 
            RoleManager<Role> roleManager,
            AuthIdentityContext context)
        {
            this._logger = logger;
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._context = context;

        }

        public async Task InitializeAsync()
        {
            try
            {
                if (_context.Database.IsSqlServer())
                {
                    await _context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initializing the database.");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await SeedUsersAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        private async Task SeedUsersAsync()
        {
            var adminUser = await _userManager.FindByNameAsync("Erandika");

            if(adminUser == null)
            {
                adminUser = new User
                {
                    UserName = "Erandika",
                    Email = "erandika1986@gmail.com",
                    EmailConfirmed = true,
                    FirstName = "Erandika",
                    LastName = "Sandaruwan",
                    Position = Position.CEO
                    //RefreshToken = string.Empty,
                    //RefreshTokenExpiryTime = DateTime.UtcNow

                };

                var adminRole = await _roleManager.FindByNameAsync("Administrator");

                var result = _userManager.CreateAsync(adminUser, "Pass123$").Result;

                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                if (adminRole != null)
                {
                    await _userManager.AddToRoleAsync(adminUser, adminRole.Name);
                }

                result = _userManager.AddClaimsAsync(adminUser, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Admin"),
                            new Claim(JwtClaimTypes.GivenName, "Admin"),
                            new Claim(JwtClaimTypes.FamilyName, "Admin"),
                            new Claim(JwtClaimTypes.WebSite, "http://admin.com"),
                            new Claim(JwtClaimTypes.Role, adminRole.Name),
                            new Claim("position", adminRole.Name),
                            new Claim("country", "USA"),
                            new Claim("subject", adminUser.Id),
                        }).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
                _logger.LogDebug("Admin");
            }
        }


    }
}
