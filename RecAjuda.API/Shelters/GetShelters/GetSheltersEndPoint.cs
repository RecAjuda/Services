namespace RecAjuda.API.Shelters.GetShelters;


public record GetShelterResponse(IEnumerable<Shelter> Shelters);
public class GetSheltersEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/shelters", async (ISender sender) =>
            {
                GetSheltersResult getSheltersResult = await sender.Send(new GetSheltersQuery());
                GetShelterResponse response = new(getSheltersResult.SheltersList);

                return Results.Ok(response);
            }).WithName("get shelters")
            .Produces<GetShelterResponse>()
            .WithDescription("Get all shelters in database")
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
