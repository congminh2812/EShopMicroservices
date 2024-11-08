using MediatR;

namespace Ordering.Domain.Abstractions
{
    public interface IDomainEvent : INotification
    {
        public Guid EventId => Guid.NewGuid();
        public DateTime OccurredOn => DateTime.UtcNow;
        public string EventType => GetType().AssemblyQualifiedName ?? "";
    }
}
