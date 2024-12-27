using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Customer : BaseEntity
{
    /// <summary>
    /// Set and get name for client
    ///  Must not be null or empty and should contain both first and last names.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Set and get age for client
    /// Must not be null.
    /// </summary>
    public int Age { get; set; } = 0;

    public Guid UserId { get; set; }
    public User User { get; set; } = new();
    
    public ValidationResultDetail Validate()
    {
        var validator = new CustomerValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}