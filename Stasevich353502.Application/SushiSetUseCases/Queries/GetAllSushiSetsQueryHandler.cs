namespace Stasevich353502.Application.SushiSetUseCases.Queries;

public class GetAllSushiSetsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllSushiSetsQuery, IEnumerable<SushiSet>>
{
    public async Task<IEnumerable<SushiSet>> Handle(GetAllSushiSetsQuery request, CancellationToken cancellationToken)
    {
        return await unitOfWork.SushiSetRepository.ListAllAsync(cancellationToken);
    }
}