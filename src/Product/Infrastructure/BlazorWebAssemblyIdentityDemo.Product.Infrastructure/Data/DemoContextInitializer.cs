using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BlazorWebAssemblyIdentityDemo.Product.Infrastructure.Data
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

            
        }

        private async Task SeedProductCategoryAsync()
        {
            //if (!_context.ProductCategories.Any())
            //{
               
            //    var productCategories = new List<ProductCategory>()
            //    {
            //        new ProductCategory()
            //        {
            //            Name = "Chocolate",
            //            CreatedById = 1,
            //            UpdatedById = 1,
            //            CreatedDate = DateTime.Now,
            //            UpdateDate = DateTime.Now
            //        },
            //        new ProductCategory()
            //        {
            //            Name = "Cake",
            //            CreatedById = 1,
            //            UpdatedById = 1,
            //            CreatedDate = DateTime.Now,
            //            UpdateDate = DateTime.Now
            //        },
            //        new ProductCategory()
            //        {
            //            Name = "Jam",
            //            CreatedById = 1,
            //            UpdatedById = 1,
            //            CreatedDate = DateTime.Now,
            //            UpdateDate = DateTime.Now
            //        },
            //        new ProductCategory()
            //        {
            //            Name = "Biscuits",
            //            CreatedById = 1,
            //            UpdatedById = 1,
            //            CreatedDate = DateTime.Now,
            //            UpdateDate = DateTime.Now
            //        },
            //        new ProductCategory()
            //        {
            //            Name = "Cereals",
            //            CreatedById = 1,
            //            UpdatedById = 1,
            //            CreatedDate = DateTime.Now,
            //            UpdateDate = DateTime.Now
            //        },
            //    };

            //    _context.ProductCategories.AddRange(productCategories);
            //    await _context.SaveChangesAsync();
            //}
        }
    }
}
