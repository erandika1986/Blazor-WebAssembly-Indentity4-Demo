using BlazorWebAssemblyIdentityDemo.Domain.Repositories.Commands.Base;
using BlazorWebAssemblyIdentityDemo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebAssemblyIdentityDemo.Infrastructure.Repositories.Commands.Base
{
    public class CommandRepository<T> : ICommandRepository<T> where T : class
    {
        protected readonly DemoContext _context;

        public CommandRepository(DemoContext context)
        {
            this._context = context;
        }
        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
        {
            await _context.Set<T>().AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task DeleteAsync(T entity, CancellationToken cancellationToken)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
