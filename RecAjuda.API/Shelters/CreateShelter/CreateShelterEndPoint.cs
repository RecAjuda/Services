using RecAjuda.API.Models;

namespace RecAjuda.API.Shelters.CreateShelter;

public record CreateShelterRequest(
    string Name,
    ShelterType Type,
    double Latitude,
    double Longitude,
    string Neighborhood,
    string Street,
    List<AgeRange> AgeRanges,
    int Capacity,
    string WorkingHourDescription,
    List<ContactModel> Contacts)
{
    public static implicit operator CreateShelterCommand(CreateShelterRequest request)
    {
        CreateShelterCommand command = new CreateShelterCommand(request.Name, request.Type, request.Latitude,
            request.Longitude, request.Neighborhood, request.Street, request.AgeRanges, request.Capacity,
            request.WorkingHourDescription, request.Contacts);

        return command;
    }
}

public record CreateShelterResponse(Guid Id);

public class CreateShelterEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/shelters", async (CreateShelterRequest request, ISender sender) =>
            {
                CreateShelterCommand commandForSendToHandlerFromRequest = request;
                CreateShelterResult result = await sender.Send(commandForSendToHandlerFromRequest);

                CreateShelterResponse response = new(result.Id);

                return Results.Created($"/shelters/{response.Id}", response.Id);
            })
            .WithName("Create shelters endpoint")
            .WithDescription("HTTP post method for create shelter")
            .Produces<CreateShelterResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create shelters");
    }
}
