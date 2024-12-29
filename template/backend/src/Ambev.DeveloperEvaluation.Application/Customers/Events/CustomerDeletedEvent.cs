using Ambev.DeveloperEvaluation.Common.Cache;
using Ambev.DeveloperEvaluation.Domain.Common;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Customers.Events;

public record CustomerDeletedEvent(Guid CustomerId) : IDomainEvent;

public class CustomerDeletedEventHandler : IDomainEventHandler<CustomerDeletedEvent>
{
    private readonly ILogger<CustomerDeletedEventHandler> _logger;
    private readonly ICacheService _cacheService;

    public CustomerDeletedEventHandler(ILogger<CustomerDeletedEventHandler> logger, ICacheService cacheService)
    {
        _logger = logger;
        _cacheService = cacheService;
    }

    public async Task Handle(CustomerDeletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Here we can publish to queues, using rabbitmq or kakfa or do some processing decoupled from the sales context. ID CUSTOMER {0}",
            notification.CustomerId);

        await _cacheService.RemoveAsync(CacheKeys.GetCustomerKey(notification.CustomerId));
    }
}