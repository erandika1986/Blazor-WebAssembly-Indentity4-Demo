using BlazorWebAssemblyIdentityDemo.Product.Domain.Entities;
using BlazorWebAssemblyIdentityDemo.Product.Domain.Repositories.Queries;
using BlazorWebAssemblyIdentityDemo.Product.Infrastructure.Data;
using BlazorWebAssemblyIdentityDemo.Product.Infrastructure.Repositories.Queries.Base;

namespace BlazorWebAssemblyIdentityDemo.Product.Infrastructure.Repositories.Queries
{
    public class ProductCategoryQueryRepository : QueryRepository<ProductCategory>, IProductCategoryQueryRepository
    {
        public ProductCategoryQueryRepository(DemoContext context) : base(context)
        {
        }
    }
}
