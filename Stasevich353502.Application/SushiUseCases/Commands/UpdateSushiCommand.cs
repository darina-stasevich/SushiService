namespace Stasevich353502.Application.SushiUseCases.Commands;

public sealed record UpdateSushiCommand(Guid sushiId, string newSushiType, string newSushiName, int newAmount) : IRequest<Sushi>;