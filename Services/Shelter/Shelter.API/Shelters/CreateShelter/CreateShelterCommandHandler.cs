using BuildingBlocks.CQRS;

namespace Shelters.API.Shelters.CreateShelter;

public record CreateShelterCommand(
    string Name,
    ShelterType Type,
    double Latitude,
    double Longitude,
    string Neighborhood,
    string Street) : ICommand<CreateShelterResult>
{
    public static implicit operator Shelter(CreateShelterCommand command)
    {
        return new Shelter(command.Name, command.Type, command.Latitude, command.Longitude,
            new Address(command.Neighborhood, command.Street));
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
