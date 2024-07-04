using BuildingBlocks.CQRS;
using RecAjuda.API.Exception.Shelter;
using RecAjuda.API.Shelters.CreateShelter;
using RecAjuda.API.ValueObjects;

namespace RecAjuda.API.Shelters.UpdateShelterById;

public record UpdateShelterByIdCommand(
    string Name,
    ShelterType Type,
    double Latitude,
    double Longitude,
    string Neighborhood,
    string Street,
    List<AgeRange> AgeRanges,
    int Capacity,
    string WorkingHourDescription,
    List<ContactModel> Contacts,
    Guid Id) : ICommand<UpdateShelterByIdResult>
{
    public void UpdateModel(Shelter shelterForUpdate)
    {
        IEnumerable<Contact> contactsFromModel = Contacts.Select(x => new Contact(x.PreFixNumber, x.Number));

        shelterForUpdate.Update(Name, Type, new GeoCoordinate(Latitude, Longitude), Capacity, WorkingHourDescription,
            AgeRanges, contactsFromModel);
    }
}

public record UpdateShelterByIdResult(Guid Id);

public class UpdateShelterByIdCommandHandler(IDocumentSession session)
    : ICommandHandler<UpdateShelterByIdCommand, UpdateShelterByIdResult>
{
    public async Task<UpdateShelterByIdResult> Handle(UpdateShelterByIdCommand request,
        CancellationToken cancellationToken)
    {
        Shelter? shelterForUpdate = await session.Query<Shelter>()
            .FirstOrDefaultAsync(s => s.Id == request.Id, token: cancellationToken);

        if (shelterForUpdate is null)
            throw new ShelterNotFoundException();

        request.UpdateModel(shelterForUpdate);
        
        session.Update(shelterForUpdate);
        await session.SaveChangesAsync(cancellationToken);

        return new UpdateShelterByIdResult(shelterForUpdate.Id);
    }
}
