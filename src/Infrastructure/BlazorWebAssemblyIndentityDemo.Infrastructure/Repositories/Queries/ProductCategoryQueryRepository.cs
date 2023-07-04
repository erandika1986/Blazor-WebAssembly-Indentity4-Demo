using BlazorWebAssemblyIdentityDemo.Domain.Entities;
using BlazorWebAssemblyIdentityDemo.Domain.Repositories.Queries;
using BlazorWebAssemblyIdentityDemo.Infrastructure.Data;
using BlazorWebAssemblyIdentityDemo.Infrastructure.Repositories.Queries.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebAssemblyIdentityDemo.Infrastructure.Repositories.Queries
{
    public class ProductCategoryQueryRepository : QueryRepository<ProductCategory>, IProductCategoryQueryRepository
    {
        public ProductCategoryQueryRepository(DemoContext context) : base(context)
        {
        }
    }
}
