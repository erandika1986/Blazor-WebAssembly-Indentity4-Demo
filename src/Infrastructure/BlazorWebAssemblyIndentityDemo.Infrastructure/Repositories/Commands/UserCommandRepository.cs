using BlazorWebAssemblyIdentityDemo.Domain.Entities;
using BlazorWebAssemblyIdentityDemo.Domain.Repositories.Commands;
using BlazorWebAssemblyIdentityDemo.Infrastructure.Data;
using BlazorWebAssemblyIdentityDemo.Infrastructure.Repositories.Commands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebAssemblyIdentityDemo.Infrastructure.Repositories.Commands
{
    public class UserCommandRepository : CommandRepository<User>, IUserCommandRepository
    {
        public UserCommandRepository(DemoContext context) : base(context)
        {

        }
    }
}
