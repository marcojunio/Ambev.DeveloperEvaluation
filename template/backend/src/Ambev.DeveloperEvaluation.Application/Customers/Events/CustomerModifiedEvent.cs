using Ambev.DeveloperEvaluation.Common.Cache;
using Ambev.DeveloperEvaluation.Domain.Common;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Customers.Events;

public record CustomerModifiedEvent(Domain.Entities.Customer Customer) : IDomainEvent;

public class CustomerModifiedEventHandler : IDomainEventHandler<CustomerModifiedEvent>
{
    private readonly ILogger<CustomerModifiedEventHandler> _logger;
    private readonly ICacheService _cacheService;

    public CustomerModifiedEventHandler(ILogger<CustomerModifiedEventHandler> logger, ICacheService cacheService)
    {
        _logger = logger;
        _cacheService = cacheService;
    }

    public async Task Handle(CustomerModifiedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Here we can publish to queues, using rabbitmq or kakfa or do some processing decoupled from the sales context. ID COMPANY {0}",
            notification.Customer.Id);

        await _cacheService.RemoveAsync(CacheKeys.GetCustomerKey(notification.Customer.Id));
    }
}