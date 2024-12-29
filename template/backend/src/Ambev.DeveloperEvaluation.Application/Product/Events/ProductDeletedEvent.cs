using Ambev.DeveloperEvaluation.Common.Cache;
using Ambev.DeveloperEvaluation.Domain.Common;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Product.Events;

public record ProductDeletedEvent(Guid ProductId) : IDomainEvent;

public class ProductDeletedEventHandler : IDomainEventHandler<ProductDeletedEvent>
{
    private readonly ILogger<ProductDeletedEventHandler> _logger;
    private readonly ICacheService _cacheService;

    public ProductDeletedEventHandler(ILogger<ProductDeletedEventHandler> logger, ICacheService cacheService)
    {
        _logger = logger;
        _cacheService = cacheService;
    }

    public async Task Handle(ProductDeletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Here we can publish to queues, using rabbitmq or kakfa or do some processing decoupled from the sales context. ID PRODUCT {0}",
            notification.ProductId);

        await _cacheService.RemoveAsync(CacheKeys.GetProductKey(notification.ProductId));
    }
}