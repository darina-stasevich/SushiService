namespace Stasevich353502.Application.SushiUseCases.Queries;

public sealed record GetSushiBySetIdQuery(Guid SushiSetId) : IRequest<IEnumerable<Sushi>>;