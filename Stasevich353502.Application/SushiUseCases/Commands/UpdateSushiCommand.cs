namespace Stasevich353502.Application.SushiUseCases.Commands;

public sealed record UpdateSushiCommand(Sushi Sushi) : IRequest<Sushi>;