namespace Stasevich353502.Application.SushiUseCases.Queries;

public class GetSushiByIdQueryHandler(IUnitOfWork UoW) : IRequestHandler<GetSushiByIdQuery, Sushi?>
{
    public async Task<Sushi?> Handle(GetSushiByIdQuery request, CancellationToken cancellationToken)
    {
        return await UoW.SushiRepository.GetByIdAsync(request.SushiId, cancellationToken);
    }
}