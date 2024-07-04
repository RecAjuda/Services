namespace RecAjuda.API.ValueObjects;

public record Address(string Neighborhood, string Street);

public record Contact(string PreFixNumber, string Number);

public record GeoCoordinate(double Latitude, double Longitude);


