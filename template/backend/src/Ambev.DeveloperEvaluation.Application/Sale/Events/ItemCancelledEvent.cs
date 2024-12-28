using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sale.Events;

public record ItemCancelledEvent(Domain.Entities.Sale SaleExisting, Domain.Entities.Sale SaleUpdated) : IDomainEvent;

public class ItemCancelledEventHandler : IDomainEventHandler<ItemCancelledEvent>
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
        _logger.LogInformation(
            "Here we can publish to queues, using rabbitmq or kakfa or do some processing decoupled from the sales context. ID SALE {0}",
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