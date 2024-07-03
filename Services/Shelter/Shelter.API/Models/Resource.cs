using BuildingBlocks.Base;

namespace Shelter.API.Models;

public class Resource(string name, string iconElementReference)
    : IEntity
{
    public string Name { get; private set; } = name;
    public string IconElementReference { get; private set; } = iconElementReference;
    public IEnumerable<ResourceShelter> ResourceShelters { get; private set; }


    public Guid Id { get; }
    public DateTime CreatedAt { get; } = DateTime.Now;
    public DateTime? UpdatedAt { get; }
    public StatusDefault StatusDefault { get; } = StatusDefault.Active;
}
