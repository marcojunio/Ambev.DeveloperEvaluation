using Ambev.DeveloperEvaluation.Domain.Common;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sale.Events;

public record SaleCancelledEvent(Domain.Entities.Sale Sale) : IDomainEvent;

public class SaleCancelledEventHandler : IDomainEventHandler<SaleCancelledEvent>
{
    private readonly ILogger<SaleCancelledEventHandler> _logger;

    public SaleCancelledEventHandler(ILogger<SaleCancelledEventHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(SaleCancelledEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Here we can publish to queues, using rabbitmq or kakfa or do some processing decoupled from the sales context. ID SALE {0}", notification.Sale.Id);
        
        await Task.FromResult(true);
    }
}