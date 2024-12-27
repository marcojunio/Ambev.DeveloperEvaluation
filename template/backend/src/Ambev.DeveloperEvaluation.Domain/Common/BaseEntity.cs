using Ambev.DeveloperEvaluation.Common.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Common;

public class BaseEntity : IComparable<BaseEntity>
{
    protected BaseEntity()
    {
        CreatedAt = DateTime.UtcNow;
    }
    
    public Guid Id { get; set; }

    /// <summary>
    /// Gets the date and time when the client was created.
    /// </summary>
    public DateTime CreatedAt { get; protected set; }

    /// <summary>
    /// Gets the date and time of the last update to the client's information.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
    
    public Task<IEnumerable<ValidationErrorDetail>> ValidateAsync()
    {
        return Validator.ValidateAsync(this);
    }

    public int CompareTo(BaseEntity? other)
    {
        if (other == null)
        {
            return 1;
        }

        return other!.Id.CompareTo(Id);
    }

    public void UpdateDate()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}
