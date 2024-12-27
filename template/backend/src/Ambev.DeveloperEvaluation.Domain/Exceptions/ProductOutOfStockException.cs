namespace Ambev.DeveloperEvaluation.Domain.Exceptions;

public class ProductOutOfStockException : DomainException
{
    public ProductOutOfStockException(string message) : base(message)
    {
    }

    public ProductOutOfStockException(string message, Exception innerException) : base(message, innerException)
    {
    }
}