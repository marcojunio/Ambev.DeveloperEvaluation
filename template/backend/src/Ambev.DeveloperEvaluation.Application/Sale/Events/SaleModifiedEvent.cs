using Ambev.DeveloperEvaluation.Domain.Common;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sale.Events;

public record SaleModifiedEvent(Domain.Entities.Sale Sale) : IDomainEvent;


public class SaleModifiedEventHandler: IDomainEventHandler<SaleCreatedEvent>
{
    private ILogger<SaleModifiedEventHandler> _logger;

    public SaleModifiedEventHandler(ILogger<SaleModifiedEventHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(SaleCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Here we can publish to queues, using rabbitmq or kakfa or do some processing decoupled from the sales context. ID SALE {0}",notification.Sale.Id);

        await Task.FromResult(true);
    }
}