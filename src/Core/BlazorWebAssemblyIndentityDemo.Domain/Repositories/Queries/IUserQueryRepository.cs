using BlazorWebAssemblyIdentityDemo.Domain.Repositories.Queries.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebAssemblyIdentityDemo.Domain.Repositories.Queries
{
    public interface IUserQueryRepository : IQueryRepository<BlazorWebAssemblyIdentityDemo.Domain.Entities.User>
    {
        Task<BlazorWebAssemblyIdentityDemo.Domain.Entities.User> GetUserByUsername(string username);
    }
}
