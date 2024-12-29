using Ambev.DeveloperEvaluation.Common.Cache;
using Ambev.DeveloperEvaluation.Domain.Common;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Product.Events;

public record ProductCreatedEvent(Domain.Entities.Product Product) : IDomainEvent;

public class ProductCreatedEventHandler : IDomainEventHandler<ProductCreatedEvent>
{
    private readonly ILogger<ProductCreatedEventHandler> _logger;
    private readonly ICacheService _cacheService;

    public ProductCreatedEventHandler(ILogger<ProductCreatedEventHandler> logger, ICacheService cacheService)
    {
        _logger = logger;
        _cacheService = cacheService;
    }

    public async Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Here we can publish to queues, using rabbitmq or kakfa or do some processing decoupled from the sales context. ID PRODUCT {0}",
            notification.Product.Id);

        await _cacheService.RemoveAsync(CacheKeys.GetProductKey(notification.Product.Id));
    }
}