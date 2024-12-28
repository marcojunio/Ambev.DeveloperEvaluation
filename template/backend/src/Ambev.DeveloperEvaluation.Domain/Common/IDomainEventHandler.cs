using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Common;

public interface IDomainEventHandler<TEvent> : INotificationHandler<TEvent> where TEvent : IDomainEvent
{
    
}