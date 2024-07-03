using BuildingBlocks.Base;
using Shelter.API.ValueObjects;

namespace Shelter.API.Models;

public class Shelter(
    string name,
    ShelterType type,
    GeoCoordinate geoCoordinate,
    Address address,
    IEnumerable<Contact> contacts,
    IEnumerable<AgeRange> ageRanges,
    int capacity,
    string workingHourDescription) : IEntity
{
    public string Name { get; private set; } = name;
    public ShelterType Type { get; private set; } = type;
    public GeoCoordinate GeoCoordinate { get; private set; } = geoCoordinate;
    public int Capacity { get; private set; } = capacity;
    public string WorkingHourDescription { get; private set; } = workingHourDescription;
    public Address Address { get; private set; } = address;
    public IEnumerable<AgeRange> AgeRanges { get; private set; } = ageRanges;
    public IEnumerable<ResourceShelter> ResourceShelters { get; private set; }
    public IEnumerable<Contact> Contacts { get; private set; } = contacts;

    public Guid Id { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; private set; }
    public StatusDefault StatusDefault { get; private set; } = StatusDefault.Active;


    public void AddResource(IEnumerable<ResourceShelteForAddModel> resourcesForAdd)
    {
        List<ResourceShelter> resourceSheltersForAdd = resourcesForAdd.Select(resourceForAdd =>
            new ResourceShelter(resourceForAdd.ResourceId, Id, resourceForAdd.RequestItemDescription,
                resourceForAdd.MinimalQuantity, resourceForAdd.IdealQuantity)).ToList();

        ResourceShelters = resourceSheltersForAdd;
    }
}

public enum ShelterType
{
    Shelter,
    SupportPoint
}

public enum AgeRange
{
    None,
    Children,
    Teenagers,
    Adults,
    Elderly
}

public record ResourceShelteForAddModel(
    Guid ResourceId,
    string RequestItemDescription,
    int MinimalQuantity,
    int IdealQuantity);
