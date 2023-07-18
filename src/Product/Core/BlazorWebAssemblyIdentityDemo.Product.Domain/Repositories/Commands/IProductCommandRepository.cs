using BlazorWebAssemblyIdentityDemo.Product.Domain.Entities;
using BlazorWebAssemblyIdentityDemo.Product.Domain.Repositories.Commands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebAssemblyIdentityDemo.Product.Domain.Repositories.Commands
{
    public interface IProductCommandRepository : ICommandRepository<BlazorWebAssemblyIdentityDemo.Product.Domain.Entities.Product>
    {
    }
}
