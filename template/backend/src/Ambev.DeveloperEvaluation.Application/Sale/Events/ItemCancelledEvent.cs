using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sale.Events;

public class ItemCancelledEvent : IEvent , INotification
{
    public ItemCancelledEvent(Domain.Entities.Sale saleExisting, Domain.Entities.Sale saleUpdated, DateTime eventDate)
    {
        SaleExisting = saleExisting;
        SaleUpdated = saleUpdated;
        EventName = GetType().Name;
        EventDate = eventDate;
    }

    public Domain.Entities.Sale SaleExisting { get; set; }
    public Domain.Entities.Sale SaleUpdated { get; set; }
    public string EventName { get; }
    public DateTime EventDate { get; }
}

public class ItemCancelledEventHandler : INotificationHandler<ItemCancelledEvent>
{
    private readonly ISaleItemRepository _saleItemRepository;
    private readonly ILogger<ItemCancelledEventHandler> _logger;

    public ItemCancelledEventHandler(ISaleItemRepository saleItemRepository, ILogger<ItemCancelledEventHandler> logger)
    {
        _saleItemRepository = saleItemRepository;
        _logger = logger;
    }

    public async Task Handle(ItemCancelledEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("ID SALE {0}",
            notification.SaleUpdated.Id);

        await RemoveDeletedItems(notification.SaleExisting, notification.SaleUpdated, cancellationToken);
    }

    private async Task RemoveDeletedItems(
        Domain.Entities.Sale existingSale,
        Domain.Entities.Sale saleUpdated,
        CancellationToken cancellationToken = default)
    {
        var ids = saleUpdated
            .Items
            .Select(item => item.Id)
            .ToHashSet();

        var itemsToRemove = existingSale
            .Items
            .Where(item => !ids
                .Contains(item.Id))
            .ToList();

        foreach (var item in itemsToRemove)
            await _saleItemRepository.DeleteAsync(item.Id, cancellationToken);
    }
}