namespace BuildingBlocks.Base;

public interface IEntity
{
    public Guid Id { get; }
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; }
    public StatusDefault StatusDefault { get;}
}

public enum StatusDefault
{
    Active,
    Inactive,
    Exclude,
    Pending
}
