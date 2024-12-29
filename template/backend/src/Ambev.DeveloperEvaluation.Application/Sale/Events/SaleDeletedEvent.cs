using Ambev.DeveloperEvaluation.Common.Cache;
using Ambev.DeveloperEvaluation.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sale.Events;

public class SaleDeletedEvent : IEvent, INotification
{
    public SaleDeletedEvent(Guid saleId, Guid userId, DateTime eventDate)
    {
        SaleId = saleId;
        UserId = userId;
        EventName = GetType().Name;
        EventDate = eventDate;
    }

    public Guid SaleId { get; set; }
    public Guid UserId { get; set; }
    public string EventName { get; }
    public DateTime EventDate { get; }
};

public class SaleDeletedEventHandler : INotificationHandler<SaleDeletedEvent>
{
    private readonly ILogger<SaleDeletedEventHandler> _logger;
    private readonly ICacheService _cacheService;

    public SaleDeletedEventHandler(ILogger<SaleDeletedEventHandler> logger, ICacheService cacheService)
    {
        _logger = logger;
        _cacheService = cacheService;
    }

    public async Task Handle(SaleDeletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "ID SALE {0}",
            notification.SaleId);

        await _cacheService.RemoveAsync(CacheKeys.GetSaleKey(notification.SaleId));

        await _cacheService.RemoveAllPrefixAsync(CacheKeys.GetAllSalesPrefix(notification.UserId));
    }
}