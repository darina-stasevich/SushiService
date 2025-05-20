namespace Stasevich353502.Application.SushiUseCases.Commands;

public class UpdateSushiCommandHandler(IUnitOfWork UoW): IRequestHandler<UpdateSushiCommand, Sushi>
{
    public async Task<Sushi> Handle(UpdateSushiCommand request, CancellationToken cancellationToken)
    {
        await UoW.SushiRepository.UpdateAsync(request.Sushi, cancellationToken);
        return request.Sushi;
    }
}