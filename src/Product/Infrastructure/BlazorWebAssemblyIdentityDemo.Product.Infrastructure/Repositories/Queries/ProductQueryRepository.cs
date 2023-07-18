using BlazorWebAssemblyIdentityDemo.Product.Domain.Repositories.Queries;
using BlazorWebAssemblyIdentityDemo.Product.Infrastructure.Data;
using BlazorWebAssemblyIdentityDemo.Product.Infrastructure.Repositories.Queries.Base;
using Microsoft.EntityFrameworkCore;

namespace BlazorWebAssemblyIdentityDemo.Product.Infrastructure.Repositories.Queries
{
    public class ProductQueryRepository : QueryRepository<BlazorWebAssemblyIdentityDemo.Product.Domain.Entities.Product>, IProductQueryRepository
    {
        public ProductQueryRepository(DemoContext context) : base(context)
        {
        }

        public async Task<List<BlazorWebAssemblyIdentityDemo.Product.Domain.Entities.Product>> GetProductByProductCategoryId(int productCategoryId, CancellationToken cancellationToken)
        {
            var products = await _context.Products.Where(x => x.ProductCategoryId == productCategoryId).ToListAsync(cancellationToken);

            return products;
        }
    }
}
