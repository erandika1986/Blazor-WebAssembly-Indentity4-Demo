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
    public class UserQueryRepository : QueryRepository<User>, IUserQueryRepository
    {
        public UserQueryRepository(DemoContext context) : base(context)
        {
        }

        public async Task<User> GetUserByUsername(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName.ToLower().Trim() == username.ToLower().Trim());

            return user;
        }
    }
}
