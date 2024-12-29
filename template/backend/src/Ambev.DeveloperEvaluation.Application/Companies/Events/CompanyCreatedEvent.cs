using Ambev.DeveloperEvaluation.Common.Cache;
using Ambev.DeveloperEvaluation.Domain.Common;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Companies.Events;

public record CompanyCreatedEvent(Domain.Entities.Company Company) : IDomainEvent;

public class CompanyCreatedEventHandler : IDomainEventHandler<CompanyCreatedEvent>
{
    private readonly ILogger<CompanyCreatedEventHandler> _logger;
    private readonly ICacheService _cacheService;

    public CompanyCreatedEventHandler(ILogger<CompanyCreatedEventHandler> logger, ICacheService cacheService)
    {
        _logger = logger;
        _cacheService = cacheService;
    }

    public async Task Handle(CompanyCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Here we can publish to queues, using rabbitmq or kakfa or do some processing decoupled from the sales context. ID COMPANY {0}",
            notification.Company.Id);

        await _cacheService.RemoveAsync(CacheKeys.GetCompanyKey(notification.Company.Id));
    }
}