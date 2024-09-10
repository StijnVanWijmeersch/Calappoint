namespace Calappoint.SharedKernel;

public abstract class Entity
{
    private readonly List<IDomainEvent> _domainEvents = [];

    public IReadOnlyList<IDomainEvent> GetDomainEvents => _domainEvents.ToList();

    protected Entity() { }

    public void ClearDomainEvents() => _domainEvents.Clear();

    public void RaiseDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);

}
