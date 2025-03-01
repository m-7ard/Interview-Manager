using Domain.DomainEvents;

namespace Domain.Abstract;

public class DomainEntity<TId>
{
    public readonly List<DomainEvent> DomainEvents = new List<DomainEvent>();

    public DomainEntity(TId id)
    {
        Id = id;
    }

    public TId Id { get; set; }
    
    public void ClearDomainEvents()
    {
        DomainEvents.Clear();
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (obj is not DomainEntity<TId> other)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        if (GetType() != other.GetType())
        {
            return false;
        }

        if (Id == null || other.Id == null)
        {
            return false;
        }

        return Id.Equals(other.Id);
    }

    public static bool operator ==(DomainEntity<TId> left, DomainEntity<TId> right)
    {
        if (left is null && right is null)
        {
            return true;
        }

        if (left is null || right is null)
        {
            return false;
        }

        return left.Equals(right);
    }

    public static bool operator !=(DomainEntity<TId> left, DomainEntity<TId> right)
    {
        return !(left == right);
    }

    public override int GetHashCode()
    {
        return (GetType().ToString() + Id).GetHashCode();
    }
}