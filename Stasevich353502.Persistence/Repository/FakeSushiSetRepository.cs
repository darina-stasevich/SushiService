using System.Linq.Expressions;

namespace Stasevich353502.Persistence.Repository;

public class FakeSushiSetRepository : IRepository<SushiSet>
{
    private readonly List<SushiSet> _sushiSets =
    [
        new SushiSet("Филадельфия бокс", 10, 260) { Id = Guid.Parse("11111111-1111-1111-1111-111111111111") },
        new SushiSet("Маки сет", 20, 300) { Id = Guid.Parse("22222222-2222-2222-2222-222222222222") }
    ];

    public Task<SushiSet?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default, params Expression<Func<SushiSet, object>>[]? includeProperties)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyList<SushiSet>> ListAllAsync(CancellationToken cancellationToken = default)
    {
        return await Task.Run(() => _sushiSets);
    }

    public Task<IReadOnlyList<SushiSet>> ListAsync(Expression<Func<SushiSet, bool>>? filter, CancellationToken cancellationToken = default,
        params Expression<Func<SushiSet, object>>[]? includeProperties)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(SushiSet? entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(SushiSet entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(SushiSet entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<SushiSet?> FirstOrDefaultAsync(Expression<Func<SushiSet, bool>>? filter, CancellationToken cancellationToken = default,
        params Expression<Func<SushiSet, object>>[]? includeProperties)
    {
        throw new NotImplementedException();
    }
}