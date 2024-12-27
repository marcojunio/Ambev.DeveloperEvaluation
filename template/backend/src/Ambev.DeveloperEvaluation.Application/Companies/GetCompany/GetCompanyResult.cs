namespace Ambev.DeveloperEvaluation.Application.Companies.GetCompany;

public class GetCompanyResult
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdateAt { get; set; }
}
