using BlazorWebAssemblyIdentityDemo.Product.Domain.Repositories.Commands;
using BlazorWebAssemblyIdentityDemo.Product.Infrastructure.Data;
using BlazorWebAssemblyIdentityDemo.Product.Infrastructure.Repositories.Commands.Base;

namespace BlazorWebAssemblyIdentityDemo.Product.Infrastructure.Repositories.Commands
{
    public class ProductCommandRepository : CommandRepository<BlazorWebAssemblyIdentityDemo.Product.Domain.Entities.Product>, IProductCommandRepository
    {
        public ProductCommandRepository(DemoContext context) :base(context)
        {
            
        }
    }
}
