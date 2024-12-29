using Ambev.DeveloperEvaluation.Common.Cache;
using Ambev.DeveloperEvaluation.Domain.Common;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Customers.Events;

public record CustomerCreatedEvent(Domain.Entities.Customer Customer) : IDomainEvent;

public class CustomerCreatedEventHandler : IDomainEventHandler<CustomerCreatedEvent>
{
    private readonly ILogger<CustomerCreatedEventHandler> _logger;
    private readonly ICacheService _cacheService;

    public CustomerCreatedEventHandler(ILogger<CustomerCreatedEventHandler> logger, ICacheService cacheService)
    {
        _logger = logger;
        _cacheService = cacheService;
    }

    public async Task Handle(CustomerCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Here we can publish to queues, using rabbitmq or kakfa or do some processing decoupled from the sales context. ID CUSTOMER {0}",
            notification.Customer.Id);

        await _cacheService.RemoveAsync(CacheKeys.GetCustomerKey(notification.Customer.Id));
    }
}