using System.Linq.Expressions;
using Stasevich353502.Domain.Entities;

namespace Stasevich353502.Domain.Abstractions;

public interface IRepository<T> where T : Entity
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default,
        params Expression<Func<T, object>>[]? includeProperties);

    Task<IReadOnlyList<T>> ListAllAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>>? filter, CancellationToken cancellationToken = default,
        params Expression<Func<T, object>>[]? includeProperties);

    Task AddAsync(T entity, CancellationToken cancellationToken = default);

    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);

    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);

    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>>? filter, CancellationToken cancellationToken = default,
        params Expression<Func<T, object>>[]? includeProperties);
}