namespace Domain.Abstract;

public abstract class AggregateRoot<TId> : DomainEntity<TId>
{
    protected AggregateRoot(TId id) : base(id)
    {
    }
}