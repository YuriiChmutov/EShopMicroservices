
namespace Ordering.Domain.Events;

public record OrderCreateEvent(Order order) : IDomainEvent;