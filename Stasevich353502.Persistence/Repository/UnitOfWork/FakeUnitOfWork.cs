namespace Stasevich353502.Persistence.Repository;

public class FakeUnitOfWork : IUnitOfWork
{
    public void Dispose()
    {
    }

    public async ValueTask DisposeAsync()
    {
    }

    public IRepository<SushiSet> SushiSetRepository { get; } = new FakeSushiSetRepository();
    public IRepository<Sushi> SushiRepository { get; } = new FakeSushiRepository();
    
    public Task SaveAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task DeleteDatabaseAsync()
    {
        throw new NotImplementedException();
    }

    public Task CreateDatabaseAsync()
    {
        throw new NotImplementedException();
    }
}