using Ambev.DeveloperEvaluation.Common.Cache;
using Ambev.DeveloperEvaluation.Domain.Common;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Companies.Events;

public record CompanyDeletedEvent(Guid CompanyId) : IDomainEvent;

public class CompanyDeletedEventHandler : IDomainEventHandler<CompanyDeletedEvent>
{
    private readonly ILogger<CompanyDeletedEventHandler> _logger;
    private readonly ICacheService _cacheService;

    public CompanyDeletedEventHandler(ILogger<CompanyDeletedEventHandler> logger, ICacheService cacheService)
    {
        _logger = logger;
        _cacheService = cacheService;
    }

    public async Task Handle(CompanyDeletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Here we can publish to queues, using rabbitmq or kakfa or do some processing decoupled from the sales context. ID COMPANY {0}",
            notification.CompanyId);

        await _cacheService.RemoveAsync(CacheKeys.GetCompanyKey(notification.CompanyId));
    }
}