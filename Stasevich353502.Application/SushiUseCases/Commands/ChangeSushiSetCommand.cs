namespace Stasevich353502.Application.SushiUseCases.Commands;

public sealed record ChangeSushiSetCommand(Sushi Sushi, Guid NewSetId) : IRequest<Sushi>;