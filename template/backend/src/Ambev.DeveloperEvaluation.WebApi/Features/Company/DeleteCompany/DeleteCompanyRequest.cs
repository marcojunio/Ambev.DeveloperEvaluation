namespace Ambev.DeveloperEvaluation.WebApi.Features.Company.DeleteCompany;

public class DeleteCompanyRequest
{
    public Guid Id { get; set; }
    
    public DeleteCompanyRequest(Guid id)
    {
        Id = id;
    }
}