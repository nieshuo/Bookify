namespace Bookify.Domain.Abstractions
{
    public abstract class Entity
    {
        public Guid Id { get; init; }
        private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();

        protected Entity(Guid id)
        {
            Id = id;
        }

        protected Entity()
        { 
        }

        public IReadOnlyList<IDomainEvent> GetDomainEvents()
        {
            return _domainEvents.ToList();
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        protected void RaiseDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}
