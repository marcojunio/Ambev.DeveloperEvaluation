using Ambev.DeveloperEvaluation.Common.Cache;
using Ambev.DeveloperEvaluation.Domain.Common;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sale.Events;

public record SaleCreatedEvent(Domain.Entities.Sale Sale) : IDomainEvent;

public class SaleCreatedEventHandler : IDomainEventHandler<SaleCreatedEvent>
{
    private readonly ILogger<SaleCreatedEventHandler> _logger;
    private readonly ICacheService _cacheService;

    public SaleCreatedEventHandler(ILogger<SaleCreatedEventHandler> logger, ICacheService cacheService)
    {
        _logger = logger;
        _cacheService = cacheService;
    }

    public async Task Handle(SaleCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Here we can publish to queues, using rabbitmq or kakfa or do some processing decoupled from the sales context. ID SALE {0}",
            notification.Sale.Id);

        await _cacheService.RemoveAsync(CacheKeys.GetSaleKey(notification.Sale.Id));
        await _cacheService.RemoveAllPrefixAsync(CacheKeys.GetAllSalesPrefix(notification.Sale.UserId));
    }
}