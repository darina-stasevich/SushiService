namespace Stasevich353502.Application.SushiSetUseCases.Commands;

public sealed record AddSushiSetCommand(string Name, decimal Price, decimal Weight) : IRequest<SushiSet>;