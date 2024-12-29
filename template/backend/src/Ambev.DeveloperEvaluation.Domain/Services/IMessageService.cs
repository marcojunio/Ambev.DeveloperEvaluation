namespace Ambev.DeveloperEvaluation.Domain.Services;

public interface IMessageService
{
    Task SendMessageAsync(string message, CancellationToken cancellationToken = default);
}