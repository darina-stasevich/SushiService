using System.Linq.Expressions;

namespace Stasevich353502.Persistence.Repository;

public class FakeSushiRepository : IRepository<Sushi>
{
    private List<Sushi> _sushi = new();

    public FakeSushiRepository()
    {
        var random = new Random();
        for (int i = 1; i <= 2; i++)
        {
            for (int j = 1; j <= 10; j++)
            {
                var sushi = new Sushi(new SushiData($"sushi {i * 10 + j}", $"{j}"), random.Next(1, 11));
                sushi.AddToSet(Guid.Parse($"{i}{i}{i}{i}{i}{i}{i}{i}-{i}{i}{i}{i}-{i}{i}{i}{i}-{i}{i}{i}{i}-{i}{i}{i}{i}{i}{i}{i}{i}{i}{i}{i}{i}"));
                _sushi.Add(sushi);
            }
        }
    }
    
    public Task<Sushi?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default, params Expression<Func<Sushi, object>>[]? includeProperties)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<Sushi>> ListAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyList<Sushi>> ListAsync(Expression<Func<Sushi, bool>>? filter, CancellationToken cancellationToken = default,
        params Expression<Func<Sushi, object>>[]? includeProperties)
    {
        var data = _sushi.AsQueryable();
        return data.Where(filter).ToList();
    }

    public Task AddAsync(Sushi? entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Sushi entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Sushi entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Sushi?> FirstOrDefaultAsync(Expression<Func<Sushi, bool>>? filter, CancellationToken cancellationToken = default,
        params Expression<Func<Sushi, object>>[]? includeProperties)
    {
        throw new NotImplementedException();
    }
}