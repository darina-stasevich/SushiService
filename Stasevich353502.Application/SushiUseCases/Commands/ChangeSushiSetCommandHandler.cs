namespace Stasevich353502.Application.SushiUseCases.Commands;

public class ChangeSushiSetCommandHandler(IUnitOfWork UoW) : IRequestHandler<ChangeSushiSetCommand, Sushi>
{
    public async Task<Sushi> Handle(ChangeSushiSetCommand request, CancellationToken cancellationToken)
    {
        var sushi = request.Sushi;
        if (request.NewSetId != Guid.NewGuid())
        {
            sushi.RemoveFromSet();
            sushi.AddToSet(request.NewSetId);
        }

        await UoW.SushiRepository.UpdateAsync(sushi, cancellationToken);
        return sushi;
    }
}