using RecAjuda.API.Shelters.CreateShelter;

namespace RecAjuda.API.Shelters.UpdateShelterById;

public record UpdateShelterByIdRequest(
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
    public UpdateShelterByIdCommand ConvertToCommandEntity(Guid id)
    {
        UpdateShelterByIdCommand command = new UpdateShelterByIdCommand(Name, Type, Latitude,
            Longitude, Neighborhood, Street, AgeRanges, Capacity,
            WorkingHourDescription, Contacts, id);

        return command;
    }
}

public record UpdateShelterByIdResponse(Guid Id);

public class UpdateShelterByIdEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/shelter/{id}", async (Guid id, UpdateShelterByIdRequest request, ISender sender) =>
            {
                UpdateShelterByIdCommand command = request.ConvertToCommandEntity(id);


                UpdateShelterByIdResult result = await sender.Send(command);

                UpdateShelterByIdResponse response = new UpdateShelterByIdResponse(result.Id);

                return Results.Ok(response);
            }).WithName("Update shelter endpoint")
            .WithDescription("Update shelter endpoint")
            .Produces<UpdateShelterByIdResponse>()
            .ProducesProblem(StatusCodes.Status500InternalServerError);
    }
}
