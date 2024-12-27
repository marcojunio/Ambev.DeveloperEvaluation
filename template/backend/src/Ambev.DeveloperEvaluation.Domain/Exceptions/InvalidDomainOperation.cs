namespace Ambev.DeveloperEvaluation.Domain.Exceptions;

public class InvalidDomainOperation : DomainException
{
    public InvalidDomainOperation(string message) : base(message)
    {
    }

    public InvalidDomainOperation(string message, Exception innerException) : base(message, innerException)
    {
    }
}