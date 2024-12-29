namespace Ambev.DeveloperEvaluation.Domain.Events;

public interface IEvent
{
    string EventName { get; }
    DateTime EventDate { get; }
}