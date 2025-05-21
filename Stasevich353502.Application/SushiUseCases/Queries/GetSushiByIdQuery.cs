namespace Stasevich353502.Application.SushiUseCases.Queries;

public sealed record GetSushiByIdQuery(Guid SushiId) : IRequest<Sushi?>;