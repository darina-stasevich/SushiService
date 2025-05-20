namespace Stasevich353502.Application.SushiUseCases.Commands;

public record AddSushiCommand(SushiData Data, int Amount, Guid? SetId) : IRequest<Sushi>;