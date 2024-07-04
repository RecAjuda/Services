using BuildingBlocks.Base;
using BuildingBlocks.CQRS;
using RecAjuda.API.Exception.Shelter;

namespace RecAjuda.API.Shelters.DeleteShelter;

public record DeleteShelterCommand(Guid Id) : ICommand<DeleteShelterResult>;

public record DeleteShelterResult(bool Result);

public class DeleteShelterCommandHandler(IDocumentSession session)
    : ICommandHandler<DeleteShelterCommand, DeleteShelterResult>
{
    public async Task<DeleteShelterResult> Handle(DeleteShelterCommand request, CancellationToken cancellationToken)
    {
        Shelter? shelterForRemove = await session.Query<Shelter>()
            .FirstOrDefaultAsync(x => x.Id == request.Id, token: cancellationToken);

        if (shelterForRemove is null)
            throw new ShelterNotFoundException();

        shelterForRemove.Delete();

        try
        {
            session.Update(shelterForRemove);
            await session.SaveChangesAsync(cancellationToken);
        }
        catch (System.Exception genericException)
        {
            return new DeleteShelterResult(false);
        }

        return new DeleteShelterResult(true);
    }
}
