using Stasevich353502.Persistence.Data;

namespace Stasevich353502.Persistence.Repository;

public class EfUnitOfWork(AppDbContext db) : IUnitOfWork, IAsyncDisposable
{
    public IRepository<SushiSet> SushiSetRepository => _sushiSetRepository.Value;
    public IRepository<Sushi> SushiRepository => _sushiRepository.Value;
    private Lazy<IRepository<SushiSet>> _sushiSetRepository { get; } = new(() => new EfRepository<SushiSet>(db));
    private Lazy<IRepository<Sushi>> _sushiRepository { get; } = new(() => new EfRepository<Sushi>(db));

    public async Task SaveAllAsync()
    {
        await db.SaveChangesAsync();
    }

    public async Task DeleteDatabaseAsync()
    {
        await db.Database.EnsureDeletedAsync();
    }

    public async Task CreateDatabaseAsync()
    {
        await db.Database.EnsureCreatedAsync();
    }

    public void Dispose()
    {
        db.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await db.DisposeAsync();
    }
}