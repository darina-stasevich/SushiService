namespace Stasevich353502.Application.SushiUseCases.Commands;

public sealed record DeleteSushiCommand(Sushi Sushi) : IRequest;