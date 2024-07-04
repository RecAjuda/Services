namespace RecAjuda.API.Shelters.DeleteShelter;

public record DeleteShelterResponse(bool IsDeleted);

public class DeleteShelterEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/shelters/{id}", async (Guid id, ISender sender) =>
            {
                DeleteShelterResult result = await sender.Send(new DeleteShelterCommand(id));

                DeleteShelterResponse response = new DeleteShelterResponse(result.Result);
                return Results.Ok(response);
            }).WithName("Delete shelter")
            .Produces<DeleteShelterResponse>()
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithDescription("Delete shelter endpoint");
    }
}
