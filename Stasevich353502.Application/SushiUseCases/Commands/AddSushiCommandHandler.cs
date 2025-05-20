namespace Stasevich353502.Application.SushiUseCases.Commands;

public class AddSushiCommandHandler(IUnitOfWork UoW) : IRequestHandler<AddSushiCommand, Sushi>
{
    public async Task<Sushi> Handle(AddSushiCommand request, CancellationToken cancellationToken)
    {
        var sushi = new Sushi(request.Data, request.Amount);

        if (request.SetId != null)
        {
            sushi.AddToSet(request.SetId.Value);
        }

        await UoW.SushiRepository.AddAsync(sushi, cancellationToken);
        await UoW.SaveAllAsync();
        return sushi;
    }
}