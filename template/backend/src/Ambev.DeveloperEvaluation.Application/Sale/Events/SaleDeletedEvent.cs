using Ambev.DeveloperEvaluation.Common.Cache;
using Ambev.DeveloperEvaluation.Domain.Common;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sale.Events;

public record SaleDeletedEvent(Guid SaleId,Guid UserId) : IDomainEvent;

public class SaleDeletedEventHandler : IDomainEventHandler<SaleDeletedEvent>
{
    private readonly ILogger<SaleDeletedEventHandler> _logger;
    private readonly ICacheService _cacheService;

    public SaleDeletedEventHandler(ILogger<SaleDeletedEventHandler> logger, ICacheService cacheService)
    {
        _logger = logger;
        _cacheService = cacheService;
    }

    public async Task Handle(SaleDeletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Here we can publish to queues, using rabbitmq or kakfa or do some processing decoupled from the sales context. ID SALE {0}",
            notification.SaleId);

        await _cacheService.RemoveAsync(CacheKeys.GetSaleKey(notification.SaleId));
        
        await _cacheService.RemoveAllPrefixAsync(CacheKeys.GetAllSalesPrefix(notification.UserId));
    }
}