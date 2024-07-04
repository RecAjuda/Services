using BuildingBlocks.Base;
using BuildingBlocks.CQRS;
using RecAjuda.API.Models;

namespace RecAjuda.API.Shelters.GetShelters;

public record GetSheltersQuery() : IQuery<GetSheltersResult>;
public record GetSheltersResult(IEnumerable<Shelter> SheltersList);

public class GetSheltersQueryHandler(IDocumentSession session) : IQueryHandler<GetSheltersQuery, GetSheltersResult>
{
    public async Task<GetSheltersResult> Handle(GetSheltersQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Shelter> sheltersReturned = await session.Query<Shelter>().ToListAsync(token: cancellationToken);

        sheltersReturned = sheltersReturned.Where(shelter => shelter.StatusDefault == StatusDefault.Active);
        return new GetSheltersResult(sheltersReturned);
    }
}
