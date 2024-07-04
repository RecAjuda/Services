namespace RecAjuda.API.Shelters.GetShelterById;


public record GetShelterByIdResponse(Shelter Shelter);

public class GetShelterByIdEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/shelter/{id}", async (Guid id, ISender sender) =>
            {
                GetShelterByIdResult result = await sender.Send(new GetShelterByIdQuery(id));
                GetShelterByIdResponse response = new(result.Shelter);
                return Results.Ok(response);
            }).WithName("Get shelter by id ")
            .Produces<GetShelterByIdResponse>()
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithDescription("Get shelter by id");
    }
}
