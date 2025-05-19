namespace Stasevich353502.Application.SushiUseCases.Queries;

internal class GetSushiBySetIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetSushiBySetIdQuery, IEnumerable<Sushi>>
{
    public async Task<IEnumerable<Sushi>> Handle(GetSushiBySetIdQuery request, CancellationToken cancellationToken)
    {
        return await unitOfWork.SushiRepository.ListAsync(sushi => sushi.SushiSetId == request.SushiSetId, cancellationToken);
    }
}