using BlazorWebAssemblyIdentityDemo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebAssemblyIdentityDemo.Infrastructure.Data
{
    public class DemoContextInitializer
    {
        private readonly ILogger<DemoContextInitializer> _logger;
        private readonly DemoContext _context;

        public DemoContextInitializer(ILogger<DemoContextInitializer> logger, DemoContext context)
        {
            this._logger = logger;
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
                await SeedUsersAndRolesAsync();
                await SeedProductCategoryAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        private async Task SeedUsersAndRolesAsync()
        {
            if (!_context.Roles.Any())
            {
                var adminRole = new Role()
                {
                    Name = "Admin"
                };

                var customerRole = new Role()
                {
                    Name = "Customer"
                };

                _context.Roles.Add(adminRole);
                _context.Roles.Add(customerRole);

                if (!_context.Users.Any())
                {
                    var admin = new User()
                    {
                        Email = "admin@gmail.com",
                        FirstName = "Admin",
                        LastName = "Admin",
                        UserName = "Admin",
                        Password = BCrypt.Net.BCrypt.HashPassword("pass@123")
                    };

                    admin.UserRoles = new List<UserRole>()
                    {
                        new UserRole() { Role = adminRole }
                    };

                    var customer = new User()
                    {
                        Email = "customer@gmail.com",
                        FirstName = "Customer",
                        LastName = "Customer",
                        UserName = "Customer",
                        Password = BCrypt.Net.BCrypt.HashPassword("pass@123")
                    };

                    customer.UserRoles = new List<UserRole>()
                    {
                        new UserRole() { Role = customerRole }
                    };

                    _context.Users.Add(admin);
                    _context.Users.Add(customer);
                }

                await _context.SaveChangesAsync();
            }
        }

        private async Task SeedProductCategoryAsync()
        {
            if (!_context.ProductCategories.Any())
            {
                var productCategories = new List<ProductCategory>()
                {
                    new ProductCategory()
                    {
                        Name = "Chocolate",
                        CreatedById = 1,
                        UpdatedById = 1,
                        CreatedDate = DateTime.Now,
                        UpdateDate = DateTime.Now
                    },
                    new ProductCategory()
                    {
                        Name = "Cake",
                        CreatedById = 1,
                        UpdatedById = 1,
                        CreatedDate = DateTime.Now,
                        UpdateDate = DateTime.Now
                    },
                    new ProductCategory()
                    {
                        Name = "Jam",
                        CreatedById = 1,
                        UpdatedById = 1,
                        CreatedDate = DateTime.Now,
                        UpdateDate = DateTime.Now
                    },
                    new ProductCategory()
                    {
                        Name = "Biscuits",
                        CreatedById = 1,
                        UpdatedById = 1,
                        CreatedDate = DateTime.Now,
                        UpdateDate = DateTime.Now
                    },
                    new ProductCategory()
                    {
                        Name = "Cereals",
                        CreatedById = 1,
                        UpdatedById = 1,
                        CreatedDate = DateTime.Now,
                        UpdateDate = DateTime.Now
                    },
                };

                _context.ProductCategories.AddRange(productCategories);
                await _context.SaveChangesAsync();
            }
        }
    }
}
