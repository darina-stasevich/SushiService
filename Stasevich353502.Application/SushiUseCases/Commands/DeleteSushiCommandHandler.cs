namespace Stasevich353502.Application.SushiUseCases.Commands;

public class DeleteSushiCommandHandler(IUnitOfWork UoW) : IRequestHandler<DeleteSushiCommand>
{
    public async Task Handle(DeleteSushiCommand request, CancellationToken cancellationToken)
    {
        await UoW.SushiRepository.DeleteAsync(request.Sushi, cancellationToken);
    }
}