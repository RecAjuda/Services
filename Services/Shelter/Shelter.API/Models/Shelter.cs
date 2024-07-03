namespace Shelters.API.Models;

public class Shelter(string name, ShelterType type, double latitude, double longitude, Address address)
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; }
    
    public string Name { get;  } = name;
    public ShelterType Type { get;  } = type;
    public double Latitude { get;  } = latitude;
    public double Longitude { get; } = longitude;
    public Address Address { get; } = address;
}


public enum ShelterType
{
    Shelter,
    SupportPoint
}

public record Address(string Neighborhood, string Street);


