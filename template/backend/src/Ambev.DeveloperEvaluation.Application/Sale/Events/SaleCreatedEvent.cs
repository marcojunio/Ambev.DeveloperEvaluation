using Ambev.DeveloperEvaluation.Common.Cache;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sale.Events;

public class SaleCreatedEvent: IEvent, INotification
{
    public SaleCreatedEvent(Domain.Entities.Sale sale,DateTime eventDate)
    {
        EventName = GetType().Name;
        Sale = sale;
        EventDate = eventDate;
    }
    public Domain.Entities.Sale Sale { get; set; }
    public string EventName { get; }
    public DateTime EventDate { get; }
};

public class SaleCreatedEventHandler : INotificationHandler<SaleCreatedEvent>
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
            "ID SALE {0}",
            notification.Sale.Id);

        await _cacheService.RemoveAsync(CacheKeys.GetSaleKey(notification.Sale.Id));
        await _cacheService.RemoveAllPrefixAsync(CacheKeys.GetAllSalesPrefix(notification.Sale.UserId));
    }
}