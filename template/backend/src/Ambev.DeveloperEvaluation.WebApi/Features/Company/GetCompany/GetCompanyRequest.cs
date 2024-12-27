namespace Ambev.DeveloperEvaluation.WebApi.Features.Company.GetCompany;

public class GetCompanyRequest
{
    public Guid Id { get; set; }
    
    public GetCompanyRequest(Guid id)
    {
        Id = id;
    }
}