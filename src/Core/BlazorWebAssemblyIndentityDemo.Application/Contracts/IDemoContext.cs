using BlazorWebAssemblyIdentityDemo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebAssemblyIdentityDemo.Application.Contracts
{
    public interface IDemoContext
    {
        DbSet<User> Users { get; }
        DbSet<Role> Roles { get; }
        DbSet<UserRole> UserRoles { get; }
        DbSet<ProductCategory> ProductCategories { get; }
        DbSet<Product> Products { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task BeginTransactionAsync(CancellationToken cancellationToken);
        Task CommitTransactionAsync(CancellationToken cancellationToken);
        Task RollbackTransactionAsync(CancellationToken cancellationToken);
        Task RetryOnExceptionAsync(Func<Task> func);
    }
}
