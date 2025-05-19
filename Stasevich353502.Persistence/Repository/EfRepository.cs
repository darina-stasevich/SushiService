using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Stasevich353502.Persistence.Data;

namespace Stasevich353502.Persistence.Repository;

public class EfRepository<T>(AppDbContext db) : IRepository<T> where T : Entity
{
    readonly DbSet<T> _entities = db.Set<T>();

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[]? includeProperties)
    {
        var query = _entities.Where(x => x.Id == id);

        if (includeProperties?.Any() == true)
        {
            foreach (var included in includeProperties)
            {
                query = query.Include(included);
            }
        }

        return await query.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<T>> ListAllAsync(CancellationToken cancellationToken = default)
    {
        return (await _entities.ToListAsync(cancellationToken))!;
    }

    public async Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>>? filter, CancellationToken cancellationToken = default,
        params Expression<Func<T, object>>[]? includeProperties)
    {
        var query = _entities.AsQueryable();
        
        if(filter != null)
            query = query.Where(filter);
        
        if (includeProperties?.Any() == true)
        {
            foreach (var included in includeProperties)
            {
                query = query.Include(included);
            }
        }

        return await query.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _entities.AddAsync(entity, cancellationToken);
    }

    public Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        db.Entry(entity).State = EntityState.Modified;
        return Task.CompletedTask;
    }

    public Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        db.Entry(entity).State = EntityState.Deleted;
        return Task.CompletedTask;
    }

    public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>>? filter, CancellationToken cancellationToken = default,
        params Expression<Func<T, object>>[]? includeProperties)
    {
        var query = _entities.AsQueryable();

        if (filter != null)
            query = query.Where(filter);

        if (includeProperties?.Any() == true)
        {
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
        }

        return await query.FirstOrDefaultAsync(cancellationToken);
    }
}