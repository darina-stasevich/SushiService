namespace Stasevich353502.Application.SushiUseCases.Commands;

public class UpdateSushiCommandHandler(IUnitOfWork UoW): IRequestHandler<UpdateSushiCommand, Sushi>
{
    public async Task<Sushi> Handle(UpdateSushiCommand request, CancellationToken cancellationToken)
    {
        var sushi = await UoW.SushiRepository.GetByIdAsync(request.sushiId, cancellationToken);
        if (sushi == null)
        {
            throw new Exception($"Sushi with id {request.sushiId} not found.");
        }

        sushi.UpdateCoreData(request.newSushiType, request.newSushiName);
        sushi.ChangeAmount(request.newAmount);
        await UoW.SushiRepository.UpdateAsync(sushi, cancellationToken);
        await UoW.SaveAllAsync();
        return sushi;
    }
}