using Ambev.DeveloperEvaluation.Common.Cache;
using Ambev.DeveloperEvaluation.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sale.Events;

public class SaleCancelledEvent : IEvent, INotification
{
    public SaleCancelledEvent(Domain.Entities.Sale sale, DateTime eventDate)
    {
        EventName = GetType().Name;
        Sale = sale;
        EventDate = eventDate;
    }

    public Domain.Entities.Sale Sale { get; set; }
    public string EventName { get; }
    public DateTime EventDate { get; }
};

public class SaleCancelledEventHandler : INotificationHandler<SaleCancelledEvent>
{
    private readonly ILogger<SaleCancelledEventHandler> _logger;
    private readonly ICacheService _cacheService;

    public SaleCancelledEventHandler(ILogger<SaleCancelledEventHandler> logger, ICacheService cacheService)
    {
        _logger = logger;
        _cacheService = cacheService;
    }

    public async Task Handle(SaleCancelledEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "ID SALE {0}",
            notification.Sale.Id);

        await _cacheService.RemoveAsync(CacheKeys.GetSaleKey(notification.Sale.Id));
        
        await _cacheService.RemoveAllPrefixAsync(CacheKeys.GetAllSalesPrefix(notification.Sale.UserId));
    }
}