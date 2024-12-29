using Ambev.DeveloperEvaluation.Domain.Services;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.MessagingBroker;

public class MessageService : IMessageService
{
    private readonly ILogger<MessageService> _logger;

    public MessageService(ILogger<MessageService> logger)
    {
        _logger = logger;
    }

    public async Task SendMessageAsync(string message, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("{Message}", message);
        
        await Task.CompletedTask;
    }
}