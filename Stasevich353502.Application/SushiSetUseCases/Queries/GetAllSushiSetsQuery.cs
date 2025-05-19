namespace Stasevich353502.Application.SushiSetUseCases.Queries;

public sealed record GetAllSushiSetsQuery : IRequest<IEnumerable<SushiSet>>;