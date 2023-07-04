using BlazorWebAssemblyIdentityDemo.Domain.Entities;
using BlazorWebAssemblyIdentityDemo.Domain.Repositories.Queries.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebAssemblyIdentityDemo.Domain.Repositories.Queries
{
    public interface IProductQueryRepository : IQueryRepository<Product>
    {
        Task<List<Product>> GetProductByProductCategoryId(int  productCategoryId, CancellationToken cancellationToken);
    }
}
