using BlazorWebAssemblyIdentityDemo.Product.Domain.Entities;
using BlazorWebAssemblyIdentityDemo.Product.Domain.Repositories.Commands;
using BlazorWebAssemblyIdentityDemo.Product.Infrastructure.Data;
using BlazorWebAssemblyIdentityDemo.Product.Infrastructure.Repositories.Commands.Base;

namespace BlazorWebAssemblyIdentityDemo.Product.Infrastructure.Repositories.Commands
{
    public class ProductCategoryCommandRepository : CommandRepository<ProductCategory>, IProductCategoryCommandRepository
    {
        public ProductCategoryCommandRepository(DemoContext context) : base(context)
        {
            
        }
    }
}
