namespace Ambev.DeveloperEvaluation.WebApi.Features.Company.UpdateCompany;

public class UpdateCompanyRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}