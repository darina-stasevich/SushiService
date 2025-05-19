using Stasevich353502.Domain.Entities;

namespace Stasevich353502.Domain.Abstractions;

public interface IUnitOfWork : IAsyncDisposable, IDisposable
{
    IRepository<SushiSet> SushiSetRepository { get; }
    IRepository<Sushi> SushiRepository { get; }

    public Task SaveAllAsync();
    public Task DeleteDatabaseAsync();
    public Task CreateDatabaseAsync();
}