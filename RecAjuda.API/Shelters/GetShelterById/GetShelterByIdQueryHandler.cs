using BuildingBlocks.CQRS;
using RecAjuda.API.Exception.Shelter;

namespace RecAjuda.API.Shelters.GetShelterById;

public record GetShelterByIdQuery(Guid Id) : IQuery<GetShelterByIdResult>;

public record GetShelterByIdResult(Shelter Shelter);

public class GetShelterByIdQueryHandler(IDocumentSession session)
    : IQueryHandler<GetShelterByIdQuery, GetShelterByIdResult>
{
    public async Task<GetShelterByIdResult> Handle(GetShelterByIdQuery request, CancellationToken cancellationToken)
    {
        Shelter? shelterByIdForReturn = await session.Query<Shelter>()
            .FirstOrDefaultAsync(shelter => shelter.Id == request.Id, token: cancellationToken);

        if (shelterByIdForReturn is null)
            throw new ShelterNotFoundException();

        return new GetShelterByIdResult(shelterByIdForReturn);
    }
}
