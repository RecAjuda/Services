using BuildingBlocks.Base;

namespace Shelter.API.Models;

public class ResourceShelter(
    Guid resourceId,
    Guid shelterId,
    string requestItemsDescription,
    int minimalQuantity,
    int idealQuantity)
    : IEntity
{
    public Guid Id { get; }
    public DateTime CreatedAt { get; } = DateTime.Now;
    public DateTime? UpdatedAt { get; }
    public StatusDefault StatusDefault { get; } = StatusDefault.Active;
    
    public Guid ResourceId { get; } = resourceId;
    public Guid ShelterId { get; } = shelterId;
    public string RequestItemsDescription { get; } = requestItemsDescription;
    public int MinimalQuantity { get; } = minimalQuantity;
    public int IdealQuantity { get; } = idealQuantity;
}
