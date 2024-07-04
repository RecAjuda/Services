using BuildingBlocks.CQRS;
using RecAjuda.API.Models;
using RecAjuda.API.ValueObjects;

namespace RecAjuda.API.Shelters.CreateShelter;

public record CreateShelterCommand(
    string Name,
    ShelterType Type,
    double Latitude,
    double Longitude,
    string Neighborhood,
    string Street,
    List<AgeRange> AgeRanges,
    int Capacity,
    string WorkingHourDescription,
    List<ContactModel> Contacts) : ICommand<CreateShelterResult>
{
    public static implicit operator Shelter(CreateShelterCommand command)
    {
        IEnumerable<Contact> contactsForSetInEntity =
            command.Contacts.Select(c => new Contact(c.PreFixNumber, c.Number));
        GeoCoordinate geoCoordinate = new GeoCoordinate(command.Latitude, command.Longitude);
        Address address = new Address(command.Neighborhood, command.Street);
        Shelter shelterForReturnFormModel = new(command.Name, command.Type,
            geoCoordinate, address,
            contactsForSetInEntity, command.AgeRanges, command.Capacity, command.WorkingHourDescription);

        return shelterForReturnFormModel;
    }
}

public record CreateShelterResult(Guid Id);

public class CreateShelterCommandHandler(IDocumentSession session)
    : ICommandHandler<CreateShelterCommand, CreateShelterResult>

{
    public async Task<CreateShelterResult> Handle(CreateShelterCommand request, CancellationToken cancellationToken)
    {
        Shelter shelterForCreate = request;
        session.Store(shelterForCreate);
        await session.SaveChangesAsync(cancellationToken);
        return new CreateShelterResult(shelterForCreate.Id);
    }
}

public record ContactModel(string PreFixNumber, string Number);
