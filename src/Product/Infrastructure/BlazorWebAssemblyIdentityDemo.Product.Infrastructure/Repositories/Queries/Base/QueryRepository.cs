using BlazorWebAssemblyIdentityDemo.Product.Domain.Repositories.Queries.Base;
using BlazorWebAssemblyIdentityDemo.Product.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BlazorWebAssemblyIdentityDemo.Product.Infrastructure.Repositories.Queries.Base
{
    public class QueryRepository<T> : IQueryRepository<T> where T : class
    {
        protected readonly DemoContext _context;
        private DbSet<T> _entities;

        public QueryRepository(DemoContext context)
        {
            this._context = context;
            this._entities = context.Set<T>();
        }
        public async Task<List<T>> GetAll(CancellationToken cancellationToken)
        {
            return await _entities.ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<T> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await _entities.FirstOrDefaultAsync(x => EF.Property<int>(x, "Id") == id, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<IQueryable<T>> Query(Expression<Func<T, bool>> expression)
        {
            return _entities.Where(expression);
        }
    }
}
