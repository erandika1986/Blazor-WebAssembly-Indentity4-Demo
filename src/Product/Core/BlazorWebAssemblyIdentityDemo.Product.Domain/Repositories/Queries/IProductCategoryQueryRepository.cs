using BlazorWebAssemblyIdentityDemo.Product.Domain.Entities;
using BlazorWebAssemblyIdentityDemo.Product.Domain.Repositories.Queries.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebAssemblyIdentityDemo.Product.Domain.Repositories.Queries
{
    public interface IProductCategoryQueryRepository : IQueryRepository<ProductCategory>
    {
    }
}
