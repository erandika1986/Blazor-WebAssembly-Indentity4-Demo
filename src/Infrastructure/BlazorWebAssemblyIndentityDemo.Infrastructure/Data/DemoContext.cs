using BlazorWebAssemblyIdentityDemo.Application.Contracts;
using BlazorWebAssemblyIdentityDemo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Diagnostics;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorWebAssemblyIdentityDemo.Infrastructure.Interceptors;

namespace BlazorWebAssemblyIdentityDemo.Infrastructure.Data
{
    public class DemoContext : DbContext, IDemoContext
    {
        private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;
        private IDbContextTransaction _dbContextTransaction;

        public DemoContext(DbContextOptions<DemoContext> options,
            AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor) 
            : base(options)
        {
            this._auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
        }


        public DbSet<ProductCategory> ProductCategories => Set<ProductCategory>();

        public DbSet<Product> Products => Set<Product>();

        public DbSet<User> Users => Set<User>();

        public DbSet<Role> Roles => Set<Role>();

        public DbSet<UserRole> UserRoles => Set<UserRole>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(message => Debug.WriteLine(message))
                .EnableSensitiveDataLogging();
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken)
        {
            _dbContextTransaction ??= await Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken)
        {
            try
            {
                await SaveChangesAsync(cancellationToken);
                _dbContextTransaction?.CommitAsync(cancellationToken);
            }
            catch
            {
                await RollbackTransactionAsync(cancellationToken);
                throw;
            }
            finally
            {
                if (_dbContextTransaction != null)
                {
                    DisposeTransaction();
                }
            }
        }

        public async Task RetryOnExceptionAsync(Func<Task> func)
        {
            await Database.CreateExecutionStrategy().ExecuteAsync(func);
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken)
        {
            try
            {
                await _dbContextTransaction?.RollbackAsync(cancellationToken);
            }
            finally
            {
                DisposeTransaction();
            }
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void DisposeTransaction()
        {
            if (_dbContextTransaction != null)
            {
                _dbContextTransaction.Dispose();
                _dbContextTransaction = null;
            }
        }
    }
}
