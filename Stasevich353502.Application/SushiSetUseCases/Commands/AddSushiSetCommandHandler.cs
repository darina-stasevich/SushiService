namespace Stasevich353502.Application.SushiSetUseCases.Commands;

public class AddSushiSetCommandHandler(IUnitOfWork UoW) : IRequestHandler<AddSushiSetCommand, SushiSet>
{
    public async Task<SushiSet> Handle(AddSushiSetCommand request, CancellationToken cancellationToken)
    {
        var sushiSet = new SushiSet(request.Name, request.Price, request.Weight);

        await UoW.SushiSetRepository.AddAsync(sushiSet, cancellationToken);
        await UoW.SaveAllAsync();
        return sushiSet;
    }
}