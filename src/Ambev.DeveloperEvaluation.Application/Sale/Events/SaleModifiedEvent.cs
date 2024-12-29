using Ambev.DeveloperEvaluation.Common.Cache;
using Ambev.DeveloperEvaluation.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sale.Events;

public class SaleModifiedEvent : IEvent, INotification
{
    public SaleModifiedEvent(Domain.Entities.Sale sale, DateTime eventDate)
    {
        Sale = sale;
        EventName = GetType().Name;
        EventDate = eventDate;
    }

    public Domain.Entities.Sale Sale { get; }
    public string EventName { get; }
    public DateTime EventDate { get; }
};

public class SaleModifiedEventHandler : INotificationHandler<SaleModifiedEvent>
{
    private readonly ILogger<SaleModifiedEventHandler> _logger;
    private readonly ICacheService _cacheService;

    public SaleModifiedEventHandler(ILogger<SaleModifiedEventHandler> logger, ICacheService cacheService)
    {
        _logger = logger;
        _cacheService = cacheService;
    }

    public async Task Handle(SaleModifiedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "ID SALE {0}",
            notification.Sale.Id);

        await _cacheService.RemoveAsync(CacheKeys.GetSaleKey(notification.Sale.Id));
        
        await _cacheService.RemoveAllPrefixAsync(CacheKeys.GetAllSalesPrefix(notification.Sale.UserId));
    }
}