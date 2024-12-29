namespace Ambev.DeveloperEvaluation.Domain.Events;

public interface IEventPublisher
{
    Task PublishEventAsync(IEvent eventPublisher,CancellationToken cancellationToken = default);
}