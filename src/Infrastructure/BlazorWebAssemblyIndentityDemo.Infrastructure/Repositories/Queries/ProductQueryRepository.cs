using BlazorWebAssemblyIdentityDemo.Domain.Entities;
using BlazorWebAssemblyIdentityDemo.Domain.Repositories.Queries;
using BlazorWebAssemblyIdentityDemo.Infrastructure.Data;
using BlazorWebAssemblyIdentityDemo.Infrastructure.Repositories.Queries.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebAssemblyIdentityDemo.Infrastructure.Repositories.Queries
{
    public class ProductQueryRepository : QueryRepository<Product>, IProductQueryRepository
    {
        public ProductQueryRepository(DemoContext context) : base(context)
        {
        }

        public async Task<List<Product>> GetProductByProductCategoryId(int productCategoryId, CancellationToken cancellationToken)
        {
            var products = await _context.Products.Where(x => x.ProductCategoryId == productCategoryId).ToListAsync(cancellationToken);

            return products;
        }
    }
}
